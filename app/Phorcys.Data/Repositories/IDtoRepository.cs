using System.Collections.Generic;
using Gcc.Architecture.Core.Interfaces;
using SharpArch.Core.DomainModel;

namespace Phorcys.Data.Repositories
{
    /// <summary>
    /// Interface for a repository that returns Dtos based on a particular entity.
    /// </summary>
    /// <typeparam name="T">An entity.</typeparam>
    /// <typeparam name="TK">A dto.</typeparam>
    public interface IDtoRepository<T, TK> where T : Entity where TK : IEntityDto
    {
        IList<TK> GetAll();
        TK Get(int id);
    }
}
