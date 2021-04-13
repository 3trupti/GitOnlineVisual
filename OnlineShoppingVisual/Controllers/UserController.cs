using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineShoppingVisual.Models;
using System.Web.Http.Cors;
/// <summary>
/// Trupti
/// </summary>


namespace OnlineShoppingVisual.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/UserApi")]
    public class UserController : ApiController
    {
        Online_ShoppingDBEntities4 db = new Online_ShoppingDBEntities4();


        [Route("api/UserApi/InsertCustomer")]

        [HttpPost]
        public bool Post([FromBody] Customer cus)
        {
            Cart car = new Cart();
            WishList Wish = new WishList();
            car.Customer_ID = cus.Customer_ID;
            Wish.Customer_ID=cus.Customer_ID;
            try
            {

                db.Customers.Add(cus);
                db.Carts.Add(car);
                db.WishLists.Add(Wish);
                var res = db.SaveChanges();
                if (res > 0)
                    return true;




            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;

        }

        [Route("api/UserApi/InsertCustomer1")]

        [HttpPost]
        public bool Post([FromBody] Retailer Ret)
        {


            try
            {

                db.Retailers.Add(Ret);
                var res = db.SaveChanges();
                if (res > 0)
                    return true;




            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }


    }
}