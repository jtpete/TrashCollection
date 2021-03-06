﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;


namespace TrashGuy.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public DateTime StartDate { get; set; }

        [ForeignKey("Schedule")]
        public int ScheduleId;
        public virtual ScheduleModel Schedule { get; set; }

        public ICollection<PickupModel> Pickups { get; set; }

        [Display(Name = "Address")]
        public string DisplayAddress
        {
            get
            {
                string displayAddress = string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
                string displayCity = string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
                string displayState = string.IsNullOrWhiteSpace(this.State) ? "" : this.State;
                string displayZipCode = string.IsNullOrWhiteSpace(this.ZipCode) ? "" : this.ZipCode;

                return string.Format($"{displayAddress} {displayCity} {displayState} {displayZipCode}");

            }
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
        public string Description { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<TrashGuy.Models.ScheduleModel> ScheduleModels { get; set; }

        public System.Data.Entity.DbSet<TrashGuy.Models.PickupModel> PickupModels { get; set; }

     

    }
}