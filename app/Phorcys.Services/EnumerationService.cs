using System.Collections.Generic;
using Gcc.Architecture.Data.Interfaces;
using Phorcys.Core.Domains;
using Phorcys.Data.Repositories;
using Phorcys.Services.Contracts;
using Phorcys.Services.Contracts.Dtos;
using Phorcys.Services.Contracts.Interfaces;
using SharpArch.Core;
using SharpArchContrib.Castle.NHibernate;

namespace Phorcys.Services.Services
{
    /// <summary>
    /// The service boundary.  This defines all of the methods typically used by the controller layer to access data.  Methods in this layer should be intent based as 
    /// much as possible -- as opposed to things like Update methods which update an entire object.
    /// 
    /// The [Transaction] attribute on methods can be nested -- calling one transaction-ed method from another will ensure that both are committed at the same time.
    /// </summary>
    /// TODO: Should this be refactored to a CQRS pair of services since reads typically use the DtoRepository and writes typically use the entity repository?
    public class EnumerationService : IEnumerationService
    {
        #region Fields
        
        private readonly IDtoRepository<Enumeration, EnumerationDto> enumerationDtoRepository;
        private readonly IGccRepository<Enumeration> enumerationRepository;
        
        #endregion Fields

        public EnumerationService(IDtoRepository<Enumeration, EnumerationDto> enumerationDtoRepository, IGccRepository<Enumeration> enumerationRepository)
		{
            Check.Require(enumerationDtoRepository != null, "EnumerationDto repository may not be null");
            Check.Require(enumerationRepository != null, "Enumeration repository may not be null");
            this.enumerationDtoRepository = enumerationDtoRepository;
            this.enumerationRepository = enumerationRepository;
		}

        /// <summary>
        /// Get an enumeration by its id.
        /// </summary>
        /// <param name="id">A valid id.</param>
        /// <returns>The enumeration identified by the given id.</returns>
        [Transaction]
        public EnumerationDto Get(int id)
        {
            return enumerationDtoRepository.Get(id);
        }

        /// <summary>
        /// Get all enumerations in the system.
        /// </summary>
        /// <returns></returns>
        [Transaction]
        public IList<EnumerationDto> GetAll()
        {
            return enumerationDtoRepository.GetAll();
        }

        /// <summary>
        /// Create a new enumeration with a given name.
        /// </summary>
        /// <param name="name">The name of the enumeration to create.</param>
        [Transaction]
        public Confirmation Create(string name)
        {
            Enumeration enumeration = new Enumeration(name);
            enumeration = enumerationRepository.SaveOrUpdate(enumeration);

            return Confirmation.CreateSuccessConfirmation("The enumeration \"" + name + "\" was changed successfully.", enumeration.Id);
        }

        /// <summary>
        /// Change the name of the enumeration.
        /// </summary>
        /// <param name="id">The id of the enumeration to be changed.</param>
        /// <param name="newName">The new name for the enumeration.</param>
        [Transaction]
        public Confirmation ChangeName(int id, string newName)
        {
            Enumeration enumeration = enumerationRepository.Get(id);
            enumeration.ChangeName(newName);

            enumeration = enumerationRepository.SaveOrUpdate(enumeration);

            return Confirmation.CreateSuccessConfirmation("The enumeration name was changed successfully.", enumeration.Id);
        }
    }
}