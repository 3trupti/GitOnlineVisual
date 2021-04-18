using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using OnlineShoppingVisual.Models;


namespace OnlineShoppingVisual.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/User")]
    public class ProductController : ApiController
    {
        Online_ShoppingDBEntities11 db = new Online_ShoppingDBEntities11();

        /*[Route("api/User/GetAllProducts")]
    [HttpGet]
    public IEnumerable<Product> Get()
    {
      return db.Products.ToList();

    }
    
     ------------------------------------------------------------------------
   
    */


        [Route("api/User/GetAllProducts")]
        [HttpGet]
        public IQueryable<ProdRetailCatModel> Get()
        //this get() method  retrieves all products from the table
        {
            try
            {
                var data = from r in db.Retailers
                           join p in db.Products on r.Retailer_ID equals p.Retailer_ID
                           join c in db.Categories on p.Category_Id equals c.Category_ID
                           select new ProdRetailCatModel
                           {
                               Product_Id = p.Product_Id,
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


        [Route("api/User/GetProductsByRetailerID/{email}/")]
        [HttpGet]
        public IQueryable<ProdRetailCatModel> Get([FromUri] string email)
        {

            try
            {
                var data = from r in db.Retailers
                           join p in db.Products on r.Retailer_ID equals p.Retailer_ID
                           join c in db.Categories on p.Category_Id equals c.Category_ID
                           where r.Retailer_Email == email

                           select new ProdRetailCatModel
                           {
                               Product_Id = p.Product_Id,
                               Product_BrandName = p.Product_BrandName,
                               Product_Image = p.Product_Image,
                               Product_Description = p.Product_Description,
                               Category_Id = p.Category_Id,
                               Retailer_ID = (int)p.Retailer_ID,
                               Product_Price = p.Product_Price,
                               Product_Quantity = (int)p.Product_Ouantity
                           };

                // var data = db.Products.Where(x => x.Product_Id == id);
                if (data == null)
                    throw new Exception("No Products Match the given ID");
                else
                    return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /* [HttpPost]
         [Route("api/User/AddProducts")]
         public bool Post([FromBody] Product prod)
         {
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
         }*/
        [Route("api/User/UpdateProduct/{id}")]
        [HttpPut]
        public bool Update(int id, [FromBody] Product newprod) //Name change from Put to Update //Angular changes
        {
            try
            {
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



        [Route("api/User/DeleteProduct/{id}")]
        [HttpDelete]

        public bool Product_Delete(int id)    ////Name Changes in angular 
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

        //--------------------------
        //-------get all products based on category name.
        [Route("api/User/GetProductsByCategoryName/{cname}")]
        [HttpGet]
        public IQueryable<ProdRetailCatModel> Category_Get([FromUri] string cname) 
        {
            try
            {
                var data = from r in db.Retailers
                           join p in db.Products on r.Retailer_ID equals p.Retailer_ID
                           join c in db.Categories on p.Category_Id equals c.Category_ID
                           where c.Category_Name == cname
                           select new ProdRetailCatModel
                           {
                               Product_Id = p.Product_Id,
                               Product_BrandName = p.Product_BrandName,
                               Product_Image = p.Product_Image,
                               Product_Description = p.Product_Description,
                               Category_Id = p.Category_Id,
                               Retailer_ID = (int)p.Retailer_ID,
                               Product_Price = p.Product_Price,
                               Product_Quantity = (int)p.Product_Ouantity
                           };

                // var data = db.Products.Where(x => x.Product_Id == id);
                if (data == null)
                    throw new Exception("No Products Match the given ID");
                else
                    return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // sort by brand name

        [Route("api/User/SortByProductBrandName/{bname}/{cname}")]
        [HttpGet]
        public IQueryable<ProdRetailCatModel> FilterByBrand([FromUri] string bname,string cname)
        {
            try
            {
                var data = from r in db.Retailers
                           join p in db.Products on r.Retailer_ID equals p.Retailer_ID
                           join c in db.Categories on p.Category_Id equals c.Category_ID
                           where p.Product_BrandName == bname && c.Category_Name==cname
                           select new ProdRetailCatModel
                           {
                               Product_Id = p.Product_Id,
                               Product_BrandName = p.Product_BrandName,
                               Product_Image = p.Product_Image,
                               Product_Description = p.Product_Description,
                               Category_Id = p.Category_Id,
                               Retailer_ID = (int)p.Retailer_ID,
                               Product_Price = p.Product_Price,
                               Product_Quantity = (int)p.Product_Ouantity
                           };

                // var data = db.Products.Where(x => x.Product_Id == id);
                if (data == null)
                    throw new Exception("No Products Match the given ID");
                else
                    return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------------------------------------------------------------------------------------------------->

        //sort product by price

        [Route("api/User/SortByProductPrice/{price}/{cname}")]
        [HttpGet]
        public IQueryable<ProdRetailCatModel> FilterByPrice([FromUri] int price, string cname)
        {
            try
            {
                var data = from r in db.Retailers
                           join p in db.Products on r.Retailer_ID equals p.Retailer_ID
                           join c in db.Categories on p.Category_Id equals c.Category_ID
                           where p.Product_Price < price && c.Category_Name == cname
                           select new ProdRetailCatModel
                           {
                               Product_Id = p.Product_Id,
                               Product_BrandName = p.Product_BrandName,
                               Product_Image = p.Product_Image,
                               Product_Description = p.Product_Description,
                               Category_Id = p.Category_Id,
                               Retailer_ID = (int)p.Retailer_ID,
                               Product_Price = p.Product_Price,
                               Product_Quantity = (int)p.Product_Ouantity
                           };

                // var data = db.Products.Where(x => x.Product_Id == id);
                if (data == null)
                    throw new Exception("No Products Match the given ID");
                else
                    return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Route("api/User/Compare/{cname}")]
        [HttpGet]
        public IQueryable<ProdRetailCatModel> Compare([FromUri] string cname)
        {
            try
            {
                var data = (from r in db.Retailers
                            join p in db.Products on r.Retailer_ID equals p.Retailer_ID
                            join c in db.Categories on p.Category_Id equals c.Category_ID
                            where c.Category_Name == cname
                            select new ProdRetailCatModel
                            {
                                Product_Id = p.Product_Id,
                                Product_BrandName = p.Product_BrandName,
                                Product_Image = p.Product_Image,
                                Product_Description = p.Product_Description,
                                Category_Id = p.Category_Id,
                                Retailer_ID = (int)p.Retailer_ID,
                                Product_Price = p.Product_Price,
                                Product_Quantity = (int)p.Product_Ouantity
                            }).Take(4);

                // var data = db.Products.Where(x => x.Product_Id == id);
                if (data == null)
                    throw new Exception("No Products Match the given ID");
                else
                    return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Sort By price and name
        [Route("api/User/SortByPrice/{cname}")]
        [HttpGet]
        public IQueryable<ProdRetailCatModel> Sort_Price([FromUri] string cname)
        {
            try
            {
                var data = from r in db.Retailers
                           join p in db.Products on r.Retailer_ID equals p.Retailer_ID
                           join c in db.Categories on p.Category_Id equals c.Category_ID
                           where c.Category_Name == cname
                           orderby p.Product_Price
                           select new ProdRetailCatModel
                           {
                               Product_Id = p.Product_Id,
                               Product_BrandName = p.Product_BrandName,
                               Product_Image = p.Product_Image,
                               Product_Description = p.Product_Description,
                               Category_Id = p.Category_Id,
                               Retailer_ID = (int)p.Retailer_ID,
                               Product_Price = p.Product_Price,
                               Product_Quantity = (int)p.Product_Ouantity
                           };

                // var data = db.Products.Where(x => x.Product_Id == id);
                if (data == null)
                    throw new Exception("No Products Match the given ID");
                else
                    return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("api/User/SortByName/{cname}")]
        [HttpGet]
        public IQueryable<ProdRetailCatModel> Sort_Name([FromUri] string cname)
        {
            try
            {
                var data = from r in db.Retailers
                           join p in db.Products on r.Retailer_ID equals p.Retailer_ID
                           join c in db.Categories on p.Category_Id equals c.Category_ID
                           where c.Category_Name == cname
                           orderby p.Product_BrandName
                           select new ProdRetailCatModel
                           {
                               Product_Id = p.Product_Id,
                               Product_BrandName = p.Product_BrandName,
                               Product_Image = p.Product_Image,
                               Product_Description = p.Product_Description,
                               Category_Id = p.Category_Id,
                               Retailer_ID = (int)p.Retailer_ID,
                               Product_Price = p.Product_Price,
                               Product_Quantity = (int)p.Product_Ouantity
                           };

                // var data = db.Products.Where(x => x.Product_Id == id);
                if (data == null)
                    throw new Exception("No Products Match the given ID");
                else
                    return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







        /* [HttpPost]
         [Route("api/User/AddProducts")]
         public bool Post([FromBody] Product prod)
         {
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
         }*/
        [Route("api/User/UpdateProduct/{id}")]
        [HttpPut]
        public bool Put(int id, [FromBody] Product newprod)
        {
            try
            {
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



        [Route("api/User/DeleteProduct/{id}")]
        [HttpDelete]

        public bool Delete(int id)
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


        ////Add to cart
        [Route("api/User/AddToCart/{id}/{email}/")]

        [HttpPost]
        public string AddToCart(int id, string email)
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

            var qty= (from p in db.Products
                      where p.Product_Id == id
                      select p.Product_Ouantity).SingleOrDefault();
            //prod.Product_Ouantity = qty;


            cartD.Cart_Id = cd;
            cartD.Product_Id = id;
            cartD.Product_Price = pri;
            cartD.Product_Quantity = 1;
            cartD.Product_Image = image1;

            var oldqty = db.Products.Where(x => x.Product_Id == id).SingleOrDefault();

            if (oldqty.Product_Ouantity< cartD.Product_Quantity)
            {
                return ("Not in Stock" + "" + oldqty.Product_Ouantity );

            }
            int minusqty;
            minusqty = Convert.ToInt32(qty) - cartD.Product_Quantity;
            oldqty.Product_Ouantity = minusqty;
            db.SaveChanges();
            


            


            try
            {
                
                
                db.Cart_Details.Add(cartD);
                var res=db.SaveChanges();
                if (res > 0)
                {

                    return cartD.Cart_Id.ToString();
                }

            }
            catch(Exception ex)
            {
                throw ex;

            }
            return "not saved";

            
            
                      


        }
    }
}
    

    

