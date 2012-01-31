using System.Collections.Generic;
using System.Linq;
using Gcc.Architecture.Data.Interfaces;
using NUnit.Framework;
using Phorcys.Core.Domains;
using Phorcys.Data.Repositories;
using Phorcys.Services.Contracts;
using Phorcys.Services.Contracts.Dtos;
using Phorcys.Services.Contracts.Interfaces;
using Phorcys.Services.Services;
using Phorcys.Tests.Unit.InstanceTestFactories;
using Rhino.Mocks;
using SharpArch.Testing.NUnit;

namespace Phorcys.Tests.Unit.Services.Services
{
    [TestFixture]
    public class EnumerationServiceTests : EnumerationInstanceTestFactory
    {
        #region Fields

        private IDtoRepository<Enumeration, EnumerationDto> enumerationDtoRepository;
        private IGccRepository<Enumeration> enumerationRepository;
        private IEnumerationService service;

        #endregion

        [SetUp]
        public void SetUp()
        {
            enumerationDtoRepository = MockRepository.GenerateMock<IDtoRepository<Enumeration, EnumerationDto>>();
            enumerationRepository = MockRepository.GenerateMock<IGccRepository<Enumeration>>();

            service = new EnumerationService(enumerationDtoRepository, enumerationRepository);
        }

        [Test]
        public void CanGetAll()
        {
            //Establish Context
            enumerationDtoRepository.Stub(e => e.GetAll()).Return(CreateEnumerationDtos());

            //Act
            IList<EnumerationDto> enumerationDtos = service.GetAll();

            //Assert
            enumerationDtos.SequenceEqual(CreateEnumerationDtos()).ShouldBeTrue();
        }

        [Test]
        public void CanGetById()
        {
            //Establish Context
            enumerationDtoRepository.Stub(e => e.Get(1)).Return(CreateEnumerationDto(1));

            //Act
            EnumerationDto enumerationDto = service.Get(1);

            //Assert
            enumerationDto.ShouldEqual(CreateEnumerationDto(1));
        }

        [Test]
        public void CanChangeName()
        {
            //Establish Context
            Enumeration enumeration = CreateEnumeration(1);
            enumeration.ChangeName("Original Name");
            enumerationRepository.Stub(e => e.Get(1)).Return(enumeration);
            enumerationRepository.Stub(e => e.SaveOrUpdate(enumeration)).Return(enumeration);
            
            //Act
            Confirmation confirmation = service.ChangeName(1, "New Name");

            //Assert
            confirmation.SavedId.ShouldEqual(1);
            confirmation.Message.ShouldEqual("The enumeration name was changed successfully.");
            confirmation.WasSuccessful.ShouldBeTrue();
        }

        [Test]
        public void CanCreate()
        {
            //Establish Context
            Enumeration savedEnumeration = CreateEnumeration(1);
            enumerationRepository.Stub(e => e.SaveOrUpdate(CreateEnumeration(0))).IgnoreArguments().Return(savedEnumeration);

            //Act
            Confirmation confirmation = service.Create("New Enumeration Name");

            //Assert
            confirmation.SavedId.ShouldEqual(1);
            confirmation.Message.ShouldEqual("The enumeration \"New Enumeration Name\" was changed successfully.");
            confirmation.WasSuccessful.ShouldBeTrue();
        }
    }
}
