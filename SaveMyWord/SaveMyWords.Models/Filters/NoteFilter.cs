using System.ComponentModel.DataAnnotations;

namespace SaveMyWord.Models.Filters
{
    public class NoteFilter: BaseFilter
    {
        [Display(Name = "Название")]
        public virtual string NoteName { get; set; }
      
        public virtual DateRange Date { get; set; }

        [Display(Name = "Автор")]
        public virtual string Author { get; set; }
    }
}
