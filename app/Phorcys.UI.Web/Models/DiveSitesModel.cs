using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Phorcys.Core;

namespace Phorcys.UI.Web.Models
{
    public class DiveSitesModel
    {
        public DiveSitesModel() {
            this.DiveSite = new DiveSite();
        }

        public int DiveLocationId { get; set; }

        [Required]
        public string Title {
            get { return DiveSite.Title == null ? "" : DiveSite.Title; }
            set { DiveSite.Title = value; } 
        }

        [Required]
        [DisplayName("Water Type")]
        public bool IsFreshWater {
            get { return DiveSite.IsFreshWater; }
            set { DiveSite.IsFreshWater = value; } 
        }

        public string WaterType { get { return IsFreshWater ? "Fresh" : "Salt"; } }

        [DisplayName("Geocode")]
        public string GeoCode {
            get { return GeoCodeSafe; }
            set { DiveSite.GeoCode = value; } 
        }

        public string GeoCodeSafe { get { return DiveSite.GeoCode == null ? "" : DiveSite.GeoCode; } }

        [DisplayName("Notes")]
        public  string Notes {
            get { return DiveSite.Notes; }
            set { DiveSite.Notes = value; } 
        }

        
        [Range(1f,36198f)]
        [DisplayName("Max Depth")]
        public float MaxDepth
        {
            get { return DiveSite.MaxDepth; }
            set { DiveSite.MaxDepth = value; }
        }

        public  int UserId {
            get { return DiveSite.UserId; }
            set { DiveSite.UserId = value; } 
        }

        public DiveSite DiveSite { get; internal set; }
        //public IList<DiveLocation> DiveLocationsList { get; set; }
        public IList<SelectListItem> DiveLocationsListItems { get; set; }

    }
}