using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace EFCarsambaPazari.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, StringLength(30)]
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [NotMapped]
        public string ProductNameWithPrice
        {
            get
            {
                return ProductName + " - " + UnitPrice.ToString("C", CultureInfo.GetCultureInfo("tr-tr"));
            }

            //set { } //ya set yapmak lazim ya da [NotMapped]
        }

        /*public override string ToString()
        {
            if (UnitPrice.HasValue)
            {
                return ProductName + " - " + UnitPrice.Value.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            }

            return ProductName;
        }*/

    }
}
