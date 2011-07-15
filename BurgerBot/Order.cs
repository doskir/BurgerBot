using System.Drawing;
using System.Drawing.Imaging;

namespace BurgerBot
{
    class Order
    {
        public Ingredient[] Ingredients = new Ingredient[9];
        public Rectangle[] IngredientRectangles = CreateRectangles();
        public int RareNeeded;
        public int MediumNeeded;
        public int WellDoneNeeded;
        static Rectangle[] CreateRectangles()
        {
            var result = new Rectangle[9];
            for (int i = 0; i < 9;i++)
                result[i] = new Rectangle(45, 260 - (i*25), 45, 23);
            return result;
        }
        public Order(Bitmap orderScreen)
        {
            //slot0 = bottom
            //slot8 = top
            for (int i = 0; i < 9; i++)
            {
                Bitmap bmp = ImageProcessing.GetRegion(orderScreen, IngredientRectangles[i]);
                Ingredient ing = BurgerIngredients.GetIngredient(bmp);
                Ingredients[i] = ing;
                switch (ing)
                {
                    case Ingredient.RareMeat:
                        RareNeeded++;
                        break;
                    case Ingredient.MediumMeat:
                        MediumNeeded++;
                        break;
                    case Ingredient.WellDoneMeat:
                        WellDoneNeeded++;
                        break;
                }
#if DEBUG
                if (Ingredients[0] == Ingredient.BottomBun && ing == Ingredient.Unknown)
                {
                    if (!System.IO.Directory.Exists("ticket"))
                        System.IO.Directory.CreateDirectory("ticket");
                    string fileString = System.Windows.Forms.Application.StartupPath + "\\ticket\\unknownslot" + i +
                                        ".png";
                    if (!System.IO.File.Exists(fileString))
                        bmp.Save(fileString, ImageFormat.Png);
                }
#endif
                bmp.Dispose();
            }
        }
        public bool IsValid
        {
            get
            {
                //im assuming that the top and bottom parts will always be buns
                if (Ingredients[0] != Ingredient.BottomBun)
                    return false;
                for (int i = 8; i >= 0; i--)
                {
                    if (Ingredients[i] == Ingredient.TopBun)
                        return true;
                    if (Ingredients[i] != Ingredient.None)
                        return false;
                }
                return false;
            }
        }

        public static Bitmap GetOrderPart(Bitmap screen)
        {
            var orderRect = new Rectangle(489, 9, 142, 285);
            return ImageProcessing.GetRegion(screen, orderRect);
        }
    }
}
