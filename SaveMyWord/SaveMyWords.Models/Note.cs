using SaveMyWord.Models.Listeners;
using SaveMyWord.Models.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyWord.Models
{
    public class Note
    {
        public virtual long Id { get; set; }

        [InFastSearch]
        public virtual string NoteName { get; set; }


        public virtual string Text { get; set; }

        [Display(Name = "Дата создания")]
        [CreationDate]
        public virtual DateTime? CreationDate { get; set; }

        [Display(Name = "Автор")]
        [InFastSearch]
        [CreationAuthor]
        public virtual User CreationAuthor { get; set; }

    }
}
