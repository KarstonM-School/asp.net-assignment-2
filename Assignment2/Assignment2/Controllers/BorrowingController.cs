using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using static System.Reflection.Metadata.BlobBuilder;

namespace Assignment2.Controllers
{
    public class BorrowingController : Controller
    {
        // List to store all borrowing records
        private static List<Borrowing> borrowings = new List<Borrowing>();

        // GET
        [Route("/borrowings")]
        public IActionResult GetBorrowings()
        {
            if (HttpContext.Request.Method != "GET") return BadRequest("The request must be GET.");

            if (borrowings.Count == 0)
            {
                return Content("No Borrowing records available.");
            }
            // Take all borrowing objects and make into strings to display
            List<string> borrowingStrs = new List<string>();
            foreach (var borrowing in borrowings)
            {
                borrowingStrs.Add($"Id: {borrowing.Id}, Book Id: {borrowing.BookId}, Borrower Id: {borrowing.BorrowerId}, Is Returned: {borrowing.IsReturned}.");
            }
            string borrowingList = string.Join("\n", borrowingStrs);

            return Content($"Here are all the Borrowing records:\n{borrowingList}");
        }

        [Route("/borrowings/{id}")]
        public IActionResult GetBorrowingId(int id)
        {
            if (HttpContext.Request.Method != "GET") return BadRequest("The request must be GET.");

            foreach (var borrowing in borrowings)
            {
                if (borrowing.Id == id)
                {
                    return Content($"Here is your Borrowing record:\nId: {borrowing.Id}, Book Id: {borrowing.BookId}, Borrower Id: {borrowing.BorrowerId}, Is Returned: {borrowing.IsReturned}.");
                }
            }
            return Content($"Borrowing record with ID {id} not found.");
        }

        // POST
        [Route("/borrowings/create")]
        public IActionResult CreateBorrowing([FromBody] Borrowing borrowing)
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
            foreach (var b in borrowings)
            {
                if (b.Id == borrowing.Id)
                {
                    return Content($"Cannot create book cause ID {borrowing.Id} is already used.");
                }
            }
            borrowings.Add(borrowing);
            return Content($"Creating new Borrowing record...\nNew Borrowing record:\nId: {borrowing.Id}, Book Id: {borrowing.BookId}, Borrower Id: {borrowing.BorrowerId}, Is Returned: {borrowing.IsReturned}.");
        }

        //PUT
        [Route("/borrowings/edit/{id}")]
        public IActionResult EditBorrowing(int id, [FromBody] Borrowing update)
        {
            if (HttpContext.Request.Method != "PUT") return BadRequest("The request method must be PUT.");

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
            foreach (var borrowing in borrowings)
            {
                if (borrowing.Id == id)
                {
                    borrowing.BookId = update.BookId;
                    borrowing.BorrowerId = update.BorrowerId;
                    borrowing.IsReturned = update.IsReturned;

                    return Content($"Borrowing record Updated:\nId: {borrowing.Id}, Book Id: {borrowing.BookId}, Borrower Id: {borrowing.BorrowerId}, Is Returned: {borrowing.IsReturned}.");
                }
            }
            return Content($"Borrowing record with ID {id} not found.");
        }

        // DELETE
        [Route("/borrowings/delete/{id}")]
        public IActionResult DeleteBorrowing(int id)
        {
            if (HttpContext.Request.Method != "DELETE") return BadRequest("The request method must be DELETE.");

            foreach (var borrowing in borrowings)
            {
                if (borrowing.Id == id)
                {
                    borrowings.Remove(borrowing);
                    return Content($"The Borrowing record with Id {borrowing.Id} was deleted.");
                }
            }
            return Content($"Borrowing record with ID {id} not found.");
        }
    }
}
