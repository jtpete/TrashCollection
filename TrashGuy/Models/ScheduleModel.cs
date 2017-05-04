using IdentitySample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashGuy.Models
{
    [Table("Schedule")]
    public class ScheduleModel
    {
        [Key, ForeignKey("ApplicationUser")]
        public string Id { get; set;  }
        
        [Display(Name = "Default Pickup Day")]
        public string DefaultPickupDay { get; set; }
        [Display(Name = "No Pickups")]

        public ICollection<BlackoutDates> BlackoutDates { get; set; }
        [Display(Name = "Extra Pickups")]

        public ICollection<SpecialPickupDates> SpecialPickupDates { get; set;}

        public virtual ApplicationUser ApplicationUser { get; set; }


    }

    public class BlackoutDates
    {
        [Key]
        public int BlackoutId { get; set; }

        public string ScheduleId { get; set; }
        [ForeignKey("ScheduleId")]
        public virtual ScheduleModel Schedule { get; set; }
        public DateTime BlackoutDate { get; set; }
    }

    public class SpecialPickupDates
    {
        [Key]
        public int SpecialPickupId { get; set; }

        public string ScheduleId { get; set; }
        [ForeignKey("ScheduleId")]
        public virtual ScheduleModel Schedule { get; set; }
        public DateTime SpecialPickupDate { get; set; }
    }

}