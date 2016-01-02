using System.Web.Mvc;
using Phorcys.Core;
using Phorcys.Services;
using Phorcys.UI.Web.Controllers;
using Phorcys.UI.Web.Models;
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
  public class DiveTypesController : Controller {
    private UserServices userServices = new UserServices();
    private User user;
    public DiveTypesController(IRepository<DiveType> diveTypeRepository) {
      Check.Require(diveTypeRepository != null, "diveTypeRepository may not be null");

      this.diveTypeRepository = diveTypeRepository;
    }

    [Transaction]
    public ActionResult Index() {
      IList<DiveType> diveTypes = diveTypeRepository.GetAll();
      return View(diveTypes);
    }

    [Authorize]
    [AcceptVerbs(HttpVerbs.Get)]
    public ActionResult Create() {
      DiveTypeModel model = new DiveTypeModel();
      return View(model);
    }

    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Create(DiveTypeModel model) {
      DiveType diveType = new DiveType();
      user = userServices.FindUser(this.User.Identity.Name);
      diveType.User = user;
      diveType.Title = model.Title;
      diveType.Notes = model.Notes;
      diveType.Created = DateTime.Now;
      diveType.LastModified = DateTime.Now;
      diveTypeRepository.SaveOrUpdate(diveType);
      TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
         "The diveType was successfully created.";
      return RedirectToAction("Index");

      return View(model);
    }

    [Transaction]
    public ActionResult Edit(int id) {
      DiveTypeModel model = new DiveTypeModel();
      DiveType diveType = diveTypeRepository.Get(id);
      model.Title = diveType.Title;
      model.Notes = diveType.Notes;
      return View(model);
    }

    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Edit(DiveTypeModel diveTypeModel) {
      DiveType diveTypeToUpdate = diveTypeRepository.Get(diveTypeModel.Id);
      TransferFormValuesTo(diveTypeToUpdate, diveTypeModel);

      if (ViewData.ModelState.IsValid && diveTypeToUpdate.IsValid()) {
        TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
  "The diveType was successfully updated.";
        return RedirectToAction("Index");
      } else {
        diveTypeRepository.DbContext.RollbackTransaction();

        DiveTypeModel viewModel = new DiveTypeModel();

        return View(viewModel);
      }
    }

    private void TransferFormValuesTo(DiveType diveTypeToUpdate, DiveTypeModel diveTypeFromForm) {
      diveTypeToUpdate.Title = diveTypeFromForm.Title;
      diveTypeToUpdate.Notes = diveTypeFromForm.Notes;
      diveTypeToUpdate.Created = diveTypeFromForm.Created;
      diveTypeToUpdate.LastModified = diveTypeFromForm.LastModified;
    }

    [ValidateAntiForgeryToken]
    [Transaction]
    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult Delete(int id) {
      string resultMessage = "The diveType was successfully deleted.";
      DiveType diveTypeToDelete = diveTypeRepository.Get(id);

      if (diveTypeToDelete != null) {
        diveTypeRepository.Delete(diveTypeToDelete);

        try {
          diveTypeRepository.DbContext.CommitChanges();
        } catch {
          resultMessage = "A problem was encountered preventing the diveType from being deleted. " +
                          "Another item likely depends on this diveType.";
          diveTypeRepository.DbContext.RollbackTransaction();
        }
      } else {
        resultMessage = "The diveType could not be found for deletion. It may already have been deleted.";
      }

      TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
      return RedirectToAction("Index");
    }

    private readonly IRepository<DiveType> diveTypeRepository;
  }
}
