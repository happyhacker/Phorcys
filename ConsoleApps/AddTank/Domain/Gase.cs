using System;
using System.Collections.Generic;

namespace AddDivePlan.Domain;

public partial class Gase
{
    public int GasId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<GasMix> GasMixes { get; set; } = new List<GasMix>();
}
