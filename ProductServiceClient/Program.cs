using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductServiceClient.BlakeReference;

namespace ProductServiceClient
{
    class Program
    {
        static void DisplayProduct(BlakeReference.Product product)
        {
            Console.WriteLine("{0} {1} {2}", product.Name, product.Price, product.Category);
        }

        // Get an entire entity set.
        static void ListAllProducts(BlakeReference.Container container)
        {
            foreach (var p in container.Products)
            {
                DisplayProduct(p);
            }
        }
  
        static void Main(string[] args)
        {
            Uri uri = new Uri("http://localhost:21900/odata/");
            var container = new BlakeReference.Container(uri);

//            container.SendingRequest2 += (s, e) =>
//            {
//                Console.WriteLine("{0} {1}", e.RequestMessage.Method, e.RequestMessage.Url);
//            };
//            ListAllProducts(container);
            var productEnumerator = container.Products.GetEnumerator();
            productEnumerator.MoveNext();
            var product = productEnumerator.Current;

            container.AddToProducts(new Product
            {
                Category = "Sports",
                Name = "head tennis racquet",
                ID = 6,
                Supplier = product.Supplier,
                SupplierId = product.SupplierId,
                Price = new decimal(200.00),
            });

            container.SaveChanges();
        }
    }
}
