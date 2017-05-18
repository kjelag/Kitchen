using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen
{
    public class Recipe
    {

        public string Name { get; set; }
        public bool Available { get; set; }
        public List<IngredientInfo> IngredientInfos { get; set; }

    }
}
