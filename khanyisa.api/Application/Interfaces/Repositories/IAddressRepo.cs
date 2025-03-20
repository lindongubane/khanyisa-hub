using Domain.Model;

namespace Application.Interfaces.Repositories;

public interface IAddressRepo
{
    Task<Address?> AddAddressAsync(Address address, CancellationToken token = default);
}
