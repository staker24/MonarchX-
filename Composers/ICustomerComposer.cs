using MonarchX.Models;

namespace MonarchX.Composers
{
    public interface ICustomerComposer : IDisposable
    {
        Task<CustomerForGetDto> GetCustomerByIdAsync(CustomerForGetDto dto);
    }
}