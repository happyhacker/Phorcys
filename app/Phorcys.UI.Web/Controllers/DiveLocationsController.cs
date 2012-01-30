using System.Web.Mvc;
using Phorcys.Core;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using System.Collections.Generic;
using System;
using SharpArch.Web.NHibernate;
using NHibernate.Validator.Engine;
using System.Text;
using SharpArch.Web.CommonValidator;
using SharpArch.Core;

namespace Phorcys.UI.Web.Controllers
{
    [HandleError]
    public class DiveLocationsController : Controller
    {
        public DiveLocationsController(IRepository<DiveLocation> diveLocationRepository) {
            Check.Require(diveLocationRepository != null, "diveLocationRepository may not be null");

            this.diveLocationRepository = diveLocationRepository;
        }

        [Authorize]
        [Transaction]
        public ActionResult Index() {
            IList<DiveLocation> diveLocations = diveLocationRepository.GetAll();
            return View(diveLocations);
        }

        [Transaction]
        public ActionResult Show(int id) {
            DiveLocation diveLocation = diveLocationRepository.Get(id);
            return View(diveLocation);
        }

        public ActionResult Create() {
            DiveLocationFormViewModel viewModel = DiveLocationFormViewModel.CreateDiveLocationFormViewModel();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(DiveLocation diveLocation) {
            if (ViewData.ModelState.IsValid && diveLocation.IsValid()) {
                diveLocationRepository.SaveOrUpdate(diveLocation);

                TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
					"The diveLocation was successfully created.";
                return RedirectToAction("Index");
            }

            DiveLocationFormViewModel viewModel = DiveLocationFormViewModel.CreateDiveLocationFormViewModel();
            viewModel.DiveLocation = diveLocation;
            return View(viewModel);
        }

        [Transaction]
        public ActionResult Edit(int id) {
            DiveLocationFormViewModel viewModel = DiveLocationFormViewModel.CreateDiveLocationFormViewModel();
            viewModel.DiveLocation = diveLocationRepository.Get(id);
            return View(viewModel);
        }

        //[ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(DiveLocation diveLocation) {
            DiveLocation diveLocationToUpdate = diveLocationRepository.Get(diveLocation.Id);
            TransferFormValuesTo(diveLocationToUpdate, diveLocation);

            if (ViewData.ModelState.IsValid) {
                TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
					"The diveLocation was successfully updated.";
                return RedirectToAction("Index");
            }
            else {
                diveLocationRepository.DbContext.RollbackTransaction();

				DiveLocationFormViewModel viewModel = DiveLocationFormViewModel.CreateDiveLocationFormViewModel();
				viewModel.DiveLocation = diveLocation;
				return View(viewModel);
            }
        }

        private void TransferFormValuesTo(DiveLocation diveLocationToUpdate, DiveLocation diveLocationFromForm) {
			//diveLocationToUpdate.Contact = diveLocationFromForm.Contact;
			diveLocationToUpdate.Title = diveLocationFromForm.Title;
			diveLocationToUpdate.Notes = diveLocationFromForm.Notes;
			diveLocationToUpdate.UserId = diveLocationFromForm.UserId;
			//diveLocationToUpdate.User = diveLocationFromForm.User;
			diveLocationToUpdate.LastModified = System.DateTime.Now;
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id) {
            string resultMessage = "The diveLocation was successfully deleted.";
            DiveLocation diveLocationToDelete = diveLocationRepository.Get(id);

            if (diveLocationToDelete != null) {
                diveLocationRepository.Delete(diveLocationToDelete);

                try {
                    diveLocationRepository.DbContext.CommitChanges();
                }
                catch {
                    resultMessage = "A problem was encountered preventing the diveLocation from being deleted. " +
						"Another item likely depends on this diveLocation.";
                    diveLocationRepository.DbContext.RollbackTransaction();
                }
            }
            else {
                resultMessage = "The diveLocation could not be found for deletion. It may already have been deleted.";
            }

            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
            return RedirectToAction("Index");
        }

		/// <summary>
		/// Holds data to be passed to the DiveLocation form for creates and edits
		/// </summary>
        public class DiveLocationFormViewModel
        {
            private DiveLocationFormViewModel() { }

			/// <summary>
			/// Creation method for creating the view model. Services may be passed to the creation 
			/// method to instantiate items such as lists for drop down boxes.
			/// </summary>
            public static DiveLocationFormViewModel CreateDiveLocationFormViewModel() {
                DiveLocationFormViewModel viewModel = new DiveLocationFormViewModel();
                
                return viewModel;
            }

            public DiveLocation DiveLocation { get; internal set; }
        }

        private readonly IRepository<DiveLocation> diveLocationRepository;
    }
}
