using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryAppTest.Models
{
    public class Product
    {
        public int PID { get; set; }
        public string PName { get; set; }
        public string PDescription { get; set; }
        public int PQuantity { get; set; }
        public decimal PPrice { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }

        public static string VailidateProductFields(Product product)
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(product.PName))
                errorMessage = "Please enter Product Name.";
            if (string.IsNullOrEmpty(product.PDescription))
                errorMessage += "\nPlease enter Product Description.";
            else if (product.PPrice <= 0)
                errorMessage += "\nPrice should be greater then 0.";
            if (product.PQuantity <= 0)
                errorMessage += "\nQuantity should be greater then 0 and must be integer.";
            return errorMessage;
        }
    }

    public class JqueryDatatableRespose
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IEnumerable<Product> data { get; set; }
        public string error { get; set; }

    }
}