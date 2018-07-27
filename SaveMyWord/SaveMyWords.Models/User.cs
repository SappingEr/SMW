using SaveMyWord.Models.Repositories;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using SaveMyWord.Models.Listeners;
using System;

namespace SaveMyWord.Models
{
    public class User : IUser<long>
    {
        public virtual long Id { get; set; }

        [Display(Name = "Имя")]
        [InFastSearch]
        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }       

        [Display(Name = "Дата авторизации")]
        [CreationDate]
        public virtual DateTime? CreationDate { get; set; }        

    }
}