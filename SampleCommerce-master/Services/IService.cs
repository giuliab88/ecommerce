using SampleCommerce.Common;

namespace SampleCommerce.Services
{
    public interface IService<TKey, TDtoResponse, TDtoCreate, TDtoUpdate>
    {
        Result<TDtoResponse> Create(TDtoCreate dto);
        Result<TDtoResponse> Read(TKey id);
        Result<List<TDtoResponse>> ReadAll();
        Result<TDtoResponse> Update(TKey id, TDtoUpdate dto);
        Result Delete(TKey id);
    }
}
