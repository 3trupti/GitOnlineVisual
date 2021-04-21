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
            car.Customer_ID = cus.Customer_ID;
            try
            {

                db.Customers.Add(cus);
                db.Carts.Add(car);
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
        [Route("api/UserApi/GetCartID/{email}/")]

        //Get Cart Id

        [HttpGet]
        public int GetCartID([FromUri ]string email)
        {
            var data = (from c in db.Customers
                        join cr in db.Carts on c.Customer_ID equals cr.Customer_ID
                        where c.Customer_Email == email
                        select cr.Cart_Id).SingleOrDefault();

               return data;

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


        [Route("api/UserApi/DeleteProduct/{id}")]
        [HttpDelete]

        public bool Product_Delete([FromUri] int id)    ////Name Changes in angular 
        {
            try
            {
                var del = db.Products.Where(x => x.Product_Id == id).SingleOrDefault();
                if (del == null)
                    throw new Exception("Id cannot be null ");
                else
                {
                    db.Products.Remove(del);
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


        /////Update Product

        [Route("api/UserApi/UpdateProduct/{id}")]
        [HttpPut]
        public bool Update(int id, [FromBody] Product newprod) //Name change from Put to Update //Angular changes
        {
            try
            {
                var string1 = newprod.Product_Image;

                //  string1.Replace("C:\fakepath\","D:\apisample\src\assets\images\");
                var st2 = string1.Remove(0, 12);
                newprod.Product_Image = st2;
                var olddata = db.Products.Where(x => x.Product_Id == id).SingleOrDefault();
                if (olddata == null)
                    throw new Exception("Invalid Input");
                else
                {
                    olddata.Product_Id = newprod.Product_Id;
                    olddata.Product_BrandName = newprod.Product_BrandName;
                    olddata.Product_Image = newprod.Product_Image;
                    olddata.Product_Description = newprod.Product_Description;
                    olddata.Category_Id = newprod.Category_Id;
                    olddata.Retailer_ID = newprod.Retailer_ID;
                    olddata.Product_Price = newprod.Product_Price;
                    olddata.Product_Ouantity = newprod.Product_Ouantity;
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


        //////new Product accept table
        [Route("api/UserApi/ProductAccept")]
        [HttpPost]
        public bool PostP([FromBody] ProductAccept prod)
        {
            var string1 = prod.Product_Image;

            //  string1.Replace("C:\fakepath\","D:\apisample\src\assets\images\");
            var st2 = string1.Remove(0, 12);
            prod.Product_Image = st2;



            try
            {
                db.ProductAccepts.Add(prod);
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


        ///get all products for admin to approve
        [Route("api/UserApi/ProductGet")]
        [HttpGet]
        public IQueryable<ProdRetailCatModel> ProductGet()
        //this get() method  retrieves all products from the table
        {
            try
            {
                var data = from r in db.Retailers
                           join p in db.ProductAccepts on r.Retailer_ID equals p.Retailer_ID
                           join c in db.Categories on p.Category_Id equals c.Category_ID
                           select new ProdRetailCatModel
                           {
                               Product_Id = p.Prod,
                               Product_BrandName = p.Product_BrandName,
                               Product_Image = p.Product_Image,
                               Product_Description = p.Product_Description,
                               Category_Id = p.Category_Id,
                               Retailer_ID = (int)p.Retailer_ID,
                               Product_Price = p.Product_Price,
                               Product_Quantity = (int)p.Product_Ouantity
                           };



                return data;

                // return (IEnumerable<Product>)data;
                //explicit typecasting

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        ////get product by id to approve
        [Route("api/UserApi/ProdAcceptId/{id}")]

        [HttpGet]
        public ProdRetailCatModel ProdAcceptId([FromUri]int id)
        {
            try
            {
                var data = (from r in db.Retailers
                            join p in db.ProductAccepts on r.Retailer_ID equals p.Retailer_ID
                            join c in db.Categories on p.Category_Id equals c.Category_ID
                            where p.Prod == id
                            select new ProdRetailCatModel
                            {
                                Product_Id = p.Prod,
                                Product_BrandName = p.Product_BrandName,
                                Product_Image = p.Product_Image,
                                Product_Description = p.Product_Description,
                                Category_Id = p.Category_Id,
                                Retailer_ID = (int)p.Retailer_ID,
                                Product_Price = p.Product_Price,
                                Product_Quantity = (int)(p.Product_Ouantity)
                            }).SingleOrDefault();

                return data;


            }



            catch (Exception ex) { throw ex; }

        }

        [Route("api/UserApi/AdminAddProduct")]
        [HttpPost]
        public bool PostP([FromBody] Product prod)
        {
           // var string1 = prod.Product_Image;

            //  string1.Replace("C:\fakepath\","D:\apisample\src\assets\images\");
            //var st2 = string1.Remove(0, 12);
            //prod.Product_Image = st2;


            

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








    }
}