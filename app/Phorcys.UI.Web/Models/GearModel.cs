using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phorcys.UI.Web.Models {
  public class GearModel {

    public int GearId { get; set; }

    [DisplayName("Date Acquired")]
    public string Title { get; set; }

    [DisplayName("Serial #")]
    public string Sn { get; set; }

    [DisplayName("Retail Price")]
    public decimal RetailPrice { get; set; }

    public decimal Paid { get; set; }

    public float Weight { get; set; }

    [DataType(DataType.Date)]
    public DateTime Acquired { get; set;  }

    public string Notes { get; set; }

    //additional tank data
    /*
    [Range(0,5000)]
    public int TankVolume { get; set; }

    [Range(0,5000)]
    public int WorkingPressure { get; set; }

    [Range(0,12)]
    public int ManufacturedMonth { get; set; }

    [Range(0,99)]
    public int ManufacturedYear { get; set; }
     */
  }
}