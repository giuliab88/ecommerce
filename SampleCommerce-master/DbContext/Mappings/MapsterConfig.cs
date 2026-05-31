using Mapster;
using SampleCommerce.DTOs.Address;
using SampleCommerce.DTOs.Users;
using SampleCommerce.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SampleCommerce.Context.Mappings
{
    public static class MapsterConfig
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public static void Configure()
        {
            TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);

            TypeAdapterConfig<string, Dictionary<string, string>>
            .NewConfig()
            .MapWith(src => string.IsNullOrEmpty(src)
                ? new Dictionary<string, string>()
                : JsonSerializer.Deserialize<Dictionary<string, string>>(src, _options));

            TypeAdapterConfig<Dictionary<string, string>, string>
                .NewConfig()
                .MapWith(src => JsonSerializer.Serialize(src, _options));

            TypeAdapterConfig<DtoUserCreateRequest, User>
                .NewConfig()
                .Ignore(dest => dest.Id)
                .Map(dest => dest.Iva, src => src.Seller ? src.Iva : null)
                .Map(dest => dest.TradingName, src => src.Seller ? src.TradingName : null);

            TypeAdapterConfig<DtoUserUpdateRequest, User>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Password)
                .Map(dest => dest.Iva, src => src.Seller ? src.Iva : null)
                .Map(dest => dest.TradingName, src => src.Seller ? src.TradingName : null);

            TypeAdapterConfig<Address, DtoAddressResponse>
                .NewConfig()
                .Map(dest => dest.IsPreferred,
                src => src.UserAddresses.Select(ua => ua.IsPreferred).FirstOrDefault());
        }
    }
}