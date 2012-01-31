using System.Web.Mvc;
using Phorcys.Core;
using Phorcys.UI.Web.Controllers;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using System.Collections.Generic;
using System;
using SharpArch.Web.NHibernate;
using NHibernate.Validator.Engine;
using System.Text;
using SharpArch.Web.CommonValidator;
using SharpArch.Core;

namespace Phorcys.Web.Controllers
{
    [HandleError]
    public class DiveTypesController : Controller
    {
        public DiveTypesController(IRepository<DiveType> diveTypeRepository) {
            Check.Require(diveTypeRepository != null, "diveTypeRepository may not be null");

            this.diveTypeRepository = diveTypeRepository;
        }

        [Transaction]
        public ActionResult Index() {
            IList<DiveType> diveTypes = diveTypeRepository.GetAll();
            return View(diveTypes);
        }

        [Transaction]
        public ActionResult Show(int id) {
            DiveType diveType = diveTypeRepository.Get(id);
            return View(diveType);
        }

        public ActionResult Create() {
            DiveTypeFormViewModel viewModel = DiveTypeFormViewModel.CreateDiveTypeFormViewModel();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(DiveType diveType) {
            if (ViewData.ModelState.IsValid && diveType.IsValid()) {
                diveTypeRepository.SaveOrUpdate(diveType);

                TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
					"The diveType was successfully created.";
                return RedirectToAction("Index");
            }

            DiveTypeFormViewModel viewModel = DiveTypeFormViewModel.CreateDiveTypeFormViewModel();
            viewModel.DiveType = diveType;
            return View(viewModel);
        }

        [Transaction]
        public ActionResult Edit(int id) {
            DiveTypeFormViewModel viewModel = DiveTypeFormViewModel.CreateDiveTypeFormViewModel();
            viewModel.DiveType = diveTypeRepository.Get(id);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(DiveType diveType) {
            DiveType diveTypeToUpdate = diveTypeRepository.Get(diveType.Id);
            TransferFormValuesTo(diveTypeToUpdate, diveType);

            if (ViewData.ModelState.IsValid && diveType.IsValid()) {
                TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = 
					"The diveType was successfully updated.";
                return RedirectToAction("Index");
            }
            else {
                diveTypeRepository.DbContext.RollbackTransaction();

				DiveTypeFormViewModel viewModel = DiveTypeFormViewModel.CreateDiveTypeFormViewModel();
				viewModel.DiveType = diveType;
				return View(viewModel);
            }
        }

        private void TransferFormValuesTo(DiveType diveTypeToUpdate, DiveType diveTypeFromForm) {
			diveTypeToUpdate.Title = diveTypeFromForm.Title;
			diveTypeToUpdate.Notes = diveTypeFromForm.Notes;
			diveTypeToUpdate.Dives = diveTypeFromForm.Dives;
			diveTypeToUpdate.User = diveTypeFromForm.User;
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
                }
                catch {
                    resultMessage = "A problem was encountered preventing the diveType from being deleted. " +
						"Another item likely depends on this diveType.";
                    diveTypeRepository.DbContext.RollbackTransaction();
                }
            }
            else {
                resultMessage = "The diveType could not be found for deletion. It may already have been deleted.";
            }

            TempData[ControllerEnums.GlobalViewDataProperty.PageMessage.ToString()] = resultMessage;
            return RedirectToAction("Index");
        }

		/// <summary>
		/// Holds data to be passed to the DiveType form for creates and edits
		/// </summary>
        public class DiveTypeFormViewModel
        {
            private DiveTypeFormViewModel() { }

			/// <summary>
			/// Creation method for creating the view model. Services may be passed to the creation 
			/// method to instantiate items such as lists for drop down boxes.
			/// </summary>
            public static DiveTypeFormViewModel CreateDiveTypeFormViewModel() {
                DiveTypeFormViewModel viewModel = new DiveTypeFormViewModel();
                
                return viewModel;
            }

            public DiveType DiveType { get; internal set; }
        }

        private readonly IRepository<DiveType> diveTypeRepository;
    }
}
