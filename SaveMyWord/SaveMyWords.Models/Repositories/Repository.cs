using SaveMyWord.Models.Filters;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;

namespace SaveMyWord.Models.Repositories
{
    public class Repository<T, FT>
        where T : class
        where FT : BaseFilter
    {
        protected ISession session;

        public Repository(ISession session)
        {
            this.session = session;
        }

        public virtual T Load(long id)
        {
            return session.Load<T>(id);
        }

        public virtual void Save(T entity)
        {
            session.SaveOrUpdate(entity);
        }

        public void InvokeInTransaction(Action action)
        {
            using (var tr = session.BeginTransaction())
            {
                action.Invoke();
                tr.Commit();
            }
        }

        public virtual void Delete(T entity)
        {
            using (var tr = session.BeginTransaction())
            {
                session.Delete(entity);
                tr.Commit();
            }
        }

        public virtual IList<T> FindAll()
        {
            var crit = session.CreateCriteria<T>();
            return crit.List<T>();
        }

        public virtual void SetupFilter(FT filter, ICriteria crit)
        {
            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.SearchString))
                {
                    SetupFastSearchFilter(crit, filter.SearchString);
                }
            }
        }

        public virtual void SetupFastSearchFilter(ICriteria crit, string searchStr)
        {
            ICriterion criterion = null;
            foreach (var prop in typeof(T).GetProperties())
            {
                var attr = prop.GetCustomAttribute<InFastSearchAttribute>();
                if (attr == null)
                {
                    continue;
                }
                var likeExpresion = Restrictions.Like(prop.Name, searchStr, MatchMode.Anywhere);
                criterion = criterion == null ? likeExpresion : Restrictions.Or(criterion, likeExpresion);
            }
            if (criterion != null)
            {
                crit.Add(criterion);
            }
        }

        public virtual void SetupFetchOptions(ICriteria crit, FetchOptions options)
        {
            if (options != null)
            {
                if (options.Start > 0)
                {
                    crit.SetFirstResult(options.Start);
                }
                if (options.Count > 0)
                {
                    crit.SetMaxResults(options.Count);
                }
                if (!string.IsNullOrEmpty(options.SortExpression))
                {
                    crit.AddOrder(options.SortDirection == SortDirection.Ascending ?
                        Order.Asc(options.SortExpression) :
                        Order.Desc(options.SortExpression));
                }
            }
        }
    }
}