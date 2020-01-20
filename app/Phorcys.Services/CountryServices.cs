using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate.Criterion;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;
using Phorcys.Core;
using Phorcys.Data;
using log4net;


namespace Phorcys.Services {
  public class CountryServices : ICountryServices {
    public IPhorcysRepository<Country> CountryRepository = new PhorcysRepository<Country>();

     protected static readonly ILog log = LogManager.GetLogger(typeof(ContactServices));

     public Country GetCountry(string id)
     {
       return CountryRepository.GetCountry(id);
     }

    public IList<Country> GetAll()
    {
      IList<Country> countries = CountryRepository.GetAllCountries();

        return countries;
      }

    } 
  }

