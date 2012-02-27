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
    public IList<T> GetSystemAndUserRecords(DetachedCriteria detachedCriteria) {
      using (var transaction = Session.BeginTransaction()) {
        IList<T> list = detachedCriteria.GetExecutableCriteria(Session).List<T>();
        transaction.Commit();
        return list;
      }
    }
  }
}
