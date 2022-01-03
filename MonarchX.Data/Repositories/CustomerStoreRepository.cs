using System;
using System.Collections.Generic;
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

    }}


