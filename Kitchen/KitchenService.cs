using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Kitchen
{
    public class KitchenService
    {
        public List<Recipe> KitchenRecipes = new List<Recipe>();
        
        public KitchenService(Fridge currentFridge)
        {
            this._currentFridge = currentFridge;
        }

        public KitchenService()
        {
            this._currentFridge = new Fridge();
        }

        public bool AddRecipe(Recipe newRecipe)
        {
            if (ValidateRecipe(newRecipe) == false) { return false; }
            KitchenRecipes.Add(newRecipe);
            return true;
        }

        private bool ValidateRecipe(Recipe newRecipe)
        {
            if (newRecipe?.Name == null) { return false; }
            return ValidateIngredientInfos(newRecipe.IngredientInfos);
        }

        private bool ValidateIngredientInfos(List<IngredientInfo> newIngredientInfos)
        {
            if (newIngredientInfos.Count == 0) { return false; }
            return newIngredientInfos.All(item => item.Name != null);
        }

        public Recipe GetRecipe(string name)
        {
            return KitchenRecipes.FirstOrDefault(recipeToCheck => recipeToCheck.Name.Equals(name));
        }

        private List<Recipe> PossibleRecipes()
        {
            List<Recipe> availableRecipes = KitchenRecipes.FindAll(recipeToReturn => recipeToReturn.Available.Equals(true));
            List<Recipe> possibleRecipes = new List<Recipe>();

            foreach (var recipe in availableRecipes)
            {
                if (IsRecepieAvailableFromFridge(recipe)) { possibleRecipes.Add(recipe); }
            }

            return possibleRecipes;
        }

        public List<string> PossibleMeals()
        {
            List<string> mealNames = new List<string>();
            List<Recipe> possibleRecipes = PossibleRecipes();

            foreach (var recipe in possibleRecipes)
            {
                mealNames.Add(recipe.Name);
            }
            return mealNames;
        }

        private bool IsRecepieAvailableFromFridge(Recipe recipe)
        {
            return IsRecepieAvailableFromFridge(recipe, 1);
        }


        private bool IsRecepieAvailableFromFridge(Recipe recipe, int noOfMeals)
        {
            foreach (var ingredientInfo in recipe.IngredientInfos)
            {
                if (_currentFridge.IsItemAvailable(ingredientInfo.Name, ingredientInfo.Quantity * noOfMeals) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool PrepareMeal(string meal, int noOfMeals)
        {
            Recipe recipe = GetRecipe(meal);

            if (recipe == null) { return false; }
            if (recipe.Available == false) { return false; }
            if (IsRecepieAvailableFromFridge(recipe, noOfMeals) == false) { return false; }

            foreach (var ingredientInfo in recipe.IngredientInfos)
            {
                _currentFridge.TakeItemFromFridge(ingredientInfo.Name, ingredientInfo.Quantity* noOfMeals);
            }

            return true;
        }
    }
    
}
