CREATE TABLE if NOT EXISTS products (
    id SERIAL PRIMARY KEY,
    filename TEXT NOT NULL UNIQUE,
    dimensionSize TEXT NOT NULL,
    type TEXT NOT NULL,
    upload_timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    path TEXT NOT NULL,
    metadata JSONB DEFAULT '{}',
    transformations JSONB DEFAULT '{}'
);

CREATE TABLE if NOT EXISTS  tags (
    id SERIAL PRIMARY KEY,
    tagname TEXT UNIQUE
);

CREATE TABLE  if NOT EXISTS productTag (
    product_id INT NOT NULL REFERENCES products(id),
    tag_id INT NOT NULL REFERENCES tags(id),
    PRIMARY KEY (product_id, tag_id)
);

INSERT INTO products (filename, dimensionSize, type, path, metadata) VALUES
    ('Logitech_G_Pro_X_Superlight', '600x600', 'image/png', '/media/Logitech_G_Pro_X_Superlight.png', '{}'),
    ('Razer_DeathAdder_V3_Pro', '600x600', 'image/png', '/media/Razer_DeathAdder_V3_Pro.png', '{}'),
    ('SteelSeries_Apex_Pro_TKL', '350x286', 'image/png', '/media/SteelSeries_Apex_Pro_TKL.png', '{}'),
    ('Corsair_K70_RGB_MK2', '1024x1024', 'image/png', '/media/Corsair_K70_RGB_MK2.png', '{}'),
    ('ASUS_ROG_Swift_PG259QN', '284x284', 'image/png', '/media/ASUS_ROG_Swift_PG259QN.png', '{}'),
    ('LG_UltraGear_27GN950', '400x500', 'image/png', '/media/LG_UltraGear_27GN950.png', '{}'),
    ('NVIDIA_GeForce_RTX_4090', '1788x1610', 'image/png', '/media/NVIDIA_GeForce_RTX_4090.png', '{}'),
    ('AMD_Radeon_RX_7900_XTX', '640x640', 'image/png', '/media/AMD_Radeon_RX_7900_XTX.png', '{}'),
    ('ASUS_ROG_Strix_G16', '600x600', 'image/png', '/media/ASUS_ROG_Strix_G16.png', '{}'),
    ('MSI_Raider_GE78_HX', '1000x649', 'image/png', '/media/MSI_Raider_GE78_HX.png', '{}'),
    ('Apple_MacBook_Air_M2', '1080x1080', 'image/png', '/media/Apple_MacBook_Air_M2.png', '{}'),
    ('Dell_XPS_13', '541x320', 'image/png', '/media/Dell_XPS_13.png', '{}'),
    ('Samsung_QN90C_Neo_QLED', '600x600', 'image/png', '/media/Samsung_QN90C_Neo_QLED.png', '{}'),
    ('LG_OLED_C3', '600x600', 'image/png', '/media/LG_OLED_C3.png', '{}'),
    ('Intel_Core_i9_13900K', '700x700', 'image/png', '/media/Intel_Core_i9_13900K.png', '{}'),
    ('AMD_Ryzen_9_7950X', '828x534', 'image/png', '/media/AMD_Ryzen_9_7950X.png', '{}'),
    ('Corsair_Vengeance_32GB_DDR5', '828x828', 'image/png', '/media/Corsair_Vengeance_32GB_DDR5.png', '{}'),
    ('G_Skill_Trident_Z5_RGB_32GB', '828x444', 'image/png', '/media/G_Skill_Trident_Z5_RGB_32GB.png', '{}'),
    ('ASUS_ROG_Strix_Z790E', '2000x2000', 'image/png', '/media/ASUS_ROG_Strix_Z790E.png', '{}'),
    ('MSI_MEG_X670E_Ace', '550x619', 'image/png', '/media/MSI_MEG_X670E_Ace.png', '{}'),
    ('Lian_Li_O11_Dynamic_EVO', '794x726', 'image/png', '/media/Lian_Li_O11_Dynamic_EVO.png', '{}'),
    ('NZXT_H7_Flow', '828x828', 'image/png', '/media/NZXT_H7_Flow.png', '{}'),
    ('Dell_XPS_Desktop', '235x402', 'image/png', '/media/Dell_XPS_Desktop.png', '{}'),
    ('HP_Omen_45L', '319x307', 'image/png', '/media/HP_Omen_45L.png', '{}') 
ON CONFLICT (filename) DO UPDATE SET
    dimensionSize = EXCLUDED.dimensionSize,
    type = EXCLUDED.type,
    path = EXCLUDED.path,
    metadata = EXCLUDED.metadata,
    transformations = EXCLUDED.transformations;

INSERT INTO tags (tagname) VALUES
    ('Desktop'), ('Mouse'), ('Keyboard'), ('Laptop'), ('Gaming'),
    ('School'), ('Monitor'), ('Component'), ('Graphics card'), ('Smart TV'),
    ('Processor'), ('RAM'), ('Motherboard'), ('Cases'), ('White'), ('Black')
ON CONFLICT (tagname) DO NOTHING;

