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
using NHibernate.Linq;

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
            divePlans = divePlans.OrderByDescending(d  => d.ScheduledTime).ToList();
            return View(divePlans);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            DivePlanModel model = new DivePlanModel();
            user = userServices.FindUser(this.User.Identity.Name);
            IList<SelectListItem> diveSites = BuildDiveSiteList(null);
            diveSites = diveSites.OrderBy(d => d.Text).ToList();
            model.DiveSiteList = diveSites;
            return View(model);
        }

        private IList<SelectListItem> BuildDiveSiteList(int? diveSiteId)
        {
            IList<SelectListItem> DiveSiteList = new List<SelectListItem>();
            IList<DiveSite> DiveSites = GetDiveSites();
            SelectListItem DiveSiteItem;

            DiveSites = DiveSites.OrderBy(m => m.Title).ToList();
            foreach (var diveSite in DiveSites)
            {
                DiveSiteItem = new SelectListItem();
                DiveSiteItem.Text = diveSite.Title;
                DiveSiteItem.Value = diveSite.Id.ToString();
                if (diveSiteId != null)
                {
                    if (diveSite.Id == diveSiteId)
                    {
                        DiveSiteItem.Selected = true;
                    }
                }
                DiveSiteList.Add(DiveSiteItem);
            }

            return DiveSiteList;
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
            divePlan.DiveSite = diveSiteServices.Get(model.DiveSiteId);
            divePlan.DiveSiteId = model.DiveSiteId;
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
            model.ScheduledTime = divePlan.ScheduledTime;
            model.MaxDepth = divePlan.MaxDepth;
            model.DiveSiteId = divePlan.DiveSite.Id;
            model.DiveSiteList = BuildDiveSiteList(divePlan.DiveSite.Id);
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


        private IList<DiveSite> GetDiveSites()
        {
            user = userServices.FindUser(this.User.Identity.Name);
            //systemUser = userServices.FindUser("system");
            IList<DiveSite> diveSites = diveSiteServices.GetAllForUser(user.Id);
            return diveSites;
        }


        private void TransferFormValuesTo(DivePlan divePlanToUpdate, DivePlanModel divePlanFromForm)
        {
            divePlanToUpdate.Title = divePlanFromForm.Title;
            divePlanToUpdate.DiveSite = diveSiteServices.Get(divePlanFromForm.DiveSiteId);
            divePlanToUpdate.MaxDepth = divePlanFromForm.MaxDepth;
            divePlanToUpdate.ScheduledTime = divePlanFromForm.ScheduledTime;
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
