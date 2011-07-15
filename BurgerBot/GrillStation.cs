using System;
using System.Linq;

namespace BurgerBot
{
    enum CookingDegrees {Rare,Medium,WellDone}
    class GrillStation
    {
        private readonly GrillSlot[] grillSlots;
        public DateTime NextAction;

        public GrillStation()
        {
            grillSlots = CreateGrillSlots();
            NextAction = DateTime.Now.AddYears(1);
        }
        public void GrillTender()
        {
            if (Movement.CurrentLocation == Movement.Location.GrillStation)
            {
                foreach (GrillSlot slot in grillSlots.Where(slot => slot.Used && DateTime.Now >= slot.FlipTime))
                {
                    FlipMeat(slot);
                }
            }
        }

        public GrillSlot MeatDone()
        {
            if (Movement.CurrentLocation == Movement.Location.GrillStation)
            {
                return grillSlots.FirstOrDefault(slot => slot.Used && DateTime.Now >= slot.DoneTime);
            }
            return null;
        }
        public void Update()
        {
            NextAction = DateTime.Now.AddYears(1);
            foreach (GrillSlot slot in grillSlots)
            {
                if (slot.Used)
                {
                    if (slot.FlipTime < NextAction)
                        NextAction = slot.FlipTime;
                    if (slot.DoneTime < NextAction)
                        NextAction = slot.DoneTime;
                }
            }
        }

        public bool PutPattyOnGrill(CookingDegrees cookingDegrees)
        {
            Log.AddMessage("Putting new patty on grill", Log.LoggingLevel.Debug);
            if (Movement.CurrentLocation != Movement.Location.GrillStation)
            {
                Movement.MoveTo(Movement.Location.GrillStation);
                return false;
            }
            foreach (GrillSlot gs in grillSlots)
            {
                if (!gs.Used)
                {
                    switch (cookingDegrees)
                    {
                        case CookingDegrees.Rare:
                            gs.FlipTime = DateTime.Now.AddSeconds(15);
                            gs.CookingDegree = cookingDegrees;
                            break;
                        case CookingDegrees.Medium:
                            gs.FlipTime = DateTime.Now.AddSeconds(30);
                            gs.CookingDegree = cookingDegrees;
                            break;
                        case CookingDegrees.WellDone:
                            gs.FlipTime = DateTime.Now.AddSeconds(50);
                            gs.CookingDegree = cookingDegrees;
                            break;

                    }
                    FlashAutomation.DragDrop(63, 328, gs.X, gs.Y);
                    FlashAutomation.AntiHover(gs.X, gs.Y);
                    gs.Used = true;
                    Update();
                    Log.AddMessage("Put patty on grill", Log.LoggingLevel.Debug);
                    return true;
                }
            }
            Log.AddMessage("ERROR: All grillSlots were used, could not put patty on grill", Log.LoggingLevel.Error);
            return false;
        }
        void FlipMeat(GrillSlot gs)
        {
            Log.AddMessage("Flipping patty...", Log.LoggingLevel.Debug);
            if (!gs.Used)
                throw new Exception("empty slot");
            FlashAutomation.Click(gs.X, gs.Y);
            FlashAutomation.AntiHover(gs.X, gs.Y);
            gs.FlipTime = DateTime.MaxValue;
            switch (gs.CookingDegree)
            {
                case CookingDegrees.Rare:
                    gs.DoneTime = DateTime.Now.AddSeconds(15);
                    break;
                case CookingDegrees.Medium:
                    gs.DoneTime = DateTime.Now.AddSeconds(30);
                    break;
                case CookingDegrees.WellDone:
                    gs.DoneTime = DateTime.Now.AddSeconds(50);
                    break;
                default:
                    break;
            }
            Log.AddMessage("Flipped patty", Log.LoggingLevel.Debug);
            Update();
        }

        public void RemoveMeat(GrillSlot gs)
        {
            Log.AddMessage("Removing patty from grill...", Log.LoggingLevel.Debug);
            if (!gs.Used)
                throw new Exception("empty slot");
            FlashAutomation.DragDrop(gs.X, gs.Y, 567, 380);
            FlashAutomation.AntiHover(567, 380);
            gs.Used = false;
            gs.DoneTime = DateTime.MaxValue;
            gs.FlipTime = DateTime.MaxValue;
            Log.AddMessage("Removed patty from grill", Log.LoggingLevel.Debug);
            Update();
        }

        

        static GrillSlot[] CreateGrillSlots()
        {
            GrillSlot[] slots = new GrillSlot[12];
            for (int column = 0; column < 3; column++)
                for (int row = 0; row < 4; row++)
                {
                    GrillSlot gs = new GrillSlot {X = 192 + column*120, Y = 128 + row*80};
                    slots[column + (row*3)] = gs;
                }
            return slots;
        }
    }
    class GrillSlot
    {
        public int X;
        public int Y;
        public bool Used;
        public DateTime FlipTime = DateTime.MaxValue;
        public DateTime DoneTime = DateTime.MaxValue;
        public CookingDegrees CookingDegree = CookingDegrees.Medium;
    }
}
