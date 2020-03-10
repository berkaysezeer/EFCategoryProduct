using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCarsambaPazari.Models
{
    public class BazaarContext : DbContext
    {
        public BazaarContext() : base("baglanti")
        {
            Database.SetInitializer(new myInitSt());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
