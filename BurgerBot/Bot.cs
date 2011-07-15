using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace BurgerBot
{
    class Bot
    {
        static Thread _botThread;
        public static void RunBot()
        {
            if (_botThread == null || !_botThread.IsAlive)
            {
                Log.AddMessage("Starting bot...", Log.LoggingLevel.Info);
                _botThread = new Thread(MainLoop);
                _botThread.Start();
            }
        }
        public static void StopBot()
        {
            Log.AddMessage("Stopping bot", Log.LoggingLevel.Info);
            if (_botThread != null)
            {
                _botThread.Abort();
                _botThread = null;
                Log.AddMessage("Bot stopped", Log.LoggingLevel.Info);
            }
        }

        public static Queue<CookingDegrees> MeatQueue = new Queue<CookingDegrees>();
        private static DateTime _lastMeatAdd = DateTime.MaxValue;
        public static bool ContinueAfterThisDay = true;
        static void MainLoop()
        {
            var orderStation = new OrderStation();
            var buildStation = new BuildStation();
            var grillStation = new GrillStation();
            Log.AddMessage("Started Bot", Log.LoggingLevel.Info);
            while(true)
            {
                while (Movement.CurrentLocation == Movement.Location.Unknown)
                {
                    Movement.UpdateLocationFromScreenshot();
                    Log.AddMessage("ERROR: Could not start bot, unknown starting location", Log.LoggingLevel.Error);
                    Thread.Sleep(100);
                }
                if(GameDone())
                {
                    bool continuePlaying = EndOfDay();

                    if (continuePlaying)
                    {
                        orderStation = new OrderStation();
                        buildStation = new BuildStation();
                        grillStation = new GrillStation();
                        Thread.Sleep(10000);
                    }
                    else
                        break;
                }
                grillStation.Update();
                buildStation.Update();

                double secondsToNextGrillStationAction = (grillStation.NextAction - DateTime.Now).TotalSeconds;
                double secondsSinceLastMeatAdd = (DateTime.Now - _lastMeatAdd).TotalSeconds;

                if (secondsToNextGrillStationAction <= 3 || 
                    (MeatQueue.Count > 0 && secondsSinceLastMeatAdd > 5))
                {
                    Movement.MoveTo(Movement.Location.GrillStation);
                }
                    //2 seconds to move , 15 to do whatever, 3 seconds to move to grillstation
                else if (secondsToNextGrillStationAction >= 20 &&
                         buildStation.OrderFulfillable(orderStation.OrderSlots))
                {
                    Movement.MoveTo(Movement.Location.BuildStation);
                }
                else if (secondsToNextGrillStationAction >= 20)
                {
                    Movement.MoveTo(Movement.Location.OrderStation);
                }


                if (Movement.CurrentLocation == Movement.Location.GrillStation)
                {
                    grillStation.GrillTender();
                    GrillSlot gs = grillStation.MeatDone();
                    while(gs != null)
                    {
                        buildStation.AddFinishedPattyToStack(gs.CookingDegree);
                        grillStation.RemoveMeat(gs);
                        gs = grillStation.MeatDone();
                    }
                    while (MeatQueue.Count > 0)
                    {
                        CookingDegrees cd = MeatQueue.Peek();
                        if (grillStation.PutPattyOnGrill(cd))
                            MeatQueue.Dequeue();
                        else
                            break;
                    }
                }
                else if (Movement.CurrentLocation == Movement.Location.BuildStation &&
                         secondsToNextGrillStationAction >= 18)
                {
                    int orders = orderStation.OrderSlots.Count(o => o.Used);
                    if (orders > 0)
                    {
                        foreach (OrderSlot os in orderStation.OrderSlots)
                        {
                            if (os.Used && os.Order.RareNeeded <= buildStation.AvailableRare &&
                                os.Order.MediumNeeded <= buildStation.AvailableMedium &&
                                os.Order.WellDoneNeeded <= buildStation.AvailableWellDone)
                            {
                                DateTime buildStart = DateTime.Now;
                                buildStation.MakeBurger(os);
                                TimeSpan buildTime = DateTime.Now - buildStart;
                                Log.AddMessage("Order fulfilled in " + buildTime.TotalSeconds + " seconds.", Log.LoggingLevel.Info);
                                break;
                            }
                        }
                    }
                }
                else if (Movement.CurrentLocation == Movement.Location.OrderStation &&
                         secondsToNextGrillStationAction >= 18)
                {
                    if (!orderStation.ActiveSlot.Used &&
                        orderStation.CustomerWaiting())
                    {
                        DateTime orderStart = DateTime.Now;
                        Order o = orderStation.AcceptCustomer();
                        orderStation.MoveActiveOrderToOrderBar();
                        foreach (Ingredient i in o.Ingredients)
                        {
                            if (i == Ingredient.RareMeat)
                                MeatQueue.Enqueue(CookingDegrees.Rare);
                            if (i == Ingredient.MediumMeat)
                                MeatQueue.Enqueue(CookingDegrees.Medium);
                            if (i == Ingredient.WellDoneMeat)
                                MeatQueue.Enqueue(CookingDegrees.WellDone);
                        }
                        _lastMeatAdd = DateTime.Now;
                        TimeSpan orderTime = DateTime.Now - orderStart;
                        Log.AddMessage("Order accepted in " + orderTime.TotalSeconds + " seconds.",
                                       Log.LoggingLevel.Info);
                    }
                }
                Thread.Sleep(500);
            }

            
        }
        //the continue button where it shows you the stats as a burger
        private static Bitmap continueButton = (Bitmap) Image.FromFile("images\\continuebutton.png");
        //the one where it shows you your "XP"
        private static Bitmap continueButton2 = (Bitmap) Image.FromFile("images\\continuebutton2.png");
        //at the shop/tips page
        private static Bitmap continueButton3 = (Bitmap) Image.FromFile("images\\continuebutton3.png");
        public static bool GameDone()
        {
            Log.AddMessage("Checking if day is over...", Log.LoggingLevel.Spam);
            bool result = false;
            Bitmap screen = FlashAutomation.ScreenShot();
            Bitmap continueButtonNow = ImageProcessing.GetRegion(screen, new Rectangle(272, 438, 98, 35));
            if (ImageProcessing.CompareBitmaps(continueButtonNow, continueButton))
                result = true;
            continueButtonNow.Dispose();
            screen.Dispose();
            return result;
        }
        
        private static bool EndOfDay()
        {
            Log.AddMessage("Day has ended", Log.LoggingLevel.Info);
            if (!ContinueAfterThisDay)
            {
                Log.AddMessage("Stopping bot", Log.LoggingLevel.Info);
                _botThread.Abort();
                return false;
            }
            FlashAutomation.Click(322, 455);
            Thread.Sleep(100);
            //we are waiting for the "XP" stats to show
            Log.AddMessage("Waiting for continueButton2...", Log.LoggingLevel.Debug);
            while(true)
            {
                Bitmap screen = FlashAutomation.ScreenShot();
                Bitmap continueButton2Now = ImageProcessing.GetRegion(screen, new Rectangle(377, 438, 97, 35));
                //continueButton2Now.Save("cb2.png",System.Drawing.Imaging.ImageFormat.Png);
                if (ImageProcessing.CompareBitmaps(continueButton2Now, continueButton2))
                    break;
                Thread.Sleep(500);
                screen.Dispose();
            }
            Log.AddMessage("ContinueButton2 found", Log.LoggingLevel.Debug);
            FlashAutomation.Click(425, 455);

            //waiting for the continue button on the shop/money page to show
            Log.AddMessage("Waiting for continueButton3...", Log.LoggingLevel.Debug);
            while(true)
            {
                Bitmap screen = FlashAutomation.ScreenShot();
                Bitmap continueButton3Now = ImageProcessing.GetRegion(screen, new Rectangle(333, 365, 97, 36));
                if (ImageProcessing.CompareBitmaps(continueButton3Now, continueButton3))
                    break;
                Thread.Sleep(500);
                screen.Dispose();
            }
            Log.AddMessage("ContinueButton3 found", Log.LoggingLevel.Debug);
            FlashAutomation.Click(385, 379);
            Thread.Sleep(1000);
            Log.AddMessage("New day has started", Log.LoggingLevel.Info);
            return true;
        }
    }
}
