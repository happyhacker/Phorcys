﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Phorcys.UI.Web.Models;
using Phorcys.Core;
//using SharpArch.Web.CommonValidator;
//using NHibernate.Validator.Engine;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core;
using log4net;

namespace Phorcys.UI.Web.Controllers {

  [HandleError]
  public class AccountController : Controller {
    private readonly IRepository<User> userRepository;  
     
    public AccountController(IRepository<User> userRepository) {
      Check.Require(userRepository != null, "userRepository may not be null");
      this.userRepository = userRepository;
    }

    public IFormsAuthenticationService FormsService { get; set; }
    public IMembershipService  MembershipService { get; set; }

    protected override void Initialize(RequestContext requestContext) {
      if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
      if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

      base.Initialize(requestContext);
    }

    // **************************************
    // URL: /Account/LogOn
    // **************************************

    public ActionResult LogOn() {
      return View();
    }

    [HttpPost]
    public ActionResult LogOn(LogOnModel model, string returnUrl) {
      if (ModelState.IsValid) {
        if (MembershipService.ValidateUser(model.UserName, model.Password)) {
          FormsService.SignIn(model.UserName, model.RememberMe);
          if (!String.IsNullOrEmpty(returnUrl)) {
            return Redirect(returnUrl);
          }
          else {
            return RedirectToAction("Index", "Home");
          }
        }
        else {
          ModelState.AddModelError("", "The user name or password provided is incorrect.");
        }
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    // **************************************
    // URL: /Account/LogOff
    // **************************************

    public ActionResult LogOff() {
      FormsService.SignOut();

      return RedirectToAction("Index", "Home");
    }

    // **************************************
    // URL: /Account/Register
    // **************************************

    public ActionResult Register() {
      ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
      return View();
    }

    [HttpPost]
    public ActionResult Register(RegisterModel model) {
      if (ModelState.IsValid) {

        if (!userExists(model.UserName)) {
          // Attempt to register the user
          MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

          if (createStatus == MembershipCreateStatus.Success) {
            FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
            return RedirectToAction("Index", "Home");
          } else {
            ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
          }
        }
       else {
         ModelState.AddModelError("","The User ID you selected already exists. Please choose a different one.");
        RegisterModel viewModel = new RegisterModel();
        ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
        return View(viewModel);

       }
     }
     // If we got this far, something failed, redisplay form
     ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
     return View(model);
    }

    // **************************************
    // URL: /Account/ChangePassword
    // **************************************

    [Authorize]
    public ActionResult ChangePassword() {
      ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
      return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult ChangePassword(ChangePasswordModel model) {
      if (ModelState.IsValid) {
        if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword)) {
          return RedirectToAction("ChangePasswordSuccess");
        }
        else {
          ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
        }
      }

      // If we got this far, something failed, redisplay form
      ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
      return View(model);
    }

    // **************************************
    // URL: /Account/ChangePasswordSuccess
    // **************************************

    public ActionResult ChangePasswordSuccess() {
      return View();
    }

    private User RetrieveUser(User user) {
      User retVal;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary.Add("LoginId", user.LoginId);
      retVal = userRepository.FindOne(dictionary);
      return retVal;
    }

    private User RetrieveUser(string username) {
      User user = new User();
      User retVal;
      user.LoginId = username;
      retVal = RetrieveUser(user);
      return retVal;
    }

    private bool userExists(string username) {
      bool retVal = false;
      User user = RetrieveUser(username);
      if (user != null) {
        retVal = true;
      }
      return retVal;
    }

  }
}
