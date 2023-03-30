using Microsoft.EntityFrameworkCore;

using Store.Models;

namespace Store.Data;

public static class SeedData
{
    public static async Task SeedDataAsync(IApplicationBuilder app)
    {
        StoreDbContext context = app.ApplicationServices
                                    .CreateScope()
                                    .ServiceProvider
                                    .GetRequiredService<StoreDbContext>();

        if(!context.categories.Any())
        {
            try
            {
                await context.AddRangeAsync
                (
                    new Category() {categoryId = 1, name = "Комплектующие", Icon = "icons/computer.svg"},
                    new Category() {categoryId = 2, name = "Периферия", Icon = "icons/mouse.svg" },
                    new Category() {categoryId = 3, name = "Сетевое оборудование", Icon = "icons/router.svg" }
                );
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException db)
            {
                System.Diagnostics.Debug.WriteLine(db);
            }
        }

        if (!context.subcategories.Any())
        {
            try
            {
                await context.AddRangeAsync
                (
                    //комплектующие  
                    new SubCategory () {typeId = 1, categoryId = 1, name = "Процессоры", Image = "catalog/cpu.webp"},
                    new SubCategory () {typeId = 2, categoryId = 1, name = "Видеокарты", Image = "catalog/gpu.webp"},
                    new SubCategory () {typeId = 3, categoryId = 1, name = "Память", Image = "catalog/ram.webp"},
                    new SubCategory () {typeId = 4, categoryId = 1, name = "Материнские платы", Image = "catalog/motherboard.webp"},
                    //периферия
                    new SubCategory () {typeId = 5, categoryId = 2, name = "Мониторы", Image = "catalog/monitor.webp"},
                    new SubCategory () {typeId = 6, categoryId = 2, name = "Мыши", Image = "catalog/mouse.webp"},
                    new SubCategory () {typeId = 7, categoryId = 2, name = "Клавиатуры", Image = "catalog/keyboard.webp"},
                    //Сетвое оборудование
                    new SubCategory () {typeId = 8, categoryId = 3, name = "Маршрутизаторы и коммутаторы", Image = "catalog/switch.webp" },
                    new SubCategory () {typeId = 9, categoryId = 3, name = "Сетевые хранилища (NAS)", Image = "catalog/nas.webp"}
                );

                await context.SaveChangesAsync();
            }
            catch (DbUpdateException db)
            {
                System.Diagnostics.Debug.WriteLine(db);
            }
        }                                     
            
        if (!context.products.Any())
        {
            try
            {
                await context.AddRangeAsync
                (
                    //процессоры
                    new Product
                    {
                        typeId = 1, name = "Процессор AMD Ryzen 5 5600X OEM", price = 13090,
                        productDescription = 
                        @"{""Socket"": ""AM4"", ""Количество ядер"": 6, ""Частота"": ""3700 МГц"", 
                        ""Технологический процесс"": ""7 нм"", ""TDP"": ""65 Вт""}"
                    },
                    new Product 
                    {
                        typeId = 1, name = "Процессор Intel Core i3 - 12100F OEM", price = 7970,
                        productDescription =
                         @"{""Socket"": ""LGA 1700"", ""Количество ядер"": 4, ""Частота"": ""3300 МГц"",
                         ""Технологический процесс"": ""10 нм"", ""TDP"": ""89 Вт""}"
                    },
                    new Product 
                    {
                        typeId = 1, name = "Процессор AMD Ryzen 5 5900X OEM", price = 23930,
                        productDescription = 
                        @"{""Socket"": ""AM4"", ""Количество ядер"": 12, ""Частота"": ""3700 МГц"", 
                        ""Технологический процесс"": ""7 нм"", ""TDP"": ""105 Вт""}" 
                    },
                    new Product 
                    {
                        typeId = 1, name = "Процессор Intel Core i7 - 12700KF OEM", price = 28210,
                        productDescription = 
                        @"{""Socket"": ""LGA 1700"", ""Количество ядер"": 12, ""Частота"": ""3600 МГц"",
                        ""Технологический процесс"": ""10 нм"", ""TDP"": ""190 Вт""}"
                    },
                    new Product 
                    {
                        typeId = 1, name = "Процессор Intel Core i7 - 13700K OEM", price = 37070,
                        productDescription = 
                        @"{""Socket"": ""LGA 1700"", ""Количество ядер"": 16, ""Частота"": ""3400 МГц"",
                        ""Технологический процесс"": ""10 нм"", ""TDP"": ""125 Вт""}"
                    },
                    new Product 
                    {
                        typeId = 1, name = "Процессор AMD Ryzen 9 7950X BOX", price = 62950,
                        productDescription = 
                        @"{""Socket"": ""AM5"", ""Количество ядер"": 16, ""Частота"": ""4500 МГц"",
                        ""Технологический процесс"": ""5 нм"", ""TDP"": ""170 Вт""}"  
                    },
                    new Product 
                    {
                        typeId = 1, name = "Процессор AMD Ryzen 7 7700X OEM", price = 34950,
                        productDescription = @"{""Socket"": ""AM5"", ""Количество ядер"": 8, ""Частота"": ""4500 МГц"",
                        ""Технологический процесс"": ""5 нм"", ""TDP"": ""105 Вт""}"
                    },
                    new Product 
                    {
                        typeId = 1, name = "Процессор Intel Core i9 - 12900K OEM", price = 43350,
                        productDescription = 
                        @"{""Socket"": ""LGA 1700"", ""Количество ядер"": 16, ""Частота"": ""3200 МГц"",
                        ""Технологический процесс"": ""10 нм"", ""TDP"": ""105 Вт""}"
                    },
                    //видеокарты 
                    new Product
                    {
                        typeId = 2, name = "Видеокарта NVIDIA Quadro RTX 8000 48Gb OEM", price = 545830,
                        productDescription = 
                        @"{""Интерфейс"": ""PCI-E 3.0"", ""Частота ядра"": ""1230 МГц"", 
                        ""Память"": ""48 Гб"", ""Типа памяти"": ""GDDR6"", ""Частота видеопамяти"": ""14000 МГц"", 
                        ""Разрядность шины"": ""384-бит""}"
                    },
                    new Product
                    {
                        typeId = 2, name = "Видеокарта NVIDIA GeForce RTX 4090 KFA2 SG 1-Click OC 24Gb", price = 156500,
                        productDescription = 
                        @"{""Интерфейс"": ""PCI-E 4.0"", ""Частота ядра"": ""2235 МГц"", 
                        ""Память"": ""24 Гб"", ""Тип памяти"": ""GDDR6X"", ""Частота видеопамяти"": ""21000 МГц"", 
                        ""Разрядность шины"": ""384-бит""}"
                    },
                    new Product
                    {
                        typeId = 2, name = "Видеокарта NVIDIA GeForce RTX 4090 Gigabyte 24Gb", price = 155300,
                        productDescription = 
                         @"{""Интерфейс"": ""PCI-E 4.0"", ""Частота ядра"": ""2135 МГц"", 
                        ""Память"": ""24 Гб"", ""Тип памяти"": ""GDDR6X"", ""Частота видеопамяти"": ""21000 МГц"", 
                        ""Разрядность шины"": ""384-бит""}"
                    },
                    new Product
                    {
                        typeId = 2, name = "Видеокарта AMD Instinct MI100 32Gb OEM", price = 517850,
                        productDescription = 
                        @"{""Интерфейс"": ""PCI-E 4.0"", ""Частота ядра"": ""1502 МГц"", 
                        ""Память"": ""32 Гб"", ""Тип памяти"": ""HBM2"", ""Разрядность шины"": ""4096-бит""}"
                    },
                    new Product
                    {
                        typeId = 2, name = "Видеокарта AMD Radeon RX 6900 XT ASRock OC Formula 16Gb", price = 78890,
                        productDescription = 
                        @"{""Интерфейс"": ""PCI-E 4.0"", ""Частота ядра"": ""2125 МГц"", 
                        ""Память"": ""16 Гб"", ""Тип памяти"": ""GDDR6"", ""Частота видеопамяти"": ""16000 МГц"", 
                        ""Разрядность шины"": ""256-бит""}"
                    },
                    new Product
                    {
                        typeId = 2, name = "Видеокарта AMD Radeon RX 6800 MSI 16Gb", price = 58170,
                        productDescription = 
                        @"{""Интерфейс"": ""PCI-E 4.0"", ""Частота ядра"": ""1850 МГц"", 
                        ""Память"": ""16 Гб"", ""Тип памяти"": ""GDDR6"", ""Частота видеопамяти"": ""16000 МГц"", 
                        ""Разрядность шины"": ""256-бит""}"
                    }
                );

                await context.SaveChangesAsync();
            }
            catch(DbUpdateException db)
            {
                System.Diagnostics.Debug.WriteLine(db);
            }  
        }
    } 
}
