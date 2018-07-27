using NHibernate;
using NHibernate.Criterion;
using SaveMyWord.Models.Filters;

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






    }
}
