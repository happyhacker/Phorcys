using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class Country : Entity {
    public virtual string CountryCode { get; set; }
    public virtual string Name { get; set; }
    //public virtual ISet<Contact> Contacts { get; set; }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as Country);
    }

    public virtual bool Equals(Country obj) {
      if (obj == null) return false;

      if (Equals(CountryCode, obj.CountryCode) == false)
        return false;

      if (Equals(Name, obj.Name) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (CountryCode != null ? CountryCode.GetHashCode() : 0);
      result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
      return result;
    }
  }
}