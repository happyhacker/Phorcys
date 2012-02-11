using System.Web.Mvc;
using Phorcys.Core;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using System.Collections.Generic;
using System;
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

      IList<DiveSite> diveSites = this.diveSiteRepository.GetSystemAndUserRecords(query); //.GetAll();
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
    public ActionResult Create() {
      DiveSiteFormViewModel viewModel = DiveSiteFormViewModel.CreateDiveSiteFormViewModel();
      return View(viewModel);
    }

    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Create(DiveSite diveSite)
    {
      Core.User user;
      if (ViewData.ModelState.IsValid) { //&& diveSite.IsValid()
        UserServices userServices = new UserServices(this.userRepository);
        user = userServices.FindUser(this.User.Identity.Name);
        diveSite.User = user;
        diveSiteRepository.SaveOrUpdate(diveSite);

        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
  "The diveSite was successfully created.";
        return RedirectToAction("Index");
      }

      DiveSiteFormViewModel viewModel = DiveSiteFormViewModel.CreateDiveSiteFormViewModel();
      viewModel.DiveSite = diveSite;
      return View(viewModel);
    }

    [Authorize]
    [Transaction]
    public ActionResult Edit(int id) {
      DiveSiteFormViewModel viewModel = DiveSiteFormViewModel.CreateDiveSiteFormViewModel();
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

    //[ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Edit(DiveSite diveSite) {
      DiveSite diveSiteToUpdate = diveSiteRepository.Get(diveSite.Id);
      //ToDo: this is total BS. Must be able to remove next line
      diveSite.User = diveSiteToUpdate.User;
      TransferFormValuesTo(diveSiteToUpdate, diveSite);
      diveSiteToUpdate.DiveLocation = locationRepository.Get(diveSite.DiveLocationId);

      if (ViewData.ModelState.IsValid) {
        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
  "The diveSite was successfully updated.";
        return RedirectToAction("Index");
      }
      else {
        diveSiteRepository.DbContext.RollbackTransaction();

        DiveSiteFormViewModel viewModel = DiveSiteFormViewModel.CreateDiveSiteFormViewModel();
        viewModel.DiveSite = diveSite;
        return View(viewModel);
      }
    }

    private void TransferFormValuesTo(DiveSite diveSiteToUpdate, DiveSite diveSiteFromForm) {
      diveSiteToUpdate.DiveLocationId = diveSiteFromForm.DiveLocationId;
      diveSiteToUpdate.Title = diveSiteFromForm.Title;
      diveSiteToUpdate.IsFreshWater = diveSiteFromForm.IsFreshWater;
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


    /// <summary>
    /// Holds data to be passed to the DiveSite form for creates and edits
    /// </summary>
    public class DiveSiteFormViewModel {
      private DiveSiteFormViewModel() { }

      /// <summary>
      /// Creation method for creating the view model. Services may be passed to the creation 
      /// method to instantiate items such as lists for drop down boxes.
      /// </summary>
      public static DiveSiteFormViewModel CreateDiveSiteFormViewModel() {
        DiveSiteFormViewModel viewModel = new DiveSiteFormViewModel();

        return viewModel;
      }

      public DiveSite DiveSite { get; internal set; }
      public IList<DiveLocation> DiveLocationsList { get; set; }
      public IList<SelectListItem> DiveLocationsListItems { get; set; }
    }

  }
}
