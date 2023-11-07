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
using MvcContrib.Sorting;
using System.Linq;

namespace Phorcys.Web.Controllers
{
    [HandleError]
    public class DivePlansController : Controller
    {
        private UserServices userServices = new UserServices();
        private readonly IDiveSiteServices diveSiteServices = new DiveSiteServices();
        private User user;
        private readonly IRepository<DivePlan> divePlanRepository;
        private DiveSitesModel viewModel = new DiveSitesModel();

        public DivePlansController(IRepository<DiveSite> diveSiteRepository, IRepository<DivePlan> divePlanRepository, IRepository<User> userRepository)
        {
            Check.Require(divePlanRepository != null, "divePlanRepository may not be null");
            Check.Require(diveSiteRepository != null, "diveSiteRepository may not be null");
            Check.Require(userRepository != null, "userRepository may not be null");

            this.divePlanRepository = divePlanRepository;
        }

        [Authorize]
        [Transaction]
        public ActionResult Index()
        {
            user = userServices.FindUser(this.User.Identity.Name);
            IList<DivePlan> divePlans = divePlanRepository.GetAll();

            return View(divePlans);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            DivePlanModel model = new DivePlanModel();
            user = userServices.FindUser(this.User.Identity.Name);
            IList<DiveSite> diveSites = diveSiteServices.GetAllForUser(user.Id);
            diveSites = diveSites.OrderBy(d => d.Title).ToList();
            model.DiveSiteList = diveSites;
            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(DivePlanModel model)
        {
            DivePlan divePlan = new DivePlan();
            user = userServices.FindUser(this.User.Identity.Name);
            divePlan.User = user;
            divePlan.Title = model.Title;
            divePlan.Notes = model.Notes;
            divePlan.MaxDepth = model.MaxDepth;
            divePlan.ScheduledTime = model.ScheduledTime;
            divePlan.Created = DateTime.Now;
            divePlan.LastModified = DateTime.Now;
            divePlanRepository.SaveOrUpdate(divePlan);
            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
               "The Dive Plan was successfully created.";
            return RedirectToAction("Index");

            //return View(model);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        [Transaction]
        public ActionResult Edit(int id)
        {
            DivePlanModel model = new DivePlanModel();
            DivePlan divePlan = divePlanRepository.Get(id);
            model.Id = divePlan.Id;
            model.Title = divePlan.Title;
            model.Notes = divePlan.Notes;
            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(DivePlanModel divePlanModel)
        {
            DivePlan divePlanToUpdate = divePlanRepository.Get(divePlanModel.Id);
            TransferFormValuesTo(divePlanToUpdate, divePlanModel);
            divePlanToUpdate.LastModified = DateTime.Now;

            if (ModelState.IsValid)
            {
                divePlanRepository.SaveOrUpdate(divePlanToUpdate);

                TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
          "The Dive Plan was successfully updated.";
                return RedirectToAction("Index");
            }
            else
            {
                divePlanRepository.DbContext.RollbackTransaction();

                return View(divePlanModel);
            }
        }

        private void TransferFormValuesTo(DivePlan divePlanToUpdate, DivePlanModel divePlanFromForm)
        {
            divePlanToUpdate.Title = divePlanFromForm.Title;
            divePlanToUpdate.Notes = divePlanFromForm.Notes;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id)
        {
            string resultMessage = "The Dive Plan was successfully deleted.";
            DivePlan divePlanToDelete = divePlanRepository.Get(id);

            if (divePlanToDelete != null)
            {
                divePlanRepository.Delete(divePlanToDelete);

                try
                {
                    divePlanRepository.DbContext.CommitChanges();
                }
                catch (Exception e)
                {
                    resultMessage = "A problem was encountered preventing the Dive Plan from being deleted. " +
                                    "Another item likely depends on this Dive Plan.";
                    divePlanRepository.DbContext.RollbackTransaction();
                }
            }
            else
            {
                resultMessage = "The Dive Plan could not be found for deletion. It may already have been deleted.";
            }

            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
            return RedirectToAction("Index");
        }

    }
}
