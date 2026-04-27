using System;
using System.Linq;
using FoodDelivery.Domain.Entities;
using Xunit;

namespace FoodDelivery.Tests.DomainTests
{
    public class AdminTests
    {
        private Admin CreateAdmin()
        {
            return new Admin(
                "admin@mail.com",
                "123456",
                "Admin",
                "User",
                new DateTime(1990, 1, 1),
                "+380501111111"
            );
        }

        private Dish CreateDish(string name = "Pizza")
        {
            return new Dish(name, "Cheese pizza", 10m);
        }

        [Fact]
        public void CreateDish_ShouldAddDishToMenu()
        {
            var admin = CreateAdmin();
            var menu = new Menu();
            var dish = CreateDish();

            var result = admin.CreateDish(menu, dish);

            Assert.True(result);
            Assert.Single(menu.GetMenu());
        }

        [Fact]
        public void CreateDish_ShouldFail_WhenNameAlreadyExists()
        {
            var admin = CreateAdmin();
            var menu = new Menu();

            var dish1 = CreateDish("Pizza");
            var dish2 = CreateDish("Pizza");

            admin.CreateDish(menu, dish1);

            var result = admin.CreateDish(menu, dish2);

            Assert.False(result);
            Assert.Single(menu.GetMenu());
        }

        [Fact]
        public void UpdateDish_ShouldModifyDish()
        {
            var admin = CreateAdmin();
            var menu = new Menu();

            var dish = CreateDish("Pizza");
            admin.CreateDish(menu, dish);

            var result = admin.UpdateDish(
                menu,
                dish.Id,
                "Burger",
                "Beef burger",
                15m,
                null
            );

            Assert.True(result);

            var updated = menu.GetMenu().First();

            Assert.Equal("Burger", updated.Name);
            Assert.Equal("Beef burger", updated.Description);
            Assert.Equal(15m, updated.Price);
        }

        [Fact]
        public void UpdateDish_ShouldFail_WhenNameAlreadyExists()
        {
            var admin = CreateAdmin();
            var menu = new Menu();

            var dish1 = CreateDish("Pizza");
            var dish2 = CreateDish("Burger");

            admin.CreateDish(menu, dish1);
            admin.CreateDish(menu, dish2);

            var result = admin.UpdateDish(
                menu,
                dish2.Id,
                "Pizza",
                "New desc",
                20m,
                null
            );

            Assert.False(result);
        }

        [Fact]
        public void DeleteDish_ShouldRemoveDish()
        {
            var admin = CreateAdmin();
            var menu = new Menu();

            var dish = CreateDish();

            admin.CreateDish(menu, dish);

            var result = admin.DeleteDish(menu, dish.Id);

            Assert.True(result);
            Assert.Empty(menu.GetMenu());
        }
    }
}