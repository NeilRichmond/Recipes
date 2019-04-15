
namespace Recipes.Classes
{
    class Ingredient
    {
        public int ingredient_id;
        public string name;
        public string variant;
        public int size;
        public int qty;
        public float energy;
        public float fat;
        public float saturates;
        public float carbohydrates;
        public float sugars;
        public float fibre;
        public float protein;
        public float salt;
        public string type;
        public float price;

        /// <summary>
        /// An ingredient
        /// </summary>
        /// <param name="id">Ingredient ID</param>
        /// <param name="n">Name</param>
        /// <param name="v">Variant</param>
        /// <param name="s">Size</param>
        /// <param name="q">Quantity</param>
        /// <param name="e">Energy</param>
        /// <param name="f">Fat</param>
        /// <param name="sa">Saturates</param>
        /// <param name="c">Carbohydrates</param>
        /// <param name="su">Sugars</param>
        /// <param name="fi">Fibre</param>
        /// <param name="p">Protein</param>
        /// <param name="sal">Salt</param>
        /// <param name="t">Type</param>
        /// <param name="pri">Price</param>
        public Ingredient(int id,
                          string n,
                          string v,
                          int s,
                          int q,
                          float e,
                          float f,
                          float sa,
                          float c,
                          float su,
                          float fi,
                          float p,
                          float sal,
                          string t,
                          float pri)
        {
            this.ingredient_id = id;
            this.name = n;
            this.variant = v;
            this.size = s;
            this.qty = q;
            this.energy = e;
            this.fat = f;
            this.saturates = s;
            this.carbohydrates = c;
            this.sugars = su;
            this.fibre = fi;
            this.protein = p;
            this.salt = sal;
            this.type = t;
            this.price = pri;
        }
    }
}
