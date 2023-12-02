using Phorcys.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Phorcys.UI.Web.Models
{
    public class DiveModel
    {

        public int Id { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string DiveSite { get; set; }
        public IList<SelectListItem> DivePlanList { get; set; }
        public int DivePlanId { get; set; }
        public int DiveNumber { get; set; }        
        public DateTime DescentTime { get; set; }
        public int Minutes { get; set; }
        public int AvgDepth { get; set; }
        public int MaxDepth { get; set; }
        public int Temperature { get; set; }
        public int AdditionalWeight { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}