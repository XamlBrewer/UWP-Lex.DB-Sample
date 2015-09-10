namespace XamlBrewer.Uwp.LexDbSample.DataAccessLayer
{
    using Lex.Db;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Windows.ApplicationModel;
    using Windows.Storage;

    public static class Dal
    {
        private static DbInstance db;

        static Dal()
        {
            // Create database
            db = new DbInstance("Storage", ApplicationData.Current.RoamingFolder);

            // Define table mapping
            // * 1st parameter is primary key
            // * 2nd parameter is autoGen e.g. auto-increment
            db.Map<VintageMuscleCar>().Automap(p => p.Id, true);

            // Initialize database
            db.Initialize();
        }

        public static IEnumerable<VintageMuscleCar> GetCars()
        {
            return db.Table<VintageMuscleCar>();
        }

        public static VintageMuscleCar GetCarById(int id)
        {
            return db.Table<VintageMuscleCar>().LoadByKey(id);
        }

        public static int SaveCar(VintageMuscleCar car)
        {
            db.Table<VintageMuscleCar>().Save(car);
            return car.Id;
        }

        public static Task SaveCars(IEnumerable<VintageMuscleCar> cars)
        {
            return db.Table<VintageMuscleCar>().SaveAsync(cars);
        }

        public static Task DeleteCars(IEnumerable<VintageMuscleCar> cars)
        {
            return db.Table<VintageMuscleCar>().DeleteAsync(cars);
        }

        public async static Task ResetCars()
        {
            // Clear
            db.Purge<VintageMuscleCar>();

            // Repopulate
            var cars = await Dal.DefaultCars();
            await Dal.SaveCars(cars);
        }

        public async static Task<List<VintageMuscleCar>> DefaultCars()
        {
            var cars = new List<VintageMuscleCar>();

            var car = new VintageMuscleCar();
            car.Name = "1965 Corvette C2 Stingray";
            car.ReleaseDate = new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            car.Description = "...";
            StorageFile file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1965_Corvette_C2_Stingray.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            car = new VintageMuscleCar();
            car.Name = "1965 Dodge Challenger";
            car.ReleaseDate = new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            car.Description = "...";
            file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1965_Dodge_Challenger.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            car = new VintageMuscleCar();
            car.Name = "1965 Ford Mustang Coupe";
            car.ReleaseDate = new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            car.Description = "...";
            file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1965_Ford_Mustang_Coupe.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            car = new VintageMuscleCar();
            car.Name = "1965 Ford Mustang Fastback";
            car.ReleaseDate = new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            car.Description = "...";
            file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1965_Ford_Mustang_Fastback.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            car = new VintageMuscleCar();
            car.Name = "1965 Pontiac GTO";
            car.ReleaseDate = new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            car.Description = "...";
            file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1965_Pontiac_GTO.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            car = new VintageMuscleCar();
            car.Name = "1965 Shelby Cobra 427";
            car.ReleaseDate = new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            car.Description = "...";
            file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1965_Shelby_Cobra_427.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            return cars;
        }
    }
}
