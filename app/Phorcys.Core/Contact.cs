using System;
using Iesi.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Phorcys.Core {
  public class Contact : Entity {
    public Contact() { }

    //[DomainSignature]
    //public virtual int ContactId { get; set; }
    public virtual string Address1 { get; set; }
    public virtual string Address2 { get; set; }
    public virtual DateTime? Birthday { get; set; }
    public virtual string CellPhone { get; set; }
    public virtual string City { get; set; }
    public virtual string Company { get; set; }
    public virtual DateTime Created { get; set; }
    public virtual string Email { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string Gender { get; set; }
    public virtual string HomePhone { get; set; }
    public virtual DateTime LastModified { get; set; }
    public virtual string LastName { get; set; }
    public virtual string Notes { get; set; }
    public virtual string PostalCode { get; set; }
    public virtual string State { get; set; }
    public virtual string WorkPhone { get; set; }
    public virtual Country Country { get; set; }
    public virtual ISet<DiveAgency> DiveAgencies { get; set; }
    public virtual ISet<DiveLocation> DiveLocations { get; set; }
    public virtual ISet<DiverCertification> DiverCertifications { get; set; }
    public virtual ISet<Diver> Divers { get; set; }
    public virtual ISet<DiveShop> DiveShops { get; set; }
    public virtual ISet<Gear> Gears { get; set; }
    public virtual ISet<Instructor> Instructors { get; set; }
    public virtual Instructor Instructor { get; set; }
    public virtual ISet<InsurancePolicy> InsurancePolicies { get; set; }
    public virtual ISet<Manufacturer> Manufacturers { get; set; }
    public virtual ISet<SoldGear> SoldGears { get; set; }
    public virtual User User { get; set; }
    //public virtual Diver Diver { get; set; }
    public virtual ISet<User> Users { get; set; }

    public override bool Equals(object obj) {
      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as Contact);
    }

    public virtual bool Equals(Contact obj) {
      if (obj == null) return false;

      if (Equals(Address1, obj.Address1) == false)
        return false;

      if (Equals(Address2, obj.Address2) == false)
        return false;

      if (Equals(Birthday, obj.Birthday) == false)
        return false;

      if (Equals(CellPhone, obj.CellPhone) == false)
        return false;

      if (Equals(City, obj.City) == false)
        return false;

      if (Equals(Company, obj.Company) == false)
        return false;

      if (Equals(Id, obj.Id) == false)
        return false;

      if (Equals(Created, obj.Created) == false)
        return false;

      if (Equals(Email, obj.Email) == false)
        return false;

      if (Equals(FirstName, obj.FirstName) == false)
        return false;

      if (Equals(Gender, obj.Gender) == false)
        return false;

      if (Equals(HomePhone, obj.HomePhone) == false)
        return false;

      if (Equals(LastModified, obj.LastModified) == false)
        return false;

      if (Equals(LastName, obj.LastName) == false)
        return false;

      if (Equals(Notes, obj.Notes) == false)
        return false;

      if (Equals(PostalCode, obj.PostalCode) == false)
        return false;

      if (Equals(State, obj.State) == false)
        return false;

      if (Equals(WorkPhone, obj.WorkPhone) == false)
        return false;

      return true;
    }

    public override int GetHashCode() {
      int result = 1;

      result = (result * 397) ^ (Address1 != null ? Address1.GetHashCode() : 0);
      result = (result * 397) ^ (Address2 != null ? Address2.GetHashCode() : 0);
      result = (result * 397) ^ (Birthday != null ? Birthday.GetHashCode() : 0);
      result = (result * 397) ^ (CellPhone != null ? CellPhone.GetHashCode() : 0);
      result = (result * 397) ^ (City != null ? City.GetHashCode() : 0);
      result = (result * 397) ^ (Company != null ? Company.GetHashCode() : 0);
      result = (result * 397) ^ Id.GetHashCode();
      result = (result * 397) ^ Created.GetHashCode();
      result = (result * 397) ^ (Email != null ? Email.GetHashCode() : 0);
      result = (result * 397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
      result = (result * 397) ^ (Gender != null ? Gender.GetHashCode() : 0);
      result = (result * 397) ^ (HomePhone != null ? HomePhone.GetHashCode() : 0);
      result = (result * 397) ^ LastModified.GetHashCode();
      result = (result * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
      result = (result * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
      result = (result * 397) ^ (PostalCode != null ? PostalCode.GetHashCode() : 0);
      result = (result * 397) ^ (State != null ? State.GetHashCode() : 0);
      result = (result * 397) ^ (WorkPhone != null ? WorkPhone.GetHashCode() : 0);
      return result;
    }
  }
}