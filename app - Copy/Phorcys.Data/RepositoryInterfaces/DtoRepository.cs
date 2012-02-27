using System;
using System.Collections.Generic;
using System.Reflection;
using Gcc.Architecture.Core.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using Phorcys.Data.Repositories;
using SharpArch.Core.DomainModel;
using SharpArch.Data.NHibernate;

namespace Phorcys.Data.RepositoryInterfaces
{
    /// <summary>
    /// Repository that returns Dtos based on a particular entity.  It does so via convention -- the properties in the dto
    /// need to have a corresponding (mapped) property in the entity.  The parameters in the dto are inspected via reflection and
    /// a projection set up against the dto to pull the appropriate properties from the entity.  The various methods then utilize
    /// a result transformer to map the properties of the entity to the property of the same name in the dto.
    /// 
    /// This is a simple way to use a repository directly to return a dto without the use of a separate mapper class.
    /// 
    /// It is important to know that this repository does NOT currently work with collections or more complex objects -- only with 
    /// primitive properties.  We are still exploring whether this can be expanded to work in more situations, but for now, those may be a case
    /// that requires use of a mapper (see Phorcys.Services.Mappers for samples of those).
    /// </summary>
    /// <typeparam name="T">An entity.</typeparam>
    /// <typeparam name="TK">A dto.</typeparam>
    public class DtoRepository<T, TK> : IDtoRepository<T, TK> where T : Entity where TK : EntityDto
    {
        #region Fields

        private readonly ProjectionList projections;

        #endregion

        #region Ctor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DtoRepository()
        {
            projections = Projections.ProjectionList();

            //Analyze the dto properties via reflection and create a ProjectionList to eventually apply to criteria for the entity.  This 
            //will ensure that NHibernate ONLY pulls those properties needed by the dto.

            Type type = typeof(TK);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                projections.Add(Projections.Alias(Projections.Property(property.Name), property.Name));
            }
        }

        #endregion

        /// <summary>
        /// Get all, mapped to dtos.
        /// </summary>
        /// <returns>List of dtos.</returns>
        public IList<TK> GetAll()
        {
            ISession session = NHibernateSession.Current;

            ICriteria criteria = session.CreateCriteria<T>();
            
            //Projection was set up in the constructor to include the appropriate fields.
            criteria.SetProjection(projections);

            //ResultTransformer to (via convention) map the result to the dto.
            criteria.SetResultTransformer(
                new NHibernate.Transform.AliasToBeanResultTransformer(typeof(TK)));

            using (session.BeginTransaction())
            {
                return criteria.List<TK>();
            }
        }

        /// <summary>
        /// Get a single instance, mapped to a dto.
        /// </summary>
        /// <param name="id">The id of the object in question.</param>
        /// <returns>List of dtos.</returns>
        public TK Get(int id)
        {
            ISession session = NHibernateSession.Current;

            ICriteria criteria = session.CreateCriteria<T>();

            //Projection was set up in the constructor to include the appropriate fields.
            criteria.SetProjection(projections);

            //ResultTransformer to (via convention) map the result to the dto.
            criteria.SetResultTransformer(
                new NHibernate.Transform.AliasToBeanResultTransformer(typeof(TK)));
            criteria.Add(Restrictions.Eq("Id", id));

            using (session.BeginTransaction())
            {
                return criteria.UniqueResult<TK>();
            }
        }
    }
}
