using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NHibernate.Criterion;
using Phorcys.Core;
using Phorcys.Data;
using Phorcys.Services.Services;
using SharpArch.Data.NHibernate;

namespace Phorcys.Services {
  public class CertificationServices : ICertificationServices {
    private IPhorcysRepository<Certification> certificationRepository = new PhorcysRepository<Certification>();
    private IPhorcysRepository<DiveAgency> diveAgencyRepository = new PhorcysRepository<DiveAgency>();

    public void Delete(Certification certificationToDelete) {
      certificationRepository.Delete(certificationToDelete);

      try {
        certificationRepository.DbContext.CommitChanges();
      }
      catch (Exception e) {
        certificationRepository.DbContext.RollbackTransaction();       
        throw new Exception("A problem was encountered preventing the certification from being deleted. " +
                            "Another item likely depends on this certification.");

      }

    }

    public Certification Get(int id) {
      Certification certification = certificationRepository.Get(id);
      return certification;
    }

    public IList<Certification> GetCertificationsForAgency(int agencyId, int systemId, int userId) {
      IList<Certification> certifications;
      DetachedCriteria query = DetachedCriteria.For(typeof(Certification));

      query.Add(Expression.Or(Expression.Eq("User.Id", userId), Expression.Eq("User.Id", systemId)));
      query.Add(Expression.Eq("DiveAgency.Id", agencyId));
      certifications = certificationRepository.GetByCriteria(query);

      return certifications;
    }

    public IList<Certification> GetAllForUser(int userId) {
      UserServices userServices = new UserServices(new Repository<User>());
      User systemUser = userServices.FindUser("system");

      IList<Certification> certifications = certificationRepository.GetAllSystemAndUser(userId, systemUser.Id);

      return certifications;
    }

    public void Save(Certification certification) {
      Certification savedCertification = certificationRepository.SaveOrUpdate(certification);
      certificationRepository.DbContext.CommitChanges();
    }

    public IList<SelectListItem> BuildSelectListForAgency(int agencyId, int? certificationId, int userId, int systemId)
    {
      IList<SelectListItem> CertificationList = new List<SelectListItem>();
      IList<Certification> Certifications = GetCertificationsForAgency(agencyId,userId, systemId);
      SelectListItem CertificationItem;

      Certifications = Certifications.OrderBy(m => m.Title).ToList();
      foreach (var certification in Certifications) {
        CertificationItem = new SelectListItem();
        CertificationItem.Text = certification.Title;
        CertificationItem.Value = certification.Id.ToString();
        if (certificationId != null) {
          if (certification.Id == certificationId) {
            CertificationItem.Selected = true;
          }
        }
        CertificationList.Add(CertificationItem);
      }

      return CertificationList;

    }
  }
}
