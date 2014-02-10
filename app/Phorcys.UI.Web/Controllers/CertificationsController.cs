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
  public class CertificationsController : Controller {
    private readonly ICertificationServices certificationServices = new CertificationServices();
    private readonly IDiveAgencyServices diveAgencyServices = new DiveAgencyServices();
    private readonly Repository<DiveAgency> repository = new Repository<DiveAgency>(); 
    private readonly UserServices userServices = new UserServices();
    private User user;
    private User systemUser;
    private Certification viewModel = new Certification();

    public CertificationsController(IRepository<Certification> certificationRepository, IRepository<User> userRepository) {
      Check.Require(certificationRepository != null, "certificationRepository may not be null");
      Check.Require(userRepository != null, "userRepository may not be null");
    }

    [Authorize]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index() {
      user = userServices.FindUser(this.User.Identity.Name);

      IList<Certification> certifications = certificationServices.GetAllForUser(user.Id);
      certifications = certifications.OrderBy(m => m.DiveAgency.Contact.Company).ToList();

      return View(certifications);
    }

    [Authorize]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult IndexForAgency(int agencyId) {
      user = userServices.FindUser(this.User.Identity.Name);
      systemUser = userServices.FindUser("system");
      IList<Certification> certifications;

      certifications = certificationServices.GetCertificationsForAgency(agencyId, systemUser.Id, user.Id);
      return View("Index", certifications);
    }

    [Authorize]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Create() {
      CertificationModel model = new CertificationModel();
      IList<SelectListItem> DiveAgencyListItems = BuildList(null);
      model.DiveAgencyListItems = DiveAgencyListItems;
      model.DiveAgencyListItems = DiveAgencyListItems.OrderBy(m => m.Text).ToList(); //this works too as opposed to the following 2 lines
      var sortedList = from row in DiveAgencyListItems orderby row.Text select row;
      model.DiveAgencyListItems = sortedList.ToList();

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
      certificationModel.DiveAgencyListItems = BuildList(null);
      return View(certificationModel);
    }


    private IList<SelectListItem> BuildList(int? DiveAgencyId) {
      IList<SelectListItem> DiveAgencyList = new List<SelectListItem>();
      IList<DiveAgency> DiveAgencies = GetDiveAgencies();
      SelectListItem DiveAgencyItem;

      DiveAgencies = DiveAgencies.OrderBy(m => m.Contact.Company).ToList();
      foreach (var agency in DiveAgencies) {
        DiveAgencyItem = new SelectListItem();
        DiveAgencyItem.Text = agency.Contact.Company;
        DiveAgencyItem.Value = agency.Id.ToString();
        if (DiveAgencyId != null) {
          if (agency.Id == DiveAgencyId) {
            DiveAgencyItem.Selected = true;
          }
        }
        DiveAgencyList.Add(DiveAgencyItem);
      }

      return DiveAgencyList;
    }

    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Delete(int id) {
      string resultMessage = "The certification was successfully deleted.";
      Certification certificationToDelete = certificationServices.Get(id);

      if (certificationToDelete != null) {
        try {
          certificationServices.Delete(certificationToDelete);
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

    private IList<DiveAgency> GetDiveAgencies() {
      IList<DiveAgency> agencies = repository.GetAll();
      return agencies;
    }

    [Authorize]
    [Transaction]
    public ActionResult Edit(int id) {
      CertificationModel viewModel = new CertificationModel();
      Certification certification = certificationServices.Get(id);
      viewModel.Id = id;
      viewModel.DiveAgencyListItems = BuildList(certification.DiveAgency.Id);
      viewModel.Title = certification.Title;
      viewModel.Notes = certification.Notes;

      return View(viewModel);
    }

     [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Edit(CertificationModel certificationModel) {
      certificationModel.DiveAgencyListItems = BuildList(certificationModel.DiveAgencyId);
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

   /*
    [Authorize]
    [Transaction]
    public ActionResult Show(int id) {
      Certification certification = certificationServices.Get(id);
      return View(certification);
    }
    */
 
  }
}
