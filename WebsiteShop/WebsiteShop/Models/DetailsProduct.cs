using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseProvider;

namespace WebsiteShop.Models
{
    public class DetailsProduct
    {
        public Product product { get; set; }
        public List<Comments> Comments;

    }
}