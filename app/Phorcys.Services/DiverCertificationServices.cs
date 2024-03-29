﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using Phorcys.Core;
using Phorcys.Data;
using Phorcys.Services.Services;
using SharpArch.Data.NHibernate;
using System.Web.Mvc;
using log4net;

namespace Phorcys.Services
{
    public class DiverCertificationServices : IDiverCertificationServices
    {
        private IPhorcysRepository<DiverCertification> diverCertificationRepository = new PhorcysRepository<DiverCertification>();
        private IPhorcysRepository<DiveAgency> diveAgencyRepository = new PhorcysRepository<DiveAgency>();
        private static readonly ILog log = LogManager.GetLogger(typeof(CertificationServices));

        public void Delete(DiverCertification diverCertificationToDelete)
        {
            diverCertificationRepository.Delete(diverCertificationToDelete);

            try
            {
                diverCertificationRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {
                log.Error(e.Message + " Inner Exception: " + e.InnerException.Message);
                diverCertificationRepository.DbContext.RollbackTransaction();
                throw new Exception("A problem was encountered preventing the certification from being deleted. " +
                                    "Another item likely depends on this certification.");

            }

        }

        public DiverCertification Get(int id)
        {
            DiverCertification diverCertification = diverCertificationRepository.Get(id);
            return diverCertification;
        }

        public IList<DiverCertification> GetAll(int diverId)
        {

            DetachedCriteria criteria = DetachedCriteria.For<DiverCertification>();
            criteria.Add(Expression.Eq("Diver.Id", diverId));
            IList<DiverCertification> diverCertifications = diverCertificationRepository.GetByCriteria(criteria);

            return diverCertifications;
        }

        public void Save(DiverCertification diverCertification)
        {
            DiverCertification savedCertification = diverCertificationRepository.SaveOrUpdate(diverCertification);
            diverCertificationRepository.DbContext.CommitChanges();
        }
    }
}
