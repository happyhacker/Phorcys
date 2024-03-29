using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class DiverQualification : Entity {
    public virtual DateTime Created {
      get;
      set;
    }
    public virtual int DiverId {
      get;
      set;
    }
    public virtual DateTime LastModified {
      get;
      set;
    }
    public virtual string Notes {
      get;
      set;
    }
    public virtual int QualificationId {
      get;
      set;
    }
    public virtual DateTime? Qualified {
      get;
      set;
    }
    public virtual Diver Diver {
      get;
      set;
    }
    public virtual Qualification Qualification {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as DiverQualification);
    }

    public virtual bool Equals(DiverQualification obj) {
      if (obj == null) return false;

      if (Equals(Created, obj.Created) == false)
        return false;

      if (Equals(DiverId, obj.DiverId) == false)
        return false;

      if (Equals(LastModified, obj.LastModified) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(QualificationId, obj.QualificationId) == false)
        return false;

      if (Equals(Qualified, obj.Qualified) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ Created.GetHashCode();
      result = (result * 397) ^ DiverId.GetHashCode();
      result = (result * 397) ^ LastModified.GetHashCode();
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ QualificationId.GetHashCode();
      result = (result * 397) ^ (Qualified != null ? Qualified.GetHashCode() : 0);
      return result;
    }
  }
}