INSERT INTO productTag (product_id, tag_id) VALUES
     -- Mice
     ((SELECT id FROM products WHERE filename = 'Logitech_G_Pro_X_Superlight'), (SELECT id FROM tags WHERE tagname = 'Mouse')),
     ((SELECT id FROM products WHERE filename = 'Razer_DeathAdder_V3_Pro'), (SELECT id FROM tags WHERE tagname = 'Mouse')),

     -- Keyboards
     ((SELECT id FROM products WHERE filename = 'SteelSeries_Apex_Pro_TKL'), (SELECT id FROM tags WHERE tagname = 'Keyboard')),
     ((SELECT id FROM products WHERE filename = 'Corsair_K70_RGB_MK2'), (SELECT id FROM tags WHERE tagname = 'Keyboard')),

     -- Monitors
     ((SELECT id FROM products WHERE filename = 'ASUS_ROG_Swift_PG259QN'), (SELECT id FROM tags WHERE tagname = 'Monitor')),
     ((SELECT id FROM products WHERE filename = 'LG_UltraGear_27GN950'), (SELECT id FROM tags WHERE tagname = 'Monitor')),

     -- Graphics Cards
     ((SELECT id FROM products WHERE filename = 'NVIDIA_GeForce_RTX_4090'), (SELECT id FROM tags WHERE tagname = 'Graphics card')),
     ((SELECT id FROM products WHERE filename = 'AMD_Radeon_RX_7900_XTX'), (SELECT id FROM tags WHERE tagname = 'Graphics card')),

     -- Laptops
     ((SELECT id FROM products WHERE filename = 'ASUS_ROG_Strix_G16'), (SELECT id FROM tags WHERE tagname = 'Laptop')),
     ((SELECT id FROM products WHERE filename = 'MSI_Raider_GE78_HX'), (SELECT id FROM tags WHERE tagname = 'Laptop')),
     ((SELECT id FROM products WHERE filename = 'Apple_MacBook_Air_M2'), (SELECT id FROM tags WHERE tagname = 'Laptop')),
     ((SELECT id FROM products WHERE filename = 'Dell_XPS_13'), (SELECT id FROM tags WHERE tagname = 'Laptop')),
    
     -- Smart TV
     ((SELECT id FROM products WHERE filename = 'Samsung_QN90C_Neo_QLED'), (SELECT id FROM tags WHERE tagname = 'Smart TV')),
     ((SELECT id FROM products WHERE filename = 'LG_OLED_C3'), (SELECT id FROM tags WHERE tagname = 'Smart TV')),

     -- Processors
     ((SELECT id FROM products WHERE filename = 'Intel_Core_i9_13900K'), (SELECT id FROM tags WHERE tagname = 'Processor')),
     ((SELECT id FROM products WHERE filename = 'AMD_Ryzen_9_7950X'), (SELECT id FROM tags WHERE tagname = 'Processor')),

     -- RAM
     ((SELECT id FROM products WHERE filename = 'Corsair_Vengeance_32GB_DDR5'), (SELECT id FROM tags WHERE tagname = 'RAM')),
     ((SELECT id FROM products WHERE filename = 'G_Skill_Trident_Z5_RGB_32GB'), (SELECT id FROM tags WHERE tagname = 'RAM')),

     -- Motherboards
     ((SELECT id FROM products WHERE filename = 'ASUS_ROG_Strix_Z790E'), (SELECT id FROM tags WHERE tagname = 'Motherboard')),
     ((SELECT id FROM products WHERE filename = 'MSI_MEG_X670E_Ace'), (SELECT id FROM tags WHERE tagname = 'Motherboard')),

     -- PC Cases
     ((SELECT id FROM products WHERE filename = 'Lian_Li_O11_Dynamic_EVO'), (SELECT id FROM tags WHERE tagname = 'Cases')),
     ((SELECT id FROM products WHERE filename = 'NZXT_H7_Flow'), (SELECT id FROM tags WHERE tagname = 'Cases')),

     -- Desktops
     ((SELECT id FROM products WHERE filename = 'Dell_XPS_Desktop'), (SELECT id FROM tags WHERE tagname = 'Desktop')),
     ((SELECT id FROM products WHERE filename = 'HP_Omen_45L'), (SELECT id FROM tags WHERE tagname = 'Desktop')),

     -- Additional Tags (Gaming, School, Color)
     ((SELECT id FROM products WHERE filename = 'Logitech_G_Pro_X_Superlight'), (SELECT id FROM tags WHERE tagname = 'Gaming')),
     ((SELECT id FROM products WHERE filename = 'Corsair_K70_RGB_MK2'), (SELECT id FROM tags WHERE tagname = 'Gaming')),
     ((SELECT id FROM products WHERE filename = 'ASUS_ROG_Swift_PG259QN'), (SELECT id FROM tags WHERE tagname = 'Gaming')),
     ((SELECT id FROM products WHERE filename = 'Apple_MacBook_Air_M2'), (SELECT id FROM tags WHERE tagname = 'School')),
     ((SELECT id FROM products WHERE filename = 'Dell_XPS_13'), (SELECT id FROM tags WHERE tagname = 'School')),
     ((SELECT id FROM products WHERE filename = 'Lian_Li_O11_Dynamic_EVO'), (SELECT id FROM tags WHERE tagname = 'White')),
     ((SELECT id FROM products WHERE filename = 'NZXT_H7_Flow'), (SELECT id FROM tags WHERE tagname = 'Black'))
            
ON CONFLICT (product_id, tag_id) DO NOTHING;