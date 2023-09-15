﻿using System;
using System.Collections.Generic;

namespace AddDivePlan.Domain;

public partial class Tank
{
    public int GearId { get; set; }

    public int? Volume { get; set; }

    public int? WorkingPressure { get; set; }

    public byte? ManufacturedMonth { get; set; }

    public byte? ManufacturedYear { get; set; }

    public virtual Gear Gear { get; set; } = null!;

    public virtual ICollection<TanksOnDive> TanksOnDives { get; set; } = new List<TanksOnDive>();
}
