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
using Phorcys.Data;

namespace Phorcys.Web.Controllers
{
    [HandleError]
    public class DivesController : Controller
    {
        private UserServices userServices = new UserServices();
        private readonly IDiveServices diveServices = new DiveServices();
        private readonly IDivePlanServices divePlanServices = new DivePlanServices();
        private User user;
        private readonly IRepository<Dive> diveRepository;
        private readonly IRepository<DivePlan> divePlanRepository;
        //private DiveSitesModel viewModel = new DiveSitesModel();

        public DivesController(IRepository<Dive> diveSiteRepository, IRepository<Dive> diveRepository, IRepository<DivePlan> divePlanRepository, IRepository<User> userRepository)
        {
            Check.Require(diveRepository != null, "divePlanRepository may not be null");
            Check.Require(diveSiteRepository != null, "diveSiteRepository may not be null");
            Check.Require(userRepository != null, "userRepository may not be null");
            this.diveRepository = diveRepository;
            this.divePlanRepository = divePlanRepository;
        }

        [Authorize]
        [Transaction]
        public ActionResult Index()
        {
            user = userServices.FindUser(this.User.Identity.Name);
            IList<Dive> dives = diveRepository.GetAll();
            dives = dives.OrderByDescending(m => m.DiveNumber).ToList();

            return View(dives);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            DiveModel model = new DiveModel();
            model.User = userServices.FindUser(this.User.Identity.Name);
            IList<SelectListItem> divePlans = BuildDivePlanList(null);
            divePlans = divePlans.ToList(); 
            //divePlans = divePlans.OrderBy(d => d.Text).ToList();
            model.DivePlanList = divePlans;
            return View(model);
        }

        private IList<SelectListItem> BuildDivePlanList(int? divePlanId)
        {
            IList<SelectListItem> DivePlanList = new List<SelectListItem>();
            IList<DivePlan> DivePlans = GetDivePlans();
            SelectListItem DivePlanItem;

            DivePlans = DivePlans.OrderByDescending(m => m.ScheduledTime).ToList();

            foreach (var divePlan in DivePlans)
            {
                DivePlanItem = new SelectListItem();
                DivePlanItem.Text = divePlan.Title;
                DivePlanItem.Value = divePlan.Id.ToString();
                if (divePlanId != null)
                {
                    if (divePlan.Id == divePlanId)
                    {
                        DivePlanItem.Selected = true;
                    }
                }
                DivePlanList.Add(DivePlanItem);
            }

            return DivePlanList;
        }


        [Authorize]
        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(DiveModel model)
        {
            Dive dive = new Dive();
            user = userServices.FindUser(this.User.Identity.Name);
            dive.User = user;
            dive.DivePlan = divePlanServices.Get(model.DivePlanId);
            dive.DiveNumber = model.DiveNumber;
            dive.Minutes = model.Minutes;
            dive.Notes = model.Notes;
            dive.MaxDepth = model.MaxDepth;
            dive.AvgDepth = model.AvgDepth;
            dive.Temperature = model.Temperature;
            dive.AdditionalWeight = model.AdditionalWeight;
            dive.DescentTime = model.DescentTime;
            dive.Created = DateTime.Now;
            dive.LastModified = DateTime.Now;
            diveRepository.SaveOrUpdate(dive);
            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
               "The Dive was successfully created.";
            return RedirectToAction("Index");

            //return View(model);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        [Transaction]
        public ActionResult Edit(int id)
        {
            DiveModel model = new DiveModel();
            Dive dive = diveRepository.Get(id);
            model.Id = dive.Id;
            model.Title = dive.Title;
            model.Notes = dive.Notes;
            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(DiveModel diveModel)
        {
            Dive diveToUpdate = diveRepository.Get(diveModel.Id);
            TransferFormValuesTo(diveToUpdate, diveModel);
            diveToUpdate.LastModified = DateTime.Now;

            if (ModelState.IsValid)
            {
                diveRepository.SaveOrUpdate(diveToUpdate);

                TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] =
          "The Dive Plan was successfully updated.";
                return RedirectToAction("Index");
            }
            else
            {
                divePlanRepository.DbContext.RollbackTransaction();

                return View(diveModel);
            }
        }


        private IList<DivePlan> GetDivePlans()
        {
            user = userServices.FindUser(this.User.Identity.Name);
            //systemUser = userServices.FindUser("system");
            IList<DivePlan> divePlans = divePlanServices.GetAllForUser(user.Id);
            return divePlans;
        }


        private void TransferFormValuesTo(Dive diveToUpdate, DiveModel diveFromForm)
        {
            diveToUpdate.Title = diveFromForm.Title;
            diveToUpdate.AdditionalWeight = diveFromForm.AdditionalWeight;
            diveToUpdate.AvgDepth = diveFromForm.AvgDepth;
            diveToUpdate.MaxDepth = diveFromForm.MaxDepth;
            diveToUpdate.DescentTime = diveFromForm.DescentTime;
            diveToUpdate.DiveNumber = diveFromForm.DiveNumber;
            diveToUpdate.Minutes = diveFromForm.Minutes;
            diveToUpdate.Temperature = diveFromForm.Temperature;
            diveToUpdate.Notes = diveFromForm.Notes;
            diveToUpdate.Created = diveFromForm.Created;
            diveToUpdate.LastModified = diveFromForm.LastModified;
            DivePlan divePlan = divePlanServices.Get(diveFromForm.DivePlanId);
            diveToUpdate.DivePlan = divePlan;
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
                    resultMessage = "A problem was encountered preventing the Dive from being deleted. " +
                                    "Another item likely depends on this Dive.";
                    divePlanRepository.DbContext.RollbackTransaction();
                }
            }
            else
            {
                resultMessage = "The Dive could not be found for deletion. It may already have been deleted.";
            }

            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
            return RedirectToAction("Index");
        }

    }
}
