using Mapster;
using SampleCommerce.Common;
using SampleCommerce.DTOs.Product;
using SampleCommerce.Models;
using SampleCommerce.Repositories;
using System.Text.Json;

namespace SampleCommerce.Services
{
    public class ProductService : IService<Guid, DtoProductResponse, DtoProductCreate, DtoProductUpdate>
    {
        private readonly ProductRepo _productRepo;
        public ProductService(ProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public Result<DtoProductResponse> Create(DtoProductCreate dto)
        {
            try
            {
                Product product = dto.Adapt<Product>();
                product.IsActive = true;  // ogni prodotto nuovo parte attivo di default
                _productRepo.Create(product);
                return Result<DtoProductResponse>.Ok(product.Adapt<DtoProductResponse>());
            }
            catch (Exception ex)
            {
                return Result<DtoProductResponse>.Fail(ex.Message);
            }
        }

        public Result Delete(Guid id)
        {
            Product? product = _productRepo.GetById(id);
            if (product is null)
                return Result.Fail(ErrorMessages.ProductNotFound);
            try
            {
                _productRepo.Delete(product);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public Result<DtoProductResponse> Read(Guid id)
        {
            Product? product = _productRepo.GetById(id);
            if (product is null)
                return Result<DtoProductResponse>.Fail(ErrorMessages.ProductNotFound);
            return Result<DtoProductResponse>.Ok(product.Adapt<DtoProductResponse>());
        }

        public Result<List<DtoProductResponse>> ReadAll()
        {
            List<Product>? products = _productRepo.GetAll();
            if(products is null || products.Count == 0)
                return Result<List<DtoProductResponse>>.Ok([]);
            return Result<List<DtoProductResponse>>.Ok(products.Adapt<List<DtoProductResponse>>());
        }

        public Result<DtoProductResponse> Update(Guid id, DtoProductUpdate dto)
        {
            Product? existingProduct = _productRepo.GetById(id);
            if (existingProduct is null)
                return Result<DtoProductResponse>.Fail(ErrorMessages.ProductNotFound);
            try
            {
                // Adapt sovrascrive solo i campi del dto sull'oggetto esistente, non crea un nuovo record
                dto.Adapt(existingProduct);
                _productRepo.Update(existingProduct);
                return Result<DtoProductResponse>.Ok(existingProduct.Adapt<DtoProductResponse>());
            }
            catch (Exception ex)
            {
                return Result<DtoProductResponse>.Fail(ex.Message);
            }
        }
    }
}
