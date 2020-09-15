using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOutProcess.Core
{
    public class Cart
    {
        private List<Item> CartItems;
        public bool IsPromotionApplied { get; set; }

        public Cart()
        {
            CartItems = new List<Item>();
            IsPromotionApplied = false;
        }
        public void AddItemsToCart(Item item)
        {
            CartItems.Add(item);
        }

        public Item RemoveCartItem(int id)
        {
            var items = CartItems.FirstOrDefault(i => i.SKU == id);
            if(items!=null)
            {
                CartItems.Remove(items);
            }
            return items;
        }

        public Item EditCart(char id,int additionalQty)
        {
            var items = CartItems.FirstOrDefault(i => i.SKU == id);
            if(items!=null)
            {
                CartItems.Remove(items);
                items.Quantity += additionalQty;
                CartItems.Add(items);
            }
            return items;
        }

        public int CountofCartItems()
        {
            return CartItems.Count();
        }
        public IEnumerable<Item> GetAllItems()
        {
            return from c in CartItems
                   orderby c.SKU
                   select c;
        }
        public Item SearchItemById(char id)
        {
            var item = CartItems.FirstOrDefault(i => i.SKUId == id);
            return item;
        }
    }
}