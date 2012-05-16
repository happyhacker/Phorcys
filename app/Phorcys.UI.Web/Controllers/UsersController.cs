using System.Web.Mvc;
using Phorcys.Core;
using Phorcys.UI.Web.Controllers;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using System.Collections.Generic;
using System;
using SharpArch.Web.NHibernate;
using NHibernate.Validator.Engine;
using System.Text;
using SharpArch.Web.CommonValidator;
using SharpArch.Core;

namespace Phorcys.Web.Controllers {
  [HandleError]
  public class UsersController : Controller {
     private readonly IRepository<User> userRepository;  
     
      public UsersController(IRepository<User> userRepository) {
      Check.Require(userRepository != null, "userRepository may not be null");
      this.userRepository = userRepository;
    }

    [Transaction]
    public ActionResult Index() {
      IList<User> users = userRepository.GetAll();
      return View(users);
    }

    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Login() {
      //UserFormViewModel viewModel = UserFormViewModel.CreateUserFormViewModel();
      return View("login");
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Login(FormCollection collection) {
      ActionResult retVal;
      string loginId = collection["LoginId"];
      string password = collection["password"];
      User user;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary.Add("LoginId", loginId);
      dictionary.Add("Password", password);
      user = userRepository.FindOne(dictionary);
      if (user == null) {
        retVal = View("login");
        return View();
      }
      else {
        user.LoginId = loginId;
        Session.Add("User", user);
        retVal = RedirectToAction("index", "Home");
      }
      return retVal;
    }

    private User RetreiveUser(User user) {
      User retVal;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary.Add("LoginId", user.LoginId);
      retVal = userRepository.FindOne(dictionary);
      return retVal;
    }

    [Transaction]
    public ActionResult Show(int id) {
      User user = userRepository.Get(id);
      return View(user);
    }

    public ActionResult Register() {
      UserFormViewModel viewModel = UserFormViewModel.CreateUserFormViewModel();
      return View(viewModel);
    }

    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Register(User user)
    {
  
      if (ViewData.ModelState.IsValid && user.IsValid())
      {
        if (RetreiveUser(user) == null)
        {
          userRepository.SaveOrUpdate(user);
          TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = "The user was successfully created.";
          return RedirectToAction("Index", "Home");
        }
      }
      TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = "The User ID you selected already exists. Please choose a different one.";
       UserFormViewModel viewModel = UserFormViewModel.CreateUserFormViewModel();
      viewModel.User = user;
      return View(viewModel);
    }

    [Transaction]
    public ActionResult Edit(int id) {
      UserFormViewModel viewModel = UserFormViewModel.CreateUserFormViewModel();
      viewModel.User = userRepository.Get(id);
      return View(viewModel);
    }

    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Edit(User user) {
      User userToUpdate = userRepository.Get(user.Id);
      TransferFormValuesTo(userToUpdate, user);

      if (ViewData.ModelState.IsValid && user.IsValid()) {
        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
  "The user was successfully updated.";
        return RedirectToAction("Index");
      }
      else {
        userRepository.DbContext.RollbackTransaction();

        UserFormViewModel viewModel = UserFormViewModel.CreateUserFormViewModel();
        viewModel.User = user;
        return View(viewModel);
      }
    }

    private void TransferFormValuesTo(User userToUpdate, User userFromForm) {
      userToUpdate.LoginId = userFromForm.LoginId;
      userToUpdate.Password = userFromForm.Password;
      userToUpdate.LoginCount = userFromForm.LoginCount;
    }

    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Delete(int id) {
      string resultMessage = "The user was successfully deleted.";
      User userToDelete = userRepository.Get(id);

      if (userToDelete != null) {
        userRepository.Delete(userToDelete);

        try {
          userRepository.DbContext.CommitChanges();
        }
        catch {
          resultMessage = "A problem was encountered preventing the user from being deleted. " +
  "Another item likely depends on this user.";
          userRepository.DbContext.RollbackTransaction();
        }
      }
      else {
        resultMessage = "The user could not be found for deletion. It may already have been deleted.";
      }

      TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
      return RedirectToAction("Index");
    }

    /// <summary>
    /// Holds data to be passed to the User form for creates and edits
    /// </summary>
    public class UserFormViewModel {
      private UserFormViewModel() { }

      /// <summary>
      /// Creation method for creating the view model. Services may be passed to the creation 
      /// method to instantiate items such as lists for drop down boxes.
      /// </summary>
      public static UserFormViewModel CreateUserFormViewModel() {
        UserFormViewModel viewModel = new UserFormViewModel();

        return viewModel;
      }

      public User User { get; internal set; }
    }


  }
}
