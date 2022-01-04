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
        public async Task<int> AddNewCustomer(string firstName, string lastName, string address, string? address2, int phone, string email){

            Customer newCustomer = new Customer(){

                FirstName = firstName,
                LastName = lastName,
                StreetAddress = address,
                //Nullable not required
                Address2 = address2,
                PhoneNumber = phone,
                Email = email

            };

            await Db.Customers.AddAsync(newCustomer);
            return await Db.SaveChangesAsync();
        }
        //Search customer with customer ID
        public async Task<Customer> GetCustomer(int id){

            Customer foundCustomer = await Db.Customers
            .Where(x=>x.CustomerId.Equals(id))
            .FirstOrDefaultAsync();

            return foundCustomer;
        }

        //Get customer by invoice number
        public async Task<Customer> GetCustomerByInvoice(string invoice){

            Sale foundSale =await Db.Sales
            .Where(x=>x.InvoiceId.Equals(invoice))
            .FirstOrDefaultAsync();

            Customer soldToCustomer = foundSale.Customer;
            return soldToCustomer;

        }

        //Get the invoice
        public async Task<Sale> GetSale(string invoice){

            Sale foundSale =await Db.Sales 
            .Where(x=>x.InvoiceId.Equals(invoice))
            .FirstOrDefaultAsync();

            return foundSale;

        }

        //Store a new sale
        public async Task<int> AddNewSale(Customer customer, decimal totalSale, List<Product> items, string notes){

            Sale newSale = new Sale(){

                Customer = customer,
                TotalSale = totalSale,
                Items = items,
                Notes = notes

            };

            await Db.Sales.AddAsync(newSale);
            return await Db.SaveChangesAsync();

        }

        //Store a new Product
        public async Task<int> AddNewProduct(string name, decimal price, int stock, string ?itemNumber){

            Product newProduct = new Product(){

                ItemName = name,
                Price = price,
                Stock = stock,
                //This item can be manually entered if desired (IMEI/or any business item classification)
                ItemNumber = itemNumber

            };

            await Db.Products.AddAsync(newProduct);
            return await Db.SaveChangesAsync();

        }

        public async Task<Product> GetProductByID(string itemNumber){

            Product findProduct =await Db.Products
            .Where(x=> x.ItemNumber.Equals(itemNumber))
            .FirstOrDefaultAsync();

            return findProduct;

        }   

    }}


