using System.Web.Mvc;
using Gcc.Architecture.Web.Controllers;
using Phorcys.Services.Contracts.Interfaces;
using Phorcys.UI.Web.Models;

namespace Phorcys.UI.Web.Controllers
{
    public class EnumerationController : BaseController
    {
        #region Fields

        private readonly IEnumerationService enumerationService;

        #endregion

        public EnumerationController(IEnumerationService enumerationService)
        {
            this.enumerationService = enumerationService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View(enumerationService.GetAll());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detail(int id)
        {
            return View(enumerationService.Get(id));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            return View(new EnumerationCreateViewModel());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(EnumerationCreateViewModel enumerationCreateViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            enumerationService.Create(enumerationCreateViewModel.Name);

            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ChangeName(int id)
        {
            EnumerationChangeNameViewModel enumerationChangeNameViewModel = new EnumerationChangeNameViewModel();
            enumerationChangeNameViewModel.Name = enumerationService.Get(id).Name;
            enumerationChangeNameViewModel.EnumerationId = id;

            return View(enumerationChangeNameViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangeName(EnumerationChangeNameViewModel enumerationChangeNameViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            enumerationService.ChangeName(enumerationChangeNameViewModel.EnumerationId, enumerationChangeNameViewModel.Name);

            return RedirectToAction("Index");
        }
    }
}
