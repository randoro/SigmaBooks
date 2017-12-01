using SigmaBooks_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SigmaBooks_API.Controllers
{
    public class BookController : ApiController
    {
        static readonly string booksXMLPath = HttpContext.Current.Server.MapPath("~/App_Data/books.xml");
        XMLTranslator xmlTranslator = new XMLTranslator();
        

        public IEnumerable<Book> GetAllBooks()
        {
            bool success = false;
            return XMLTranslator.Deserialize<BookCatalog>(booksXMLPath, out success).bookList;
        }

        public IHttpActionResult GetBook(string id)
        {
            var product = GetAllBooks().FirstOrDefault((p) => p.id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
