using AutoMapper;
using F2022A1THH.Data;
using F2022A1THH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// ************************************************************************************
// WEB524 Project Template V1 == 2227-c670383a-2f1b-46f6-b27f-e86bc5a8f8d6
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace F2022A1THH.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();
                cfg.CreateMap<Venue, VenueBaseViewModel>();

                cfg.CreateMap<VenueAddViewModel, Venue>();

                cfg.CreateMap<VenueBaseViewModel, VenueEditFormViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }


        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().

        public IEnumerable<VenueBaseViewModel> VenueGetAll()
        {
            var allVenues = from v in ds.Venues
                            orderby v.Company
                            select v;

            return mapper.Map<IEnumerable<Venue>, IEnumerable<VenueBaseViewModel>>(allVenues);
        }

        public VenueBaseViewModel VenueGetById(int id)
        {
            var venue = ds.Venues.Find(id);

            return venue == null ? null : mapper.Map<Venue, VenueBaseViewModel>(venue);
        }

        public VenueBaseViewModel VenueAdd(VenueAddViewModel newVenue)
        {
            var addedVenue = ds.Venues.Add(mapper.Map<VenueAddViewModel, Venue>(newVenue));
            ds.SaveChanges();

            return addedVenue == null ? null : mapper.Map<Venue, VenueBaseViewModel>(addedVenue);
        }

        // edit
        public VenueBaseViewModel VenueEditContactInfo(VenueEditViewModel Venue)
        {
            // Attempt to fetch the object. 
            var obj = ds.Venues.Find(Venue.VenueId);

            if (obj == null)
            {
                // Venue was not found, return null. 
                return null;
            }
            else
            {
                // Venue was found.  Update the entity object 
                // with the incoming values then save the changes. 
                ds.Entry(obj).CurrentValues.SetValues(Venue);
                ds.SaveChanges();

                // Prepare and return the object. 
                return mapper.Map<Venue, VenueBaseViewModel>(obj);
            }
        }

        // delete
        public bool VenueDelete(int id)
        {
            // Attempt to fetch the object to be deleted 
            var venueToDelete = ds.Venues.Find(id);

            if (venueToDelete == null)
                return false;
            else
            {
                // Remove the object 
                ds.Venues.Remove(venueToDelete);
                ds.SaveChanges();

                return true;
            }
        }
    }
}