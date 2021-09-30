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
            var adminAddress = new PostalAddress
            {
                Id = 1,
                Street = "4366 Hickory Lane",
                City = "Washington",
                Postcode = "20005",
                Country = "Washington DC, US"
            };

            var admin = new User
            {
                Id = 1,
                FirstName = "Grace",
                LastName = "Moorman",
                Email = "grace.moorman@gmail.com",
                Password = "password",
                AddressId = adminAddress.Id,
                PhoneNumber = "202-523-4209",
                Role = Role.Admin
            };

            var adminProfilePhoto = new ProfilePhoto
            {
                Id = 1,
                Path = "https://randomuser.me/api/portraits/women/57.jpg",
                ThumbnailPath = "https://randomuser.me/api/portraits/women/57.jpg",
                UserId = admin.Id
            };

            var userAddress = new PostalAddress
            {
                Id = 2,
                Street = "4627 Dawson Drive",
                City = "Little Rock",
                Postcode = "72212",
                Country = "Arkansas, US"
            };

            var user = new User
            {
                Id = 2,
                FirstName = "Aston",
                LastName = "Matthews",
                Email = "aston.matthews@gmail.com",
                Password = "password",
                AddressId = userAddress.Id,
                PhoneNumber = "501-868-1478",
                Role = Role.User
            };

            var userProfilePhoto = new ProfilePhoto
            {
                Id = 2,
                Path = "https://randomuser.me/api/portraits/women/82.jpg",
                ThumbnailPath = "https://randomuser.me/api/portraits/women/82.jpg",
                UserId = user.Id
            };

            var userAddress2 = new PostalAddress
            {
                Id = 3,
                Street = "2151 Woodstock Drive",
                City = "Arcadia",
                Postcode = "91006",
                Country = "California, US"
            };

            var user2 = new User
            {
                Id = 3,
                FirstName = "Robert",
                LastName = "Walker",
                Email = "robert.walker@gmail.com",
                Password = "password",
                AddressId = userAddress2.Id,
                PhoneNumber = "626-445-3370",
                Role = Role.User
            };

            var userProfilePhoto2 = new ProfilePhoto
            {
                Id = 3,
                Path = "https://randomuser.me/api/portraits/men/44.jpg",
                ThumbnailPath = "https://randomuser.me/api/portraits/men/44.jpg",
                UserId = user2.Id
            };

            var userAddress3 = new PostalAddress
            {
                Id = 4,
                Street = "1132 Morris Street",
                City = "Mount Plesant",
                Postcode = "48804",
                Country = "Michigan, US"
            };

            var user3 = new User
            {
                Id = 4,
                FirstName = "Lee",
                LastName = "Kelly",
                Email = "lee.kelly@gmail.com",
                Password = "password",
                AddressId = userAddress3.Id,
                PhoneNumber = "830-261-8696",
                Role = Role.User
            };

            var userProfilePhoto3 = new ProfilePhoto
            {
                Id = 4,
                Path = "https://randomuser.me/api/portraits/men/18.jpg",
                ThumbnailPath = "https://randomuser.me/api/portraits/men/18.jpg",
                UserId = user3.Id
            };

            var genus = new Genus
            {
                Id = 1,
                Name = "Echinocactus"
            };

            var genus2 = new Genus
            {
                Id = 2,
                Name = "Parodia"
            };

            var species = new Species
            {
                Id = 1,
                Name = "Golden Barrel",
                LatinName = "Echinocactus grusonii",
                GenusId = genus.Id,
                SuggestedById = user.Id,
                ConfirmedById = admin.Id
            };

            var species2 = new Species
            {
                Id = 2,
                Name = "Silver Ball",
                LatinName = "Parodia scopa",
                GenusId = genus2.Id,
                SuggestedById = user3.Id,
                ConfirmedById = admin.Id
            };

            var cactus = new Cactus
            {
                Id = 1,
                SpeciesId = species.Id,
                OwnerId = user.Id,
                
            };

            var cactus2 = new Cactus
            {
                Id = 2,
                SpeciesId = species2.Id,
                OwnerId = user3.Id
            };

            var cactusPhoto = new CactusPhoto
            {
                Id = 1,
                Path = "https://images.unsplash.com/photo-1459411552884-841db9b3cc2a",
                ThumbnailPath = "https://images.unsplash.com/photo-1459411552884-841db9b3cc2a",
                UploaderId = user.Id,
                CactusId = cactus.Id
            };

            var cactusPhoto2 = new CactusPhoto
            {
                Id = 2,
                Path = "https://images.unsplash.com/photo-1459411552884-841db9b3cc2a",
                ThumbnailPath = "https://images.unsplash.com/photo-1459411552884-841db9b3cc2a",
                UploaderId = user3.Id,
                CactusId = cactus2.Id
            };

            modelBuilder.Entity<PostalAddress>().HasData(adminAddress, userAddress, userAddress2, userAddress3);
            modelBuilder.Entity<User>().HasData(admin, user, user2, user3);
            modelBuilder.Entity<ProfilePhoto>().HasData(adminProfilePhoto, userProfilePhoto, userProfilePhoto2, userProfilePhoto3);
            modelBuilder.Entity<Genus>().HasData(genus, genus2);
            modelBuilder.Entity<Species>().HasData(species, species2);
            modelBuilder.Entity<Cactus>().HasData(cactus, cactus2);
            modelBuilder.Entity<CactusPhoto>().HasData(cactusPhoto, cactusPhoto2);

            var offerRejected = new MyBaseOffer
            {
                Id = 1,
                SenderId = user2.Id,
                ReceiverId = user3.Id,
                Response = OfferResponse.Declined
            };

            modelBuilder.Entity<MyBaseOffer>().HasData(offerRejected);
            modelBuilder.Entity<CactusOffered>().HasData(new CactusOffered { CactusId = cactus2.Id, OfferId = offerRejected.Id });
            modelBuilder.Entity<CactusRequested>().HasData(new CactusRequested { CactusId = cactus.Id, OfferId = offerRejected.Id });

            var offerAccepted = new MyBaseOffer
            {
                Id = 2,
                PreviousOfferId = offerRejected.Id,
                SenderId = user3.Id,
                ReceiverId = user2.Id,
                Response = OfferResponse.Accepted,
                OfferedMoney = 50
            };

            modelBuilder.Entity<MyBaseOffer>().HasData(offerAccepted);
            modelBuilder.Entity<CactusRequested>().HasData(new CactusRequested { CactusId = cactus2.Id, OfferId = offerAccepted.Id });

            var shipment = new Shipment
            {
                OfferId = offerAccepted.Id,
                Status = ShipmentStatus.Delivered,
                TrackingCode = "LT1660944412375",
                DateShipped = new System.DateTime(2021, 9, 25),
                DateConfirmed = new System.DateTime(2021, 9, 28)
            };

            modelBuilder.Entity<Shipment>().HasData(shipment);
        }
    }
}
