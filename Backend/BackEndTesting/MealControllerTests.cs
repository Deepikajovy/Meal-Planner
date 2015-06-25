using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Backend.Controllers;
using Backend.Models;
using Moq;
using NUnit.Framework;

namespace BackEndTesting
{
    [TestFixture]
    public class MealControllerTests
    {
        Mock<IApplicationDbContext> db = new Mock<IApplicationDbContext>();

        [SetUp]
        public void TestSetup()
        {
            //Arrange
            IQueryable<Meal> Meals = new List<Meal>()
            {
                new Meal()
                {
                    Id = 1,
                    Name = "Noodles",
                    Description = "Hot Spicy Ramen Noodles",
                    ImageUrl = "www.nowfood.com/foodimage.jpeg",
                },
                new Meal()
                {
                    Id = 2,
                    Name = "Dumplings",
                    Description = "Warm Boiled Dumplings in Soup",
                    ImageUrl = "www.nowfood.com/dumplingimage.jpeg"
                }
            }.AsQueryable();

            var mockMealSet = new Mock<IDbSet<Meal>>();

            mockMealSet.As<IQueryable<Meal>>().Setup(x => x.Provider).Returns(Meals.Provider);
            mockMealSet.As<IQueryable<Meal>>().Setup(x => x.ElementType).Returns(Meals.ElementType);
            mockMealSet.As<IQueryable<Meal>>().Setup(x => x.Expression).Returns(Meals.Expression);
            mockMealSet.As<IQueryable<Meal>>().Setup(x => x.GetEnumerator()).Returns(Meals.GetEnumerator());

            db.Setup(m => m.Meals).Returns(mockMealSet.Object);    
        }

        [Test]
        public void GetMeals_Returns_Data()
        {
            //Act
            MealsController controller = new MealsController(db.Object);

            var meals = controller.GetMeals();

            //Assert
            Assert.True(meals.FirstOrDefault().Name == "Noodles");
        }


    }
}
