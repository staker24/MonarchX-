using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MonarchX.Data.Models;

namespace MonarchX.Data
{
    public class CustomerStoreRepository : Repository<Customer>, ICustomerStoreRepository
    {
        public CustomerStoreDbContext Db => DbContext as CustomerStoreDbContext;

        public CustomerStoreRepository(CustomerStoreDbContext context) : base(context)
        {

        }

        //Saves new customer into the database 
        public Customer AddNewCustomer(string firstName, string lastName, string address, string? address2, PhoneAttribute phone, string email){

            Customer newCustomer = new Customer(){

                FirstName = firstName,
                LastName = lastName,
                StreetAddress = address,
                //Nullable not required
                Address2 = address2,
                PhoneNumber = phone,
                Email = email

            };

            Db.Customers.Add(newCustomer);
            var recordsAffected = Db.SaveChanges();
            return newCustomer;
        }

    }}


