using System.Web.Mvc;
using Phorcys.Core;
using Phorcys.Services;
using Phorcys.Services.Services;
using SharpArch.Core.PersistenceSupport;
using System.Collections.Generic;
using System;
//using SharpArch.Web.NHibernate;
using SharpArch.Core;

namespace Phorcys.UI.Web.Controllers {
  [HandleError]
  public class DiveLocationsController : Controller {
    private readonly IRepository<User> userRepository;
    private readonly ILocationServices locationServices = new LocationServices();
    private readonly UserServices userServices = new UserServices();
    private User systemUser;
    private User user;

    public DiveLocationsController(IRepository<DiveLocation> diveLocationRepository, IRepository<User> userRepository) {
      Check.Require(diveLocationRepository != null, "diveLocationRepository may not be null");
      Check.Require(userRepository != null, "userRepository may not be null");

      //this.diveLocationRepository = diveLocationRepository;
      this.userRepository = userRepository;

    }

    [Authorize]
    public ActionResult Index() {
      systemUser = userServices.FindUser("system");
      user = userServices.FindUser(this.User.Identity.Name);

      IList<DiveLocation> diveLocations = locationServices.GetAllSystemAndUser(systemUser.Id, user.Id); //diveLocationRepository.GetAll();
      return View(diveLocations);
    }

    [Authorize]
    public ActionResult Show(int id) {
      DiveLocation diveLocation = locationServices.Get(id);
      return View(diveLocation);
    }

    [Authorize]
    public ActionResult Create() {
      DiveLocationFormViewModel viewModel = DiveLocationFormViewModel.CreateDiveLocationFormViewModel();
      return View(viewModel);
    }

    //[ValidateAntiForgeryToken]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Create(DiveLocation diveLocation) {
      User user;
      if (ViewData.ModelState.IsValid) {
        diveLocation.Created = System.DateTime.Now;
        diveLocation.LastModified = System.DateTime.Now;
        UserServices userServices = new UserServices(this.userRepository);
        user = userServices.FindUser(this.User.Identity.Name);
        diveLocation.User = user;
        diveLocation.UserId = user.Id;
        locationServices.Create(diveLocation);
        //diveLocationRepository.SaveOrUpdate(diveLocation);

        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
  "The diveLocation was successfully created.";
        return RedirectToAction("Index");
      }

      DiveLocationFormViewModel viewModel = DiveLocationFormViewModel.CreateDiveLocationFormViewModel();
      viewModel.DiveLocation = diveLocation;
      return View(viewModel);
    }

    [Authorize]
    public ActionResult Edit(int id) {
      DiveLocationFormViewModel viewModel = DiveLocationFormViewModel.CreateDiveLocationFormViewModel();
      viewModel.DiveLocation = locationServices.Get(id);
      return View(viewModel);
    }

    //[ValidateAntiForgeryToken]
    [Authorize]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Edit(DiveLocation diveLocation) {
      DiveLocation diveLocationToUpdate = locationServices.Get(diveLocation.Id);
      TransferFormValuesTo(diveLocationToUpdate, diveLocation);

      if (ViewData.ModelState.IsValid) {
        locationServices.Save(diveLocation);
        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = "The diveLocation was successfully updated.";
        return RedirectToAction("Index");
      }
      else {
        DiveLocationFormViewModel viewModel = DiveLocationFormViewModel.CreateDiveLocationFormViewModel();
        viewModel.DiveLocation = diveLocation;
        return View(viewModel);
      }
    }

    private void TransferFormValuesTo(DiveLocation diveLocationToUpdate, DiveLocation diveLocationFromForm) {
      //diveLocationToUpdate.Contact = diveLocationFromForm.Contact;
      diveLocationToUpdate.Title = diveLocationFromForm.Title;
      diveLocationToUpdate.Notes = diveLocationFromForm.Notes;
      diveLocationToUpdate.UserId = diveLocationFromForm.UserId;
      //diveLocationToUpdate.User = diveLocationFromForm.User;
      diveLocationToUpdate.LastModified = System.DateTime.Now;
    }

    //[ValidateAntiForgeryToken]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Delete(int id) {
      string resultMessage = "The diveLocation was successfully deleted.";
      DiveLocation diveLocationToDelete = locationServices.Get(id);

      if (diveLocationToDelete != null) {
        try {
          locationServices.Delete(diveLocationToDelete);
        }
        catch {
          resultMessage = "A problem was encountered preventing the diveLocation from being deleted. " +
                          "Another item likely depends on this diveLocation.";
        }
      }
      else {
        resultMessage = "The diveLocation could not be found for deletion. It may already have been deleted.";
      }

      TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
      return RedirectToAction("Index");
    }

    /// <summary>Holds data to be passed to the DiveLocation form for creates and edits</summary>
    public class DiveLocationFormViewModel {
      private DiveLocationFormViewModel() { }

      /// <summary>
      /// Creation method for creating the view model. Services may be passed to the creation 
      /// method to instantiate items such as lists for drop down boxes.
      /// </summary>
      public static DiveLocationFormViewModel CreateDiveLocationFormViewModel() {
        DiveLocationFormViewModel viewModel = new DiveLocationFormViewModel();

        return viewModel;
      }

      public DiveLocation DiveLocation { get; internal set; }
    }

  }
}
