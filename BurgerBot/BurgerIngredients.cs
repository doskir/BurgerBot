using System.Drawing;

namespace BurgerBot
{
    internal enum Ingredient
    {
        None,
        Unknown,
        BottomBun,
        RareMeat,
        MediumMeat,
        WellDoneMeat,
        Cheese,
        Ketchup,
        Mustard,
        Cabbage,
        TopBun,
        Tomato,
        Mayo,
        Onion,
        BBQ,
        Pickle
    }

    internal class BurgerIngredients
    {
        //ugly ahead
        #region IngredientCheck

        #region Bitmaps

        private static readonly Bitmap bbq1 = (Bitmap) Image.FromFile("images\\bbq1.png");
        private static readonly Bitmap bbq2 = (Bitmap) Image.FromFile("images\\bbq2.png");
        private static readonly Bitmap bbq3 = (Bitmap) Image.FromFile("images\\bbq3.png");
        private static readonly Bitmap bbq4 = (Bitmap) Image.FromFile("images\\bbq4.png");

        private static readonly Bitmap bottomBun1 = (Bitmap) Image.FromFile("images\\bottombun1.png");
        private static readonly Bitmap bottomBun2 = (Bitmap) Image.FromFile("images\\bottombun2.png");
        private static readonly Bitmap bottomBun3 = (Bitmap) Image.FromFile("images\\bottombun3.png");

        private static readonly Bitmap cabbage1 = (Bitmap) Image.FromFile("images\\cabbage1.png");
        private static readonly Bitmap cabbage2 = (Bitmap) Image.FromFile("images\\cabbage2.png");
        private static readonly Bitmap cabbage3 = (Bitmap) Image.FromFile("images\\cabbage3.png");
        private static readonly Bitmap cabbage4 = (Bitmap) Image.FromFile("images\\cabbage4.png");

        private static readonly Bitmap cheese1 = (Bitmap) Image.FromFile("images\\cheese1.png");
        private static readonly Bitmap cheese2 = (Bitmap) Image.FromFile("images\\cheese2.png");
        private static readonly Bitmap cheese3 = (Bitmap) Image.FromFile("images\\cheese3.png");

        private static readonly Bitmap ketchup1 = (Bitmap) Image.FromFile("images\\ketchup1.png");
        private static readonly Bitmap ketchup2 = (Bitmap) Image.FromFile("images\\ketchup2.png");
        private static readonly Bitmap ketchup3 = (Bitmap) Image.FromFile("images\\ketchup3.png");

        private static readonly Bitmap mayo1 = (Bitmap) Image.FromFile("images\\mayo1.png");
        private static readonly Bitmap mayo2 = (Bitmap) Image.FromFile("images\\mayo2.png");
        private static readonly Bitmap mayo3 = (Bitmap) Image.FromFile("images\\mayo3.png");
        private static readonly Bitmap mayo4 = (Bitmap) Image.FromFile("images\\mayo4.png");

        private static readonly Bitmap mediummeat1 = (Bitmap) Image.FromFile("images\\mediummeat1.png");
        private static readonly Bitmap mediummeat2 = (Bitmap) Image.FromFile("images\\mediummeat2.png");

        private static readonly Bitmap mustard1 = (Bitmap) Image.FromFile("images\\mustard1.png");
        private static readonly Bitmap mustard2 = (Bitmap) Image.FromFile("images\\mustard2.png");
        private static readonly Bitmap mustard3 = (Bitmap) Image.FromFile("images\\mustard3.png");
        private static readonly Bitmap mustard4 = (Bitmap) Image.FromFile("images\\mustard4.png");

        private static readonly Bitmap none1 = (Bitmap) Image.FromFile("images\\none1.png");
        private static readonly Bitmap none2 = (Bitmap) Image.FromFile("images\\none2.png");

