using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  [Serializable]
  public class Diver : Entity {
    /*public virtual int DiverId { get; set; }*/
    public virtual string Notes { get; set; }
    public virtual double? RestingSacRate { get; set; }
    public virtual double? WorkingSacRate { get; set; }
    public virtual DateTime Created { get; set; }
    public virtual DateTime LastModifed { get; set; }
    public virtual Contact Contact { get; set; }
    public virtual ISet<Dive> Dives { get; set; }
    public virtual ISet<DiverCertification> DiverCertifications {  get; set; }
    public virtual ISet<DiverQualification> DiverQualifications { get; set; }
    public virtual ISet<DivePlan> DivePlans { get; set; }
    public virtual ISet<Gear> Gears { get; set; }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as Diver);
    }

    public virtual bool Equals(Diver obj) {
      if (obj == null) return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(RestingSacRate, obj.RestingSacRate) == false)
        return false;

      if (Equals(WorkingSacRate, obj.WorkingSacRate) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

       result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ (RestingSacRate != null ? RestingSacRate.GetHashCode() : 0);
      result = (result * 397) ^ (WorkingSacRate != null ? WorkingSacRate.GetHashCode() : 0);
      return result;
    }
  }
}