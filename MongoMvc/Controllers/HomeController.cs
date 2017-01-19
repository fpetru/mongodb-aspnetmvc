using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MongoMvc.Interfaces;
using MongoMvc.Model;

namespace MongoMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public HomeController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Read()
        {
            const string nodeId = "2";
            Note noteElement = await _noteRepository.GetNote(nodeId) ?? new Note();
            ViewData["Message"] = string.Format($"Note Id: {nodeId} - Body: {noteElement.Body}");

            return View();
        }

        public IActionResult Init()
        {
            _noteRepository.RemoveAllNotes();
            _noteRepository.AddNote(new Note() { Id = "1", Body = "Test note 1", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 1 });
            _noteRepository.AddNote(new Note() { Id = "2", Body = "Test note 2", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 1 });
            _noteRepository.AddNote(new Note() { Id = "3", Body = "Test note 3", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 2 });
            _noteRepository.AddNote(new Note() { Id = "4", Body = "Test note 4", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 2 });

            ViewData["Message"] = string.Format($"Filled in 4 records");
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
