using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phorcys.UI.Web.Models {
  public class GearModel {

    public int GearId { get; set; }

    [Required]
    public string Title { get; set; }

    public string Sn { get; set; }

    [Range(0,int.MaxValue)]
    public decimal RetailPrice { get; set; }

    [Range(0,int.MaxValue)]
    public decimal Paid { get; set; }

    [Range(0,int.MaxValue)]
    public double Weight { get; set; }

    public DateTime Acquired { get; set;  }

    public string Notes { get; set; }

    //additional tank data
    [Range(0,5000)]
    public int TankVolume { get; set; }

    [Range(0,5000)]
    public int WorkingPressure { get; set; }

    [Range(0,12)]
    public int ManufacturedMonth { get; set; }

    [Range(0,99)]
    public int ManufacturedYear { get; set; }
  }
}