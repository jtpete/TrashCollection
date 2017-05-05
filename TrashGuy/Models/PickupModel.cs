using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrashGuy.Models
{
    [Table("PickupDates")]
    public class PickupModel
    {
        [Key]
        public int PickupId { get; set; }

        [ForeignKey("AppUser")]
        public string Id { get; set; }

        [Display(Name = "Pickup Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PickupDate { get; set; }

        public virtual ApplicationUser AppUser { get; set; }
    }
}