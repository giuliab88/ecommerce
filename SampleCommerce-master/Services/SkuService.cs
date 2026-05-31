using Mapster;
using SampleCommerce.Common;
using SampleCommerce.DTOs.SKUs;
using SampleCommerce.Models;
using SampleCommerce.Repositories;

namespace SampleCommerce.Services
{
    public class SkuService : IService<Guid, DtoSkusResponse, DtoSkusCreate, DtoSkusUpdate>
    {
        private readonly SkusRepo _repository;
        public SkuService(SkusRepo repository)
        {
            _repository = repository;
        }
        public Result<DtoSkusResponse> Create(DtoSkusCreate dto)
        {
            try
            {
                StockKeepingUnit sku = dto.Adapt<StockKeepingUnit>();
                _repository.Create(sku);
                return Result<DtoSkusResponse>.Ok(sku.Adapt<DtoSkusResponse>());
            }
            catch (Exception ex)
            {
                return Result<DtoSkusResponse>.Fail(ex.Message);
            }
        }
        public Result<DtoSkusResponse> Read(Guid id)
        {
            StockKeepingUnit? sku = _repository.GetById(id);
            if (sku is null)
                return Result<DtoSkusResponse>.Fail(ErrorMessages.SKUsNotFound);
            return Result<DtoSkusResponse>.Ok(sku.Adapt<DtoSkusResponse>());
        }
        public Result<List<DtoSkusResponse>> ReadAll()
        {
            // gli sku non si leggono mai tutti insieme, arrivano sempre dentro il prodotto
            throw new NotImplementedException();
        }
        public Result<DtoSkusResponse> Update(Guid id, DtoSkusUpdate dto)
        {
            StockKeepingUnit? existingSku = _repository.GetById(id);
            if (existingSku is null)
                return Result<DtoSkusResponse>.Fail(ErrorMessages.SKUsNotFound);

            try
            {
                dto.Adapt(existingSku);
                _repository.Update(existingSku);
                return Result<DtoSkusResponse>.Ok(existingSku.Adapt<DtoSkusResponse>());
            }
            catch (Exception ex)
            {
                return Result<DtoSkusResponse>.Fail(ex.Message);
            }
        }
        public Result Delete(Guid id)
        {
            StockKeepingUnit? sku = _repository.GetById(id);
            if (sku is null)
                return Result.Fail(ErrorMessages.SKUsNotFound);
            try
            {
                _repository.Delete(sku);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
