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
    
    public partial class ProductAccept
    {
        public int Prod { get; set; }
        public string Product_BrandName { get; set; }
        public string Product_Image { get; set; }
        public string Product_Description { get; set; }
        public string Category_Id { get; set; }
        public Nullable<int> Retailer_ID { get; set; }
        public int Product_Price { get; set; }
        public Nullable<int> Product_Ouantity { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Retailer Retailer { get; set; }
    }
}
