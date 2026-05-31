using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SampleCommerce.Common;
using SampleCommerce.DTOs.Order;
using SampleCommerce.DTOs.OrderItem;
using SampleCommerce.Models;
using SampleCommerce.Repositories;

namespace SampleCommerce.Services
{
    public class OrdersService : IService<int, DtoOrderResponse, DtoOrderCreate, DtoOrderUpdate>
    {
        private readonly OrdersRepo _ordersRepo;
        private readonly SkusRepo _skusRepo;
        public OrdersService(OrdersRepo ordersRepo, SkusRepo skusRepo)
        {
            _ordersRepo = ordersRepo;
            _skusRepo = skusRepo;
        }

        public Result<DtoOrderResponse> Create(DtoOrderCreate dto)
        {
            // controllo subito che le quantità abbiano senso prima di toccare il db
            foreach (DtoOrderItemCreate dtoItemOrder in dto.Items)
            {
                if (dtoItemOrder.Quantity <= 0)
                    return Result<DtoOrderResponse>.Fail(ErrorMessages.InvalidQuantity);
            }

            // uso la transazione perché devo scalare lo stock e creare l'ordine insieme —
            // se una delle due fallisce voglio tornare indietro su tutto
            using IDbContextTransaction transaction = _ordersRepo.BeginTransaction();
            try
            {
                // carico tutti gli sku in una volta sola invece di fare una query per ognuno
                List<Guid> skuIds = dto.Items.Select(i => i.SkuId).ToList();
                List<StockKeepingUnit> skus = _skusRepo.GetWhere(s => skuIds.Contains(s.Id)).ToList();

                Order order = new Order
                {
                    UserId = dto.UserId,
                    AddressId = dto.AddressId,
                    CreatedAt = DateTime.Now,
                    TotalPrice = 0
                };

                foreach (DtoOrderItemCreate dtoItemOrder in dto.Items)
                {
                    StockKeepingUnit? sku = skus.FirstOrDefault(s => s.Id == dtoItemOrder.SkuId);
                    // lancio eccezione così la transazione fa rollback in automatico
                    if (sku == null)
                        throw new Exception(ErrorMessages.ProductNotFound);
                    if (sku.Stock < dtoItemOrder.Quantity)
                        throw new Exception(ErrorMessages.InsufficientStock);
                    sku.Stock -= dtoItemOrder.Quantity;
                    OrderItem orderItem = new OrderItem
                    {
                        SkuId = sku.Id,
                        Quantity = dtoItemOrder.Quantity,
                        MomentPrice = sku.Price  // salvo il prezzo del momento, può cambiare in futuro
                    };
                    order.OrderItems.Add(orderItem);
                    order.TotalPrice += (decimal)(orderItem.MomentPrice * orderItem.Quantity);
                }
                _ordersRepo.Create(order);
                transaction.Commit();
                return Result<DtoOrderResponse>.Ok(order.Adapt<DtoOrderResponse>());
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return Result<DtoOrderResponse>.Fail(ex.Message);
            }
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Result<DtoOrderResponse> Read(int id)
        {
            Order? order = _ordersRepo.GetById(id);
            if(order == null)
                return Result<DtoOrderResponse>.Fail(ErrorMessages.OrderNotFound);
            return Result<DtoOrderResponse>.Ok(order.Adapt<DtoOrderResponse>());
        }

        public Result<List<DtoOrderResponse>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Result<DtoOrderResponse> Update(int id, DtoOrderUpdate dto)
        {
            throw new NotImplementedException();
        }

        // questa è quella che uso davvero — gli ordini si leggono sempre per utente
        public Result<List<DtoOrderResponse>> ReadAllByUser(Guid userId)
        {
            List<Order>? orders = _ordersRepo.GetAllByUser(userId);
            if(orders is null || orders.Count == 0)
                return Result<List<DtoOrderResponse>>.Ok([]);
            return Result<List<DtoOrderResponse>>.Ok(orders.Adapt<List<DtoOrderResponse>>());
        }
    }
}
