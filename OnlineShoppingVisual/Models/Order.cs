//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineShoppingVisual.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Ordered_ID { get; set; }
        public int Cart_Id { get; set; }
        public int Product_Id { get; set; }
        public string Product_Image { get; set; }
        public Nullable<int> Product_Price { get; set; }
        public Nullable<int> Product_Quantity { get; set; }
        public Nullable<int> Total_Price { get; set; }
    
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}