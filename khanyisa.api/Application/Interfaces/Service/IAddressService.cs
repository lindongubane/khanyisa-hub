using Contracts.Requests;
using Contracts.Responses;

namespace Application.Interfaces.Service;

public interface IAddressService
{
    Task<AddressResponse> CreateAddressAsync(AddressRequest request, CancellationToken tokem = default); 
}
