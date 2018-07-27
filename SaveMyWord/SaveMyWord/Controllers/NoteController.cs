using System;
using SaveMyWord.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaveMyWord.Models.Repositories;

namespace SaveMyWord.Controllers
{
    [Authorize]
    public class NoteController : BaseController
    {
        protected NoteRepository noteRepository;

        public NoteController(UserRepository userRepository, NoteRepository noteRepository) :
            base(userRepository)
        {
            this.noteRepository = noteRepository;
        }

        public ActionResult Index()
        {
            var note = noteRepository.FindAll();
            return View(note);
        }

        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Note model)
        {

            noteRepository.Save(model);

            return RedirectToBackUrl();

        }











    }
}