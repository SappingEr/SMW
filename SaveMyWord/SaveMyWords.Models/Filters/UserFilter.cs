using System.ComponentModel.DataAnnotations;

namespace SaveMyWord.Models.Filters

{
    public class UserFilter : BaseFilter
    {
        [Display(Name = "Имя")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public DateRange Date { get; set; }
    }
}
