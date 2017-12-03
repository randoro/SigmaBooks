using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using SigmaBooks_API.Models;

namespace SigmaBooks_API.Controllers
{
    /// <summary>
    ///     Controller class linked to api/book uri
    /// </summary>
    public class BookController : ApiController
    {
        /// <summary>
        ///     Path to datasource file.
        /// </summary>
        private static readonly string booksXMLPath = HttpContext.Current.Server.MapPath("~/App_Data/books.xml");

        /// <summary>
        ///     Creates a collection of all books deserialized by the translator.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> GetAllBooks()
        {
            var success = false;
            return XMLTranslator.Deserialize<BookCatalog>(booksXMLPath, out success).bookList;
        }

        /// <summary>
        ///     Creates a filtered collection of books that meet certain conditions defined in the method parameters.
        /// </summary>
        /// <param name="advancedSearch">Decides of the search uses advanced searching options.</param>
        /// <param name="useTitle">Decides if the searchString should be matched against book titles.</param>
        /// <param name="useAuthor">Decides if the searchString should be matched against book authors.</param>
        /// <param name="useDescription">Decides if the searchString should be matched against words in the book description.</param>
        /// <param name="limitPrice">Decides if the search results should be limited by pricing.</param>
        /// <param name="minPrice">The minimum price for a book to be listed in the result collection (with limitPrice enabled).</param>
        /// <param name="maxPrice">The maximum price for a book to be listed in the result collection (with limitPrice enabled).</param>
        /// <param name="limitDate">Decides if the search results should be limited by publication date.</param>
        /// <param name="minDate">
        ///     The minimum publication date for a book to be listed in the result collection (with limitDate
        ///     enabled).
        /// </param>
        /// <param name="maxDate">
        ///     The maximum publication date for a book to be listed in the result collection (with limitDate
        ///     enabled).
        /// </param>
        /// <param name="limitGenre">Decides if the search results should be limited by genre.</param>
        /// <param name="genreComputer">Decides if books of the 'Computer' genre are to be included.</param>
        /// <param name="genreFantasy">Decides if books of the 'Fantasy' genre are to be included.</param>
        /// <param name="genreHorror">Decides if books of the 'Horror' genre are to be included.</param>
        /// <param name="genreRomance">Decides if books of the 'Romance' genre are to be included.</param>
        /// <param name="genreScienceFiction">Decides if books of the 'Science Fiction' genre are to be included.</param>
        /// <param name="searchString">Optional Searchstring used to match Title/Author/Description.</param>
        /// <returns>A filtered collection of books.</returns>
        public IEnumerable<Book> GetFilteredBooks(bool advancedSearch, bool useTitle, bool useAuthor,
            bool useDescription, bool limitPrice, string minPrice, string maxPrice, bool limitDate, string minDate,
            string maxDate, bool limitGenre, bool genreComputer, bool genreFantasy, bool genreHorror, bool genreRomance,
            bool genreScienceFiction, string searchString = "")
        {
            var floatMinPrice = 0.0f;
            var floatMaxPrice = 0.0f;
            float.TryParse(minPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out floatMinPrice);
            float.TryParse(maxPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out floatMaxPrice);
            var minDateTime = Convert.ToDateTime(minDate);
            var maxDateTime = Convert.ToDateTime(maxDate);
            var lowerSearchString = searchString.ToLower();

            IEnumerable<Book> filteredBooks = null;
            //If the advanced searching option is disabled, use free search.
            if (!advancedSearch)
            {
                //Free search (Matches with any text from Title/Author/Description
                filteredBooks = GetAllBooks()
                    .Where(p => p.title.ToLower().Contains(lowerSearchString) ||
                                p.author.ToLower().Contains(lowerSearchString) ||
                                p.description.ToLower().Contains(lowerSearchString));
            }
            else
            {
                //Below Filtering is broken into several LINQ where-clauses if conditions are met, This is done for better overview, however this can affect performance in bigger collections.

                //First get all books to the collection.
                filteredBooks = GetAllBooks();

                //If there is a searchString filter the collection.
                if (searchString.Length > 0)
                    filteredBooks = filteredBooks.Where(p =>
                        useTitle && p.title.ToLower().Contains(lowerSearchString) ||
                        useAuthor && p.author.ToLower().Contains(lowerSearchString) ||
                        useDescription && p.description.ToLower().Contains(lowerSearchString));

                //If price is to be limited, filter the collection.
                if (limitPrice)
                    filteredBooks = filteredBooks.Where(p => p.price >= floatMinPrice && p.price <= floatMaxPrice);

                //If publication date is to be limited, filter the collection.
                if (limitDate)
                    filteredBooks = filteredBooks.Where(
                        p => p.secret_publish_date.Ticks >= minDateTime.Ticks &&
                             p.secret_publish_date.Ticks <= maxDateTime.Ticks);

                //If genre is to be limited, filter the collection.
                if (limitGenre)
                    filteredBooks = filteredBooks.Where(
                        p => genreComputer && p.genre == "Computer" || genreFantasy && p.genre == "Fantasy" ||
                             genreHorror && p.genre == "Horror" || genreRomance && p.genre == "Romance" ||
                             genreScienceFiction && p.genre == "ScienceFiction");
            }

            return filteredBooks;
        }
    }
}