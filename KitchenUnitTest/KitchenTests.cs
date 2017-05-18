using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kitchen;
using System.Collections.Generic;

namespace KitchenUnitTest
{
    [TestClass]
    public class KitchenTests
    {

        public class ToKitchen
        { 
            [TestMethod]
            public void AddRecipe()
            {
                KitchenService currentKitchen = new KitchenService();
                Recipe newRecipe = new Recipe
                {
                    Name = "SausageStroganoff",
                    Available = true
                };
                newRecipe.IngredientInfos.Add(new IngredientInfo {Name = "Sausage", Quantity = 1});
                newRecipe.IngredientInfos.Add(new IngredientInfo {Name = "Cream", Quantity = 2.5});
                newRecipe.IngredientInfos.Add(new IngredientInfo {Name = "Tomato puree", Quantity = 2});

                currentKitchen.AddRecipe(newRecipe);
                Recipe result = currentKitchen.GetRecipe(newRecipe.Name);
                Assert.AreEqual(newRecipe.Name, result.Name);
            }

            [TestMethod]
            public void AddRecipeWithoutName()
            {
                KitchenService currentKitchen = new KitchenService();
                Recipe newRecipe = new Recipe();
                newRecipe.IngredientInfos.Add(new IngredientInfo {Name = "Sausage", Quantity = 1});
                Assert.AreEqual(false, currentKitchen.AddRecipe(newRecipe));
            }

            [TestMethod]
            public void AddRecipeWithoutIngredientInfos()
            {
                KitchenService currentKitchen = new KitchenService();
                Recipe newRecipe = new Recipe {Name = "PyttIPanna"};
                Assert.AreEqual(false, currentKitchen.AddRecipe(newRecipe));
            }
        }

        public class FromKitchenWithoutMeals
        {
           [TestMethod]
            public void GetRecipeSimilarName()
            {
                KitchenService currentKitchen = new KitchenService();

                Recipe newRecipe = new Recipe {Name = "SausageStroganoffVeggie"};
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "VeggieSausage", Quantity=1});
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Cream", Quantity=2.5});
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Tomato puree", Quantity=2});
                currentKitchen.AddRecipe(newRecipe);

                newRecipe = new Recipe {Name = "SausageStroganoff"};
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Sausage", Quantity=1});
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Cream", Quantity =2.5});
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Tomato puree", Quantity=2});
                currentKitchen.AddRecipe(newRecipe);

                Recipe result = currentKitchen.GetRecipe("SausageStroganoff");
                Assert.AreEqual(newRecipe.Name, result.Name);

            }
            
            [TestMethod]
            public void WithoutRecipesGetPossibleMeals()
            {
                KitchenService currentKitchen = new KitchenService();
                List<string> result = currentKitchen.PossibleMeals();
                Assert.AreEqual(true, result != null);
                Assert.AreEqual(0, result.Count);
            }
 
