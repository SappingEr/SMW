using System.Collections.Generic;
using SaveMyWord.Models.Filters;
using NHibernate;
using NHibernate.Criterion;

namespace SaveMyWord.Models.Repositories
{
    [Repository]
    public class NoteRepository: Repository<Note, NoteFilter>
    {
        public NoteRepository(ISession session):
            base(session)
        {
        }

        public override void SetupFilter(NoteFilter filter, ICriteria crit)
        {
            base.SetupFilter(filter, crit);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.NoteName))
                {
                    crit.Add(Restrictions.Like("NoteName", filter.NoteName, MatchMode.Anywhere));
                }

                if (!string.IsNullOrEmpty(filter.Author))
                {
                    crit.Add(Restrictions.Like("Author", filter.Author, MatchMode.Anywhere));
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

        public IList<Note> Find(NoteFilter filter, FetchOptions options = null)
        {
            var crit = session.CreateCriteria<Note>();
            SetupFilter(filter, crit);
            SetupFetchOptions(crit, options);
            return crit.List<Note>();
        }

        public IList<Note> NoteByUser(User user, FetchOptions options = null)
        {
            var crit = session.CreateCriteria<Note>().Add(Restrictions.Eq("user.Id", user));
            return crit.List<Note>();
        }





    }
}
