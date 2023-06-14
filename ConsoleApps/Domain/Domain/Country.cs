using System;
using System.Collections.Generic;

namespace AddDivePlan.Domain;

public partial class Country
{
    public string CountryCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
