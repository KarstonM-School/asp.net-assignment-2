using Microsoft.AspNetCore.Mvc;
using Assignment2.Models;

namespace Assignment2.Controllers
{
    public class BookController : Controller
    {
        // List to store all created books
        private static List<Book> books = new List<Book>();

        // GET
        [Route("/books")]
        public IActionResult GetBooks()
        {
            if(HttpContext.Request.Method != "GET") return BadRequest("The request must be GET.");
            
            if(books.Count == 0)
            {
                return Content("No books available.");
            }
            // Take all book objects and make into strings to display
            List<string> bookStrs = new List<string>();
            foreach (var book in books)
            {
                bookStrs.Add($"ID: {book.Id}, Name: {book.Name}, Author: {book.Author}, Year: {book.YearPublished}, Genre: {book.Genre}");
            }
            string bookList = string.Join("\n", bookStrs);

            return Content($"Here are all the books:\n{bookList}");
        }

        [Route("/books/{id}")]
        public IActionResult GetBookId(int id)
        {
            if (HttpContext.Request.Method != "GET") return BadRequest("The request must be GET.");
            
            foreach (var book in books)
            {
                if(book.Id == id)
                {
                    return Content($"Here is your book:\nID: {book.Id}, Name: {book.Name}, Author: {book.Author}, Year: {book.YearPublished}, Genre: {book.Genre}");
                }
            }
            return Content($"Book with ID {id} not found.");    
        }

        // POST
        [Route("/books/create")]
        public IActionResult CreateBook([FromBody] Book book)
        {
            if (HttpContext.Request.Method != "POST") return BadRequest("The request method must be POST.");

            // Model Validation
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (var i in ModelState.Values)
                {
                    foreach (var err in i.Errors)
                    {
                        errors.Add(err.ErrorMessage);
                    }
                }
                string errMessage = string.Join("\n", errors);
                return BadRequest(errMessage);
            }
            foreach (var b in books)
            {
                if(b.Id == book.Id)
                {
                    return Content($"Cannot create book cause ID {book.Id} is already used.");
                }
            }      
            books.Add(book);
            return Content($"Creating new Book...\nNew Book:\nId: {book.Id}, Name: {book.Name}, Author: {book.Author}, Year Published: {book.YearPublished}, Genre: {book.Genre}.");
        }

        //PUT
       [Route("/books/edit/{id}")]
        public IActionResult EditBook(int id, [FromBody] Book update)
        {
            if(HttpContext.Request.Method != "PUT") return BadRequest("The request method must be PUT.");

            // Model Validation
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (var i in ModelState.Values)
                {
                    foreach (var err in i.Errors)
                    {
                        errors.Add(err.ErrorMessage);
                    }
                }
                string errMessage = string.Join("\n", errors);
                return BadRequest(errMessage);
            }
            // Update
            foreach (var book in books)
            {
                if(book.Id == id)
                {
                    book.Name = update.Name;
                    book.Author = update.Author;
                    book.YearPublished = update.YearPublished;
                    book.Genre = update.Genre;

                    return Content($"Book Updated:\nID: {book.Id}, Name: {book.Name}, Author: {book.Author}, Year: {book.YearPublished}, Genre: {book.Genre}");
                }
            }
            return Content($"Book with ID {id} not found.");    
        }

        // DELETE
        [Route("/books/delete/{id}")]
        public IActionResult DeleteBook(int id)
        {
            if(HttpContext.Request.Method != "DELETE") return BadRequest("The request method must be DELETE.");
            
            foreach (var book in books)
            {
                if(book.Id == id)
                {
                    books.Remove(book);
                    return Content($"The Book {book.Name} was deleted.");
                }
            }
            return Content($"Book with ID {id} not found.");    
        }
    }
}