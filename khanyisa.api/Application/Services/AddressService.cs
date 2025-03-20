using Application.Interfaces.Repositories;
using Application.Interfaces.Service;
using Application.Mappings;
using Contracts.Requests;
using Contracts.Responses;
using Domain.Model;
using FluentValidation;

namespace Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepo _addressRepo;
    private readonly IValidator<Address> _addressValidator;
    public AddressService(IAddressRepo addressRepo, IValidator<Address> addressValidator)
    {
        _addressRepo = addressRepo;
        _addressValidator = addressValidator;
    }

    public async Task<AddressResponse> CreateAddressAsync(AddressRequest request, CancellationToken token = default)
    {
        Address address = request.MapAddress();
        await _addressValidator.ValidateAndThrowAsync(address, cancellationToken: token);

        Address? results = await _addressRepo.AddAddressAsync(address, token) ?? throw new InvalidOperationException("Failed to add address.");

        return results.MapToResponse();
    }
}
