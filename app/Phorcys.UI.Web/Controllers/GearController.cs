using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phorcys.Data;
using Phorcys.Services;
using Phorcys.UI.Web.Models;
using SharpArch.Core;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Web.NHibernate;
using Phorcys.Core;
using log4net;
using HackLib.UIHelpers;
using FluentNHibernate.Utils;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Phorcys.UI.Web.Controllers
{
    public class GearController : Controller
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(GearController));
        private readonly GearServices gearServices = new GearServices();
        private readonly UserServices userServices = new UserServices();
        private readonly SelectListHelper selectListHelper = new SelectListHelper();
        private User user;

        private readonly IRepository<Gear> gearRepository;

        public GearController(IRepository<Gear> gearRepository)
        {
            Check.Require(gearRepository != null, "gearRepository may not be null");
            this.gearRepository = new PhorcysRepository<Gear>();
        }

        //
        // GET: /Gear/
        [Authorize]
        [Transaction]
        public ActionResult Index()
        {
            user = userServices.FindUser(this.User.Identity.Name);
            IList<Gear> gear = gearServices.GetAllForUser(user.Id).OrderByDescending(m => m.Id).ToList();
            //IList<Gear> gear = gearRepository.GetAll().OrderByDescending(m => m.GearId).ToList();
            return View(gear);
        }

        //
        // GET: /Gear/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Gear/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Gear/Create
        [HttpPost]
        public ActionResult Create(GearModel model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                saveNewGear(model);

                TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = model.Title +
                                                                                          " was successfully created.";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                log.Error("Unable to create " + model.Title + ". " + ex.Message);
                return View();
            }
        }

        private void saveNewGear(GearModel model)
        {
            this.user = userServices.FindUser(this.User.Identity.Name);
            Gear gear = new Gear();
            Tank tank = null;

            gear.Title = model.Title;
            gear.Sn = model.Sn;
            gear.Acquired = model.Acquired;
            gear.RetailPrice = model.RetailPrice;
            gear.Paid = model.Paid;
            gear.Notes = model.Notes;
            gear.Weight = model.Weight;
            gear.User = this.user;
            gear.Created = DateTime.Now;
            gear.LastModified = DateTime.Now;
            gearServices.Save(gear);

            //if they enter any tank info at all...
            if (model.TankVolume > 0 || model.WorkingPressure > 0 || model.ManufacturedMonth > 0 || model.ManufacturedYear > 0)
            {
                //tank = new Tank { Id = Gear.Id }; Error CS0272  The property or indexer 'EntityWithTypedId<int>.Id' cannot be used in this context because the set accessor is inaccessible
                //tank.Id = Gear.Id; same error
                tank = new Tank(gear.Id);
                tank.Volume = model.TankVolume;
                tank.WorkingPressure = model.WorkingPressure;
                tank.ManufacturedMonth = model.ManufacturedMonth;
                tank.ManufacturedYear = model.ManufacturedYear;
                tank.Gear = gear;
                gear.Tank = tank;
                gearServices.Save(gear);
            }
        }

        // GET: /Gear/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            GearModel viewModel = getGearView(id);
            return View(viewModel);
        }

        // POST: /Gear/Edit/5
        [HttpPost]
        public ActionResult Edit(GearModel model)
        {
            try
            {
                saveEditedGear(model);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error("Unable to update " + model.Title + ". " + ex.Message);
                return View();
            }
        }

        private void saveEditedGear(GearModel model)
        {
            this.user = userServices.FindUser(this.User.Identity.Name);
            Gear gear = gearServices.GetGear(model.GearId);

            gear.Title = model.Title;
            gear.Sn = model.Sn;
            gear.Acquired = model.Acquired;
            gear.NoLongerUse = model.NoLongerUse;
            gear.RetailPrice = model.RetailPrice;
            gear.Paid = model.Paid;
            gear.Notes = model.Notes;
            gear.Weight = model.Weight;
            gear.User = this.user;
            gear.LastModified = DateTime.Now;
            gearServices.Save(gear);

            //if they enter any tank info at all...
            if (model.TankVolume > 0 || model.WorkingPressure > 0 || model.ManufacturedMonth > 0 || model.ManufacturedYear > 0)
            {
                if (gear.Tank == null)
                {
                    gear.Tank = new Tank(gear.Id);
                    gear.Tank.Gear = gear; // model.GearId;
                }
                gear.Tank.Volume = model.TankVolume;
                gear.Tank.WorkingPressure = model.WorkingPressure;
                gear.Tank.ManufacturedMonth = model.ManufacturedMonth;
                gear.Tank.ManufacturedYear = model.ManufacturedYear;
                gearServices.Save(gear);
            }
        }


        private GearModel getGearView(int id)
        {
            //bool isTank = false;
            GearModel viewModel = new GearModel();
            Gear gear = gearServices.GetGear(id);
            try
            {
                viewModel.GearId = gear.Id;
                viewModel.Acquired = gear.Acquired;
                viewModel.NoLongerUse = gear.NoLongerUse;
                viewModel.Notes = gear.Notes;
                viewModel.Paid = Math.Round(gear.Paid, 2);
                viewModel.RetailPrice = Math.Round(gear.RetailPrice, 2);
                viewModel.Sn = gear.Sn;
                viewModel.Title = gear.Title;
                viewModel.Weight = gear.Weight;
                viewModel.MonthSelectList = selectListHelper.GetMonthsList(0);

                if (gear.Tank != null)
                {
                    //isTank = true;
                    viewModel.GearId = gear.Tank.Id;
                    viewModel.ManufacturedMonth = gear.Tank.ManufacturedMonth;
                    viewModel.ManufacturedYear = gear.Tank.ManufacturedYear;
                    viewModel.TankVolume = gear.Tank.Volume;
                    viewModel.WorkingPressure = gear.Tank.WorkingPressure;
                    viewModel.MonthSelectList = selectListHelper.GetMonthsList(gear.Tank.ManufacturedMonth);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message + " Inner Exception: " + e.InnerException.Message);
                gear.Tank = null;
            }
            return viewModel;
        }

        //IList<SelectListItem> GetMonthsList(int monthNum) {
        //  IList<SelectListItem> MonthsSelectList = new List<SelectListItem>();
        //  SelectListItem item;
        //  int count = 0;
        //  List<string> months = DateTimeFormatInfo.CurrentInfo.MonthNames.Take(12).ToList();
        //  foreach (string month in months )
        //  {
        //    count++;
        //    item = new SelectListItem();
        //    item.Value = count.ToString();
        //    item.Text = month;
        //    if(count == monthNum)
        //    {
        //      item.Selected = true;
        //    }
        //    MonthsSelectList.Add(item);
        //  }

        //  return MonthsSelectList;
        //}


        //
        // GET: /Gear/Delete/5

        public ActionResult Delete(int id)
        {

            return View();
        }

        //
        // POST: /Gear/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string resultMessage = "The gear was successfully deleted.";
            Gear gearToDelete = gearServices.GetGear(id);

            if (gearToDelete != null)
            {
                try
                {
                    gearServices.Delete(gearToDelete);
                }
                catch
                {
                    resultMessage = "A problem was encountered preventing this piece of gear from being deleted. " +
                                    "A dive probably references this piece of gear.";
                }
            }
            else
            {
                resultMessage = "This piece of gear could not be found for deletion. It may already have been deleted.";
            }

            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
            return RedirectToAction("Index");
        }
    }
}
