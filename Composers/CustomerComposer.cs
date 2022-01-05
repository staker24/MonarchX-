using AutoMapper;
using MonarchX.Data;
using MonarchX.Models;

namespace MonarchX.Composers
{
    public class CustomerComposer : ICustomerComposer
    {
         private readonly IMapper _mapper;
        private readonly ICustomerStoreRepository _customerRepo;
        protected readonly ICustomerComposer _api;


        public CustomerComposer(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ICustomerStoreRepository customerRepo)
    
        {
            _mapper = mapper;
            _customerRepo = customerRepo;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(CustomerForGetDto dto){

            var customer = await _customerRepo.GetCustomer(dto.Id);
            
            CustomerDto customerDto = null;

            //Map from Customer to Dto to pass to front
            if(customer != null){

                customerDto = _mapper.Map<CustomerDto>(customer);

            }

            return customerDto;

        }

        Task<CustomerForGetDto> ICustomerComposer.GetCustomerByIdAsync(CustomerForGetDto dto)
        {
            throw new NotImplementedException();
        }
    }
}