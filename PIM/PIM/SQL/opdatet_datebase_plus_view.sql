DROP VIEW IF EXISTS
    export_view;

DROP FUNCTION IF EXISTS
    FindProducts,
    exportSearch;

DROP TABLE IF EXISTS
    types,
    categories,
    products,
    users;

--Tables----------------------------------------------------------------------------------------------------------------
CREATE TABLE categories(
    id serial PRIMARY KEY,
    category varchar (30) NOT NULL UNIQUE
);

CREATE TABLE products(
    sku serial PRIMARY KEY,
    name varchar (50) NOT NULL,
    manufacturer varchar (50) NOT NULL,
    retail_price decimal (10, 2) NOT NULL,
    sales_price_vat decimal (10, 2) NOT NULL,--10 before the comma, 2 after
    sales_price decimal (10,2) GENERATED ALWAYS AS (sales_price_vat * 0.8) STORED,
    height integer NOT NULL,
    width integer NOT NULL,
    depth integer NOT NULL,
    weight decimal NOT NULL,
    color varchar (30) NOT NULL,
    long_descr text NOT NULL,
    short_descr text GENERATED ALWAYS AS (LEFT(long_descr, 150)) STORED,--takes the first 150 chars of long_descr
    date_added timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE types(
    product integer NOT NULL REFERENCES products(sku),
    category integer NOT NULL REFERENCES categories(id)
);

--Login--
CREATE TABLE IF NOT EXISTS users (
    username VARCHAR(50) PRIMARY KEY,
    password VARCHAR(100) NOT NULL
);

--Insert----------------------------------------------------------------------------------------------------------------
INSERT INTO categories (category) VALUES
('Desktop'),
('Mouse'),
('Keyboard'),
('Laptop'),
('Gaming'),
('School'),
('Monitor'),
('Component'),
('Graphics card'),
('Processor'),
('RAM'),
('Motherboard'),
('Cases'),
('TV'),
('Smart');


INSERT INTO products (name, manufacturer, retail_price, sales_price_vat, height, width, depth, weight, color, long_descr) VALUES
-- Mouse
('Logitech G Pro X Superlight', 'Logitech', 300, 999, 125, 63, 40, 0.063, 'Black', 'Logitech G Pro X Superlight er en ultralet mus med HERO 25K sensor, trådløs LIGHTSPEED teknologi, og op til 70 timers batterilevetid. Designet til professionelle esports-atleter, reducerer denne mus friktion og vægt for optimal præcision og ydeevne.'),
('Razer DeathAdder V3 Pro', 'Razer', 400, 1199, 127, 68, 44, 0.063, 'White', 'Razer DeathAdder V3 Pro er en trådløs gaming-mus med Razer Focus Pro 30K optisk sensor, ultralet design på kun 63g og HyperSpeed Wireless teknologi. Med en batterilevetid på op til 90 timer er den perfekt til intense gaming-sessioner og turneringsbrug.'),
--Keyboard
('SteelSeries Apex Pro TKL', 'SteelSeries', 500, 1699, 355, 128, 42, 1.1, 'Black', 'SteelSeries Apex Pro TKL er et mekanisk gaming-tastatur med OmniPoint 2.0 justerbare switcher, som giver mulighed for lynhurtige tastetryk. TKL-designet gør det pladsbesparende, mens magnetiske håndledsstøtte sikrer komfort. RGB-baggrundsbelysning og aluminium chassis sikrer holdbarhed og stil.'),
('Corsair K70 RGB MK.2', 'Corsair', 400, 1399, 438, 166, 40, 1.25, 'Black', 'Corsair K70 RGB MK.2 er et mekanisk gaming-tastatur med Cherry MX Red switches, holdbart aluminium chassis og RGB-baggrundsbelysning. Med dedikerede multimedieknapper og 8 MB indbygget hukommelse til makroer er det et perfekt valg til seriøse gamere og professionelle brugere.'),
-- Monitors
('ASUS ROG Swift PG259QN', 'ASUS', 1000, 4499, 557, 379, 236, 6.2, 'Black', 'ASUS ROG Swift PG259QN er en 24.5-tommer FHD skrærm med en opdateringshastighed på 360Hz og 1ms responstid. Med NVIDIA G-Sync teknologi, HDR10-understøttelse og ergonomisk design er den ideel til esports-entusiaster, der kræver den bedste ydeevne og minimal input lag.'),
('LG UltraGear 27GN950', 'LG', 1000, 5999, 614, 364, 271, 7.3, 'Black', 'LG UltraGear 27GN950 er en 27-tommer 4K UHD skærm med Nano IPS teknologi, 1ms responstid og 144Hz opdateringshastighed. Den understøtter NVIDIA G-Sync, HDR600 og har et bredt farvespektrum, hvilket gør den perfekt til både gaming og kreativt arbejde.'),
-- Graphics Cards
('NVIDIA GeForce RTX 4090', 'NVIDIA', 4000, 15999, 310, 140, 60, 2.2, 'Black', 'NVIDIA GeForce RTX 4090 er et high-end grafikkort med Ada Lovelace arkitektur, 24GB GDDR6X VRAM og Ray Tracing-teknologi. Ideelt til 4K gaming og AI-accelererede opgaver, giver dette kort ekstrem ydeevne til entusiaster og professionelle brugere.'),
('AMD Radeon RX 7900 XTX', 'AMD', 2000 , 10999, 287, 120, 50, 1.9, 'Black', 'AMD Radeon RX 7900 XTX leverer topklasse gaming-ydelse med RDNA 3 arkitektur, 24GB GDDR6 hukommelse og support for ray tracing. Ideelt til 4K gaming med avanceret billedbehandling og høj energieffektivitet.'),
-- Laptops
('ASUS ROG Strix G16', 'ASUS', 3500, 14999, 354, 259, 26, 2.5, 'Black', 'ASUS ROG Strix G16 er en kraftfuld gaming-laptop med en Intel Core i9-processor, NVIDIA RTX 4070-grafikkort og en 16-tommer QHD-skærm med 240Hz opdateringshastighed, der leverer en hurtig og flydende gamingoplevelse.'),
('MSI Raider GE78 HX', 'MSI', 5000, 18999, 380, 275, 30, 2.9, 'Black', 'MSI Raider GE78 HX er en high-end gaming-laptop med Intel Core i9 13. gen-processor, NVIDIA RTX 4080 GPU, mekanisk RGB-tastatur og en 17,3-tommer QHD-skærm med 240Hz opdatering.'),
('Apple MacBook Air M2', 'Apple', 2000, 10999, 304, 212, 16, 1.2, 'Silver', 'Apple MacBook Air M2 leverer en kraftfuld oplevelse med M2-chip, 13,6-tommer Liquid Retina-skærm, 18-timers batterilevetid og et let og elegant design ideelt til studerende og professionelle.'),
('Dell XPS 13', 'Dell', 1500, 9999, 295, 199, 15, 1.1, 'Platinum Silver', 'Dell XPS 13 er en ultra-kompakt laptop med 13,4-tommer InfinityEdge FHD+ skærm, Intel Core i7 12. gen-processor, og op til 16GB RAM, perfekt til studerende og kontorbrugere.'),
--Smart TV
('Samsung QN90C Neo QLED', 'Samsung', 3500, 14999, 1447, 829, 30, 18.5, 'Black', 'Samsung QN90C er et 55-tommer Neo QLED Smart TV med Quantum HDR 2000, 120Hz opdateringshastighed, og AI-drevet 4K-opskalering, perfekt til film, serier og gaming.'),
('LG OLED C3', 'LG', 2500, 16999, 1450, 850, 20, 17.8, 'Black', 'LG OLED C3 er et 55-tommer OLED Smart TV med selvoplyste pixels, α9 Gen6 AI-processor, Dolby Vision og Dolby Atmos, hvilket giver fantastisk billedkvalitet og fordybende lyd.'),
-- Processors (CPU)
('Intel Core i9-13900K', 'Intel', 750, 5799, 45, 45, 5, 0.1, 'Silver', 'Intel Core i9-13900K er en 24-core processor med en clockhastighed op til 5,8GHz, designet til gaming, kreativt arbejde og tung multitasking.'),
('AMD Ryzen 9 7950X', 'AMD', 1000, 5499, 40, 40, 5, 0.1, 'Silver', 'AMD Ryzen 9 7950X leverer 16 kerner, 32 tråde og boost clock op til 5,7GHz, optimeret til gaming og professionelt arbejde.'),
-- RAM
('Corsair Vengeance 32GB DDR5', 'Corsair', 500, 1499, 133, 31, 5, 0.05, 'Black', 'Corsair Vengeance 32GB DDR5 RAM med 6000MHz hastighed og CL30 latens er ideelt til gaming-pcer og workstation-opbygninger.'),
('G.Skill Trident Z5 RGB 32GB', 'G.Skill', 450, 1699, 135, 35, 5, 0.05, 'Black', 'G.Skill Trident Z5 RGB 32GB DDR5 RAM med 6400MHz hastighed og avanceret køling giver høj ydeevne til entusiaster og gamere.'),
-- Motherboards
('ASUS ROG Strix Z790-E', 'ASUS', 1200, 3499, 305, 244, 6, 1.5, 'Black', 'ASUS ROG Strix Z790-E er et high-end bundkort med PCIe 5.0, Wi-Fi 6E, DDR5-support og AI-overclocking for ultimativ Intel 13. gen. CPU-ydeevne.'),
('MSI MEG X670E Ace', 'MSI', 1100, 3999, 305, 244, 6, 1.6, 'Black', 'MSI MEG X670E Ace er et avanceret AMD-bundkort med DDR5-understøttelse, PCIe 5.0 og udvidet VRM-køling for optimal ydeevne.'),
-- Computer Cases
('Lian Li O11 Dynamic EVO', 'Lian Li', 350, 1699, 471, 285, 465, 10.8, 'Black', 'Lian Li O11 Dynamic EVO er et modulært mid-tower kabinet med høj luftstrøm, panoramisk glasdesign og plads til omfattende vandkølingsløsninger.'),
('NZXT H7 Flow', 'NZXT', 200, 1299, 480, 230, 505, 9.5, 'Black', 'NZXT H7 Flow er et minimalistisk mid-tower kabinet med perforeret frontpanel for øget airflow, rummeligt interiør og nem kabelstyring.'),
-- Desktop PC
('Dell XPS Desktop', 'Dell', 12999, 12999, 350, 180, 400, 8.5, 'Black', 'Dell XPS Desktop is a high-performance computer with Intel Core i7 processor, 32GB RAM, 1TB SSD, and NVIDIA RTX 3060 graphics card, perfect for work and gaming.'),
('HP Omen 45L', 'HP', 15999, 15999, 450, 200, 500, 10.2, 'Black', 'HP Omen 45L is a powerful gaming desktop with AMD Ryzen 9 processor, 64GB RAM, 2TB SSD, and NVIDIA RTX 4080, designed for extreme gaming performance and creative work.');

INSERT INTO types (product, category) VALUES
(1, 2),
(2, 2),
(2, 5),
(3, 3),
(3, 5),
(4, 3),
(4, 5),
(5, 7),
(6, 7),
(7, 9),
(7, 8),
(8, 9),
(8, 8),
(9, 4),
(9, 5),
(10, 4),
(10, 5),
(11, 4),
(11, 6),
(12, 4),
(13, 14),
(13, 15),
(14, 14),
(14, 15),
(15, 8),
(15, 10),
(16, 8),
(16, 10),
(17, 5),
(17, 8),
(17, 11),
(18, 8),
(18, 11),
(19, 8),
(19, 12),
(20, 8),
(20, 12),
(21, 13),
(22, 13),
(23, 1),
(24, 1),
(24, 5);

--login--
INSERT INTO users (username, password) VALUES
('moha', 'PIM'),
('test', 'test'),
('Amalie', 'Amalie');

--View------------------------------------------------------------------------------------------------------------------
CREATE VIEW export_view AS
    SELECT
        products.name,
        products.manufacturer,
        products.retail_price,
        products.sales_price_vat,
        products.height,
        products.width,
        products.depth,
        products.weight,
        products.color,
        products.long_descr,
        STRING_AGG(categories.category, ',' order by categories.category) as categories
        --it will put the categories in one string and put them in alfabetical order
FROM products
INNER JOIN types ON products.sku = types.product
INNER JOIN categories ON categories.id = types.category
GROUP BY products.sku;

select * FROM export_view;

--Function------------------------------------------------------------------------------------------------------------------
--Find product--
-- *** How the function works ***
-- Each parameter is explained below.
-- Filters products by category if categoryTerm is provided.
-- Filters products by name if searchTerm is provided.
-- Sorts results by name, price, or date added.

-- *** Examples of the function ***
-- Retrieve all products sorted by name : SELECT * FROM FindProductsBySorting();
-- Retrieve all products in the category "Gaming": SELECT * FROM FindProductsBySorting('Gaming');
-- Retrieve alle products including "laptop" in their name: SELECT * FROM FindProductsBySorting(NULL, 'Laptop');
-- Retrieve all products in the category "gaming" and sort it by price: SELECT * FROM FindProductsBySorting('Gaming', NULL, 'retail_price', 'ASC');

CREATE OR REPLACE FUNCTION FindProducts(
    categoryTerm VARCHAR(50) DEFAULT NULL, -- category
    searchTerm VARCHAR(50) DEFAULT NULL -- name
)
RETURNS TABLE (
    sku INT,
    name VARCHAR,
    sales_price_vat DECIMAL,
    height INT,
    width INT,
    depth INT,
    weight DECIMAL,
    color VARCHAR,
    short_descr TEXT
)
AS $$
BEGIN
    RETURN QUERY
    SELECT DISTINCT ON (products.sku)
	    products.sku, products.name, products.sales_price_vat, products.height, products.width,
	    products.depth, products.weight, products.color,products.short_descr
    FROM products
    LEFT JOIN types ON products.sku = types.product
    LEFT JOIN categories ON types.category = categories.id
    WHERE (categoryTerm IS NULL OR categories.category = categoryTerm)
    AND (searchTerm IS NULL OR products.name ILIKE '%' || searchTerm || '%');
END;
$$ LANGUAGE plpgsql;

SELECT * FROM FindProducts(NULL, NULL); -- Test

--Export Search--
CREATE OR REPLACE FUNCTION exportSearch(
    categoryTerm VARCHAR(50) DEFAULT NULL, -- category
    searchTerm VARCHAR(50) DEFAULT NULL -- name
)
RETURNS TABLE (
    name VARCHAR,
    manufacturer VARCHAR,
    retail_price DECIMAL,
    sales_price_vat DECIMAL,
    height INT,
    width INT,
    depth INT,
    weight DECIMAL,
    color VARCHAR,
    long_descr TEXT,
    categories TEXT
)
AS $$
    BEGIN
    RETURN QUERY SELECT
        products.name,
        products.manufacturer,
        products.retail_price,
        products.sales_price_vat,
        products.height,
        products.width,
        products.depth,
        products.weight,
        products.color,
        products.long_descr,
        STRING_AGG(categories.category, ',' order by categories.category) --as categories
        --it will put the categories in one string and put them in alfabetical order

    FROM products
    INNER JOIN types ON products.sku = types.product
    INNER JOIN categories ON categories.id = types.category
    WHERE
        (searchTerm IS NULL OR products.name ILIKE '%' || searchTerm || '%')
        AND (
        categoryTerm IS NULL OR EXISTS (
        SELECT * FROM types
        INNER JOIN categories on categories.id = types.category
        Where types.product = products.sku AND categories.category = categoryTerm)
        )
    GROUP BY products.sku;
END;
$$ LANGUAGE plpgsql;

select * FROM exportSearch('Gaming','Pro');--test