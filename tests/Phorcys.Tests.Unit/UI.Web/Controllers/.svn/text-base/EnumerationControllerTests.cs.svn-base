using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Phorcys.Services.Contracts;
using Phorcys.UI.Web.Models;
using SharpArch.Testing.NUnit;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Phorcys.Services.Contracts.Dtos;
using Phorcys.Services.Contracts.Interfaces;
using Phorcys.Tests.Unit.InstanceTestFactories;
using Phorcys.UI.Web.Controllers;
using Rhino.Mocks;

namespace Phorcys.Tests.Unit.UI.Web.Controllers
{
    [TestFixture]
    public class EnumerationControllerTests : EnumerationInstanceTestFactory 
    {
        #region Fields

        private IEnumerationService enumerationService;
        private EnumerationController controller;
        
        #endregion Fields

        [SetUp]
        public void SetUp()
        {
            enumerationService = MockRepository.GenerateMock<IEnumerationService>();

            controller = new EnumerationController(enumerationService);
        }

        [Test]
        public void CanInitializeIndex()
        {
            //Establish Context
            enumerationService.Stub(e => e.GetAll()).Return(CreateEnumerationDtos());

            //Act
            ActionResult result = controller.Index();

            //Assert
            result.AssertViewRendered();
            IList<EnumerationDto> model = controller.ViewData.Model as List<EnumerationDto>;
            model.ShouldNotBeNull();
            model.SequenceEqual(CreateEnumerationDtos()).ShouldBeTrue();
        }

        [Test]
        public void CanInitializeDetail()
        {
            //Establish Context
            enumerationService.Stub(e => e.Get(1)).Return(CreateEnumerationDto(1));

            //Act
            ActionResult result = controller.Detail(1);

            //Assert
            result.AssertViewRendered();
            EnumerationDto model = controller.ViewData.Model as EnumerationDto;
            model.ShouldNotBeNull();
            model.ShouldEqual(CreateEnumerationDto(1));
        }

        [Test]
        public void CanInitializeCreate()
        {
            //Act
            ActionResult result = controller.Create();

            //Assert
            result.AssertViewRendered();
            EnumerationCreateViewModel model = controller.ViewData.Model as EnumerationCreateViewModel;
            model.ShouldNotBeNull();
            model.Name.ShouldBeNull();
        }

        [Test]
        public void CanPostCreate()
        {
            //Establish Context
            EnumerationCreateViewModel enumerationCreateViewModel = new EnumerationCreateViewModel();
            enumerationCreateViewModel.Name = "New Enumeration Name";
            enumerationService.Stub(e => e.Create("New Enumeration Name")).Return(Confirmation.CreateSuccessConfirmation("created"));
            enumerationService.Stub(e => e.GetAll()).Return(CreateEnumerationDtos());

            //Act
            ActionResult result = controller.Create(enumerationCreateViewModel);

            //Assert
            result.AssertActionRedirect().ToAction("Index");
        }

        [Test]
        public void CanInitializeChangeName()
        {
            //Establish Context
            EnumerationDto enumerationDto = CreateEnumerationDto(1);
            enumerationService.Stub(e => e.Get(1)).Return(enumerationDto);

            //Act
            ActionResult result = controller.ChangeName(1);

            //Assert
            result.AssertViewRendered();
            EnumerationChangeNameViewModel model = controller.ViewData.Model as EnumerationChangeNameViewModel;
            model.ShouldNotBeNull();
            model.Name.ShouldEqual(enumerationDto.Name);
            model.EnumerationId.ShouldEqual(1);
        }

        [Test]
        public void CanPostChangeName()
        {
            //Establish Context
            EnumerationChangeNameViewModel enumerationChangeNameViewModel = new EnumerationChangeNameViewModel();
            enumerationChangeNameViewModel.Name = "Updated Enumeration Name";
            enumerationChangeNameViewModel.EnumerationId = 1;
            enumerationService.Stub(e => e.ChangeName(1, "Updated Enumeration Name")).Return(Confirmation.CreateSuccessConfirmation("name changed"));
            enumerationService.Stub(e => e.GetAll()).Return(CreateEnumerationDtos());

            //Act
            ActionResult result = controller.ChangeName(enumerationChangeNameViewModel);

            //Assert
            result.AssertActionRedirect().ToAction("Index");
        }
    }
}
