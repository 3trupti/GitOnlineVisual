using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingVisual.Models
{
    public class CartDetails_Cart
    {
        public int Cart_Details_Id { get; set; }
        public int Cart_Id { get; set; }
        public string Product_Image { get; set; }
        public int Product_Id { get; set; }
        public int Product_Price { get; set; }
        public int Product_Quantity { get; set; }

        public int Total_Order_Amount
        {
            get; set;
        }
    }
}