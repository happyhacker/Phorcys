using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpArch.Web.NHibernate;
using SharpArch.Core;
using SharpArch.Core.PersistenceSupport;
using log4net;
using Phorcys.Core;
using Phorcys.Data;
using Phorcys.Services;
using Phorcys.UI.Web.Models;

namespace Phorcys.UI.Web.Controllers
{
    public class ContactsController : Controller {
      protected static readonly ILog log = LogManager.GetLogger(typeof(GearController));
      private readonly ContactServices contactServices = new ContactServices();
      private readonly UserServices userServices = new UserServices();

      private readonly IRepository<Contact> contactRepository;
      private User user;

        public ContactsController(IRepository<Contact> contactRepository) {
          Check.Require(contactRepository != null, "contactRepository may not be null");
          this.contactRepository = new PhorcysRepository<Contact>();
        }

        public ActionResult Index() {
          user = userServices.FindUser(this.User.Identity.Name);
          IList<Contact> contacts = contactServices.GetAllForUser(user.Id);
          IList<ContactsIndexModel> contactsModelList = new List<ContactsIndexModel>();
          ContactsIndexModel contactsModel;

          foreach (Contact c in contacts) {
            contactsModel = new ContactsIndexModel();
            contactsModel.ContactId = c.Id;
            contactsModel.Company = c.Company;
            contactsModel.FirstName = c.FirstName;
            contactsModel.LastName = c.LastName;
            contactsModel.Email = c.Email;
            contactsModel.User = c.User.LoginId;
            contactsModel.tags = getTags(c);
            contactsModelList.Add(contactsModel);
          }

            return View(contactsModelList);
        }

        private string getTags(Contact c) {
          IList<String> tags = new List<String>();
          if (! c.DiveAgencies.IsEmpty) 
            tags.Add("DiveAgency");
          if (! c.Divers.IsEmpty) 
            tags.Add("Diver");
          if (! c.DiveShops.IsEmpty) 
            tags.Add("DiveShop");
          
          return string.Join(", ", tags.ToArray());
        }

        //
        // GET: /Contacts/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Contacts/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Contacts/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Contacts/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Contacts/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Contacts/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Contacts/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
