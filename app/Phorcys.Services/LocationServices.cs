﻿using System;
using System.Collections;
using System.Collections.Generic;
using Phorcys.Core;
using Phorcys.Data;
using log4net;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;

namespace Phorcys.Services {
  public class LocationServices : ILocationServices {
    public IPhorcysRepository<DiveLocation> Repository = new PhorcysRepository<DiveLocation>();
    protected static readonly ILog log = LogManager.GetLogger(typeof(LocationServices));

    public DiveLocation Create(DiveLocation location) {
      DiveLocation retVal = new DiveLocation();

      try {
        Repository.SaveOrUpdate(location);
        Repository.DbContext.CommitChanges();
      }
      catch (Exception e) {
      
      }
      return retVal;
    }

    public DiveLocation Delete(DiveLocation location) {
      DiveLocation retVal = new DiveLocation();

      try {
        Repository.Delete(location);
        Repository.DbContext.CommitChanges();
      }
      catch (Exception e)
      {
        log.Error("Could not delete " + location.Title, e);
        throw e;
      }
      return retVal;
    }

    public DiveLocation Get(int id) {
      return Repository.Get(id);
    }

    public IList<DiveLocation> GetAllSystemAndUser(int systemId, int userId) {
      IList<DiveLocation> locations = Repository.GetAllSystemAndUser(systemId, userId);
      return locations;
    }

    public DiveLocation Save(DiveLocation location) {
      DiveLocation retVal = new DiveLocation();
      try {
        retVal = Repository.SaveOrUpdate(location);
        Repository.DbContext.CommitChanges();
      }
      catch (Exception e) {
        //add logging
      }
      return retVal;
    }
  }
}
