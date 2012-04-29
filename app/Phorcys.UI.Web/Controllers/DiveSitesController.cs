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
  public class DiveSitesController : Controller {
    private readonly IDiveSiteServices diveSiteServices = new DiveSiteServices();
    private readonly IPhorcysRepository<DiveSite> diveSiteRepository;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<DiveLocation> locationRepository;
    private DiveSitesModel viewModel = new DiveSitesModel();

    public DiveSitesController(IRepository<DiveSite> diveSiteRepository, IRepository<User> userRepository) {

      Check.Require(diveSiteRepository != null, "diveSiteRepository may not be null");
      Check.Require(userRepository != null, "userRepository may not be null");

      //this.userRepository = SharpArch.Data.NHibernate.Repository<User>();
      this.diveSiteRepository = new PhorcysRepository<DiveSite>();
      this.locationRepository = new PhorcysRepository<DiveLocation>();
      this.userRepository = userRepository;
    }

 
    [Authorize]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Index() {
      UserServices userServices = new UserServices(userRepository);
      DiveSitesIndexModel model = new DiveSitesIndexModel();

      User user = userServices.FindUser(this.User.Identity.Name);
      IList<DiveSite> diveSites = diveSiteServices.GetAllForUser(user.Id);
      diveSites = diveSites.OrderBy(m => m.Title).ToList();
      model.DiveSiteList = diveSites;

      return View(model);
    }

    [Authorize]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult IndexForLocation(int locationId) {
      UserServices userServices = new UserServices(userRepository);
      User user = userServices.FindUser(this.User.Identity.Name);
      User system = userServices.FindUser("system");
      IList<DiveSite> diveSites;

      diveSites = diveSiteServices.GetDiveSitesForLocation(locationId, system.Id, user.Id);
      DiveSitesIndexModel viewModel = new DiveSitesIndexModel();
      viewModel.DiveSiteList = diveSites;
      return View("Index", viewModel);
    }

    [Authorize]
    [Transaction]
    public ActionResult Show(int id) {
      DiveSite diveSite = diveSiteRepository.Get(id);
      return View(diveSite);
    }

    [Authorize]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Create() {
      //DiveSiteFormViewModel viewModel = DiveSiteFormViewModel.CreateDiveSiteFormViewModel();
      viewModel = new DiveSitesModel();
      viewModel.DiveSite = new DiveSite();
      IList<SelectListItem> DiveLocationsListItems = BuildLocationList(null);
      viewModel.DiveLocationsListItems = DiveLocationsListItems;
      //viewModel.DiveLocationsListItems = DiveLocationsListItems.OrderBy(m => m.Text).ToList(); //this works to as opposed to the following 2 lines
      //var sortedList = from row in DiveLocationsListItems orderby row.Text select row;
      //viewModel.DiveLocationsListItems = sortedList.ToList();
      return View(viewModel);
    }

    [Authorize]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult CreateForLocation(int locationId) {
      viewModel = new DiveSitesModel();
      IList<SelectListItem> DiveLocationsListItems = BuildLocationList(locationId);
      viewModel.DiveLocationsListItems = DiveLocationsListItems;

      return View("Create", viewModel);
    }

    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Create(DiveSitesModel diveSitesModel) {
      Core.User user;
      if (ModelState.IsValid) {
        UserServices userServices = new UserServices(this.userRepository);
        user = userServices.FindUser(this.User.Identity.Name);
        diveSitesModel.DiveSite.User = user;
        diveSitesModel.DiveSite.DiveLocation = locationRepository.Get(diveSitesModel.DiveSite.DiveLocationId);
        diveSitesModel.DiveSite.Created = DateTime.Now;
        diveSitesModel.DiveSite.LastModified = DateTime.Now;
        diveSiteRepository.SaveOrUpdate(diveSitesModel.DiveSite);

        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = "The diveSite was successfully created.";
        return RedirectToAction("Index");
      }
      diveSitesModel.DiveLocationsListItems = BuildLocationList(null);
      return View(diveSitesModel);
    }

    [Authorize]
    [Transaction]
    public ActionResult Edit(int id) {
      //DiveSiteFormViewModel viewModel = DiveSiteFormViewModel.CreateDiveSiteFormViewModel();
      viewModel = new DiveSitesModel();
      IList<DiveLocation> DiveLocations = GetDiveLocations();
      SelectListItem locationItem;
      IList<SelectListItem> DiveLocationsList = new List<SelectListItem>();

      viewModel.DiveSite = diveSiteRepository.Get(id);
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
    public ActionResult Edit(DiveSitesModel diveSitesModel) {
      diveSitesModel.DiveLocationsListItems = BuildLocationList(diveSitesModel.DiveSite.Id);
      DiveSite diveSiteToUpdate = diveSiteRepository.Get(diveSitesModel.DiveSite.Id);
      diveSitesModel.DiveSite.User = diveSiteToUpdate.User;
      TransferFormValuesTo(diveSiteToUpdate, diveSitesModel.DiveSite);
      diveSiteToUpdate.DiveLocation = locationRepository.Get(diveSitesModel.DiveSite.DiveLocationId);

      if (ModelState.IsValid) {
        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
           "The diveSite was successfully updated.";
        return RedirectToAction("Index");
      }
      else {
        diveSiteRepository.DbContext.RollbackTransaction();

        return View(diveSitesModel);
      }
    }

    private void TransferFormValuesTo(DiveSite diveSiteToUpdate, DiveSite diveSiteFromForm) {
      diveSiteToUpdate.DiveLocationId = diveSiteFromForm.DiveLocationId;
      diveSiteToUpdate.Title = diveSiteFromForm.Title;
      diveSiteToUpdate.IsFreshWater = diveSiteFromForm.IsFreshWater;
      diveSiteToUpdate.MaxDepth = diveSiteFromForm.MaxDepth;
      diveSiteToUpdate.GeoCode = diveSiteFromForm.GeoCode;
      diveSiteToUpdate.Notes = diveSiteFromForm.Notes;
      diveSiteToUpdate.User = diveSiteFromForm.User;
    }

    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Delete(int id) {
      string resultMessage = "The diveSite was successfully deleted.";
      DiveSite diveSiteToDelete = diveSiteRepository.Get(id);

      if (diveSiteToDelete != null) {
        diveSiteRepository.Delete(diveSiteToDelete);

        try {
          diveSiteRepository.DbContext.CommitChanges();
        }
        catch {
          resultMessage = "A problem was encountered preventing the diveSite from being deleted. " +
                          "Another item likely depends on this diveSite.";
          diveSiteRepository.DbContext.RollbackTransaction();
        }
      }
      else {
        resultMessage = "The diveSite could not be found for deletion. It may already have been deleted.";
      }

      TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
      return RedirectToAction("Index");
    }

    private IList<DiveLocation> GetDiveLocations() {
      IList<DiveLocation> locations = locationRepository.GetAll();
      return locations;
    }

  }
}
