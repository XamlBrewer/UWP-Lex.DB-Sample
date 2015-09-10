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
            car.Description = "The Chevrolet Corvette (C2) (C2 for Second Generation), also known as the Corvette Stingray, is a sports car that was produced by Chevrolet for the 1963 to 1967 model years. \n\nFor its third season, the 1965 Corvette Stingray further cleaned up style-wise and was muscled up with the addition of an all-new braking system and larger powerplants. 1965 styling alterations were subtle, confined to a smoothed -out hood now devoid of scoop indentations, a trio of working vertical exhaust vents in the front fenders that replaced the previous nonfunctional horizontal 'speedlines,' restyled wheel covers and rocker - panel moldings, and minor interior trim revisions.The 1965 Corvette Stingray became ferocious with the mid - year debut of a big-block V - 8, the 425 hp(317 kW) 396 in³ (6.5 L) ('big block') V8.";
            StorageFile file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1965_Corvette_C2_Stingray.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            car = new VintageMuscleCar();
            car.Name = "1969 Dodge Challenger";
            car.ReleaseDate = new DateTime(1969, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            car.Description = "The Challenger was Dodge's answer to the Mustang and Camaro. From 1969 to 1974, the first generation Dodge Challenger pony car was built using the Chrysler E platform, sharing major components with the Plymouth Barracuda. The Challenger was available as a two-door in either a hardtop coupe or a convertible body design, and in two models for its introductory year. The base model was the 'Challenger' with either a I6 or V8 engine, as well as a 'Challenger R/T' that included a 383 cu in (6.28 L) V8.";
            file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1969_Dodge_Challenger.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            car = new VintageMuscleCar();
            car.Name = "1965 Ford Mustang Coupe";
            car.ReleaseDate = new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            car.Description = "The first-generation Ford Mustang was manufactured by Ford from April 1964 until 1973. The introduction of the Mustang created a new class of automobile known as the pony car. The Mustang’s styling, with its long hood and short deck, proved wildly popular and inspired a host of imitators. It was initially introduced as a hardtop and convertible with the fastback version put on sale the following year. At the time of its introduction, the Mustang, sharing its underpinnings with the Falcon, was slotted into a compact car segment.\n\nWith each revision, the Mustang saw an increase in overall dimensions and in engine power.";
            file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1965_Ford_Mustang_Coupe.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            car = new VintageMuscleCar();
            car.Name = "1965 Ford Mustang Fastback";
            car.ReleaseDate = new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            car.Description = "The first-generation Ford Mustang was manufactured by Ford from April 1964 until 1973. The introduction of the Mustang created a new class of automobile known as the pony car. The Mustang’s styling, with its long hood and short deck, proved wildly popular and inspired a host of imitators. At the time of its introduction, the Mustang, sharing its underpinnings with the Falcon, was slotted into a compact car segment.\n\nA 'Fastback 2+2' model traded the conventional trunk space for increased interior volume as well as giving exterior lines similar to those of the second series of the Corvette Sting Ray and European sports cars such as the Jaguar E-Type.";
            file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1965_Ford_Mustang_Fastback.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            car = new VintageMuscleCar();
            car.Name = "1965 Pontiac GTO";
            car.ReleaseDate = new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            car.Description = "The GTO was the brainchild of Pontiac engineer Russell Gee, an engine specialist; Bill Collins, a chassis engineer; and Pontiac chief engineer John DeLorean. In early 1963, General Motors' management issued an edict banning divisions from involvement in auto racing. By the early 1960s, Pontiac's advertising and marketing approach was heavily based on performance, and racing was an important component of that strategy. With GM's ban on factory-sponsored racing, Pontiac's young, visionary management turned its attention to emphasizing street performance.\n\nThe name, which was DeLorean's idea, was inspired by the Ferrari 250 GTO, the successful race car. It is an Italian abbreviation for Gran Turismo Omologato, ('grand tourer homologated') which means officially certified for racing in the grand tourer class.";
            file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1965_Pontiac_GTO.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            car = new VintageMuscleCar();
            car.Name = "1965 Shelby Cobra 427";
            car.ReleaseDate = new DateTime(1965, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            car.Description = "The AC Cobra, often known colloquially as the Shelby Cobra, is an American sports car produced intermittently since 1962. Like many British specialist manufacturers, AC Cars had been using the Bristol straight-6 engine in its small-volume production, including its AC Ace two-seater roadster. Bristol decided in 1961 to cease production of its engine and instead to use Chrysler 331 cu in (5.4 L) V8 engines. AC started using the 2.6 litre Ford Zephyr engine in its cars.\n\nIn September 1961, American automotive designer Carroll Shelby wrote to AC asking if they would build him a car modified to accept a V8 engine. AC agreed, provided a suitable engine could be found. Shelby went to Chevrolet to see if they would provide him with engines, but not wanting to add competition to the Corvette they said no.";
            file = await StorageFile.GetFileFromPathAsync(Path.Combine(Package.Current.InstalledLocation.Path, @"Assets\Pictures\1965_Shelby_Cobra_427.jpg"));
            car.Picture = await file.AsByteArray();
            cars.Add(car);

            // Feel free to add the Camaro and Fitrebird ...

            return cars;
        }
    }
}
