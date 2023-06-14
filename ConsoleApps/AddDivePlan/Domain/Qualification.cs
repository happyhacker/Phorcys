﻿using System;
using System.Collections.Generic;

namespace AddDivePlan.Domain;

public partial class Qualification
{
    public int QualificationId { get; set; }

    public string Title { get; set; } = null!;

    public string? Notes { get; set; }

    public int UserId { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastModified { get; set; }

    public virtual ICollection<DiverQualification> DiverQualifications { get; set; } = new List<DiverQualification>();

    public virtual User User { get; set; } = null!;
}
