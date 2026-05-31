using BlazorSampleCommerce.DTOs;

namespace BlazorSampleCommerce.Services
{
    public class CartService
    {
        private readonly List<CartItemDto> _items = new();
        private readonly LocalStorageService _storage;
        private const string CartKey = "archetipo_cart";

        public IReadOnlyList<CartItemDto> Items => _items;
        public event Action? OnCartChanged;
        public int TotalItems => _items.Sum(i => i.Quantity);
        public decimal Total => _items.Sum(i => i.Subtotal);

        public CartService(LocalStorageService storage)
        {
            _storage = storage;
        }

        public async Task InitializeAsync()
        {
            try
            {
                var saved = await _storage.GetAsync<List<CartItemDto>>(CartKey);
                if (saved is { Count: > 0 })
                {
                    _items.Clear();
                    _items.AddRange(saved);
                    OnCartChanged?.Invoke();
                }
            }
            catch { }
        }

        private async Task PersistAsync()
        {
            try { await _storage.SetAsync(CartKey, _items.ToList()); }
            catch { }
        }

        public async Task AddItemAsync(Guid skuId, Guid productId, string productName, string skuLabel, decimal price, string? imageUrl = null)
        {
            var existing = _items.FirstOrDefault(i => i.SkuId == skuId);
            if (existing is not null)
                existing.Quantity++;
            else
                _items.Add(new CartItemDto
                {
                    SkuId       = skuId,
                    ProductId   = productId,
                    ProductName = productName,
                    SkuLabel    = skuLabel,
                    ImageUrl    = imageUrl,
                    Price       = price,
                    Quantity    = 1
                });
            OnCartChanged?.Invoke();
            await PersistAsync();
        }

        public async Task RemoveItemAsync(Guid skuId)
        {
            var item = _items.FirstOrDefault(i => i.SkuId == skuId);
            if (item is not null)
            {
                _items.Remove(item);
                OnCartChanged?.Invoke();
                await PersistAsync();
            }
        }

        public async Task UpdateQuantityAsync(Guid skuId, int qty)
        {
            var item = _items.FirstOrDefault(i => i.SkuId == skuId);
            if (item is null) return;
            if (qty <= 0)
                _items.Remove(item);
            else
                item.Quantity = qty;
            OnCartChanged?.Invoke();
            await PersistAsync();
        }

        public async Task ClearAsync()
        {
            _items.Clear();
            OnCartChanged?.Invoke();
            await PersistAsync();
        }
    }
}
