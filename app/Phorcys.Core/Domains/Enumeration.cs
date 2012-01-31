using SharpArch.Core.DomainModel;

namespace Phorcys.Core.Domains
{
    /// <summary>
    /// A basic entity class for an enumeration.
    /// Following DDD principles, all of the properties have protected setters.  Individual tasks (such as ChangeName) are used to modify parameters based on intent.
    /// Instantiation of the object requires all information needed to create a "valid" instance of the object.
    /// </summary>
    public class Enumeration : Entity
    {
        [DomainSignature]
        public virtual string Name { get; protected set; }

        [DomainSignature]
        public virtual string DataType { get; protected set; }

        [DomainSignature]
        public virtual bool Active { get; protected set; }

        #region Ctors

        /// <summary>
        /// NHibernate needs a parameter-less constructor.  This is protected because noone else should be creating instances of entities this way.
        /// </summary>
        protected Enumeration()
        {
        }

        public Enumeration(string name)
        {
            Name = name;
            DataType = "String";
            Active = true;
        }

        #endregion

        /// <summary>
        /// Change the name of the enumeration.
        /// </summary>
        /// <param name="name">The new enumeration name.</param>
        public virtual void ChangeName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Deactivates the enumeration.
        /// </summary>
        public virtual void Deactivate()
        {
            Active = false;
        }

        /// <summary>
        /// Activates the enumeration.
        /// </summary>
        public virtual void Activate()
        {
            Active = true;
        }
    }
}
