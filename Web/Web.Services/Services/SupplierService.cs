using AutoMapper;
using Web.DataAccess.Abstractions;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public SupplierService(
        ISupplierRepository supplierRepository,
        IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<SupplierDto?> GetAsync()
    {
        var supplier = await _supplierRepository.GetAsync();

        if (supplier == null)
        {
            return null;
        }

        return _mapper.Map<SupplierDto>(supplier);
    }
}