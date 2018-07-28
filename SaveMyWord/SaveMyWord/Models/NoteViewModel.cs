using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SaveMyWord.Models
{
    public class NoteViewModel: EntityViewModel<Note>
    {

        
        [Display(Name = "Текст")]
        public string Text { get; set; }

    }
}