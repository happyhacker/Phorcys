using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class DiveAgency : Entity {
    //public virtual int DiveAgencyId { get; set; }
    public virtual string Notes {get; set; }
    //public virtual ISet<Certification> Certifications { get; set; }
    public virtual Contact Contact { get; set; }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as DiveAgency);
    }

    public virtual bool Equals(DiveAgency obj) {
      if (obj == null) return false;

      /*if (Equals(DiveAgencyId, obj.DiveAgencyId) == false)
        return false;
      */
      if (Equals(Notes, obj.Notes) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      //result = (result * 397) ^ DiveAgencyId.GetHashCode();
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      return result;
    }
  }
}