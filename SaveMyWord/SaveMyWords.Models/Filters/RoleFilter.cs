using System.ComponentModel.DataAnnotations;

namespace SaveMyWord.Models.Filters
{
    public class RoleFilter : BaseFilter
    {
        [Display(Name = "Имя")]
        public string Name { get; set; }
    }
}