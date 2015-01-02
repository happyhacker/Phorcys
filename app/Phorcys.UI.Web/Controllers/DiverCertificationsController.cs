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
    private readonly InstructorServices instructorServices = new InstructorServices();
    private User user;
    private User systemUser;
    private DiverCertification viewModel = new DiverCertification();

    public DiverCertificationsController(IRepository<DiverCertification> diverCertificationRepository, IRepository<User> userRepository) {
      Check.Require(diverCertificationRepository != null, "diverCertificationRepository may not be null");
      Check.Require(userRepository != null, "userRepository may not be null");
    }

    [Authorize]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index() {
      user = userServices.FindUser(this.User.Identity.Name);

      IList<DiverCertification> diverCertifications = diverCertificationServices.GetAll(user.Id);
      diverCertifications = diverCertifications.OrderBy(m => m.Certification.Title).ToList();

      return View(diverCertifications);
    }


    [Authorize]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Create() {
      DiverCertificationModel model = new DiverCertificationModel();

     return View(model);
    }

    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Create(DiverCertificationModel model) {
      user = userServices.FindUser(this.User.Identity.Name);

      if (ModelState.IsValid) {
        DiverCertification diverCertification = new DiverCertification();

        diverCertification.Notes = model.Notes;
        diverCertification.CertificationNum = model.CertificationNum;
        diverCertification.Certified = model.Certified;
        diverCertification.Instructor = instructorServices.GetInstructor(model.InstructorId);
        diverCertification.Certification = certificationServices.Get(model.CertificationId);
        diverCertification.Created = System.DateTime.Now;
        diverCertification.LastModified = System.DateTime.Now;

        diverCertificationServices.Save(diverCertification);

        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = "The certification was successfully created.";
        return RedirectToAction("Index");
      }
      return View(model);
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
      DiverCertificationModel viewModel = new DiverCertificationModel();
      DiverCertification diverCertification = diverCertificationServices.Get(id);
      viewModel.Id = id;
      viewModel.Notes = diverCertification.Notes;
      viewModel.Certified = diverCertification.Certified;
      viewModel.CertificationNum = diverCertification.CertificationNum;
      viewModel.InstructorId = diverCertification.Instructor.Id;
      viewModel.CertificationId = diverCertification.Certification.Id;
      viewModel.Created = diverCertification.Created;
      viewModel.LastModified = diverCertification.LastModified;

      return View(viewModel);
    }

     [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Edit(DiverCertificationModel model) {
      DiverCertification diverCertificationToUpdate = diverCertificationServices.Get(model.Id);
      TransferFormValuesTo(diverCertificationToUpdate, model);
      //diverCertificationToUpdate.DiveAgency = diveAgencyServices.GetDiveAgency(certificationModel.DiveAgencyId);

      if (ModelState.IsValid) {
        diverCertificationServices.Save(diverCertificationToUpdate);
        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
           "The certifiation was successfully updated.";
        return RedirectToAction("Index");
      }
      else {
        return View(model);
      }
    }

    private void TransferFormValuesTo(DiverCertification diverCertificationToUpdate, DiverCertificationModel diverCertificationFromForm)
    {
      diverCertificationToUpdate.Notes = diverCertificationFromForm.Notes;
      diverCertificationToUpdate.CertificationNum = diverCertificationFromForm.CertificationNum;
      diverCertificationToUpdate.Certified = diverCertificationFromForm.Certified;
      diverCertificationToUpdate.Instructor = instructorServices.GetInstructor(diverCertificationFromForm.InstructorId);
      diverCertificationToUpdate.Certification = certificationServices.Get(diverCertificationFromForm.CertificationId);
      diverCertificationToUpdate.LastModified = System.DateTime.Now;
    }

  }
}
