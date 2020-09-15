using CheckOutProcess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CheckOutProcess.Test
{
    public class TestCases
    {
        InMemoryData data;
        [Fact]
        public void Test1()
        {
            data = new InMemoryData();
            data.AddItemsToCart('A', 1);
            data.AddItemsToCart('B', 1);
            data.AddItemsToCart('C', 1);
            int result = data.CalculateCheckOutPrice();

            Assert.Equal(100, result);
        }

        [Fact]
        public void Test2()
        {
            data = new InMemoryData();
            data.AddItemsToCart('A', 5);
            data.AddItemsToCart('B', 5);
            data.AddItemsToCart('C', 1);

            int result = data.CalculateCheckOutPrice();

            Assert.Equal(370, result);
        }

        [Fact]
        public void Test3()
        {
            data = new InMemoryData();
            data.AddItemsToCart('A', 3);
            data.AddItemsToCart('B', 5);
            data.AddItemsToCart('C', 1);
            data.AddItemsToCart('D', 1);

            int result = data.CalculateCheckOutPrice();

            Assert.Equal(280, result);
        }
    }
}
