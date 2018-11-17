using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Encoding.Models
{
    public class MyContext:DbContext
    {
        public MyContext():base("DefaultConnection")
        {

           
        }
        public DbSet<User> Users { get; set; }
    }
}