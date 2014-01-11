using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.FluentHtml.Elements;
using SharpArch.Web.NHibernate;
using SharpArch.Core;
using SharpArch.Core.PersistenceSupport;
using log4net;
using Phorcys.Core;
using Phorcys.Data;
using Phorcys.Services;
using Phorcys.UI.Web.Models;

namespace Phorcys.UI.Web.Controllers {
  public class ContactsController : Controller {
    protected static readonly ILog log = LogManager.GetLogger(typeof(ContactsController));
    private readonly ContactServices contactServices = new ContactServices();
    private readonly DiverServices diverServices = new DiverServices();
    private readonly InstructorServices instructorServices = new InstructorServices();
    private readonly DiveAgencyServices diveAgencyServices = new DiveAgencyServices();
    private readonly ManufacturerServices manufacturerServices = new ManufacturerServices();
    private readonly DiveShopServices diveShopServices = new DiveShopServices();
    private readonly CountryServices countryServices = new CountryServices();
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
      if (!c.DiveAgencies.IsEmpty)
        tags.Add("Agency");
      if (!c.Divers.IsEmpty)
        tags.Add("Diver");
      if (!c.DiveShops.IsEmpty)
        tags.Add("DiveShop");
      if (!c.Instructors.IsEmpty)
        tags.Add("Instructor");
      if (!c.Manufacturers.IsEmpty)
        tags.Add("Manufacturer");

      return string.Join(", ", tags.ToArray());
    }

    private IList<SelectListItem> BuildCountryList(string code) {
      IList<SelectListItem> CountryList = new List<SelectListItem>();
      IList<Country> countries = GetCountries();
      SelectListItem CountryItem;

      countries = countries.OrderBy(m => m.Name).ToList();
      foreach (var country in countries) {
        CountryItem = new SelectListItem();
        CountryItem.Text = country.Name;
        CountryItem.Value = country.CountryCode;
        if (code != "") {
          if (country.CountryCode == code) {
            CountryItem.Selected = true;
          }
        }
        CountryList.Add(CountryItem);
      }
      return CountryList;
    }

    private IList<Country> GetCountries() {
      IList<Country> countries = countryServices.GetAll();

      return countries;
    }


    //
    // GET: /Contacts/Details/5

    public ActionResult Details(int id) {
      return View();
    }

    //
    // GET: /Contacts/Create

    [Authorize]
    public ActionResult Create() {
      ContactModel viewModel = new ContactModel();
      viewModel.Countries = BuildCountryList("");

      return View("Create", viewModel);
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

      } catch (Exception ex) {
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
      contact = UpdateContactFromModel(contact, model);
      contact.User = this.user;
      contact.Created = DateTime.Now;
      contact.LastModified = DateTime.Now;
      contactServices.Save(contact);
 
      if (model.isDiver) {
        Diver diver = new Diver();
        diver.Contact = contact;
        diverServices.Save(diver);
      }
      if (model.isInstructor) {
        Instructor instructor = new Instructor();
        instructor.Contact = contact;
        instructorServices.Save(instructor);
      }

      if (model.isAgency) {
        DiveAgency agency = new DiveAgency();
        agency.Contact = contact;
        diveAgencyServices.Save(agency);
      }

      if (model.isManufacturer) {
        Manufacturer manufacturer = new Manufacturer();
        manufacturer.Contact = contact;
        manufacturerServices.Save(manufacturer);
      }

      if (model.isDiveShop)
      {
        DiveShop diveShop = new DiveShop();
        diveShop.Contact = contact;
        diveShopServices.Save(diveShop);
      }
    }

    private Contact UpdateContactFromModel(Contact contact, ContactModel model) {
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

      return contact;
    }

    //
    // GET: /Contacts/Edit/5
    [Authorize]
    public ActionResult Edit(int id) {
      ContactModel model = new ContactModel();
      Contact contact = contactServices.GetContact(id);
      model.Address1 = contact.Address1;
      model.Address2 = contact.Address2;
      model.City = contact.City;
      model.Company = contact.Company;
      model.ContactId = contact.Id;
      model.Email = contact.Email;
      model.FirstName = contact.FirstName;
      model.LastName = contact.LastName;
      model.Notes = contact.Notes;
      model.PostalCode = contact.PostalCode;
      model.State = contact.State;

      return View("Edit", model);
    }

    [Authorize]
    public ActionResult EditProfile() {
      user = userServices.FindUser(this.User.Identity.Name);
      if (user.Contact == null) {
        Contact contact = new Contact();
        contact.User = user;
        contact.Created = System.DateTime.Now;
        contactServices.Save(contact);
        user.Contact = contact;
        userServices.Save(user);
      }
      return (Edit(user.Contact.Id));
    }

    //
    // POST: /Contacts/Edit/5

    [HttpPost]
    public ActionResult Edit(int id, ContactModel model) {
      try {
        Contact contact = contactServices.GetContact(id);
        contact = UpdateContactFromModel(contact, model);
        contactServices.Save(contact);

        return RedirectToAction("Index");
      } catch {
        return View();
      }
    }

    //
    // GET: /Contacts/Delete/5
    [Authorize]
    public ActionResult Delete(int id) {
      return View();
    }

    //
    // POST: /Contacts/Delete/5

    [HttpPost]
    public ActionResult Delete(int id, FormCollection collection) {
      string resultMessage = "The contact was successfully deleted.";
      Contact contactToDelete = contactServices.GetContact(id);

      if (contactToDelete != null) {
        try {
          contactServices.Delete(contactToDelete);
        } catch {
          resultMessage = "A problem was encountered preventing this contact from being deleted. " +
                          "something references this contact.";
        }
      } else {
        resultMessage = "This contact could not be found for deletion. It may already have been deleted.";
      }

      TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
      return RedirectToAction("Index");
    }
  }
}
