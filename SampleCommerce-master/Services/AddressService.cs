using Mapster;
using Microsoft.EntityFrameworkCore;
using SampleCommerce.Common;
using SampleCommerce.DTOs.Address;
using SampleCommerce.Models;
using SampleCommerce.Repositories;

namespace SampleCommerce.Services
{
    public class AddressService : IService<int, DtoAddressResponse, DtoAddressCreate, DtoAddressUpdate>
    {
        private readonly AddressRepo _addressRepo;
        public AddressService(AddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }
        public Result<DtoAddressResponse> Create(DtoAddressCreate dto)
        {
            // se questo indirizzo deve essere il preferito, prima tolgo il flag a tutti gli altri dell'utente
            if (dto.IsPreferred)
            {
                _addressRepo.ClearPreferredAddress(dto.UserId);
            }

            // l'indirizzo e il link utente-indirizzo vanno creati insieme nella stessa operazione
            Address address = dto.Adapt<Address>();
            address.IsActive = true;
            UserAddress link = new UserAddress
            {
                UserId = dto.UserId,
                IsPreferred = dto.IsPreferred,
                Address = address
            };
            address.UserAddresses.Add(link);

            try
            {
                _addressRepo.Create(address);
                return Result<DtoAddressResponse>.Ok(address.Adapt<DtoAddressResponse>());
            }
            catch (Exception ex)
            {
                return Result<DtoAddressResponse>.Fail(ex.Message);
            }
        }
        public Result Delete(int id, Guid userId)
        {
            // elimino solo il collegamento tra utente e indirizzo, non l'indirizzo fisico
            UserAddress? userLink = _addressRepo.GetUserAddress(id, userId);

            if (userLink is null)
                return Result.Fail(ErrorMessages.AddressNotFound);
            try
            {
                _addressRepo.DeleteUserAddress(userLink);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

            // se nessun altro utente usa questo indirizzo, lo metto inattivo invece di cancellarlo
            bool IsInUse = _addressRepo.CheckForOtherUsers(id, userId);
            if (!IsInUse)
            {
                Address? address = _addressRepo.GetById(id);
                if(address is not null)
                {
                    address.IsActive = false;
                    try
                    {
                        _addressRepo.Update(address);
                    }
                    catch (Exception ex)
                    {
                        return Result.Fail(ex.Message);
                    }
                }
            }
            return Result.Ok();
        }
        public Result Delete(int id)
        {
            // non ha senso cancellare un indirizzo senza sapere a quale utente appartiene
            throw new NotImplementedException();
        }
        public Result<DtoAddressResponse> Read(int id)
        {
            Address? address = _addressRepo.GetById(id);
            if (address is null)
                return Result<DtoAddressResponse>.Fail(ErrorMessages.AddressNotFound);
            return Result<DtoAddressResponse>.Ok(address.Adapt<DtoAddressResponse>());
        }
        public Result<List<DtoAddressResponse>> ReadAll()
        {
            List<Address> address = _addressRepo.GetAll();
            if (address is null || address.Count == 0)
            {
                return Result<List<DtoAddressResponse>>.Ok([]);
            }
            return Result<List<DtoAddressResponse>>.Ok(address.Adapt<List<DtoAddressResponse>>());
        }

        public Result<DtoAddressResponse> Update(int id, DtoAddressUpdate dto)
        {
            Address? existingAddress = _addressRepo.GetById(id);
            if (existingAddress is null)
                return Result<DtoAddressResponse>.Fail(ErrorMessages.AddressNotFound);

            // se sta mettendo questo come preferito, prima pulisco gli altri e poi segno questo
            if (dto.IsPreferred && existingAddress.UserAddresses.Any())
            {
                _addressRepo.ClearPreferredAddress(dto.UserId);
                UserAddress? link = existingAddress.UserAddresses.FirstOrDefault(ua => ua.UserId == dto.UserId);
                if (link is not null)
                    link.IsPreferred = true;
            }

            dto.Adapt(existingAddress);
            try
            {
                _addressRepo.Update(existingAddress);
                return Result<DtoAddressResponse>.Ok(existingAddress.Adapt<DtoAddressResponse>());
            }
            catch (Exception ex)
            {
                return Result<DtoAddressResponse>.Fail(ex.Message);
            }
        }

        public Result<List<DtoAddressResponse>>? ReadAllByUser(Guid userId)
        {
            List<Address>? address = _addressRepo.GetAllByUser(userId);
            if (address is null || address.Count == 0)
                return Result<List<DtoAddressResponse>>.Ok([]);
            return Result<List<DtoAddressResponse>>.Ok(address.Adapt<List<DtoAddressResponse>>());
        }
    }
}
