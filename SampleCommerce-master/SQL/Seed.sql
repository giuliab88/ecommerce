USE SampleEcommerceDb;
GO

-- ─────────────────────────────────────────────
--  Se vuoi ripulire tutto prima di ricaricare:
--  DELETE FROM OrderItems; DELETE FROM Orders;
--  DELETE FROM Reviews; DELETE FROM StockKeepingUnits;
--  DELETE FROM Products; DELETE FROM UserAddresses;
--  DELETE FROM Addresses; DELETE FROM Users;
-- ─────────────────────────────────────────────

-- Utente venditore (lo studio Archetipo)
-- Password: archetipo2025 (BCrypt hash, work factor 11)
INSERT INTO Users (Id, Name, Email, Password, Seller, Iva, TradingName)
VALUES (
    'A0000000-0000-0000-0000-000000000001',
    'Archetipo Studio',
    'studio@archetipo.it',
    '$2a$11$DR0AHtuR7C01inxhJcd3tulv5hGqRasu0nO.4dAbsMlVNoNtiGQoK',
    1,
    '12345678901',
    'Archetipo Studio'
);
GO

-- ─────────────────────────────────────────────
--  PRODOTTI
-- ─────────────────────────────────────────────

INSERT INTO Products (Id, Name, Brand, Description, ImageUrl, IsActive, TotalReviews, MediaReviews, SellerId)
VALUES

-- 1. Vaso Scia
(
    'B0000000-0001-0000-0000-000000000001',
    'Vaso Scia',
    'Quiete',
    'Plasmato a mano in argilla grigia, tornito lentamente e lasciato asciugare al sole. Ogni segno è rimasto. Non esiste un secondo pezzo uguale a questo.',
    '/images/vaso-anfora.png',
    1, 0, 0,
    'A0000000-0000-0000-0000-000000000001'
),

-- 2. Taccuino del Viandante
(
    'B0000000-0002-0000-0000-000000000002',
    'Taccuino del Viandante',
    'Memoria',
    'Copertina in pelle tinta a mano, rilegato con spago grezzo di cotone. Le pagine sono in carta cotone — non sono mai perfettamente uguali, e questo è il punto.',
    '/images/taccuino-del-viandante.png',
    1, 0, 0,
    'A0000000-0000-0000-0000-000000000001'
),

-- 3. Scultura Radice
(
    'B0000000-0003-0000-0000-000000000003',
    'Scultura Radice',
    'Radici',
    'Un pezzo di cedro trovato dopo una tempesta. La forma era già lì — ho solo tolto il superfluo. Ogni nodo è una storia che il bosco ha vissuto prima di me.',
    '/images/radice.png',
    1, 0, 0,
    'A0000000-0000-0000-0000-000000000001'
),

-- 4. Scialle delle Erbe
(
    'B0000000-0004-0000-0000-000000000004',
    'Scialle delle Erbe',
    'Cura',
    'Seta grezza tinta con corteccia di noce e foglie di indaco raccolte in luglio. Il colore cambia con la luce — di mattina è caldo, la sera quasi viola.',
    '/images/scialle-alle-erbe.png',
    1, 0, 0,
    'A0000000-0000-0000-0000-000000000001'
),

-- 5. Lampada Terracotta
(
    'B0000000-0005-0000-0000-000000000005',
    'Lampada Terracotta',
    'Terra',
    'Tornita in terracotta rossa, con incisioni a mano che filtrano la luce in forme di foglie. Non esistono due lampade con lo stesso disegno — la mano cambia ogni volta.',
    '/images/lampada-in-terracotta.png',
    1, 0, 0,
    'A0000000-0000-0000-0000-000000000001'
),

-- 6. Tazza Orbite
(
    'B0000000-0006-0000-0000-000000000006',
    'Tazza Orbite',
    'Silenzio',
    'Costruita con la tecnica a colombino — anello su anello, lenta. La superficie è volutamente irregolare. Tiene il calore in modo diverso da ogni tazza che hai in casa.',
    '/images/tazzona-grezza.png',
    1, 0, 0,
    'A0000000-0000-0000-0000-000000000001'
),

-- 7. Cesto del Mattino
(
    'B0000000-0007-0000-0000-000000000007',
    'Cesto del Mattino',
    'Tempo',
    'Intrecciato con giunco palustre raccolto a mano in autunno, quando il fusto è ancora flessibile. Ogni cesto richiede due settimane di lavorazione.',
    '/images/cestino-del-mattino.png',
    1, 0, 0,
    'A0000000-0000-0000-0000-000000000001'
),

-- 8. Candela di Resina
(
    'B0000000-0008-0000-0000-000000000008',
    'Candela di Resina',
    'Quiete',
    'Cera d''api vergine miscelata con resina di pino. Brucia lentamente — quaranta ore — e sprigiona un profumo che sembra un bosco al tramonto d''ottobre.',
    '/images/candela-in-resina.png',
    1, 0, 0,
    'A0000000-0000-0000-0000-000000000001'
);
GO

