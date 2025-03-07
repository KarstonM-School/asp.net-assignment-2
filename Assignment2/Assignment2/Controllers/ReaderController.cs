using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace Assignment2.Controllers
{
    public class ReaderController : Controller
    {
        // List to store all created readers
        private static List<Reader> readers = new List<Reader>();

        // GET
        [Route("/readers")]
        public IActionResult GetReaders()
        {
            if (HttpContext.Request.Method != "GET") return BadRequest("The request must be GET.");

            if (readers.Count == 0)
            {
                return Content("No reader records available.");
            }
            // Take all reader objects and make into strings to display
            List<string> readerStrs = new List<string>();
            foreach (var reader in readers)
            {
                readerStrs.Add($"Id: {reader.Id}, Name: {reader.Name}, Email: {reader.Email}, Phone Number: {reader.PhoneNumber}, Address: {reader.Address}.");
            }
            string readerList = string.Join("\n", readerStrs);

            return Content($"Here are all the Readers:\n{readerList}");
        }

        [Route("/readers/{id}")]
        public IActionResult GetReaderId(int id)
        {
            if (HttpContext.Request.Method != "GET") return BadRequest("The request must be GET.");

            foreach (var reader in readers)
            {
                if (reader.Id == id)
                {
                    return Content($"Here is your Reader:\nId: {reader.Id}, Name: {reader.Name}, Email: {reader.Email}, Phone Number: {reader.PhoneNumber}, Address: {reader.Address}.");
                }
            }
            return Content($"Reader with ID {id} not found.");
        }

        // POST
        [Route("/readers/create")]
        public IActionResult CreateReader([FromBody] Reader reader)
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
            foreach (var r in readers)
            {
                if (r.Id == reader.Id)
                {
                    return Content($"Cannot create book cause ID {reader.Id} is already used.");
                }
            }
            readers.Add(reader);
            return Content($"Creating new Reader...\nNew Reader:\nId: {reader.Id}, Name: {reader.Name}, Email: {reader.Email}, Phone Number: {reader.PhoneNumber}, Address: {reader.Address}.");
        }

        //PUT
        [Route("/readers/edit/{id}")]
        public IActionResult EditReader(int id, [FromBody] Reader update)
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
            foreach (var reader in readers)
            {
                if (reader.Id == id)
                {
                    reader.Name = update.Name;
                    reader.Email = update.Email;
                    reader.PhoneNumber = update.PhoneNumber;
                    reader.Address = update.Address;

                    return Content($"Reader Updated:\nId: {reader.Id}, Name: {reader.Name}, Email: {reader.Email}, Phone Number: {reader.PhoneNumber}, Address: {reader.Address}.");
                }
            }
            return Content($"Reader with ID {id} not found.");
        }

        // DELETE
        [Route("/readers/delete/{id}")]
        public IActionResult DeleteReader(int id)
        {
            if (HttpContext.Request.Method != "DELETE") return BadRequest("The request method must be DELETE.");

            foreach (var reader in readers)
            {
                if (reader.Id == id)
                {
                    readers.Remove(reader);
                    return Content($"The Reader {reader.Name} was deleted.");
                }
            }
            return Content($"Reader with ID {id} not found.");
        }
    }
}
