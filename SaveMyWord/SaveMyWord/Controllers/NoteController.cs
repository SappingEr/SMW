

using SaveMyWord.Models;
using SaveMyWord.Models.Filters;
using SaveMyWord.Models.Repositories;
using System.Web.Mvc;

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

        [AllowAnonymous]
        public ActionResult Start()
        {
            return View();
        }

        public ActionResult Index(NoteFilter noteFilter, FetchOptions options)
        {
            var notes = noteRepository.Find(noteFilter, options);
            return View(notes);
        }

        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Note note)
        {
            noteRepository.InvokeInTransaction(() =>
            {
                noteRepository.Save(note);
            });
            return RedirectToBackUrl();
        }

        public ActionResult Delete(long id)
        {
            var note = noteRepository.Load(id);
            noteRepository.Delete(note);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(long id)
        {
            var note = noteRepository.Load(id);
            return View(note);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(long id, Note model)
        {

            var note = noteRepository.Load(model.Id);

            note.NoteName = model.NoteName;
            note.Text = model.Text;
            noteRepository.Save(note);

            return RedirectToBackUrl();
        }








    }
}