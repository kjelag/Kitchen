using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen
{
    public interface IRecipeRepository
    {
        List<Recipe> List();
        Recipe Get(string name);
        void AddRecipe(Recipe recipe);
    }
}
