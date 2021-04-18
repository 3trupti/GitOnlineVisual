using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineShoppingVisual.Models;
using System.Web.Http.Cors;

namespace OnlineShoppingVisual.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/UserC")]
    public class CartController : ApiController
    {
        Online_ShoppingDBEntities11 db = new Online_ShoppingDBEntities11();


        [Route("api/UserC/GetCartDetails/{id}")]
        [HttpGet]
        public IQueryable<CartDetails_Cart> Cart_Details([FromUri] int id)
        {
            try
            {
                var data = from cd in db.Cart_Details
                           join p in db.Products on cd.Product_Id equals p.Product_Id
                           join ct in db.Carts on cd.Cart_Id equals ct.Cart_Id
                           where ct.Cart_Id == id
                           select new CartDetails_Cart
                           {
                               Cart_Details_Id = cd.Cart_Details_Id,
                               Cart_Id = cd.Cart_Id,
                               Product_Image=cd.Product_Image,
                               Product_Id = cd.Product_Id,
                               Product_Price = (int)cd.Product_Price,
                               Product_Quantity = (int)cd.Product_Quantity,
                               Total_Order_Amount = (int)cd.Total_Order_Amount
                           };

                // var data = db.Products.Where(x => x.Product_Id == id);
                if (data == null)
                    throw new Exception("No Cart_Details Match the given ID");
                else
                    return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //Delete cart items

        [Route("api/UserC/DeleteCart_Item/{id}")]
        [HttpDelete]

        public bool DeleteCart_Item(int id)
        {
            Product pd = new Product();

            try
            {
                //for updation of product table
                var prodid = (from cd in db.Cart_Details
                              join p in db.Products on cd.Product_Id equals p.Product_Id
                              join ct in db.Carts on cd.Cart_Id equals ct.Cart_Id
                              where cd.Cart_Details_Id==id
                              select cd.Product_Id).SingleOrDefault();

               /* var oldqty = (from pro in db.Products
                           where pro.Product_Id == prodid
                           select pro.Product_Ouantity).SingleOrDefault();*/


                var oldqty = db.Products.Where(x => x.Product_Id == prodid).SingleOrDefault();

                oldqty.Product_Ouantity = oldqty.Product_Ouantity + 1;
                db.SaveChanges();






                var del = db.Cart_Details.Where(x => x.Cart_Details_Id == id).SingleOrDefault();
                
                if (del == null)
                    throw new Exception("Id cannot be null ");
                else
                {
                    db.Cart_Details.Remove(del);
                    var res = db.SaveChanges();
                    if (res > 0)
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }


    }
}
