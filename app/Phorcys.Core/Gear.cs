using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
 
  public class Gear : Entity {
    public Gear() { }

    public virtual DateTime? Acquired {get;set;}
    //public virtual int GearId {get;set;}
    public virtual DateTime? NoLongerUse {get;set;}
    public virtual string Notes {get;set;}
    public virtual decimal Paid {get;set;}
    public virtual decimal RetailPrice {get;set;}
    public virtual string Sn {get;set;}
    public virtual string Title {get;set;}
    public virtual float Weight {get;set;}
    public virtual DateTime Created { get; set; }
    public virtual DateTime? LastModified { get; set; }
    public virtual Contact Contact {get;set;}
    public virtual ISet<Dive> Dives {get;set;}
    public virtual ISet<Diver> Divers {get;set;}
    public virtual ISet<GearServiceEvent> GearServiceEvents {get;set;}
    public virtual ISet<InsurancePolicy> InsurancePolicies {get;set;}
    public virtual Manufacturer Manufacturer {get;set;}
    public virtual ISet<ServiceSchedule> ServiceSchedules {get;set;}
    public virtual SoldGear SoldGear {get;set;}
    public virtual Tank Tank {get;set;}
    public virtual User User {get;set;}

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as Gear);
    }

    public virtual bool Equals(Gear obj) {
      if (obj == null) return false;

      if (Equals(Acquired, obj.Acquired) == false)
        return false;

      //if (Equals(GearId, obj.GearId) == false)
       // return false;

      if (Equals(NoLongerUse, obj.NoLongerUse) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(Paid, obj.Paid) == false)
        return false;

      if (Equals(RetailPrice, obj.RetailPrice) == false)
        return false;

      if (Equals(Sn, obj.Sn) == false)
        return false;

      if (Equals(Title, obj.Title) == false)
        return false;

      if (Equals(Weight, obj.Weight) == false)
        return false;

      if (Equals(Created, obj.Created) == false)
        return false;

      if (Equals(Created, obj.Created) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (Acquired != null ? Acquired.GetHashCode() : 0);
      result = (result * 397) ^ (NoLongerUse != null ? NoLongerUse.GetHashCode() : 0);
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ (Paid != null ? Paid.GetHashCode() : 0);
      result = (result * 397) ^ (RetailPrice != null ? RetailPrice.GetHashCode() : 0);
      result = (result * 397) ^ (Sn != null ? Sn.GetHashCode() : 0);
      result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
      result = (result * 397) ^ (Weight != null ? Weight.GetHashCode() : 0);
      return result;
    }
  }
}