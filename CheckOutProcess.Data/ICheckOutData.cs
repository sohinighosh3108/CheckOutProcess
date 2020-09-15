using CheckOutProcess.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckOutProcess.Data
{
    interface ICheckOutData
    {
        void AddItemsToCart(char id, int qty);
        IEnumerable<Item> GetAllItemsFromCart();
        void RemoveItemsFromCart(char id);
        void EditItemsFromCart(char id, int qty);
        int CalculateCheckOutPrice();
    }
}