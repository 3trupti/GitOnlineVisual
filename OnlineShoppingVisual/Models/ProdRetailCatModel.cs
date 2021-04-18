using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingVisual.Models
{
    public class ProdRetailCatModel
    {
        public int Product_Id { get; set; }
        public string Product_BrandName { get; set; }
        public string Product_Image { get; set; }
        public string Product_Description { get; set; }
        public string Category_Id { get; set; }
        public int Retailer_ID { get; set; }
        public int Product_Price { get; set; }
        public int Product_Quantity { get; set; }
    }
}