using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseIO;
using DatabaseProvider;


namespace WebsiteShop.Models
{
    public class InfoDetail
    {
        public Cart cart { get; set; }
        public Product product { get; set; }
    }
}