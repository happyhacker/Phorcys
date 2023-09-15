﻿using System;
using System.Collections.Generic;

namespace AddDivePlan.Domain;

public partial class DiveShop
{
    public int DiveShopId { get; set; }

    public int ContactId { get; set; }

    public string? Notes { get; set; }

    public virtual Contact Contact { get; set; } = null!;

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