        private static readonly Bitmap onion1 = (Bitmap) Image.FromFile("images\\onion1.png");
        private static readonly Bitmap onion2 = (Bitmap) Image.FromFile("images\\onion2.png");
        private static readonly Bitmap onion3 = (Bitmap) Image.FromFile("images\\onion3.png");
        private static readonly Bitmap onion4 = (Bitmap) Image.FromFile("images\\onion4.png");

        private static readonly Bitmap pickle1 = (Bitmap) Image.FromFile("images\\pickle1.png");
        private static readonly Bitmap pickle2 = (Bitmap) Image.FromFile("images\\pickle2.png");
        private static readonly Bitmap pickle3 = (Bitmap) Image.FromFile("images\\pickle3.png");
        private static readonly Bitmap pickle4 = (Bitmap) Image.FromFile("images\\pickle4.png");

        private static readonly Bitmap raremmeat1 = (Bitmap) Image.FromFile("images\\raremeat1.png");
        private static readonly Bitmap raremmeat2 = (Bitmap) Image.FromFile("images\\raremeat2.png");

        private static readonly Bitmap tomato1 = (Bitmap) Image.FromFile("images\\tomato1.png");
        private static readonly Bitmap tomato2 = (Bitmap) Image.FromFile("images\\tomato2.png");
        private static readonly Bitmap tomato3 = (Bitmap) Image.FromFile("images\\tomato3.png");
        private static readonly Bitmap tomato4 = (Bitmap) Image.FromFile("images\\tomato4.png");

        private static readonly Bitmap topBun1 = (Bitmap) Image.FromFile("images\\topbun1.png");
        private static readonly Bitmap topBun2 = (Bitmap) Image.FromFile("images\\topbun2.png");
        private static readonly Bitmap topBun3 = (Bitmap) Image.FromFile("images\\topbun3.png");
        private static readonly Bitmap topBun4 = (Bitmap) Image.FromFile("images\\topbun4.png");

        private static readonly Bitmap wellDoneMeat1 = (Bitmap) Image.FromFile("images\\welldonemeat1.png");
        private static readonly Bitmap wellDoneMeat2 = (Bitmap) Image.FromFile("images\\welldonemeat2.png");

        #endregion Bitmaps

