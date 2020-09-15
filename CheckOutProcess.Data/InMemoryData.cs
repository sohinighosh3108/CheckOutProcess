using CheckOutProcess.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutProcess.Data
{
    public class InMemoryData : ICheckOutData
    {
        List<Item> items;
        Cart cart;
        List<Promotions> promotions;
        public InMemoryData()
        {
            items = new List<Item>()
            {
                new Item{SKU='A',Quantity=0,Price=50 },
                new Item{SKU='B',Quantity=0,Price=30 },
                new Item{SKU='C',Quantity=0,Price=20 },
                new Item{SKU='D',Quantity=0,Price=15 }
            };
            cart = new Cart();
            foreach(var item in items)
            {
                cart.AddItemsToCart(item);
            }
            promotions = new List<Promotions>()
            {
                new Promotions{Promotion=1, PromoType="Single",PromoDetails="3 'A's priced at 130",PrimarySKUId='A', PromoPrice=130, PromoQty=3},
                new Promotions{Promotion=2,PromoType="Single", PromoDetails="2 'B's priced at 45", PrimarySKUId='B', PromoPrice=45,PromoQty=2},
                new Promotions{Promotion=3, PromoType="Pair", PromoDetails="C+D priced at 30", PrimarySKUId='C',SecondarySKUId='D', PromoPrice=30, PromoQty=1 }
            };
        }
        public void AddItemsToCart(char id, int qty)
        {
            var item = cart.SearchItemById(id);
            if(item!=null)
            {
                cart.RemoveCartItem(id);
                item.Quantity = qty;
                cart.AddItemsToCart(item);
            }
        }

        public int CalculateCheckOutPrice()
        {
            int price = 0;
            int cartPrice = 0;
            int finalPrice = 0;
            var cartItems = cart.GetAllItems();
            foreach(var item in cartItems)
            {
                price = item.Price * item.Quantity;
                cartPrice += price;
            }
            finalPrice = cartPrice;
            
            if(!ApplyPromotions.CheckPromotionStatus(cart))
            {
                finalPrice= ApplyPromotions.ApplyPromotion(cart, promotions);
            }

            return finalPrice;
        }

        public void EditItemsFromCart(char id, int qty)
        {
            cart.EditCart(id, qty);
        }

        public IEnumerable<Item> GetAllItemsFromCart()
        {
            return cart.GetAllItems();
        }

        public void RemoveItemsFromCart(char id)
        {
            cart.RemoveCartItem(id);
        }
    }
}