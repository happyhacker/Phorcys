using System.Linq;
using System.Collections.Generic;
using Gcc.Architecture.Services.Interfaces;
using Phorcys.Core.Domains;
using Phorcys.Services.Contracts.Dtos;

namespace Phorcys.Services.Mappers
{
    /// <summary>
    /// This mapper demonstrates how to map instances of an enumeration to an enumeration dto so the results can be passed across a service boundary.
    /// </summary>
    public class EnumerationMapper : IMapper<Enumeration, EnumerationDto>
    {
        /// <summary>
        /// Map an Enumeration to an EnumerationDto.
        /// </summary>
        /// <param name="mapFrom">Enumeration</param>
        /// <returns>EnumerationDto</returns>
        public EnumerationDto Map(Enumeration mapFrom)
        {
            EnumerationDto mapTo = new EnumerationDto
            {
                Id = mapFrom.Id, 
                Name = mapFrom.Name
            };
            
            return mapTo;
        }

        /// <summary>
        /// Maps a list of Enumeration to a list of EnumerationDto.
        /// </summary>
        /// <param name="list">list of Enumeration</param>
        /// <returns>list of EnumerationDto</returns>
        public IList<EnumerationDto> MapList(IList<Enumeration> list)
        {
            return list.Select(Map).ToList();
        }

        
    }
}
