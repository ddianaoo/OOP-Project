using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodDelivery.Tests.Domain
{
    public class CartTests
    {
        private Cart CreateCart()
        {
            return new Cart(Guid.NewGuid());
        }

        [Fact]
        public void AddItem_Should_Add_New_Item()
        {
            var cart = CreateCart();
            var dishId = Guid.NewGuid();

            cart.AddItem(dishId, 1);

            Assert.Single(cart.Items);
            Assert.Equal(dishId, cart.Items[0].DishId);
            Assert.Equal(1, cart.Items[0].Quantity);
        }

        [Fact]
        public void AddItem_Should_Increase_Quantity_If_Item_Exists()
        {
            var cart = CreateCart();
            var dishId = Guid.NewGuid();

            cart.AddItem(dishId, 1);
            cart.AddItem(dishId, 2);

            Assert.Single(cart.Items);
            Assert.Equal(3, cart.Items[0].Quantity);
        }

        [Fact]
        public void RemoveItem_Should_Remove_Item()
        {
            var cart = CreateCart();
            var dishId = Guid.NewGuid();

            cart.AddItem(dishId, 1);

            cart.RemoveItem(dishId);

            Assert.Empty(cart.Items);
        }

        [Fact]
        public void RemoveItem_Should_Do_Nothing_If_Item_Not_Exists()
        {
            var cart = CreateCart();

            cart.RemoveItem(Guid.NewGuid());

            Assert.Empty(cart.Items);
        }

        [Fact]
        public void Clear_Should_Remove_All_Items()
        {
            var cart = CreateCart();

            cart.AddItem(Guid.NewGuid(), 1);
            cart.AddItem(Guid.NewGuid(), 2);

            cart.Clear();

            Assert.Empty(cart.Items);
        }
    }
}
