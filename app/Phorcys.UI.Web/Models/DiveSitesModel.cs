using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Phorcys.UI.Web.Models
{
    public class DiveSitesModel
    {
        public int DiveLocationId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayName("Water Type")]
        public bool IsFreshWater { get; set; }

        public string WaterType { get { return IsFreshWater ? "Fresh" : "Salt"; } }

        [DisplayName("Geocode")]
        public string GeoCode { get; set; }

        public string GeoCodeSafe { get { return GeoCode == null ? "" : GeoCode; } }

        [DisplayName("Notes")]
        public  string Notes { get; set; }

        public  int UserId { get; set; }


    }
}