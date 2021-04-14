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
        Online_ShoppingDBEntities7 db = new Online_ShoppingDBEntities7();


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


        //////////////////Pratiksha Login
        [Route("api/UserApi/Login/CustomerHome")]
        [HttpGet]
        public void Customerhome()
        {

        }

        [Route("api/UserApi/Login/RetailerHome")]
        [HttpGet]
        public void Retailhome()
        {

        }

        [Route("api/UserApi/Login/AdminHome")]
        [HttpGet]
        public void Adminhome()
        {

        }

        [Route("api/UserApi/Login/{name}/{pwd}")]
        [HttpGet]
        public string Get(string name, string pwd)
        {
            string result = "";

            try
            {


                var data = db.Customers.Where(x => x.Customer_Name == name && x.Customer_Password == pwd);
                var data2 = db.Retailers.Where(x => x.Retailer_Name == name && x.Retailer_Password == pwd);
                var data3 = db.Admin_Details.Where(x => x.Admin_Name == name && x.Admin_Password == pwd);
                if (data.Count() == 0 && data2.Count() == 0 && data3.Count() == 0)
                {
                    result = "Invalid Credentials";

                }
                else
                {
                    if (data.Count() != 0 && data2.Count() == 0 && data3.Count() == 0)
                    {
                        result = "Customer";
                    }
                    else if (data.Count() == 0 && data2.Count() != 0 && data3.Count() == 0)
                    {
                        result = "Retailer";
                    }
                    else if (data.Count() == 0 && data2.Count() == 0 && data3.Count() != 0)
                    {
                        result = "Admin";
                    }



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }

        [Route("api/UserApi/GetAllProducts")]
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return db.Products.ToList();
        }

    }
}