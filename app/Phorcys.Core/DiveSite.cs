using System;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  public class DiveSite : Entity, IDiveSite
  {
    public DiveSite() { }

   [DomainSignature]
   public virtual int DiveLocationId { get; set; }

    public virtual string Title { get; set; }

    public virtual bool IsFreshWater { get; set; }

    public virtual string WaterType { get { return IsFreshWater ? "Fresh" : "Salt"; } }

    [DomainSignature]
    public virtual string GeoCode { get; set; }

    public virtual string GeoCodeSafe { get { return GeoCode == null ? "" : GeoCode; } }

    [DomainSignature]
    public virtual string Notes { get; set; }

    [DomainSignature]
    public virtual int UserId { get; set; }

    public virtual DateTime Created { get; set; }

    public virtual DateTime LastModified {  get; set; }

    public virtual float MaxDepth { get; set; }

    public virtual DiveLocation DiveLocation {
      get;
      set;
    }

    public virtual ISet<DiveSiteUrl> DiveSiteUrls {
      get;
      set;
    }
    public virtual User User {
      get;
      set;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as DiveSite);
    }

    public virtual bool Equals(DiveSite obj) {
      if (obj == null) return false;

      if (Equals(Created, obj.Created) == false)
        return false;

      if (Equals(Id, obj.Id) == false)
        return false;

      if (Equals(GeoCode, obj.GeoCode) == false)
        return false;

      if (Equals(IsFreshWater, obj.IsFreshWater) == false)
        return false;

      if (Equals(LastModified, obj.LastModified) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(Title, obj.Title) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (Created.GetHashCode());
      result = (result * 397) ^ (Id.GetHashCode());
      result = (result * 397) ^ (GeoCode != null ? GeoCode.GetHashCode() : 0);
      result = (result * 397) ^ (IsFreshWater.GetHashCode());
      result = (result * 397) ^ (LastModified.GetHashCode());
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
      return result;
    }
  }
}
