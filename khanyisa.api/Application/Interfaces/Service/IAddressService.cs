using Contracts.Requests;
using Contracts.Responses;

namespace Application.Interfaces.Service;

public interface IAddressService
{
    Task<AddressResponse> AddAddress(AddressRequest request); 
    Task<AddressResponse> UpdateAddress(AddressRequest request); 
    Task<AddressResponse> DeleteAddress(int id); 
    Task<AddressResponse> GetUserAddressList(AddressRequest request); 
}
