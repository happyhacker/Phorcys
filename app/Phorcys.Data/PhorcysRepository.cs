using System;
using System.Collections;
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

    public Country GetCountry(string Id) {
      DetachedCriteria criteria = DetachedCriteria.For(typeof(T));
      criteria.Add(Expression.Eq("CountryId", Id));

      using (var transaction = Session.BeginTransaction()) {
        Country country = criteria.GetExecutableCriteria(Session).UniqueResult<Country>();
        transaction.Commit();

        return country;
      }
    }

    public IList<Country> GetAllCountries() {
      //DetachedCriteria criteria = DetachedCriteria.For(typeof(Country));
      //criteria.Add(Expression.IsNotEmpty("CountryCode"));

      //using (var transaction = Session.BeginTransaction())
      //{
      //  IList<Country> retVal = criteria.GetExecutableCriteria(Session).List<Country>();
      //  transaction.Commit();

      ICriteria criteria = Session.CreateCriteria(typeof(Country));
      IList<Country> retVal = criteria.List<Country>();

      return retVal;
    }

  }

}

