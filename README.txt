READ ME

How to use:
1. Open SigmaBooks.sln using Visual Studio (Tested with Visual Studio Community 2015)
2. Press F5 or "Start Debugging", this will start the .NET web service locally on a development web server (using Visual studio's IIS Express support).
3. A new page (http://localhost:58963/index.html) should open in your default browser (or the emulator browser choosen in Visual Studio's dropdown).
4. The website defaults to show all books available. Narrow the search results by writing a search word in the textbox and pressing the search button. 

The 'Simple Search' feature will match the search word with any word in the book's Title OR Authorname OR Description by default.
Use the 'Advanced Search' to filter search on all the different data variables available. 
Note that when you use advanced search the book must meet all of the conditions you set in the search options to show up in the results.
For example if you want all books under 20$ that were published between 1950-2000, you leave the searchbox blank, limit price, 
set the maximum slider to 20, limit publication date and set the specific minimum maximum years.

Troubleshooting:
If for some reason the application/server isnt working (even though it's working very well on my side):
1. Make sure you are running Visual Studio with admin privileges.
2. Try using a browser like Google Chrome (I tested Chrome, IE 11, Edge).
3. If you see Visual Studio debugging running but no page opens, Visit http://localhost:58963/index.html on your own.
4. If index.html isn't working try checking http://localhost:58963/api/book (Which is a direct link to the Web API) and make sure it returns a XML tree.


About development:
Part 1.
The Web API is made using ASP .NET. It uses the XMLSerializer class to bind Models/Classes directly to XML trees when serializing/deserializing 
which makes the XMLTranslator class a generical tool that can be used on any xml file with the correct setup classes to support it. 
There are two Models that use this kind of XML bind (BookCatalog and Book). The BookController has two Actions that are linked to api uri. GetAllBooks and GetFilteredBooks.
Almost all of the calculations/filtering is done on server side. Only parameter collection is done on (browser)clientside of the search function.
For filtering out objects from the collections I chose to use LINQ. 
I chose to split the where clauses (in GetFilteredBooks) this can impact performance in big collections. In my opinion the positives (easier understandability/maintenance) outweighs the negatives.
For big collections a database would be prefered over XML data storage either way.
The books.xml is stored in the App_Data folder of the project.
Part 2.
The website is plain html with some JavaScripts and JQuery. Notice that I use getJSON for the HTTP GET mostly because it's much simpler than XMLHttpRequest objects in javascript.
The page uses public css files from W3Schools and Google.
I tested the page in Chrome (works best), Edge (works well, spacing is a little bit off on the slidebars), Internet Explorer (limited). Internet Explorer doesn't support the html 'input date' to full extent (one has to write the date by hand).

END OF FILE