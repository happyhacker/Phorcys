using System.Collections.Generic;
using Phorcys.Core.Domains;
using Phorcys.Services.Contracts.Dtos;

namespace Phorcys.Tests.Unit.InstanceTestFactories
{
    public class EnumerationInstanceTestFactory
    {
        private class EnumerationFacade : Enumeration
        {
            public EnumerationFacade(int id, string name, string dataType, bool active)
            {
                Id = id;
                Name = name;
                DataType = dataType;
                Active = active;
            }
        }


        /// <summary>
        /// Creates a valid, Enumeration; typical of something retrieved back from a form submission
        /// </summary>
        public static Enumeration CreateEnumeration(int id)
        {
            EnumerationFacade enumeration = new EnumerationFacade(id, "TestName", "String", true);

            return enumeration;
        }

        /// <summary>
        /// Creates a valid, EnumerationDto; typical of something retrieved back from a form submission
        /// </summary>
        public static EnumerationDto CreateEnumerationDto(int id)
        {
            EnumerationDto enumerationDto = new EnumerationDto
            {
                Name = "TestName",
                Id = id
            };

            return enumerationDto;
        }

        /// <summary>
        /// Creates a valid, list of Enumerations; typical of something retrieved back from a form submission
        /// </summary>
        public static List<Enumeration> CreateEnumerations()
        {
            List<Enumeration> enumerations = new List<Enumeration>();

            Enumeration enumeration1 = CreateEnumeration(1);
            Enumeration enumeration2 = CreateEnumeration(2);

            enumerations.Add(enumeration1);
            enumerations.Add(enumeration2);

            return enumerations;
        }

        /// <summary>
        /// Creates a valid, list of EnumerationDtos; typical of something retrieved back from a form submission
        /// </summary>
        public static List<EnumerationDto> CreateEnumerationDtos()
        {
            List<EnumerationDto> enumerationDtos = new List<EnumerationDto>();

            EnumerationDto enumerationDto1 = CreateEnumerationDto(1);
            EnumerationDto enumerationDto2 = CreateEnumerationDto(2);

            enumerationDtos.Add(enumerationDto1);
            enumerationDtos.Add(enumerationDto2);

            return enumerationDtos;
        }
    }
}
