using Microsoft.AspNetCore.Mvc;
using ReadingList.Models;
using System.Collections.Generic;
using System.Net;

namespace ReadingList.Controllers
{
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookManager _bookManager;

        public BooksController(BookManager bookManager)
        {
            _bookManager = bookManager;
        }

        // Get all neutral books (acts as GetAll() upon app startup and after refreshing rate books page)
        [Route("api/books/")]
        [HttpGet]
        public List<Book> GetAllNeutral()
        {
            return _bookManager.GetNeutralBooks();
        }

        [Route("api/books/shelvedBooks")]
        [HttpGet]
        public List<Book> GetShelvedBooks()
        {
            return _bookManager.GetShelvedBooks();
        }

        // Get user-specified book from my shelf
        [Route("api/books/shelvedBook/{id}")]
        [HttpGet]
        public Book GetShelvedBook(string id)
        {
            long idAsLong = 0;
            if (!long.TryParse(id, out idAsLong))
            {
                throw new HttpListenerException(404, "Invalid id");
            }

            Book book = _bookManager.GetShelvedBook(idAsLong);

            if (book == null)
            {
                throw new HttpListenerException(404, $"Book not found for id {id}");
            }

            return book;
        }

        [Route("api/books/neutralBook")]
        [HttpGet]
        // Get a random book from the neutral books list
        public Book GetNeutralBook()
        {
            if (!_bookManager.NeutralIsEmpty())
            {
                return _bookManager.GetNeutralBook(); 
            }
            else
            {
                return null;
            }
        }

        [Route("api/books/neutralIsEmpty")]
        [HttpGet]
        public bool NeutralIsEmpty()
        {
            return _bookManager.NeutralIsEmpty();
        }

        [Route("api/books/addShelved/{id}")]
        [HttpPut]
        public void AddShelvedBook(long id)
        {
            _bookManager.AddShelvedBook(id);
        }

        [Route("api/books/addRejected/{id}")]
        [HttpPut]
        public void AddRejectedBook(long id)
        {
            _bookManager.AddRejectedBook(id);
        }

        [Route("api/books/addAll")]
        [HttpPut]
        public void AddAllToShelf()
        {
            _bookManager.AddAllToShelf();
        }

        [Route("api/books/rejectAll")]
        [HttpPut]
        public void AddAllToRejected()
        {
            _bookManager.AddAllToShelf();
        }

        [Route("api/books/removeNeutral/{id}")]
        [HttpPut]
        public void RemoveNeutralBook(long id)
        {
            _bookManager.RemoveNeutralBook(id);
        }

        [Route("api/books/removeShelved/{id}")]
        [HttpPut]
        public void RemoveShelvedBook(long id)
        {
            _bookManager.RemoveShelvedBook(id);
        }

        [Route("api/books/removeRejected/{id}")]
        [HttpPut]
        public void RemoveRejectedBook(long id)
        {
            _bookManager.RemoveNeutralBook(id);
        }

        [Route("api/books/resetAll")]
        [HttpPut]
        public void ResetAllBooks()
        {
            _bookManager.ResetAllBooks();
        }

        [Route("api/books/finishedBook/{id}")]
        [HttpPut]
        public void FinishedBook(int id)
        {
            _bookManager.FinishedBook(id);
        }

        [Route("api/books/finishedToString/{name}/{id}")]
        [HttpGet]
        public string FinishedToString(string name, long id)
        {
            return _bookManager.FinishedToString(name, id);
        }
    }
}
