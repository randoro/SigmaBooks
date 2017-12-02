using SigmaBooks_API.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public IEnumerable<Book> GetBook(bool advancedSearch, bool useTitle, bool useAuthor, bool useDescription, bool limitPrice, string minPrice, string maxPrice, bool limitDate, string minDate, string maxDate, bool limitGenre, bool genreComputer, bool genreFantasy, bool genreHorror, bool genreRomance, bool genreScienceFiction, string searchString = "")
        {
            float floatMinPrice = 0.0f;
            float floatMaxPrice = 0.0f;
            float.TryParse(minPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out floatMinPrice);
            float.TryParse(maxPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out floatMaxPrice);
            DateTime minDateTime = Convert.ToDateTime(minDate);
            DateTime maxDateTime = Convert.ToDateTime(maxDate);

            IEnumerable<Book> filteredBooks = null;
            if (!advancedSearch)
            {
                //Free search (Matches with any text from Title/Author/Description
                filteredBooks = GetAllBooks().Where((p) => p.title.ToLower().Contains(searchString.ToLower()) || p.author.ToLower().Contains(searchString.ToLower()) || p.description.ToLower().Contains(searchString.ToLower()));
            }
            else
            {

                System.Diagnostics.Debug.WriteLine("SearchString:" + searchString);
                //First keyword filtering on temporary collection.
                filteredBooks = GetAllBooks();
                if (searchString.Length > 0)
                {
                    filteredBooks = filteredBooks.Where((p) =>
                    (useTitle && p.title.ToLower().Contains(searchString.ToLower())) ||
                    (useAuthor && p.author.ToLower().Contains(searchString.ToLower())) ||
                    (useDescription && p.description.ToLower().Contains(searchString.ToLower())));
                }

                //Filter is broken into several where clauses if conditions are met, This is also done for better overview, however this can affect performance in bigger collections.
                if (limitPrice)
                {
                    filteredBooks = filteredBooks.Where((p) => p.price >= floatMinPrice && p.price <= floatMaxPrice);
                }

                if (limitDate)
                {
                    filteredBooks = filteredBooks.Where((p) => p.publish_date.Ticks >= minDateTime.Ticks && p.publish_date.Ticks <= maxDateTime.Ticks);
                }

                if (limitGenre)
                {
                    filteredBooks = filteredBooks.Where((p) => (genreComputer && p.genre == "Computer") || (genreFantasy && p.genre == "Fantasy") || (genreHorror && p.genre == "Horror") || (genreRomance && p.genre == "Romance") || (genreScienceFiction && p.genre == "ScienceFiction"));
                }


            }
            if (filteredBooks == null)
            {
                return null;
            }
            return filteredBooks;
        }
        //uri + '/' + searchString + '/' + useTitle + '/' + useAuthor + '/' + useDescription + '/' + limitPrice + '/' + minPrice + '/' + maxPrice 
        //   + '/' + limitDate + '/' + minDate + '/' + maxDate + '/' + limitGenre + '/' + genreComputer + '/' + genreFantasy + '/' + genreHorror + '/' + genreRomance + '/' + genreScienceFiction

        //public IHttpActionResult GetBook(string id)
        //{
        //    var product = GetAllBooks().FirstOrDefault((p) => p.id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}
    }
}
