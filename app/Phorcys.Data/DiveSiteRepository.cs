﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using Phorcys.Core;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;

namespace Phorcys.Data {
  public class DiveSiteRepository<T> : Repository<T>, IRepository<T>, Phorcys.Data.IDiveSiteRepository<T> {
    public IList<T> GetAllForUser(int userId, int systemId) {
      DetachedCriteria criteria = DetachedCriteria.For(typeof(DiveSite));
      criteria.Add(Expression.Or(Expression.Eq("User.Id", userId), Expression.Eq("User.Id", systemId)));

      using (var transaction = Session.BeginTransaction()) {
        IList<T> list = criteria.GetExecutableCriteria(Session).List<T>();
        transaction.Commit();
        return list;
      }
    }
  }
}
