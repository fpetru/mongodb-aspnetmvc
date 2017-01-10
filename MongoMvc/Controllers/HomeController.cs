using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";
            Note noteElement = await GetNoteByIdInternal("2");

            return View(noteElement.Body);
        }

        public IActionResult Init()
        {
            _noteRepository.RemoveAllNotes();
            _noteRepository.AddNote(new Note() { Id = "1", Body = "Test note 1", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 1 });
            _noteRepository.AddNote(new Note() { Id = "2", Body = "Test note 2", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 1 });
            _noteRepository.AddNote(new Note() { Id = "3", Body = "Test note 3", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 2 });
            _noteRepository.AddNote(new Note() { Id = "4", Body = "Test note 4", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 2 });

            return View();
        }

        private async Task<Note> GetNoteByIdInternal(string id)
        {
            return await _noteRepository.GetNote(id) ?? new Note();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
