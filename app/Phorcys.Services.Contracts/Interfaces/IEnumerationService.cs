using System.Collections.Generic;
using Phorcys.Services.Contracts.Dtos;

namespace Phorcys.Services.Contracts.Interfaces
{
    /// <summary>
    /// The interface for an enumeration service.
    /// </summary>
    public interface IEnumerationService
    {
        EnumerationDto Get(int id);
        IList<EnumerationDto> GetAll();
        Confirmation Create(string name);
        Confirmation ChangeName(int id, string newName);
    }
}

