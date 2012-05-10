using Iesi.Collections.Generic;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;
using System;

namespace Phorcys.Core {
  public class DiveLocation : Entity {
    public DiveLocation() { }

    //[DomainSignature]
    //public virtual int DiveLocationId { get; set; }

    //[DomainSignature]
    //public virtual Contact Contact { get; set; }

    [DomainSignature]
    [NotNull, NotEmpty]
    public virtual string Title { get; set; }

    [DomainSignature]
    public virtual string Notes { get; set; }

    [DomainSignature]
    public virtual int UserId { get; set; }

    //[DomainSignature]
    //public virtual ISet<DiveSite> DiveSites { get; set; }

    [DomainSignature]
    [NotNull]
    public virtual User User { get; set; }

    //[DomainSignature]
    public virtual DateTime Created { get; set; }

    //[DomainSignature]
    public virtual DateTime LastModified { get; set; }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as DiveLocation);
    }

    public virtual bool Equals(DiveLocation obj) {
      if (obj == null) return false;

      if (Equals(Created, obj.Created) == false)
        return false;

      if (Equals(Id, obj.Id) == false)
        return false;

      if (Equals(Title, obj.Title) == false)
        return false;

      if (Equals(LastModified, obj.LastModified) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ Created.GetHashCode();
      result = (result * 397) ^ Id.GetHashCode();
      result = (result * 397) ^ LastModified.GetHashCode();
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
      return result;
    }
  }
}
