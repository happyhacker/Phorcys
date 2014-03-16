using System.Web.Mvc;
using Phorcys.Core;
using Phorcys.Services;
using SharpArch.Core.PersistenceSupport;
using System.Collections.Generic;
using System;
using System.Linq;
using SharpArch.Data.NHibernate;
using SharpArch.Web.NHibernate;
using SharpArch.Core;
using Phorcys.Services.Services;
using Phorcys.Data;
using Phorcys.UI.Web.Models;

namespace Phorcys.UI.Web.Controllers {
  [HandleError]
  public class DiverCertificationsController : Controller {
    private readonly IDiverCertificationServices diverCertificationServices = new DiverCertificationServices();
    private readonly ICertificationServices certificationServices = new CertificationServices();
    private readonly IDiveAgencyServices diveAgencyServices = new DiveAgencyServices();
    private readonly Repository<DiveAgency> repository = new Repository<DiveAgency>(); 
    private readonly UserServices userServices = new UserServices();
    private User user;
    private User systemUser;
    private Certification viewModel = new Certification();

    public DiverCertificationsController(IRepository<DiverCertification> diverCertificationRepository, IRepository<User> userRepository) {
      Check.Require(diverCertificationRepository != null, "diverCertificationRepository may not be null");
      Check.Require(userRepository != null, "userRepository may not be null");
    }

    [Authorize]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index() {
      user = userServices.FindUser(this.User.Identity.Name);

      IList<DiverCertification> diverCertifications = diverCertificationServices.GetAll(3);
      diverCertifications = diverCertifications.OrderBy(m => m.Certification.Title).ToList();

      return View(diverCertifications);
    }


    [Authorize]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Create() {
      CertificationModel model = new CertificationModel();

     return View(model);
    }

    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Create(CertificationModel certificationModel) {
      user = userServices.FindUser(this.User.Identity.Name);

      if (ModelState.IsValid) {
        Certification certification = new Certification();
        certification.User = user;
        certification.Title = certificationModel.Title;
        certification.Notes = certificationModel.Notes;
        certification.DiveAgency = diveAgencyServices.GetDiveAgency(certificationModel.DiveAgencyId);
        certification.Created = DateTime.Now;
        certification.LastModified = DateTime.Now;
        certificationServices.Save(certification);

        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = "The certification was successfully created.";
        return RedirectToAction("Index");
      }
      return View(certificationModel);
    }


    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Delete(int id) {
      string resultMessage = "The certification was successfully deleted.";
      DiverCertification certificationToDelete = diverCertificationServices.Get(id);

      if (certificationToDelete != null) {
        try {
          diverCertificationServices.Delete(certificationToDelete);
        } catch {
          resultMessage = "A problem was encountered preventing the certification from being deleted. " +
                          "Another item likely depends on this certification.";
        }
      } else {
        resultMessage = "The certification could not be found for deletion. It may already have been deleted.";
      }

      TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
      return RedirectToAction("Index");
    }


    [Authorize]
    [Transaction]
    public ActionResult Edit(int id) {
      CertificationModel viewModel = new CertificationModel();
      Certification certification = certificationServices.Get(id);
      viewModel.Id = id;
      viewModel.Title = certification.Title;
      viewModel.Notes = certification.Notes;

      return View(viewModel);
    }

     [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Edit(CertificationModel certificationModel) {
      Certification certificationToUpdate = certificationServices.Get(certificationModel.Id);
      //certificationModel.User = certificationToUpdate.User;
      TransferFormValuesTo(certificationToUpdate, certificationModel);
      certificationToUpdate.DiveAgency = diveAgencyServices.GetDiveAgency(certificationModel.DiveAgencyId);

      if (ModelState.IsValid) {
        certificationServices.Save(certificationToUpdate);
        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
           "The certifiation was successfully updated.";
        return RedirectToAction("Index");
      }
      else {
        return View(certificationModel);
      }
    }

    private void TransferFormValuesTo(Certification certificationToUpdate, CertificationModel certificationFromForm)
    {
      certificationToUpdate.Title = certificationFromForm.Title;
      certificationToUpdate.Notes = certificationFromForm.Notes;
      certificationToUpdate.LastModified = System.DateTime.Now;
    }

  }
}
