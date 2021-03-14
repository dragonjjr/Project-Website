using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseProvider;
using System.Data.SqlClient;

namespace DatabaseIO
{
    public class DBIO
    {
        MyDb mydb = new MyDb();

        //add object
        public void addObject<T>(T obj)
        {
            mydb.Set(obj.GetType()).Add(obj);
        }
        //delete object
        public void DeleteObject<T>(T obj)
        {
            mydb.Set(obj.GetType()).Remove(obj);
        }
        //save
        public void Save()
        {
            mydb.SaveChanges();
        }

        //user
        public Account getUser(string email)
        {
            return mydb.Account.Where(a => a.email == email).FirstOrDefault();
        }
        //

        //product
        public Product getProduct(string idProduct)
        {
            return mydb.Product.Where(pd => pd.productID == idProduct).FirstOrDefault();
        }

        public List<Product> getListProduct()
        {
            return mydb.Product.ToList();
        }


        //comment
        public List<Comments> getComments(string idProduct)
        {
            return mydb.Comments.Where(cm => cm.productID == idProduct).ToList();
        }

        //cart
        public List<Cart> getInfoCart(string userEmail)
        {
            return mydb.Cart.Where(c => c.email == userEmail).ToList();
        }
        public Cart getProdCart(string email, string idProd)
        {
            return mydb.Cart.Where(c => c.email == email && c.productID == idProd).FirstOrDefault();
        }
        public int getAmountProd(string email)
        {
            return mydb.Cart.Where(c => c.email == email).Count();
        }

        //order
        public int getAmountOrder()
        {
            return mydb.Orders.Count();
        }
        public Orders getOrder(string id)
        {
            return mydb.Orders.Where(od => od.ID == id).FirstOrDefault();
        }

        //order detail
        public int getAmountOrderDetail(string id)
        {
            return mydb.DetailOrder.Where(dt => dt.ID == id).Count();
        }
        public float totalPriceOrder(string idOrder)
        {
            var LstPrice = mydb.DetailOrder.Where(dt=>dt.ID== idOrder).Join(mydb.Product, dt => dt.ProductID, p => p.productID, (dt, p) => new { dt, p })
                    .Select(t => new
                    {
                        sumPrice = t.p.price * t.dt.amount
                    }).ToList();

            float total=0;

            for (int i=0;i<LstPrice.Count;i++)
            {
                total += (float)LstPrice[i].sumPrice;
            }
            
            return total;
        }
}
}
