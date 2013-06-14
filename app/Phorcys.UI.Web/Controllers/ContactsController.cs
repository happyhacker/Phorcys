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
      protected static readonly ILog log = LogManager.GetLogger(typeof(ContactsController));
      private readonly ContactServices contactServices = new ContactServices();
      private readonly UserServices userServices = new UserServices();

      private readonly IRepository<Contact> contactRepository;
      private User user;

        public ContactsController(IRepository<Contact> contactRepository) {
          Check.Require(contactRepository != null, "contactRepository may not be null");
          this.contactRepository = new PhorcysRepository<Contact>();
        }

        [Authorize]
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

        [Authorize]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Contacts/Create

        [HttpPost]
        public ActionResult Create(ContactModel model) {

          if (!ModelState.IsValid) {
            return View();
          }

          try {
            saveNewContact(model);

            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = "Contact was successfully created.";

            return RedirectToAction("Index");

          }
          catch (Exception ex) {
            StringBuilder errMsg = new StringBuilder();
            errMsg.Append("Unable to create " + model.Company + ", " + model.FirstName + " " + model.LastName + ". " + ex.Message);
            log.Error(errMsg.ToString());
            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = errMsg.ToString();
            return View();
          }
        }

        private void saveNewContact(ContactModel model) {
          this.user = userServices.FindUser(this.User.Identity.Name);
          Contact contact = new Contact();

          contact.Company = model.Company == null ? "" : model.Company;
          contact.FirstName = model.FirstName == null ? "" : model.FirstName;
          contact.LastName = model.LastName == null ? "" : model.LastName;
          contact.Gender = model.Gender == null ? "" : model.Gender;
          contact.Address1 = model.Address1 == null ? "" : model.Address1;
          contact.Address2 = model.Address2 == null ? "" : model.Address2;
          contact.City = model.City == null ? "" : model.City;
          contact.State = model.State == null ? "" : model.State;
          contact.PostalCode = model.PostalCode == null ? "" : model.PostalCode;
          contact.CellPhone = model.CellPhone == null ? "" : model.CellPhone;
          contact.HomePhone = model.HomePhone == null ? "" : model.HomePhone;
          contact.WorkPhone = model.WorkPhone == null ? "" : model.WorkPhone;
          contact.Email = model.Email == null ? "" : model.Email;
          contact.Birthday = model.Birthday;
          contact.Notes = model.Notes;

          contact.User = this.user;
          contact.Created = DateTime.Now;
          contact.LastModified = DateTime.Now;
          contactServices.Save(contact);
        }


        
        //
        // GET: /Contacts/Edit/5
        [Authorize]
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
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Contacts/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
          string resultMessage = "The contact was successfully deleted.";
          Contact contactToDelete = contactServices.GetContact(id);

          if (contactToDelete != null) {
            try {
              contactServices.Delete(contactToDelete);
            }
            catch {
              resultMessage = "A problem was encountered preventing this contact from being deleted. " +
                              "A dive probably references this contact.";
            }
          }
          else {
            resultMessage = "This contact could not be found for deletion. It may already have been deleted.";
          }

          TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
          return RedirectToAction("Index");
        }
    }
}
