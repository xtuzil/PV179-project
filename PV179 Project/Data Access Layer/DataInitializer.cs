using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public static class DataInitializer
    {
        //Specifying IDs is mandatory if seeding db through OnModelCreating method
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var user = new User
            {
                FirstName = "Aston",
                LastName = "Matthews",
                Email = "aston.matthews@gmail.com",
                Password = "password",
                Address = new PostalAddress
                {
                    Street = "4627 Dawson Drive",
                    City = "Little Rock",
                    Postcode = "72212",
                    Country = "Arkansas, US"
                },
                PhoneNumber = "501-868-1478",
                ProfilePhoto = new ProfilePhoto
                {
                    Path = "https://randomuser.me/api/portraits/women/82.jpg",
                    ThumbnailPath = "https://randomuser.me/api/portraits/women/82.jpg"
                },
                Role = Role.User
            };

            var admin = new User
            {
                FirstName = "Grace",
                LastName = "Moorman",
                Email = "grace.moorman@gmail.com",
                Password = "password",
                Address = new PostalAddress
                {
                    Street = "4366 Hickory Lane",
                    City = "Washington",
                    Postcode = "20005",
                    Country = "Washington DC, US"
                },
                PhoneNumber = "202-523-4209",
                ProfilePhoto = new ProfilePhoto
                {
                    Path = "https://randomuser.me/api/portraits/women/57.jpg",
                    ThumbnailPath = "https://randomuser.me/api/portraits/women/57.jpg"
                },
                Role = Role.Admin
            };


            var species = new Species
            {
                Name = "Golden Barrel",
                LatinName = "Echinocactus Grusonii",
                SuggestedBy = user,
                ConfirmedBy = admin
            };

            var cactus = new Cactus
            {
                Species = species,
                Owner = user,
                Photos = new List<CactusPhoto>() { new CactusPhoto {
                    Path = "https://images.unsplash.com/photo-1459411552884-841db9b3cc2a",
                    ThumbnailPath = "https://images.unsplash.com/photo-1459411552884-841db9b3cc2a",
                    Uploader = user
                } }
            };

            modelBuilder.Entity<User>().HasData(user, admin);
            modelBuilder.Entity<Species>().HasData(species);
            modelBuilder.Entity<Cactus>().HasData(cactus);
        }
    }
}
