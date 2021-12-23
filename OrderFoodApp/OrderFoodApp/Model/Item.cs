using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFoodApp
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public bool Favourite { get; set; }
        public string Descr { get; set; }
        public int Price { get; set; }
        public string MenuId { get; set; }
    }
}
