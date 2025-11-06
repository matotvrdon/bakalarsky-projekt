using AutoMapper;
using Web.Domain.Abstractions;
using Web.Services.Abstractions;
using Web.Services.DTOs.Supplier;

namespace Web.Services.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<SupplierDto?> GetByIdAsync(int id)
    {
        return _mapper.Map<SupplierDto>(await _supplierRepository.GetByIdAsync(id));
    }
}