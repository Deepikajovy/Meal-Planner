using System;
using System.Collections.Generic;
using Backend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;

namespace BackEndTesting
{
    [TestFixture]
    public class MealControllerTests
    {
        [Test]
        public void GetMeals_Returns_Data_Test()
        {
            //Arrange
            var db = new Mock<IApplicationDbContext>();
            List<Meal> Meals = new List<Meal>();

            Meals.Add(new Meal()
            {
                Id = 1,
                Name = "Noodles", 
                Description = "Hot Spicy Ramen Noodles",
                ImageUrl = "www.nowfood.com/foodimage.jpeg",
            }), 
            new Meal()
            {
                Id = 2,
                Name = "Dumplings",
                Description = "Warm Boiled Dumplings in Soup",
                ImageUrl = "www.nowfood.com/dumplingimage.jpeg"
            };
        }
    }
}
