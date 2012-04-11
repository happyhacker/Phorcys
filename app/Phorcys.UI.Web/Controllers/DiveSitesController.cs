using System.Web.Mvc;
using Phorcys.Core;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using SharpArch.Data.NHibernate;
using SharpArch.Web.NHibernate;
using NHibernate.Validator.Engine;
using System.Text;
using SharpArch.Web.CommonValidator;
using SharpArch.Core;
using SharpArch.Data;
using Phorcys.Web.Controllers;
using Phorcys.Services.Services;
using Phorcys.Data;
using NHibernate.Criterion;
using Phorcys.UI.Web.Models;

namespace Phorcys.UI.Web.Controllers {
  [HandleError]
  public class DiveSitesController : Controller
  {
    private readonly IPhorcysRepository<DiveSite> diveSiteRepository;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<DiveLocation> locationRepository;

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
      User user = userServices.FindUser(this.User.Identity.Name);
      User system = userServices.FindUser("system");
      DetachedCriteria query = DetachedCriteria.For(typeof(DiveSite));
      int locationId = Int32.Parse(Request.Params.Get("locationId") ?? "0");
      
      query.Add(Expression.Or(Expression.Eq("User.Id", user.Id), Expression.Eq("User.Id", system.Id)));
      if (locationId > 0)
      {
          query.Add(Expression.Eq("LocationId", locationId));
      }
      //query.AddOrder(Order.Asc("DiveLocation.Title")); //didn't understand this property for some reason
      IList<DiveSite> diveSites = this.diveSiteRepository.GetSystemAndUserRecords(query); //.GetAll();
      diveSites = diveSites.OrderBy(m => m.Title).ToList();
      return View(diveSites);
    }

   [Authorize]
   [Transaction]
   [AcceptVerbs(HttpVerbs.Get)]
   public ActionResult IndexForLocation(int locationId)
   {
       UserServices userServices = new UserServices(userRepository);
       User user = userServices.FindUser(this.User.Identity.Name);
       User system = userServices.FindUser("system");
       DetachedCriteria query = DetachedCriteria.For(typeof(DiveSite));

       query.Add(Expression.Or(Expression.Eq("User.Id", user.Id), Expression.Eq("User.Id", system.Id)));
       query.Add(Expression.Eq("DiveLocation.Id", locationId));
       IList<DiveSite> diveSites = this.diveSiteRepository.GetSystemAndUserRecords(query); //.GetAll();
       return View("Index",diveSites);
   }

    [Authorize]
    [Transaction]
    public ActionResult Show(int id) {
      DiveSite diveSite = diveSiteRepository.Get(id);
      return View(diveSite);
    }

    [Authorize]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Create()
    {
      //DiveSiteFormViewModel viewModel = DiveSiteFormViewModel.CreateDiveSiteFormViewModel();
        DiveSitesModel viewModel = new DiveSitesModel();
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
    public ActionResult CreateForLocation(int locationId)
    {
        DiveSitesModel viewModel = new DiveSitesModel();
        IList<SelectListItem> DiveLocationsListItems = BuildLocationList(locationId);
        viewModel.DiveLocationsListItems = DiveLocationsListItems;

        return View("Create",viewModel);
    }

    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Create(DiveSite diveSite)
    {
      Core.User user;
      if (ModelState.IsValid) {
        UserServices userServices = new UserServices(this.userRepository);
        user = userServices.FindUser(this.User.Identity.Name);
        diveSite.User = user;
        diveSite.DiveLocation = locationRepository.Get(diveSite.DiveLocationId);
        diveSite.Created = DateTime.Now;
        diveSite.LastModified = DateTime.Now;
        diveSiteRepository.SaveOrUpdate(diveSite);

        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = "The diveSite was successfully created.";
        return RedirectToAction("Index");
      }
      return View(diveSite);
    }

    [Authorize]
    [Transaction]
    public ActionResult Edit(int id) {
      //DiveSiteFormViewModel viewModel = DiveSiteFormViewModel.CreateDiveSiteFormViewModel();
       DiveSitesModel viewModel = new DiveSitesModel();
      IList<DiveLocation> DiveLocations = getDiveLocations();
      SelectListItem locationItem;
      IList<SelectListItem> DiveLocationsList = new List<SelectListItem>();

      viewModel.DiveSite = diveSiteRepository.Get(id);
      foreach (var location in DiveLocations)
      {
          locationItem = new SelectListItem();
          locationItem.Text = location.Title;
          locationItem.Value = location.Id.ToString();
          if (viewModel.DiveSite.DiveLocation != null)
          {
              if (location.Id == viewModel.DiveSite.DiveLocation.Id)
              {
                  locationItem.Selected = true;
              }
          }
          DiveLocationsList.Add(locationItem);
      }
      viewModel.DiveLocationsListItems = DiveLocationsList;
      return View(viewModel);
    }

    private IList<SelectListItem> BuildLocationList(int ?locationId)
    {
        IList<SelectListItem> LocationList = new List<SelectListItem>();
        IList<DiveLocation> DiveLocations = getDiveLocations();
        SelectListItem LocationItem;
        DiveLocations = DiveLocations.OrderBy(m => m.Title).ToList();
        foreach (var location in DiveLocations)
        {
            LocationItem = new SelectListItem();
            LocationItem.Text = location.Title;
            LocationItem.Value = location.Id.ToString();
            if (locationId != null)
            {
                if (location.Id == locationId)
                {
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
    public ActionResult Edit(DiveSitesModel diveSitesModel)
    {
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

        //DiveSiteFormViewModel viewModel = DiveSiteFormViewModel.CreateDiveSiteFormViewModel();
        //DiveSitesModel viewModel = new DiveSitesModel();
        //viewModel.DiveSite = diveSite;
        return View(diveSitesModel); //(viewModel);
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

    private IList<DiveLocation> getDiveLocations()
    {
        IList<DiveLocation> locations = locationRepository.GetAll();
        return locations;
    }

  }
}