-- ─────────────────────────────────────────────
--  SKU — varianti per ogni prodotto
--  Features è un JSON: {"Chiave":"Valore", ...}
-- ─────────────────────────────────────────────

INSERT INTO StockKeepingUnits (Id, ProductId, Price, Stock, Features)
VALUES

-- Vaso Scia
(
    'C0000000-0001-0001-0000-000000000001',
    'B0000000-0001-0000-0000-000000000001',
    68.00, 4,
    '{"Materiale":"Argilla bianca","Altezza":"22 cm","Finitura":"Naturale opaca"}'
),
(
    'C0000000-0001-0002-0000-000000000001',
    'B0000000-0001-0000-0000-000000000001',
    124.00, 2,
    '{"Materiale":"Argilla bianca","Altezza":"38 cm","Finitura":"Naturale opaca"}'
),

-- Taccuino del Viandante
(
    'C0000000-0002-0001-0000-000000000002',
    'B0000000-0002-0000-0000-000000000002',
    34.00, 8,
    '{"Materiale":"Pelle tinta","Formato":"A5","Pagine":"120"}'
),
(
    'C0000000-0002-0002-0000-000000000002',
    'B0000000-0002-0000-0000-000000000002',
    52.00, 5,
    '{"Materiale":"Cotone","Formato":"A4","Pagine":"160"}'
),

-- Scultura Radice (pezzo unico)
(
    'C0000000-0003-0001-0000-000000000003',
    'B0000000-0003-0000-0000-000000000003',
    280.00, 1,
    '{"Materiale":"Cedro di recupero","Dimensione":"45 × 18 cm","Peso":"1.4 kg"}'
),

-- Scialle delle Erbe
(
    'C0000000-0004-0001-0000-000000000004',
    'B0000000-0004-0000-0000-000000000004',
    110.00, 3,
    '{"Materiale":"Seta grezza","Colore":"Ocra di noce","Dimensione":"180 × 70 cm"}'
),
(
    'C0000000-0004-0002-0000-000000000004',
    'B0000000-0004-0000-0000-000000000004',
    110.00, 2,
    '{"Materiale":"Seta grezza","Colore":"Indaco naturale","Dimensione":"180 × 70 cm"}'
),

-- Lampada Terracotta
(
    'C0000000-0005-0001-0000-000000000005',
    'B0000000-0005-0000-0000-000000000005',
    89.00, 5,
    '{"Materiale":"Terracotta rossa","Altezza":"18 cm","Motivo":"Foglie di fico"}'
),
(
    'C0000000-0005-0002-0000-000000000005',
    'B0000000-0005-0000-0000-000000000005',
    148.00, 3,
    '{"Materiale":"Terracotta rossa","Altezza":"32 cm","Motivo":"Foglie di fico"}'
),

-- Tazza Orbite
(
    'C0000000-0006-0001-0000-000000000006',
    'B0000000-0006-0000-0000-000000000006',
    42.00, 6,
    '{"Materiale":"Ceramica chamotte","Colore":"Naturale","Capacità":"380 ml"}'
),
(
    'C0000000-0006-0002-0000-000000000006',
    'B0000000-0006-0000-0000-000000000006',
    48.00, 4,
    '{"Materiale":"Ceramica chamotte","Colore":"Smalto scuro","Capacità":"380 ml"}'
),

-- Cesto del Mattino
(
    'C0000000-0007-0001-0000-000000000007',
    'B0000000-0007-0000-0000-000000000007',
    58.00, 4,
    '{"Materiale":"Giunco palustre","Diametro":"28 cm","Manico":"Naturale"}'
),
(
    'C0000000-0007-0002-0000-000000000007',
    'B0000000-0007-0000-0000-000000000007',
    84.00, 2,
    '{"Materiale":"Giunco palustre","Diametro":"42 cm","Manico":"Naturale"}'
),

-- Candela di Resina
(
    'C0000000-0008-0001-0000-000000000008',
    'B0000000-0008-0000-0000-000000000008',
    28.00, 10,
    '{"Materiale":"Cera d''api","Profumo":"Pino e resina","Durata":"40 ore"}'
),
(
    'C0000000-0008-0002-0000-000000000008',
    'B0000000-0008-0000-0000-000000000008',
    28.00, 8,
    '{"Materiale":"Cera d''api","Profumo":"Lavanda selvatica","Durata":"40 ore"}'
),
(
    'C0000000-0008-0003-0000-000000000008',
    'B0000000-0008-0000-0000-000000000008',
    28.00, 6,
    '{"Materiale":"Cera d''api","Profumo":"Cedro e muschio","Durata":"40 ore"}'
);
GO
