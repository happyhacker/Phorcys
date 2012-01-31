using System.Collections.Generic;
using Gcc.Architecture.Services.Interfaces;
using NUnit.Framework;
using Phorcys.Core.Domains;
using Phorcys.Services.Contracts.Dtos;
using Phorcys.Services.Mappers;
using Phorcys.Tests.Unit.InstanceTestFactories;
using SharpArch.Testing.NUnit;

namespace Phorcys.Tests.Unit.Services.Mappers
{
    [TestFixture]
    public class EnumerationMapperTests
    {
        #region Fields

        private IMapper<Enumeration, EnumerationDto> mapper = new EnumerationMapper();

        #endregion

        [Test]
        public void CanMapToEnumerationDto()
        {
            //Establish Context
            Enumeration enumeration = EnumerationInstanceTestFactory.CreateEnumeration(1);

            //Act
            EnumerationDto enumerationDto = mapper.Map(enumeration);

            //Assert
            enumerationDto.Id.ShouldEqual(enumeration.Id);
            enumeration.Name.ShouldEqual(enumeration.Name);
        }

        [Test]
        public void CanMapToEnumerationDtoList()
        {
            //Establish Context
            IList<Enumeration> enumerations = EnumerationInstanceTestFactory.CreateEnumerations();

            //Act
            IList<EnumerationDto> enumerationDtos = mapper.MapList(enumerations);

            //Assert
            enumerationDtos.Count.ShouldEqual(2);
            enumerationDtos[0].Id.ShouldEqual(enumerations[0].Id);
            enumerationDtos[0].Name.ShouldEqual(enumerations[0].Name);
            enumerationDtos[1].Id.ShouldEqual(enumerations[1].Id);
            enumerationDtos[1].Name.ShouldEqual(enumerations[1].Name);
        }
    }
}
