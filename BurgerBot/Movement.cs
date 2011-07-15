using System;
using System.Drawing;
using System.Threading;

namespace BurgerBot
{
    class Movement
    {
        public enum Location {Unknown,Moving,OrderStation,GrillStation,BuildStation}

        public static Location CurrentLocation = Location.Unknown;
        static Location _movingTo;

        public static bool MoveTo(Location location)
        {
            
            if (location == CurrentLocation)
                return true;
            if (CurrentLocation == Location.Moving)
                return false;
            switch (location)
            {
                case Location.OrderStation:
                    FlashAutomation.Click(220, 455);
                    break;
                case Location.GrillStation:
                    FlashAutomation.Click(330, 455);
                    break;
                case Location.BuildStation:
                    FlashAutomation.Click(430, 455);
                    break;
                default:
                    return false;
            }
            CurrentLocation = Location.Moving;
            _movingTo = location;

            _movingThread = new Thread(MovingDelay);
            _movingThread.Start();
            Log.AddMessage("Moving to " + Enum.GetName(typeof (Location), location), Log.LoggingLevel.Debug);
            return true;
        }
        private static Thread _movingThread;
        private static void MovingDelay()
        {
            Thread.Sleep(2000);
            CurrentLocation = _movingTo;
            Log.AddMessage("Moved to " + Enum.GetName(typeof (Location), CurrentLocation), Log.LoggingLevel.Debug);
        }

        private static Bitmap atOrderStationButton = (Bitmap)Image.FromFile("images\\orderstationbutton.png");
        private static Bitmap atGrillStationButton = (Bitmap)Image.FromFile("images\\grillstationbutton.png");
        private static Bitmap atBuildStationButton = (Bitmap)Image.FromFile("images\\buildstationbutton.png");
        
        public static void UpdateLocationFromScreenshot()
        {
            Log.AddMessage("Trying to get location from screenshot...", Log.LoggingLevel.Debug);
            Bitmap screen = FlashAutomation.ScreenShot();
            Bitmap orderStationButton = ImageProcessing.GetRegion(screen, new Rectangle(162, 434, 105, 43));
            Bitmap grillStationButton = ImageProcessing.GetRegion(screen, new Rectangle(268, 434, 105, 43));
            Bitmap buildStationButton = ImageProcessing.GetRegion(screen, new Rectangle(373, 434, 105, 43));
            screen.Dispose();
            if(ImageProcessing.CompareBitmaps(orderStationButton,atOrderStationButton))
            {
                CurrentLocation = Location.OrderStation;
            }
            else if(ImageProcessing.CompareBitmaps(grillStationButton,atGrillStationButton))
            {
                CurrentLocation = Location.GrillStation;
            }
            else if(ImageProcessing.CompareBitmaps(buildStationButton, atBuildStationButton))
            {
                CurrentLocation = Location.BuildStation;
            }
            orderStationButton.Dispose();
            grillStationButton.Dispose();
            buildStationButton.Dispose();
            Log.AddMessage("Got location from screenshot: " + CurrentLocation, Log.LoggingLevel.Debug);
        }


    }
}
