using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class DiverCertification : Entity {
    public virtual string CertificationNum {get; set;}
    public virtual DateTime? Certified {get; set;}
    public virtual DateTime Created {get; set;}
    public virtual DateTime LastModified {get; set;}
    public virtual string Notes {get; set;}
    public virtual Certification Certification {get; set;}
    public virtual Instructor Instructor { get; set; }
    public virtual Diver Diver { get; set; }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as DiverCertification);
    }

    public virtual bool Equals(DiverCertification obj) {
      if (obj == null) return false;

      if (Equals(CertificationNum, obj.CertificationNum) == false)
        return false;

      if (Equals(Certified, obj.Certified) == false)
        return false;

      if (Equals(Created, obj.Created) == false)
        return false;

      if (Equals(LastModified, obj.LastModified) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (CertificationNum != null ? CertificationNum.GetHashCode() : 0);
      result = (result * 397) ^ (Certified != null ? Certified.GetHashCode() : 0);
      result = (result * 397) ^ Created.GetHashCode();
      result = (result * 397) ^ LastModified.GetHashCode();
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      return result;
    }
  }
}