        public static Ingredient GetIngredient(Bitmap ingredientField)
        {
            if (ImageProcessing.CompareBitmaps(ingredientField, bbq1))
                return Ingredient.BBQ;
            if (ImageProcessing.CompareBitmaps(ingredientField, bbq2))
                return Ingredient.BBQ;
            if (ImageProcessing.CompareBitmaps(ingredientField, bbq3))
                return Ingredient.BBQ;
            if (ImageProcessing.CompareBitmaps(ingredientField, bbq4))
                return Ingredient.BBQ;

            if (ImageProcessing.CompareBitmaps(ingredientField, bottomBun1))
                return Ingredient.BottomBun;
            if (ImageProcessing.CompareBitmaps(ingredientField, bottomBun2))
                return Ingredient.BottomBun;
            if (ImageProcessing.CompareBitmaps(ingredientField, bottomBun3))
                return Ingredient.BottomBun;

            if (ImageProcessing.CompareBitmaps(ingredientField, cabbage1))
                return Ingredient.Cabbage;
            if (ImageProcessing.CompareBitmaps(ingredientField, cabbage2))
                return Ingredient.Cabbage;
            if (ImageProcessing.CompareBitmaps(ingredientField, cabbage3))
                return Ingredient.Cabbage;
            if (ImageProcessing.CompareBitmaps(ingredientField, cabbage4))
                return Ingredient.Cabbage;

            if (ImageProcessing.CompareBitmaps(ingredientField, cheese1))
                return Ingredient.Cheese;
            if (ImageProcessing.CompareBitmaps(ingredientField, cheese2))
                return Ingredient.Cheese;
            if (ImageProcessing.CompareBitmaps(ingredientField, cheese3))
                return Ingredient.Cheese;

            if (ImageProcessing.CompareBitmaps(ingredientField, ketchup1))
                return Ingredient.Ketchup;
            if (ImageProcessing.CompareBitmaps(ingredientField, ketchup2))
                return Ingredient.Ketchup;
            if (ImageProcessing.CompareBitmaps(ingredientField, ketchup3))
                return Ingredient.Ketchup;

            if (ImageProcessing.CompareBitmaps(ingredientField, mayo1))
                return Ingredient.Mayo;
            if (ImageProcessing.CompareBitmaps(ingredientField, mayo2))
                return Ingredient.Mayo;
            if (ImageProcessing.CompareBitmaps(ingredientField, mayo3))
                return Ingredient.Mayo;
            if (ImageProcessing.CompareBitmaps(ingredientField, mayo4))
                return Ingredient.Mayo;

            if (ImageProcessing.CompareBitmaps(ingredientField, mediummeat1))
                return Ingredient.MediumMeat;
            if (ImageProcessing.CompareBitmaps(ingredientField, mediummeat2))
                return Ingredient.MediumMeat;


            if (ImageProcessing.CompareBitmaps(ingredientField, mustard1))
                return Ingredient.Mustard;
            if (ImageProcessing.CompareBitmaps(ingredientField, mustard2))
                return Ingredient.Mustard;
            if (ImageProcessing.CompareBitmaps(ingredientField, mustard3))
                return Ingredient.Mustard;
            if (ImageProcessing.CompareBitmaps(ingredientField, mustard4))
                return Ingredient.Mustard;

            if (ImageProcessing.CompareBitmaps(ingredientField, none1))
                return Ingredient.None;
            if (ImageProcessing.CompareBitmaps(ingredientField, none2))
                return Ingredient.None;

            if (ImageProcessing.CompareBitmaps(ingredientField, onion1))
                return Ingredient.Onion;
            if (ImageProcessing.CompareBitmaps(ingredientField, onion2))
                return Ingredient.Onion;
            if (ImageProcessing.CompareBitmaps(ingredientField, onion3))
                return Ingredient.Onion;
            if (ImageProcessing.CompareBitmaps(ingredientField, onion4))
                return Ingredient.Onion;

            if (ImageProcessing.CompareBitmaps(ingredientField, pickle1))
                return Ingredient.Pickle;
            if (ImageProcessing.CompareBitmaps(ingredientField, pickle2))
                return Ingredient.Pickle;
            if (ImageProcessing.CompareBitmaps(ingredientField, pickle3))
                return Ingredient.Pickle;
            if (ImageProcessing.CompareBitmaps(ingredientField, pickle4))
                return Ingredient.Pickle;

            if (ImageProcessing.CompareBitmaps(ingredientField, raremmeat1))
                return Ingredient.RareMeat;
            if (ImageProcessing.CompareBitmaps(ingredientField, raremmeat2))
                return Ingredient.RareMeat;

            if (ImageProcessing.CompareBitmaps(ingredientField, tomato1))
                return Ingredient.Tomato;
            if (ImageProcessing.CompareBitmaps(ingredientField, tomato2))
                return Ingredient.Tomato;
            if (ImageProcessing.CompareBitmaps(ingredientField, tomato3))
                return Ingredient.Tomato;
            if (ImageProcessing.CompareBitmaps(ingredientField, tomato4))
                return Ingredient.Tomato;

            if (ImageProcessing.CompareBitmaps(ingredientField, topBun1))
                return Ingredient.TopBun;
            if (ImageProcessing.CompareBitmaps(ingredientField, topBun2))
                return Ingredient.TopBun;
            if (ImageProcessing.CompareBitmaps(ingredientField, topBun3))
                return Ingredient.TopBun;
            if (ImageProcessing.CompareBitmaps(ingredientField, topBun4))
                return Ingredient.TopBun;

            if (ImageProcessing.CompareBitmaps(ingredientField, wellDoneMeat1))
                return Ingredient.WellDoneMeat;
            if (ImageProcessing.CompareBitmaps(ingredientField, wellDoneMeat2))
                return Ingredient.WellDoneMeat;

            return Ingredient.Unknown;
        }

        #endregion
        //end of ugly
    }
}