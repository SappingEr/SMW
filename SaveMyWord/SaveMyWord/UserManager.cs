using SaveMyWord.Models;
using Microsoft.AspNet.Identity;
using System;

namespace SaveMyWord
{
    public class UserManager : UserManager<User, long>
    {
        public UserManager(IUserStore<User, long> store)
            : base(store)
        {
            UserValidator = new UserValidator<User, long>(this);
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4                 
            };
        }

        internal void CreateAsync(IUser user, string v)
        {
            throw new NotImplementedException();
        }
    }
}