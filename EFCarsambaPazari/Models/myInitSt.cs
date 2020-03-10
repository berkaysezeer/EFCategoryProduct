using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace EFCarsambaPazari.Models
{
    public class myInitSt : DropCreateDatabaseAlways<BazaarContext>
    {
        protected override void Seed(BazaarContext context)
        {
            context.Categories.Add(new Category
            {

                CategoryName = "Refreshments",
                Products = new List<Product>
                {
                    new Product{ProductName="Cola",UnitPrice=3.99m},
                    new Product{ProductName="Fanta"},
                    new Product{ProductName="Ayran"},
                    new Product{ProductName="Water"},
                    new Product{ProductName="Mineral Water"},
                }

            });

            context.Categories.Add(new Category
            {

                CategoryName = "Snacks",
                Products = new List<Product>
                {
                    new Product{ProductName="Cips"},
                    new Product{ProductName="Cookies"},
                    new Product{ProductName="Popcorn"},
                }

            });
        }
    }
}
