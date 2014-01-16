using System.Web.Mvc;
using Phorcys.Core;
using Phorcys.Services;
using SharpArch.Core.PersistenceSupport;
using System.Collections.Generic;
using System;
using System.Linq;
using SharpArch.Web.NHibernate;
using SharpArch.Core;
using Phorcys.Services.Services;
using Phorcys.Data;
using Phorcys.UI.Web.Models;

namespace Phorcys.UI.Web.Controllers {
  [HandleError]
  public class CertificationsController : Controller {
    private readonly ICertificationServices certificationServices = new CertificationServices();
    //private readonly IDiveAgencyServices diveAgencyServices = new DiveAgencyServices();
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
      certifications = certifications.OrderBy(m => m.Title).ToList();

      return View(certifications);
    }

    [Authorize]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult IndexForLocation(int agencyId) {
      user = userServices.FindUser(this.User.Identity.Name);
      systemUser = userServices.FindUser("system");
      IList<Certification> certifications;

      certifications = certificationServices.GetCertificationsForAgency(agencyId, systemUser.Id, user.Id);
      return View("Index", certifications);
    }

    /*
    [Authorize]
    [Transaction]
    public ActionResult Show(int id) {
      Certification certification = certificationServices.Get(id);
      return View(certification);
    }

    [Authorize]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Create() {
      //DiveSiteFormViewModel viewModel = DiveSiteFormViewModel.CreateDiveSiteFormViewModel();
      viewModel = new DiveSitesModel();
      viewModel.DiveSite = new DiveSite();
      IList<SelectListItem> DiveLocationsListItems = BuildLocationList(null);
      viewModel.DiveLocationsListItems = DiveLocationsListItems;
      //viewModel.DiveLocationsListItems = DiveLocationsListItems.OrderBy(m => m.Text).ToList(); //this works too as opposed to the following 2 lines
      //var sortedList = from row in DiveLocationsListItems orderby row.Text select row;
      //viewModel.DiveLocationsListItems = sortedList.ToList();
      return View(viewModel);
    }
    */

    /*
    [Authorize]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult CreateForLocation(int locationId) {
      viewModel = new DiveSitesModel();
      IList<SelectListItem> DiveLocationsListItems = BuildLocationList(locationId);
      viewModel.DiveLocationsListItems = DiveLocationsListItems;

      return View("Create", viewModel);
    }
    */

    /*
    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Create(CertificationsModel certificationsModel) {
      user = userServices.FindUser(this.User.Identity.Name);

      if (ModelState.IsValid) {
        certificationModel.DiveSite.User = user;
        certificationModel.DiveSite.DiveLocation = agencyServices.Get(certificationModel.DiveSite.DiveLocationId);
        certificationModel.DiveSite.Created = DateTime.Now;
        certificationModel.DiveSite.LastModified = DateTime.Now;
        diveSiteServices.Save(certificationModel.DiveSite);

        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = "The diveSite was successfully created.";
        return RedirectToAction("Index");
      }
      certificationModel.DiveLocationsListItems = BuildLocationList(null);
      return View(certificationModel);
    }
    */

    /*
    [Authorize]
    [Transaction]
    public ActionResult Edit(int id) {
      //DiveSiteFormViewModel viewModel = DiveSiteFormViewModel.CreateDiveSiteFormViewModel();
      viewModel = new DiveSitesModel();
      IList<DiveLocation> DiveLocations = GetDiveLocations();
      SelectListItem locationItem;
      IList<SelectListItem> DiveLocationsList = new List<SelectListItem>();

      viewModel.DiveSite = diveSiteServices.Get(id);
      foreach (var location in DiveLocations) {
        locationItem = new SelectListItem();
        locationItem.Text = location.Title;
        locationItem.Value = location.Id.ToString();
        if (viewModel.DiveSite.DiveLocation != null) {
          if (location.Id == viewModel.DiveSite.DiveLocation.Id) {
            locationItem.Selected = true;
          }
        }
        DiveLocationsList.Add(locationItem);
      }
      viewModel.DiveLocationsListItems = DiveLocationsList;
      return View(viewModel);
    }
*/
    /*
    private IList<SelectListItem> BuildLocationList(int? locationId) {
      IList<SelectListItem> LocationList = new List<SelectListItem>();
      IList<DiveLocation> DiveLocations = GetDiveLocations();
      SelectListItem LocationItem;

      DiveLocations = DiveLocations.OrderBy(m => m.Title).ToList();
      foreach (var location in DiveLocations) {
        LocationItem = new SelectListItem();
        LocationItem.Text = location.Title;
        LocationItem.Value = location.Id.ToString();
        if (locationId != null) {
          if (location.Id == locationId) {
            LocationItem.Selected = true;
          }
        }
        LocationList.Add(LocationItem);
      }

      return LocationList;
    }

    //[ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Edit(CertificationsModel CertificationsModel) {
      certificationsModel.DiveAgenciesListItems = BuildDiveAgencyList(certificationModel.DiveSite.Id);
      Certification certificationToUpdate = certificationServices.Get(certificationModel.DiveSite.Id);
      certificationModel.DiveSite.User = certificationToUpdate.User;
      TransferFormValuesTo(certificationToUpdate, certificationModel.DiveSite);
      certificationToUpdate.DiveAgency = diveAgencyServices.Get(certificationModel.DiveSite.DiveLocationId);

      if (ModelState.IsValid) {
        certificationServices.Save(certificationToUpdate);
        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
           "The diveSite was successfully updated.";
        return RedirectToAction("Index");
      }
      else {
        return View(certificationsModel);
      }
    }

    private void TransferFormValuesTo(Certification certificationToUpdate, Certification certificationFromForm) {
      //certificationToUpdate.DiveAgencyId = certificationFromForm.DiveAgencyId;
      certificationToUpdate.Title = certificationFromForm.Title;
      certificationToUpdate.Notes = certificationFromForm.Notes;
      certificationToUpdate.User = certificationFromForm.User;
    }
*/
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Delete(int id) {
      string resultMessage = "The certification was successfully deleted.";
      Certification certificationToDelete = certificationServices.Get(id);

      if (certificationToDelete != null) {
        try {
          certificationServices.Delete(certificationToDelete);
        }
        catch {
          resultMessage = "A problem was encountered preventing the certification from being deleted. " +
                          "Another item likely depends on this certification.";
        }
      }
      else {
        resultMessage = "The certification could not be found for deletion. It may already have been deleted.";
      }

      TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
      return RedirectToAction("Index");
    }

    /*private IList<DiveAgency> GetDiveAgencies() {
      user = userServices.FindUser(this.User.Identity.Name);
      systemUser = userServices.FindUser("system");
      IList<DiveAgency> agencies = diveAgencyServices.GetAllSystemAndUser(systemUser.Id, user.Id);
      return agencies;
    }
    */
  }
}
