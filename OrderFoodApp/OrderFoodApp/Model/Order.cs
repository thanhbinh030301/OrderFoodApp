using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFoodApp
{
    public class Order
    {
        public string Name { get; set; }
        public int Price { get; set; }            
        public string Img { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
    }
}
