using System;
using System.Collections.Generic;
using System.Drawing;

namespace BurgerBot
{
    class BuildStation
    {
        public List<Patty> PattySlots;
        public int AvailableRare;
        public int AvailableMedium;
        public int AvailableWellDone;
        public BuildStation()
        {
            PattySlots = CreatePattySlots();
        }
        public void Update()
        {
            //reorder list
            for (int i = 0; i < PattySlots.Count - 1; i++)
            {
                if (!PattySlots[i].Used && PattySlots[i + 1].Used)
                {
                    PattySlots[i].Used = true;
                    PattySlots[i].CookingDegree = PattySlots[i + 1].CookingDegree;
                    PattySlots[i + 1].Used = false;
                    i--;
                }
            }


            //count pattys
            AvailableRare = 0;
            AvailableMedium = 0;
            AvailableWellDone = 0;
            foreach (Patty ps in PattySlots)
            {
                if (ps.Used)
                {
                    switch (ps.CookingDegree)
                    {
                        case CookingDegrees.Rare:
                            AvailableRare++;
                            break;
                        case CookingDegrees.Medium:
                            AvailableMedium++;
                            break;
                        case CookingDegrees.WellDone:
                            AvailableWellDone++;
                            break;
                    }
                }
            }
        }

        void BuildBurger(Order o)
        {
            Log.AddMessage("Building burger...", Log.LoggingLevel.Debug);
            for(int i = 0;i < o.Ingredients.Length;i++)
            {
                Point loc = Point.Empty;
                switch (o.Ingredients[i])
                {
                    case Ingredient.BottomBun:
                        loc = new Point(60, 400);
                        break;
                    case Ingredient.Cheese:
                        loc = new Point(60, 360);
                        break;
                    case Ingredient.Pickle:
                        loc = new Point(60, 300);
                        break;
                    case Ingredient.Onion:
                        loc = new Point(60, 260);
                        break;
                    case Ingredient.Cabbage:
                        loc = new Point(60, 210);
                        break;
                    case Ingredient.Tomato:
                        loc = new Point(60, 160);
                        break;
                    case Ingredient.TopBun:
                        loc = new Point(60, 110);
                        break;

                    case Ingredient.Ketchup:
                        loc = new Point(445, 370);
                        break;
                    case Ingredient.Mustard:
                        loc = new Point(500, 370);
                        break;
                    case Ingredient.Mayo:
                        loc = new Point(550, 370);
                        break;
                    case Ingredient.BBQ:
                        loc = new Point(600, 370);
                        break;

                    case Ingredient.RareMeat:
                        loc = GetMeat(CookingDegrees.Rare);
                        break;
                    case Ingredient.MediumMeat:
                        loc = GetMeat(CookingDegrees.Medium);
                        break;
                    case Ingredient.WellDoneMeat:
                        loc = GetMeat(CookingDegrees.WellDone);
                        break;

                }
                if (o.Ingredients[i] != Ingredient.None)
                {
                    Log.AddMessage("Adding ingredient" + i + ":" + Enum.GetName(typeof (Ingredient), o.Ingredients[i]),
                                   Log.LoggingLevel.Debug);
                    FlashAutomation.DragDrop(loc.X, loc.Y, 329, 100);
                    System.Threading.Thread.Sleep(100);
                    //condiments and meat need additional wait due to return and stack dropping animations
                    if (o.Ingredients[i] != Ingredient.TopBun && i < 8)
                    {
                        if (o.Ingredients[i + 1] == Ingredient.Ketchup || o.Ingredients[i + 1] == Ingredient.Mayo ||
                            o.Ingredients[i + 1] == Ingredient.BBQ || o.Ingredients[i + 1] == Ingredient.Mustard)
                            System.Threading.Thread.Sleep(500);
                    }
                    if ((o.Ingredients[i] == Ingredient.RareMeat || o.Ingredients[i] == Ingredient.MediumMeat ||
                         o.Ingredients[i] == Ingredient.WellDoneMeat) &&
                        (o.Ingredients[i+1] == Ingredient.RareMeat || o.Ingredients[i+1] == Ingredient.MediumMeat ||
                         o.Ingredients[i+1] == Ingredient.WellDoneMeat))
                    {
                        System.Threading.Thread.Sleep(500);
                    }
                }

            }
            System.Threading.Thread.Sleep(1000);
            Log.AddMessage("Burger built", Log.LoggingLevel.Debug);
        }

        private Point GetMeat(CookingDegrees cd)
        {
            Point result = Point.Empty;
            foreach(Patty ps in PattySlots)
            {
                if(ps.Used && ps.CookingDegree == cd)
                {
                    ps.Used = false;
                    result = new Point(ps.X, ps.Y);
                    break;
                }
            }
            Update();
            return result;
        }

        static void GiveBurger(OrderSlot os)
        {
            Log.AddMessage("Giving burger...", Log.LoggingLevel.Debug);
            FlashAutomation.DragDrop(os.X, os.Y, 189, 343);
            System.Threading.Thread.Sleep(10000);
            Log.AddMessage("Gave burger", Log.LoggingLevel.Debug);
        }
        public void MakeBurger(OrderSlot os)
        {
            if (os.Used && os.Order.RareNeeded <= AvailableRare &&
                os.Order.MediumNeeded <= AvailableMedium && 
                os.Order.WellDoneNeeded <= AvailableWellDone)
            {
                Log.AddMessage("Making burger...", Log.LoggingLevel.Debug);
                BuildBurger(os.Order);
                AvailableRare -= os.Order.RareNeeded;
                AvailableMedium -= os.Order.MediumNeeded;
                AvailableWellDone -= os.Order.WellDoneNeeded;
                GiveBurger(os);
                os.Used = false;
                os.Order = null;
                Log.AddMessage("Made burger", Log.LoggingLevel.Debug);
                return;
            }
        }

        public bool OrderFulfillable(OrderSlot[] orderSlots)
        {
            foreach (OrderSlot os in orderSlots)
            {
                if (os.Used && os.Order.RareNeeded <= AvailableRare &&
                    os.Order.MediumNeeded <= AvailableMedium &&
                    os.Order.WellDoneNeeded <= AvailableWellDone)
                {
                    return true;
                }
            }
            return false;
        }

        static List<Patty> CreatePattySlots()
        {
            List<Patty> list = new List<Patty>();
            for (int i = 0; i < 25; i++)
            {
                list.Add(new Patty(188, 415 - (i*14)));
            }

            return list;
        }
        public void AddFinishedPattyToStack(CookingDegrees cookingDegree)
        {
            foreach(Patty ps in PattySlots)
            {
                if(!ps.Used)
                {
                    ps.CookingDegree = cookingDegree;
                    ps.Used = true;
                    string pattyDegree = Enum.GetName(typeof (CookingDegrees), cookingDegree);
                    Log.AddMessage("Finished cooking " + pattyDegree + " patty.", Log.LoggingLevel.Debug);
                    return;
                }
            }
        }
    }
    class Patty
    {
        public bool Used;
        public CookingDegrees CookingDegree;
        public int X;
        public int Y;
        public Patty(int x,int y)
        {
            X = x;
            Y = y;
            Used = false;
            CookingDegree = CookingDegrees.WellDone;
        }
    }
}
