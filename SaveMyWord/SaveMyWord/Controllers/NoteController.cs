using SaveMyWord.Models;
using SaveMyWord.Models.Filters;
using SaveMyWord.Models.Repositories;

using System.Collections.Generic;
using System.IO;
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

        public ActionResult Index(NoteFilter noteFilter, UserFilter userFilter, FetchOptions options)
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
            if (ModelState.IsValid)
            {
                List<Document> documents = new List<Document>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        Document document = new Document()
                        {
                            Name = fileName,
                            Path = Path.GetExtension(fileName)                            
                        };
                        documents.Add(document);

                        var path = Path.Combine(Server.MapPath("~/Content/Upload/"), document.Id + document.Path);
                        file.SaveAs(path);
                    }
                }
                note.Documents = documents;
                noteRepository.Save(note);
                return RedirectToBackUrl();
            }
            return View(note);
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

        public ActionResult UserIndex(FetchOptions options)
        {
            var user = userRepository.GetCurrentUser(User);
            var notes = noteRepository.NoteByUser(user, options);
           
            return View(notes);
        }

        public ActionResult Info(long id)
        {
            var note = noteRepository.Load(id);
            return View(note);
        }






    }
}