using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen
{
    class RecipeRepository : IRecipeRepository
    {
        private static List<Recipe> recipes = new List<Recipe>();

        public List<Recipe> List()
        {
            return recipes;
        }

        public Recipe Get(string name)
        {
            return recipes.Find(recipe => recipe.Name == name);
        }

        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }
    }
}
