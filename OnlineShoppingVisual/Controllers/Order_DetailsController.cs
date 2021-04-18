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
    public class Order_DetailsController : ApiController
    {
        Online_ShoppingDBEntities11 db = new Online_ShoppingDBEntities11();


        [Route("api/UserO/AddOrderDetails/{Cart_Id}")]
        [HttpPost]
        ///Adding to table orders that is customer order details
        
        public string AddOrderDetails(int id)
        {
            
                Cart_Details cartD = new Cart_Details();
                Customer cus = new Customer();
                Product prod = new Product();
                int cd = (from c in db.Carts
                          join cu in db.Customers on c.Customer_ID equals cu.Customer_ID
                          where cu.Customer_Email == email
                          select c.Cart_Id).SingleOrDefault();
                int pri = (from p in db.Products
                           where p.Product_Id == id
                           select p.Product_Price).SingleOrDefault();

                string image1 = (from p in db.Products
                                 where p.Product_Id == id
                                 select p.Product_Image).SingleOrDefault();

                var qty = (from p in db.Products
                           where p.Product_Id == id
                           select p.Product_Ouantity).SingleOrDefault();
                //prod.Product_Ouantity = qty;


                cartD.Cart_Id = cd;
                cartD.Product_Id = id;
                cartD.Product_Price = pri;
                cartD.Product_Quantity = 1;
                cartD.Product_Image = image1;

                var oldqty = db.Products.Where(x => x.Product_Id == id).SingleOrDefault();

                if (oldqty.Product_Ouantity < cartD.Product_Quantity)
                {
                    return ("Not in Stock" + "" + oldqty.Product_Ouantity);

                }
                int minusqty;
                minusqty = Convert.ToInt32(qty) - cartD.Product_Quantity;
                oldqty.Product_Ouantity = minusqty;
                db.SaveChanges();






                try
                {


                    db.Cart_Details.Add(cartD);
                    var res = db.SaveChanges();
                    if (res > 0)
                    {

                        return cartD.Cart_Id.ToString();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;

                }
                return "not saved";






            }


        }
}
