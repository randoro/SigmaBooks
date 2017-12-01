using SigmaBooks_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SigmaBooks_API.Controllers
{
    public class BookController : ApiController
    {

        XMLTranslator trans = new XMLTranslator();
        //Book[] products = new Book[]
        //{
        //    new Book { id = "test", author = "J.K Rowlings", title = "Harry potter", genre = "Magic", price = 10, publish_date = DateTime.Now, description = "a book" },
        //    new Book { id = "test2", author = "J.K Rowlings", title = "Harry potter 2", genre = "Magic", price = 10, publish_date = DateTime.Now, description = "a book" },
        //    new Book { id = "test3", author = "J.K Rowlings", title = "Harry potter 3", genre = "Magic", price = 10, publish_date = DateTime.Now, description = "a book" }
        //};

        public IEnumerable<Book> GetAllProducts()
        {
            return trans.TestAll();
        }

        public IHttpActionResult GetProduct(string id)
        {
            var product = trans.TestAll().FirstOrDefault((p) => p.id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
