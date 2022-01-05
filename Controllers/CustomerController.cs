using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MonarchX.Composers;
using MonarchX.Data;
using MonarchX.Models;

namespace MonarchX.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerStoreDbContext _context;
        private readonly ICustomerComposer _api;


        public CustomerController(
            ILogger<CustomerController> logger, 
            CustomerStoreDbContext context,
            ICustomerComposer customerComposer ) : base()
        {   
            _context = context;
            _logger = logger;
            _api = customerComposer;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id){

            //Call composer to generate the method and call repo from the the
            var dto = new CustomerForGetDto(){

                Id = id
            };

            var res =  await _api.GetCustomerByIdAsync(dto);

            return new CustomerDto();

            
        }
        
    }
}
