using CactusDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CactusDAL
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
                Id = 3,
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
                Role = Role.User,
                AccountBalance = 50
            };

            var userProfilePhoto = new ProfilePhoto
            {
                Id = 4,
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
                Id = 5,
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
                Id = 6,
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
                Approved = true
            };

            var species2 = new Species
            {
                Id = 2,
                Name = "Silver Ball",
                LatinName = "Parodia scopa",
                GenusId = genus2.Id,
                Approved = true
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
                CactusId = cactus.Id
            };

            var cactusPhoto2 = new CactusPhoto
            {
                Id = 2,
                Path = "https://images.unsplash.com/photo-1459411552884-841db9b3cc2a",
                ThumbnailPath = "https://images.unsplash.com/photo-1459411552884-841db9b3cc2a",
                CactusId = cactus2.Id
            };

            modelBuilder.Entity<PostalAddress>().HasData(adminAddress, userAddress, userAddress2, userAddress3);
            modelBuilder.Entity<User>().HasData(admin, user, user2, user3);
            modelBuilder.Entity<ProfilePhoto>().HasData(adminProfilePhoto, userProfilePhoto, userProfilePhoto2, userProfilePhoto3);
            modelBuilder.Entity<Genus>().HasData(genus, genus2);
            modelBuilder.Entity<Species>().HasData(species, species2);
            modelBuilder.Entity<Cactus>().HasData(cactus, cactus2);
            modelBuilder.Entity<CactusPhoto>().HasData(cactusPhoto, cactusPhoto2);


            var offerRejected = new Offer
            {
                Id = 1,
                SenderId = user2.Id,
                ReceiverId = user3.Id,
                Response = OfferStatus.Declined
            };

            modelBuilder.Entity<Offer>().HasData(offerRejected);
            modelBuilder.Entity<CactusOffer>().HasData(new CactusOffer { Id = 1, CactusId = cactus2.Id, OfferId = offerRejected.Id });
            modelBuilder.Entity<CactusOffer>().HasData(new CactusOffer { Id = 2, CactusId = cactus.Id, OfferId = offerRejected.Id });


            var comment = new Comment
            {
                Id = 1,
                AuthorId = 1,
                Text = "A random comment",
                OfferId = 1,
            };


            modelBuilder.Entity<Comment>().HasData(comment);

            var offerAccepted = new Offer
            {
                Id = 2,
                PreviousOfferId = offerRejected.Id,
                SenderId = user3.Id,
                ReceiverId = user2.Id,
                Response = OfferStatus.Accepted,
                OfferedMoney = 50
            };

            modelBuilder.Entity<Offer>().HasData(offerAccepted);
            modelBuilder.Entity<CactusOffer>().HasData(new CactusOffer { Id = 3, CactusId = cactus2.Id, OfferId = offerAccepted.Id });

            var moneyTransfer = new Transfer
            {
                Id = 1,
                OfferId = offerAccepted.Id,
                ReceiverReviewId = 1,
                SenderReviewId = 2,
            };

            modelBuilder.Entity<Transfer>().HasData(moneyTransfer);

            var receiveReview = new Review
            {
                Id = 1,
                AuthorId = 1,
                UserId = 2,
                TransferId = 1,
                Score = 5,
                Text = "Everything ok"
            };

            var sendReview = new Review
            {
                Id = 2,
                AuthorId = 2,
                UserId = 1,
                TransferId = 1,
                Score = 2,
                Text = "Cactuses are in bad condition"
            };

            modelBuilder.Entity<Review>().HasData(receiveReview, sendReview);

        }
    }
}
