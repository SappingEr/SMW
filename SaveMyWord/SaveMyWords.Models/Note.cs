using SaveMyWord.Models.Listeners;
using SaveMyWord.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyWord.Models
{
    public class Note
    {
        public virtual long Id { get; set; }

        [Display(Name = "Название")]
        [InFastSearch]
        public virtual string NoteName { get; set; }

        [Display(Name = "Текст")]
        public virtual string Text { get; set; }

       
        [Display(Name = "Дата создания")]
        [CreationDate]
        public virtual DateTime? CreationDate { get; set; }        

        [Display(Name = "Автор")]
        [InFastSearch]
        [CreationAuthor]
        public virtual User CreationAuthor { get; set; }

        public virtual IList<Document> Documents { get; set; }





    }
}
