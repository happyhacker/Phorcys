using Iesi.Collections.Generic;
using NHibernate.Validator.Constraints;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Core.DomainModel;
using System;

namespace Phorcys.Core {
  public class User : Entity {
    public User() { }

    //[DomainSignature]
    //[NotNull, NotEmpty]
    //public virtual int UserId { get; set; }

    [DomainSignature]
    [NotNull, NotEmpty]
    public virtual string LoginId { get; set; }

    [DomainSignature]
    [NotNull, NotEmpty]
    public virtual string Password { get; set; }

    [DomainSignature]
    public virtual int LoginCount { get; set; }

    public virtual DateTime Created {
      get;
      set;
    }
    public virtual DateTime LastModified {
      get;
      set;
    }

    public virtual ISet<Attribute> Attributes {
      get;
      set;
    }
    public virtual ISet<Certification> Certifications {
      get;
      set;
    }
    public virtual Contact Contact {
      get;
      set;
    }
    public virtual ISet<Contact> Contacts {
      get;
      set;
    }
    public virtual ISet<DiveLocation> DiveLocations {
      get;
      set;
    }
    public virtual ISet<Dive> Dives {
      get;
      set;
    }
    public virtual ISet<DiveSite> DiveSites {
      get;
      set;
    }
    public virtual ISet<DiveType> DiveTypes {
      get;
      set;
    }
    public virtual ISet<Friend> Friends {
      get;
      set;
    }
    public virtual ISet<Friend> Friends1 {
      get;
      set;
    }
    public virtual ISet<Gear> Gears {
      get;
      set;
    }
    public virtual ISet<Qualification> Qualifications {
      get;
      set;
    }
    public virtual ISet<Role> Roles {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as User);
    }

    public virtual bool Equals(User obj) {
      if (obj == null) return false;

      if (Equals(Created, obj.Created) == false)
        return false;

      if (Equals(LastModified, obj.LastModified) == false)
        return false;

      if (Equals(LoginCount, obj.LoginCount) == false)
        return false;

      if (Equals(LoginId, obj.LoginId) == false)
        return false;

      if (Equals(Password, obj.Password) == false)
        return false;

      if (Equals(Id, obj.Id) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ Created.GetHashCode();
      result = (result * 397) ^ LastModified.GetHashCode();
      result = (result*397) ^ LoginCount.GetHashCode();
      result = (result * 397) ^ (LoginId != null ? LoginId.GetHashCode() : 0);
      result = (result * 397) ^ (Password != null ? Password.GetHashCode() : 0);
      result = (result * 397) ^ Id.GetHashCode();
      return result;
    }

  }
}