            [TestMethod]
            public void WithRecipeGetPossibleMeals()
            {
                KitchenService currentKitchen = new KitchenService();

                Recipe newRecipe = new Recipe {Name = "SausageStroganoffVeggie"};
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "VeggieSausage", Quantity=1});
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Cream", Quantity = 2.5});
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Tomato puree", Quantity = 2});
                currentKitchen.AddRecipe(newRecipe);

                List<string> result = currentKitchen.PossibleMeals();
                Assert.AreEqual(true, result != null);
                Assert.AreEqual(0, result.Count);
            }

            [TestMethod]
            public void PrepareMeal()
            {
                KitchenService currentKitchen = new KitchenService();

                Recipe newRecipe = new Recipe
                {
                    Name = "SausageStroganoff",
                    Available = false
                };
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Sausage", Quantity = 1 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Cream", Quantity = 2.5 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Tomato puree", Quantity = 2 });
                currentKitchen.AddRecipe(newRecipe);

                Assert.AreEqual(false, currentKitchen.PrepareMeal("SausageStroganoff", 1));
            }
        }

        public class FromKitchenWithAvailableMeals
        {
            [TestMethod]
            public void WithEmptyFridgeGetPossibleMeals()
            {
                Fridge currentFridge = new Fridge();
                KitchenService currentKitchen = new KitchenService(currentFridge);

                Recipe newRecipe = new Recipe
                {
                    Name = "SausageStroganoffVeggie",
                    Available = true
                };
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "VeggieSausage", Quantity = 1 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Cream", Quantity = 2.5 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Tomato puree", Quantity = 2 });
                currentKitchen.AddRecipe(newRecipe);

                List<string> result = currentKitchen.PossibleMeals();
                Assert.AreEqual(true, result != null);
                Assert.AreEqual(0, result.Count);
            }

            [TestMethod]
            public void WithFullFridgeGetPossibleMeals()
            {
                Fridge currentFridge = new Fridge();
                currentFridge.AddIngredientToFridge("VeggieSausage", 5);
                currentFridge.AddIngredientToFridge("Cream", 7.5);
                currentFridge.AddIngredientToFridge("Tomato puree", 22);

                KitchenService currentKitchen = new KitchenService(currentFridge);

                Recipe newRecipe = new Recipe
                {
                    Name = "SausageStroganoffVeggie",
                    Available = true
                };
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "VeggieSausage", Quantity = 1 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Cream", Quantity = 2.5 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Tomato puree", Quantity = 2 });
                currentKitchen.AddRecipe(newRecipe);

                List<string> result = currentKitchen.PossibleMeals();
                Assert.AreEqual(true, result != null);
                Assert.AreEqual(1, result.Count);
            }

            [TestMethod]
            public void WithNotEnoughFullFridgeGetPossibleMeals()
            {
                Fridge currentFridge = new Fridge();
                currentFridge.AddIngredientToFridge("VeggieSausage", 5);
                currentFridge.AddIngredientToFridge("Cream", 2);
                currentFridge.AddIngredientToFridge("Tomato puree", 22);

                KitchenService currentKitchen = new KitchenService(currentFridge);

                Recipe newRecipe = new Recipe
                {
                    Name = "SausageStroganoffVeggie",
                    Available = true
                };
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "VeggieSausage", Quantity = 1 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Cream", Quantity = 2.5 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Tomato puree", Quantity = 2 });
                currentKitchen.AddRecipe(newRecipe);

                List<string> result = currentKitchen.PossibleMeals();
                Assert.AreEqual(true, result != null);
                Assert.AreEqual(0, result.Count);
            }

            [TestMethod]
            public void WithFullFridgePrepareMeal()
            {
                Fridge currentFridge = new Fridge();
                currentFridge.AddIngredientToFridge("Sausage", 5);
                currentFridge.AddIngredientToFridge("Cream", 7.5);
                currentFridge.AddIngredientToFridge("Tomato puree", 22);

                KitchenService currentKitchen = new KitchenService(currentFridge);

                Recipe newRecipe = new Recipe
                {
                    Name = "SausageStroganoff",
                    Available = true
                };
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Sausage", Quantity = 1 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Cream", Quantity = 2.5 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Tomato puree", Quantity = 2 });
                currentKitchen.AddRecipe(newRecipe);

                Assert.AreEqual(true, currentKitchen.PrepareMeal("SausageStroganoff", 1));
            }

            [TestMethod]
            public void WithFullFridgePrepareTwoMeals()
            {
                Fridge currentFridge = new Fridge();
                currentFridge.AddIngredientToFridge("Sausage", 5);
                currentFridge.AddIngredientToFridge("Cream", 7.5);
                currentFridge.AddIngredientToFridge("Tomato puree", 22);

                KitchenService currentKitchen = new KitchenService(currentFridge);

                Recipe newRecipe = new Recipe
                {
                    Name = "SausageStroganoff",
                    Available = true
                };
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Sausage", Quantity = 1 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Cream", Quantity = 2.5 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Tomato puree", Quantity = 2 });
                currentKitchen.AddRecipe(newRecipe);

                Boolean result = currentKitchen.PrepareMeal("SausageStroganoff", 2);

                Assert.AreEqual(true, result);
                Assert.AreEqual(3, currentFridge.GetInventoryItem("Sausage").Quantity);
                Assert.AreEqual(2.5, currentFridge.GetInventoryItem("Cream").Quantity);
                Assert.AreEqual(18, currentFridge.GetInventoryItem("Tomato puree").Quantity);

            }

            [TestMethod]
            public void WithFullFridgePrepareTooManyMeals()
            {
                Fridge currentFridge = new Fridge();
                currentFridge.AddIngredientToFridge("Sausage", 5);
                currentFridge.AddIngredientToFridge("Cream", 7.5);
                currentFridge.AddIngredientToFridge("Tomato puree", 22);

                KitchenService currentKitchen = new KitchenService(currentFridge);

                Recipe newRecipe = new Recipe()
                {
                    Name = "SausageStroganoff",
                    Available = true
                };
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Sausage", Quantity = 1 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Cream", Quantity = 2.5 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Tomato puree", Quantity = 2 });
                currentKitchen.AddRecipe(newRecipe);

                Assert.AreEqual(false, currentKitchen.PrepareMeal("SausageStroganoff", 4));
            }
            
            [TestMethod]
            public void WithFullFridgeIncorrectMealPrepareMeal()
            {
                Fridge currentFridge = new Fridge();
                currentFridge.AddIngredientToFridge("Sausage", 5);
                currentFridge.AddIngredientToFridge("Cream", 7.5);
                currentFridge.AddIngredientToFridge("Tomato puree", 22);

                KitchenService currentKitchen = new KitchenService(currentFridge);

                Recipe newRecipe = new Recipe
                {
                    Name = "SausageStroganoff",
                    Available = true
                };
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Sausage", Quantity = 1 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Cream", Quantity = 2.5 });
                newRecipe.IngredientInfos.Add(new IngredientInfo { Name = "Tomato puree", Quantity = 2 });
                currentKitchen.AddRecipe(newRecipe);

                Assert.AreEqual(false, currentKitchen.PrepareMeal("Squirrel", 1));
            }

        }


    }
}
