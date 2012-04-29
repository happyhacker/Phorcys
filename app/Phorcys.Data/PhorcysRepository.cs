using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;
using SharpArch.Core.PersistenceSupport;
using Phorcys.Core;


namespace Phorcys.Data {
  public class PhorcysRepository<T> : Repository<T>, IRepository<T>, Phorcys.Data.IPhorcysRepository<T> {

    public IList<T> GetByCriteria(DetachedCriteria criteria) {
      using (var transaction = Session.BeginTransaction()) {
        IList<T> list = criteria.GetExecutableCriteria(Session).List<T>();
        transaction.Commit();
        return list;
      }
    }

    public IList<T> GetAllSystemAndUser(int userId, int systemId) {
      DetachedCriteria criteria = DetachedCriteria.For(typeof(T));
      criteria.Add(Expression.Or(Expression.Eq("User.Id", userId), Expression.Eq("User.Id", systemId)));

      using (var transaction = Session.BeginTransaction()) {
        IList<T> list = criteria.GetExecutableCriteria(Session).List<T>();
        transaction.Commit();

        return list;
      }
    }
  }
}
