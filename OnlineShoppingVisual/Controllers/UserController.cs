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
        Online_ShoppingDBEntities11 db = new Online_ShoppingDBEntities11();


        [Route("api/UserApi/InsertCustomer")]

        [HttpPost]
        public bool Post([FromBody] Customer cus)
        {
            Cart car = new Cart();
            WishList Wish = new WishList();
            car.Customer_ID = cus.Customer_ID;
            Wish.Customer_ID = cus.Customer_ID;
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



        [Route("api/UserApi/Login/{email}/{pwd}")]
        [HttpGet]
        public string Get(string email, string pwd)
        {
            string result = "";

            try
            {


                var data = db.Customers.Where(x => x.Customer_Email == email && x.Customer_Password == pwd);
                var data2 = db.Retailers.Where(x => x.Retailer_Email == email && x.Retailer_Password == pwd);
                var data3 = db.Admin_Details.Where(x => x.Admin_Email == email && x.Admin_Password == pwd);
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

        //Pratiksha AddProducts part
        [Route("api/UserApi/AddProduct")]
        [HttpPost]
        public bool Post([FromBody] Product prod)
        {
            var string1 = prod.Product_Image;

            //  string1.Replace("C:\fakepath\","D:\apisample\src\assets\images\");
            var st2 = string1.Remove(0, 12);
            prod.Product_Image = st2;

            try
            {
                db.Products.Add(prod);
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

        /*[Route("api/UserApi/GetAllProducts")]
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return db.Products.ToList();
        }*/


        ////////////////Viswa Admin profile

        [Route("api/UserApi/AdminHome")]
        [HttpGet]
        ////////Method for retrieving data from Retailer table
        public IEnumerable<Retailer> Get()
        {
            try
            {
                /* var data = from e in db.Retailers
                            select new Retailer { Retailer_ID = e.Retailer_ID, Retailer_Name = e.Retailer_Name, 
                                Retailer_ContactNo = e.Retailer_ContactNo, Retailer_Address = e.Retailer_Address, Retailer_Country = e.Retailer_Country, Retailer_State = e.Retailer_State, Retailer_City = e.Retailer_City, Retailer_PostalCode = e.Retailer_PostalCode, Retailer_Email = e.Retailer_Email, Retailer_Password = e.Retailer_Password };*/


                return db.Retailers.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Code to delete Retailer for row wise by admin
        [Route("api/UserApi/DeleteRetailer/{id}")]
        [HttpDelete]
        public bool Delete([FromUri] int id)
        {
            try
            {
                var del = db.Retailers.Where(X => X.Retailer_ID == id).SingleOrDefault();
                if (del == null)
                {
                    throw new Exception("Invalid Data");
                }
                else
                {
                    db.Retailers.Remove(del);
                    var res = db.SaveChanges();
                    if (res > 0)
                        return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }



        
         



    }
}