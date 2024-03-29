using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core
{
    [Serializable]
    public class Dive : Entity
    {
        public virtual int DiveId { get; set;}
        public virtual string Title { get; set; }
        public virtual int? AdditionalWeight {get; set;}
        public virtual int? AvgDepth { get;set;}
        public virtual DateTime Created { get; set;}
        public virtual DateTime? DescentTime { get; set;}
        public virtual int DiveNumber { get; set; }
        public virtual DateTime LastModified { get;set; }
        public virtual int? MaxDepth  {get;set;}
        public virtual int? Minutes { get;set; }
        public virtual string Notes { get;set;}
        public virtual int? Temperature { get;set; }
        public virtual DivePlan DivePlan  { get;set;}
        //public virtual ISet<DiveUrl> DiveUrls { get; set;}
        public virtual Diver Diver { get;set; }
        //public virtual ISet<Gear> Gears {get;set;}
        public virtual Role Role {get; set; }
        //public virtual ISet<TanksOnDive> TanksOnDives { get;set;}
        public virtual User User { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as Dive);
        }

        public virtual bool Equals(Dive obj)
        {
            if (obj == null) return false;

            if (Equals(AdditionalWeight, obj.AdditionalWeight) == false)
                return false;

            if (Equals(AvgDepth, obj.AvgDepth) == false)
                return false;

            if (Equals(Created, obj.Created) == false)
                return false;

            if (Equals(DescentTime, obj.DescentTime) == false)
                return false;

            if (Equals(DiveId, obj.DiveId) == false)
                return false;

            if (Equals(DiveNumber, obj.DiveNumber) == false)
                return false;

            if (Equals(LastModified, obj.LastModified) == false)
                return false;

            if (Equals(MaxDepth, obj.MaxDepth) == false)
                return false;

            if (Equals(Minutes, obj.Minutes) == false)
                return false;

            if (Equals(Notes, obj.Notes) == false)
                return false;

            if (Equals(Temperature, obj.Temperature) == false)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            int result = 1;

            result = (result * 397) ^ (AdditionalWeight != null ? AdditionalWeight.GetHashCode() : 0);
            result = (result * 397) ^ (AvgDepth != null ? AvgDepth.GetHashCode() : 0);
            result = (result * 397) ^ Created.GetHashCode();
            result = (result * 397) ^ (DescentTime != null ? DescentTime.GetHashCode() : 0);
            result = (result * 397) ^ DiveId.GetHashCode();
            result = (result * 397) ^ DiveNumber.GetHashCode();
            result = (result * 397) ^ LastModified.GetHashCode();
            result = (result * 397) ^ (MaxDepth != null ? MaxDepth.GetHashCode() : 0);
            result = (result * 397) ^ (Minutes != null ? Minutes.GetHashCode() : 0);
            result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
            result = (result * 397) ^ (Temperature != null ? Temperature.GetHashCode() : 0);
            return result;
        }
    }
}