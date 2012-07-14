using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phorcys.Data;
using Phorcys.Services.Services;
using Phorcys.UI.Web.Models;
using SharpArch.Core;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Web.NHibernate;
using Phorcys.Core;
using log4net;

namespace Phorcys.UI.Web.Controllers {
  public class GearController : Controller {
    protected static readonly ILog log = LogManager.GetLogger(typeof(GearController));
    private readonly UserServices userServices = new UserServices();
    private User user;

    private readonly IRepository<Gear> gearRepository;

    public GearController(IRepository<Gear> gearRepository, IRepository<User> userRepository ) {
      Check.Require(gearRepository != null, "gearRepository may not be null");
      this.gearRepository = new PhorcysRepository<Gear>();
    }

    //
    // GET: /Gear/
    [Authorize]
    [Transaction]
    public ActionResult Index() {
      IList<Gear> gear = gearRepository.GetAll();
      return View(gear);
    }

    //
    // GET: /Gear/Details/5

    public ActionResult Details(int id) {
      return View();
    }

    //
    // GET: /Gear/Create

    public ActionResult Create() {
      return View();
    }

    //
    // POST: /Gear/Create

    [HttpPost]
    public ActionResult Create(GearModel model) {  //FormCollection collection) {
      try {
        this.user = userServices.FindUser(this.User.Identity.Name);

        Gear gear = new Gear();
        gear.Title = model.Title;
        gear.Sn = model.Sn;
        gear.Acquired = model.Acquired;
        gear.RetailPrice = model.RetailPrice;
        gear.Paid = model.Paid;
        gear.Notes = model.Notes;
        gear.Weight = model.Weight;
        gear.User = this.user;
        gearRepository.SaveOrUpdate(gear);

        return RedirectToAction("Index");
      }
      catch(Exception ex) {
        log.Error(ex.Message);
        return View();
      }
    }

    //
    // GET: /Gear/Edit/5

    public ActionResult Edit(int id) {
      return View();
    }

    //
    // POST: /Gear/Edit/5

    [HttpPost]
    public ActionResult Edit(int id, FormCollection collection) {
      try {
        // TODO: Add update logic here

        return RedirectToAction("Index");
      }
      catch {
        return View();
      }
    }

    //
    // GET: /Gear/Delete/5

    public ActionResult Delete(int id) {
      return View();
    }

    //
    // POST: /Gear/Delete/5

    [HttpPost]
    public ActionResult Delete(int id, FormCollection collection) {
      try {
        // TODO: Add delete logic here

        return RedirectToAction("Index");
      }
      catch {
        return View();
      }
    }
  }
}
