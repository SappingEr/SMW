using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using SaveMyWord.Models.Filters;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.Criterion;

namespace SaveMyWord.Models.Repositories
{
    [Repository]
    public class UserRepository : Repository<User, UserFilter>
    {
        public UserRepository(ISession session) :
            base(session)
        {
        }

        public override void SetupFilter(UserFilter filter, ICriteria crit)
        {
            base.SetupFilter(filter, crit);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.UserName))
                {
                    crit.Add(Restrictions.Eq("UserName", filter.UserName));
                }                

                if (filter.Date != null)
                {
                    if (filter.Date.From.HasValue)
                    {
                        crit.Add(Restrictions.Ge("CreationDate", filter.Date.From.Value));
                    }

                    if (filter.Date.To.HasValue)
                    {
                        crit.Add(Restrictions.Le("CreationDate", filter.Date.To.Value));
                    }
                }
            }
        }

        public IList<User> Find(UserFilter filter, FetchOptions options = null)
        {
            var crit = session.CreateCriteria<User>();
            SetupFilter(filter, crit);
            SetupFetchOptions(crit, options);
            return crit.List<User>();
        }

        public long Count(UserFilter filter)
        {
            var crit = session.CreateCriteria<User>();
            SetupFilter(filter, crit);
            crit.SetProjection(Projections.Count("Id"));
            return Convert.ToInt64(crit.UniqueResult());
        }

        public User GetCurrentUser(IPrincipal user = null)
        {
            user = user ?? HttpContext.Current.User;
            if (user == null || user.Identity == null)
            {
                return null;
            }
            var currentUserId = user.Identity.GetUserId();
            long userId;
            if (string.IsNullOrEmpty(currentUserId) || !long.TryParse(currentUserId, out userId))
            {
                return null;
            }
            return Load(userId);
        }
    }
}