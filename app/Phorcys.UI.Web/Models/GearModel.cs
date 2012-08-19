using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Phorcys.UI.Web.Models {
  public class GearModel {

    public int GearId { get; set; }

    [Required(ErrorMessage = "*")]
    public string Title { get; set; }

    [DisplayName("Serial #")]
    public string Sn { get; set; }

    [DisplayName("Retail Price")]
    public decimal RetailPrice { get; set; }

    public decimal Paid { get; set; }

    public float Weight { get; set; }

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