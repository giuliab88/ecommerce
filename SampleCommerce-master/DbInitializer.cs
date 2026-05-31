using SampleCommerce.Context;
using SampleCommerce.Models;

namespace SampleCommerce
{
    public static class DbInitializer
    {
        public static void Initialize(EcommerceDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any()) return;

            var sellerId = new Guid("A0000000-0000-0000-0000-000000000001");

            context.Users.Add(new User
            {
                Id = sellerId,
                Name = "Archetipo Studio",
                Email = "studio@archetipo.it",
                Password = "$2a$11$DR0AHtuR7C01inxhJcd3tulv5hGqRasu0nO.4dAbsMlVNoNtiGQoK",
                Seller = true,
                Iva = "12345678901",
                TradingName = "Archetipo Studio",
                EmailConfirmed = true
            });

            var products = new[]
            {
                new Product
                {
                    Id = new Guid("B0000000-0001-0000-0000-000000000001"),
                    Name = "Vaso Scia", Brand = "Quiete",
                    Description = "Plasmato a mano in argilla grigia, tornito lentamente e lasciato asciugare al sole. Ogni segno è rimasto. Non esiste un secondo pezzo uguale a questo.",
                    ImageUrl = "/images/vaso-anfora.png", IsActive = true, SellerId = sellerId,
                    StockKeepingUnits = new List<StockKeepingUnit>
                    {
                        new() { Id = new Guid("C0000000-0001-0001-0000-000000000001"), Price = 68.00m, Stock = 4, Features = "{\"Materiale\":\"Argilla bianca\",\"Altezza\":\"22 cm\",\"Finitura\":\"Naturale opaca\"}" },
                        new() { Id = new Guid("C0000000-0001-0002-0000-000000000001"), Price = 124.00m, Stock = 2, Features = "{\"Materiale\":\"Argilla bianca\",\"Altezza\":\"38 cm\",\"Finitura\":\"Naturale opaca\"}" }
                    }
                },
                new Product
                {
                    Id = new Guid("B0000000-0002-0000-0000-000000000002"),
                    Name = "Taccuino del Viandante", Brand = "Memoria",
                    Description = "Copertina in pelle tinta a mano, rilegato con spago grezzo di cotone. Le pagine sono in carta cotone — non sono mai perfettamente uguali, e questo è il punto.",
                    ImageUrl = "/images/taccuino-del-viandante.png", IsActive = true, SellerId = sellerId,
                    StockKeepingUnits = new List<StockKeepingUnit>
                    {
                        new() { Id = new Guid("C0000000-0002-0001-0000-000000000002"), Price = 34.00m, Stock = 8, Features = "{\"Materiale\":\"Pelle tinta\",\"Formato\":\"A5\",\"Pagine\":\"120\"}" },
                        new() { Id = new Guid("C0000000-0002-0002-0000-000000000002"), Price = 52.00m, Stock = 5, Features = "{\"Materiale\":\"Cotone\",\"Formato\":\"A4\",\"Pagine\":\"160\"}" }
                    }
                },
                new Product
                {
                    Id = new Guid("B0000000-0003-0000-0000-000000000003"),
                    Name = "Scultura Radice", Brand = "Radici",
                    Description = "Un pezzo di cedro trovato dopo una tempesta. La forma era già lì — ho solo tolto il superfluo. Ogni nodo è una storia che il bosco ha vissuto prima di me.",
                    ImageUrl = "/images/radice.png", IsActive = true, SellerId = sellerId,
                    StockKeepingUnits = new List<StockKeepingUnit>
                    {
                        new() { Id = new Guid("C0000000-0003-0001-0000-000000000003"), Price = 280.00m, Stock = 1, Features = "{\"Materiale\":\"Cedro di recupero\",\"Dimensione\":\"45 × 18 cm\",\"Peso\":\"1.4 kg\"}" }
                    }
                },
                new Product
                {
                    Id = new Guid("B0000000-0004-0000-0000-000000000004"),
                    Name = "Scialle delle Erbe", Brand = "Cura",
                    Description = "Seta grezza tinta con corteccia di noce e foglie di indaco raccolte in luglio. Il colore cambia con la luce — di mattina è caldo, la sera quasi viola.",
                    ImageUrl = "/images/scialle-alle-erbe.png", IsActive = true, SellerId = sellerId,
                    StockKeepingUnits = new List<StockKeepingUnit>
                    {
                        new() { Id = new Guid("C0000000-0004-0001-0000-000000000004"), Price = 110.00m, Stock = 3, Features = "{\"Materiale\":\"Seta grezza\",\"Colore\":\"Ocra di noce\",\"Dimensione\":\"180 × 70 cm\"}" },
                        new() { Id = new Guid("C0000000-0004-0002-0000-000000000004"), Price = 110.00m, Stock = 2, Features = "{\"Materiale\":\"Seta grezza\",\"Colore\":\"Indaco naturale\",\"Dimensione\":\"180 × 70 cm\"}" }
                    }
                },
                new Product
                {
                    Id = new Guid("B0000000-0005-0000-0000-000000000005"),
                    Name = "Lampada Terracotta", Brand = "Terra",
                    Description = "Tornita in terracotta rossa, con incisioni a mano che filtrano la luce in forme di foglie. Non esistono due lampade con lo stesso disegno — la mano cambia ogni volta.",
                    ImageUrl = "/images/lampada-in-terracotta.png", IsActive = true, SellerId = sellerId,
                    StockKeepingUnits = new List<StockKeepingUnit>
                    {
                        new() { Id = new Guid("C0000000-0005-0001-0000-000000000005"), Price = 89.00m, Stock = 5, Features = "{\"Materiale\":\"Terracotta rossa\",\"Altezza\":\"18 cm\",\"Motivo\":\"Foglie di fico\"}" },
                        new() { Id = new Guid("C0000000-0005-0002-0000-000000000005"), Price = 148.00m, Stock = 3, Features = "{\"Materiale\":\"Terracotta rossa\",\"Altezza\":\"32 cm\",\"Motivo\":\"Foglie di fico\"}" }
                    }
                },
                new Product
                {
                    Id = new Guid("B0000000-0006-0000-0000-000000000006"),
                    Name = "Tazza Orbite", Brand = "Silenzio",
                    Description = "Costruita con la tecnica a colombino — anello su anello, lenta. La superficie è volutamente irregolare. Tiene il calore in modo diverso da ogni tazza che hai in casa.",
                    ImageUrl = "/images/tazzona-grezza.png", IsActive = true, SellerId = sellerId,
                    StockKeepingUnits = new List<StockKeepingUnit>
                    {
                        new() { Id = new Guid("C0000000-0006-0001-0000-000000000006"), Price = 42.00m, Stock = 6, Features = "{\"Materiale\":\"Ceramica chamotte\",\"Colore\":\"Naturale\",\"Capacità\":\"380 ml\"}" },
                        new() { Id = new Guid("C0000000-0006-0002-0000-000000000006"), Price = 48.00m, Stock = 4, Features = "{\"Materiale\":\"Ceramica chamotte\",\"Colore\":\"Smalto scuro\",\"Capacità\":\"380 ml\"}" }
                    }
                },
                new Product
                {
                    Id = new Guid("B0000000-0007-0000-0000-000000000007"),
                    Name = "Cesto del Mattino", Brand = "Tempo",
                    Description = "Intrecciato con giunco palustre raccolto a mano in autunno, quando il fusto è ancora flessibile. Ogni cesto richiede due settimane di lavorazione.",
                    ImageUrl = "/images/cestino-del-mattino.png", IsActive = true, SellerId = sellerId,
                    StockKeepingUnits = new List<StockKeepingUnit>
                    {
                        new() { Id = new Guid("C0000000-0007-0001-0000-000000000007"), Price = 58.00m, Stock = 4, Features = "{\"Materiale\":\"Giunco palustre\",\"Diametro\":\"28 cm\",\"Manico\":\"Naturale\"}" },
                        new() { Id = new Guid("C0000000-0007-0002-0000-000000000007"), Price = 84.00m, Stock = 2, Features = "{\"Materiale\":\"Giunco palustre\",\"Diametro\":\"42 cm\",\"Manico\":\"Naturale\"}" }
                    }
                },
                new Product
                {
                    Id = new Guid("B0000000-0008-0000-0000-000000000008"),
                    Name = "Candela di Resina", Brand = "Quiete",
                    Description = "Cera d'api vergine miscelata con resina di pino. Brucia lentamente — quaranta ore — e sprigiona un profumo che sembra un bosco al tramonto d'ottobre.",
                    ImageUrl = "/images/candela-in-resina.png", IsActive = true, SellerId = sellerId,
                    StockKeepingUnits = new List<StockKeepingUnit>
                    {
                        new() { Id = new Guid("C0000000-0008-0001-0000-000000000008"), Price = 28.00m, Stock = 10, Features = "{\"Materiale\":\"Cera d'api\",\"Profumo\":\"Pino e resina\",\"Durata\":\"40 ore\"}" },
                        new() { Id = new Guid("C0000000-0008-0002-0000-000000000008"), Price = 28.00m, Stock = 8,  Features = "{\"Materiale\":\"Cera d'api\",\"Profumo\":\"Lavanda selvatica\",\"Durata\":\"40 ore\"}" },
                        new() { Id = new Guid("C0000000-0008-0003-0000-000000000008"), Price = 28.00m, Stock = 6,  Features = "{\"Materiale\":\"Cera d'api\",\"Profumo\":\"Cedro e muschio\",\"Durata\":\"40 ore\"}" }
                    }
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
