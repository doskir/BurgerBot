using System.Drawing;

namespace BurgerBot
{
    class OrderStation
    {
        public OrderSlot[] OrderSlots;
        public OrderSlot ActiveSlot;
        public OrderStation()
        {
            OrderSlots = CreateOrderSlots();
            ActiveSlot = new OrderSlot(560, 32);
        }


        private static OrderSlot[] CreateOrderSlots()
        {
            var result = new OrderSlot[10];
            for (int i = 0; i < result.Length; i++)
                result[i] = new OrderSlot(70 + (i * 40), 12);
            return result;
        }

        public bool CustomerWaiting()
        {
            Log.AddMessage("Checking for waiting customers", Log.LoggingLevel.Spam);
            Bitmap screen = FlashAutomation.ScreenShot();
            bool result = screen.GetPixel(170, 200) == Color.FromArgb(255, 255, 255, 255);
            screen.Dispose();
            return result;
        }

        public Order AcceptCustomer()
        {
            Log.AddMessage("Accepting order...", Log.LoggingLevel.Debug);
            //current ticket window needs to be empty!
            FlashAutomation.Click(171, 179);
            Order o;
            do
            {
                Bitmap screen = FlashAutomation.ScreenShot();
                Bitmap orderPart = Order.GetOrderPart(screen);
                System.Threading.Thread.Sleep(100);
                o = new Order(orderPart);
                orderPart.Dispose();
                screen.Dispose();
            } while (!o.IsValid);
            ActiveSlot.Order = o;
            ActiveSlot.Used = true;
            Log.AddMessage("Wating for customer to shut up", Log.LoggingLevel.Debug);
            System.Threading.Thread.Sleep(3000);
            Log.AddMessage("Accepted new Order", Log.LoggingLevel.Debug);
            return o;
        }

        public bool MoveActiveOrderToOrderBar()
        {
            Log.AddMessage("Moving order to orderbar", Log.LoggingLevel.Debug);
            foreach (OrderSlot os in OrderSlots)
                if (!os.Used)
                {
                    FlashAutomation.DragDrop(ActiveSlot.X, ActiveSlot.Y, os.X, os.Y);
                    os.Used = true;
                    os.Order = ActiveSlot.Order;
                    ActiveSlot.Used = false;
                    ActiveSlot.Order = null;
                    Log.AddMessage("Moved order to orderbar", Log.LoggingLevel.Debug);
                    return true;
                }
            return false;
        }
    }
    class OrderSlot
    {
        public int X;
        public int Y;
        public bool Used;
        public Order Order;
        public OrderSlot(int x, int y)
        {
            X = x;
            Y = y;
            Used = false;
        }
    }
}
