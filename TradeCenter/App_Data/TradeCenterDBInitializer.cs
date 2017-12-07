using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using TradeCenter.Models;

namespace TradeCenter
{
    public class TradeCenterDBInitializer : DropCreateDatabaseIfModelChanges<TradeCenterDB>
    {
        private static Random _rand = new Random();

        protected override void Seed(TradeCenterDB context)
        {
            GenerateUsers(context, 10);
            GenerateProducts(context);
        }

        private void GenerateUsers(TradeCenterDB context, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                var user = context.Users.Add(new User()
                {
                    FirstName = GetRandomFirstName(),
                    LastName = GetRandomLastName(),
                    BirthDate = GetRandomDate(-120, -18),
                });

                user.Email = $"{user.FirstName}.{user.LastName}@gmail.com";
                user.UserName = $"user{i:D2}";
                user.Password = $"pass{i:D2}";
                user.ConfirmPassword = user.Password;
            }

            context.SaveChanges();
        }

        private void GenerateProducts(TradeCenterDB context)
        {
            int count = 0;
            _products.ForEach(p =>
            {
                count++;

                // Assign the first 2 products to user with ID = 1 
                p.OwnerID = count < 3 ? 1 : GetRandomUserId(context);

                p.DatePublished = GetRandomDate(-1);

                p.Picture1 = ImageFileToByteArray($"Content/Images/Products/{count:D2}/{count * 10 + 1}.jpg");
                p.Picture1_MimeType = "image/jpg";

                p.Picture2 = ImageFileToByteArray($"Content/Images/Products/{count:D2}/{count * 10 + 2}.jpg");
                p.Picture2_MimeType = "image/jpg";

                p.Picture3 = ImageFileToByteArray($"Content/Images/Products/{count:D2}/{count * 10 + 3}.jpg");
                p.Picture3_MimeType = "image/jpg";

                p.State = ProductState.Avaialble;

                context.Products.Add(p);
            });

            context.SaveChanges();
        }

        private long GetRandomUserId(TradeCenterDB context)
        {
            return context.Users
                .OrderBy(u => u.ID)
                .Skip(_rand.Next(0, context.Users.Count()))
                .Select(u => u.ID)
                .First();
        }

        private static string GetRandomFirstName()
        {
            return _firstNames[_rand.Next(0, _firstNames.Count)];
        }

        private static string GetRandomLastName()
        {
            return _lastNames[_rand.Next(0, _lastNames.Count)];
        }

        private static DateTime GetRandomDate(int startRangeInYears, int endRangeInYears = 0)
        {
            return DateTime.Now.Add(new TimeSpan(
                _rand.Next(startRangeInYears * 365, endRangeInYears * 365), 0, 0, 0)).Date;
        }

        private static byte[] ImageFileToByteArray(string fileName)
        {
            fileName = HttpContext.Current.Server.MapPath($"~/{fileName}");

            Image image = Image.FromFile(fileName);

            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        #region Data Lists
        private static List<string> _firstNames = new List<string>
        {
            "James", "John", "Robert", "Michael", "William", "David", "Richard", "Charles", "Joseph", "Thomas", "Christopher", "Daniel", "Paul", "Mark", "Donald", "George", "Kenneth", "Steven", "Edward", "Brian", "Ronald", "Anthony", "Kevin", "Jason", "Matthew", "Gary", "Timothy", "Jose", "Larry", "Jeffrey", "Frank", "Scott", "Eric", "Stephen", "Andrew", "Raymond", "Gregory", "Joshua", "Jerry", "Dennis", "Walter", "Patrick", "Peter", "Harold", "Douglas", "Henry", "Carl", "Arthur", "Ryan", "Roger", "Mary", "Patricia", "Linda", "Barbara", "Elizabeth", "Jennifer", "Maria", "Susan", "Margaret", "Dorothy", "Lisa", "Nancy", "Karen", "Betty", "Helen", "Sandra", "Donna", "Carol", "Ruth", "Sharon", "Michelle", "Laura", "Sarah", "Kimberly", "Deborah", "Jessica", "Shirley", "Cynthia", "Angela", "Melissa", "Brenda", "Amy", "Anna", "Rebecca", "Virginia", "Kathleen", "Pamela", "Martha", "Debra", "Amanda", "Stephanie", "Carolyn", "Christine", "Marie", "Janet", "Catherine", "Frances", "Ann", "Joyce", "Diane"
        };

        private static List<string> _lastNames = new List<string>
        {
            "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King", "Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter", "Mitchell", "Perez", "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins", "Stewart", "Sanchez", "Morris", "Rogers", "Reed", "Cook", "Morgan", "Bell", "Murphy", "Bailey", "Rivera", "Cooper", "Richardson", "Cox", "Howard", "Ward", "Torres", "Peterson", "Gray", "Ramirez", "James", "Watson", "Brooks", "Kelly", "Sanders", "Price", "Bennett", "Wood", "Barnes", "Ross", "Henderson", "Coleman", "Jenkins", "Perry", "Powell", "Long", "Patterson", "Hughes", "Flores", "Washington", "Butler", "Simmons", "Foster", "Gonzales", "Bryant", "Alexander", "Russell", "Griffin", "Diaz", "Hayes"
        };

        private static List<Product> _products = new List<Product>
        {
            new Product
            {
                Title = "Samsung UN65KS8000 65-Inch Premium Flat 4K SUHD TV",
                ShortDescription = "Samsung UN65KS8000 65-Inch 4K Ultra HD Smart LED TV (2016 Model)",
                LongDescription = "The Samsung 4K SUHD TV completely redefines the viewing experience with the revolutionary Quantum Dot nano-crystal technology. The KS8000 features innovations that produce a remarkable High Dynamic Range picture experience, regardless of room-light interference. Access your favorite streaming content services easier and faster with our premium design Smart Remote Controller",
                Price = 1499.99M
            },
            new Product
            {
                Title = "Sony XBR43X800D 43-Inch 4K Ultra HD TV",
                ShortDescription = "Sony XBR43X800D 43-Inch 4K Premium 4K HDR Ultra HD TV (2016 Model)",
                LongDescription = "Enjoy truly remarkable clarity, color and contrast with 4K HDR and TRILUMINOS Display for picture that’s closer than ever to real-world colors. Android TV adds Voice Search, Google Cast and a huge selection of apps, games and content from Google Play so you can watch your way.",
                Price = 648
            },
            new Product
            {
                Title = "LG Electronics Flat 55-Inch 4K Ultra HD Smart TV",
                ShortDescription = "LG Electronics OLED55B6P Flat 55-Inch 4K Ultra HD Smart OLED TV (2016 Model)",
                LongDescription = "Stop Watching. Start Seeing. With perfect black and intense color enhanced by both standard and Dolby Vision HDR, LG OLED brings you a world of beauty without compromise. The individually illuminating OLED pixels can brighten, dim and power off fully to achieve perfect black. That means contrast - the ratio between the lightest and darkest areas of the screen—is truly infinite. Perfect black is essential to a more lifelike image as well as the great shadow detail you can only get with OLED. LG OLED TVs display a color palette that virtually matches the vast range of hues seen in today’s high-end digital cinemas. With over a billion rich colors at its disposal, LG OLED TV delivers a theater-quality experience at home. OLED HDR delivers a stunning high dynamic range picture, including support for Dolby Vision content. Enjoy brilliant brights and the deepest darks for infinite contrast, rich color and an exceptional viewing experience, closer to what filmmakers intended. With their perfect black and cinematic color, LG OLED TVs have also earned prestigious Ultra HD Premium certification.",
                Price = 2297
            },
            new Product
            {
                Title = "KitchenAid Stand Mixer",
                ShortDescription = "KitchenAid Stand Mixer RRK150wh Artisan Tilt White 325-watt with 10 speeds",
                LongDescription = @"5-quart stainless steel bowl,
Tilt-back head for easy access to mixture,
Includes flat beater, dough hook, and wire whip; pouring shield not included,
Reconditioned by the manufacturer to 'like new' condition,
Measures 14 by 8-2/3 inches by 14 inches",
                Price = 199.99M
            },
            new Product
            {
                Title = "K&H Backpack Pet Carrier",
                ShortDescription = "K&H Manufacturing Comfy Go Backpack Pet Carrier",
                LongDescription = @"Allows you to carry your pet and have your hands free
Mesh windows for pet to see
Carriers break down in a snap for easy storage
Recommended for pets weighing up to 15 lbs
Safety leash included
Sherpa pad included
Color: Purple, black, lime green",
                Price =60.99M
            },
            new Product
            {
                Title = "Canon T6 DSLR Camera 18-55 & 75-300mm Lens",
                ShortDescription = "Canon EOS Rebel T6 DSLR Camera w/ EF-S 18-55mm + EF 75-300mm Lens Printer Bundle",
                LongDescription = "Share Photos that Impress - The camera with the quality your photos deserve, the EOS Rebel T6 can be ideal for smartphone or digital point-and-shoot camera users looking to step up their imaging game. It's equipped with an 18.0 Megapixel CMOS image sensor and the DIGIC 4+ Image Processor for highly detailed, vibrant photos and videos even in low light. Whether you're out on an adventure hike or snapping candids of your friends during a late night out, the EOS Rebel T6 can help you take photos you'll want to show off. Built-in Wi-Fi and NFC connectivity make it easy to get your favorite pictures up on select social media sites for your friends, family and the world to see. If you're new to DSLRs, Scene Intelligent Auto mode can conveniently and automatically adjust the camera's settings to suit your subject. Easy to use and simple to share with, the EOS Rebel T6 delivers high image quality that's sure to catch the audience's eye",
                Price =699
            },
            new Product
            {
                Title = "20 Inch Expandable Hardside Carry-On Luggage",
                ShortDescription = "Travelers Club Luggage Madison 20 Inch Hardside Expandable Carry-On",
                LongDescription = "Available in six attractive hues and a fashionable chevron print, the Travelers Club Madison 20 Inch Carry-On is constructed from durable ABS material designed to absorb impact under stress and prevent permanent dents by 'flexing' back to its original shape. Strong, flexible, and lightweight (6.3 LBS), this suitcase is an ideal companion piece for any weekend getaway. It is loaded with extra features. Expandable by up to 25% additional packing space, the main compartment features a fully-lined interior with crisscrossing tie-down straps to hold your contents securely and a zip-around garment divider. The 4-wheel spinners allow for complete upright mobility without putting any pressure on your arms, ensuring smooth travels to your destination.",
                Price =29.99M
            },
            new Product
            {
                Title = "Copper Frying Pan",
                ShortDescription = "2 Inch Deep Square Copper Frying Pan",
                LongDescription = "As Seen on TV Gotham Steel 2 Inch Deep Square Copper Frying Pan - BRAND NEW!",
                Price =16.99M
            },
            new Product
            {
                Title = "FPV Drone with HD Camera",
                ShortDescription = "DJI Phantom 3 Standard FPV Drone with 2.7K 12 Megapixel HD Camera",
                LongDescription = @"Easy to Fly: An intelligent flight system automatically keeps your Phantom 3 Standard in the air and under your control.
Amazing Images: Take stunning 2.7K HD videos and 12 Megapixel photos with the integrated aerial camera.
Stable Footage: DJI's advanced gimbal stabilization technology gives you movie-quality results no matter how you fly.
Enjoy the View: A live video feed gives you a 720p HD real-time view of what your camera sees right on your mobile device.
Peace of Mind: Fly up to 25 minutes on a single charge, and the Intelligent Flight Battery will automatically remind you when power is running low.",
                Price =335
            },
            new Product
            {
                Title = "2010 Chevrolet Corvette Grand Sport Coupe 2-Door",
                ShortDescription = "2010 CHEVY CORVETTE Z16 GRAND SPORT 3LT Z51 NAV HUD 18K #106444 Texas Direct",
                LongDescription = @"Vehicle Features:
Z51 Performance Package
6.2L V8 Engine
Automatic Transmission
Two-Tone Leather Seats
Power Driver Seat
Heated Front Seats
Leather Steering Wheel Trim
Cruise Control
Audio Steering Wheel Controls
Bluetooth Connectivity
CD Audio System
BOSE Sound System
Navigation System
Keyless Ignition
Power Door Locks
Power Windows
Power Exterior Mirrors
Fog Lights
Xenon Headlights
Chrome Alloy Wheels
One Key
No Floormats
No Books",
                Price =30100
            },
            new Product
            {
                Title = "Samsung Gear S2 Classic T-Mobile Smartwatch",
                ShortDescription = "Samsung Gear S2 Classic T-Mobile Smartwatch w/ Leather Band LARGE Black SM-735T",
                LongDescription = "A classic take on a modern innovation, the Samsung Gear S2 classic combines the look and feel of a traditional wristwatch with the technological advances and convenience of a smartwatch. The Gear S2 classic is super easy to navigate, thanks to a rotating circular bezel that lets you move between notification, apps, and widgets with ease and speed. It also keeps you up to date and in touch with text messages, incoming calls, social media alerts, and even news updates delivered right to your wrist. The Samsung Gear S2 classic also helps you stay in shape and reach your fitness goals with the built-in S Health app. The S Health app monitors your daily activity and heart rate, as well as reminds you when you've been sitting still for too long. With elegant curves, a sleek finish, and personalized watch faces and bands, the Samsung Gear S2 classic is designed to keep up with the speed and style of your life.",
                Price =125.99M
            },
            new Product
            {
                Title = "Jacob Bromwell All-American Flour Sifter",
                ShortDescription = "Sift up to 5 cups of flour in a design that remains true to the original.",
                LongDescription = @"•Surface: Non-reactive
•Materials:
•Handle attachment: Riveted
•Care instructions: Hand wash
•Finish: Mirror
•Dimensions: 7.5 inches long x 7 inches wide x 6.25 inches high 
•Our most famous and instantly recognizable product. Undoubtedly, the All-American Flour Sifter is our most famous and instantly recognizable product, which can be found in millions of American households
•It's still made the good old fashioned way. The handle is attached to the sifter using old-fashioned metalworking techniques, just like the sifters made by our company nearly 200 years ago
•Our secret is the uniquely-designed 4-wire agitator. Each wire smoothly sweeps the screen below it, creating the smoothest flour possible for a consistent measure every time
•It's easy to use. Just pour in your desired amount of wheat, acorn, almond, chestnut, maize or rough whole grain flour, turn the handle and fill your mixing bowl. You can also fill the sifter with powdered sugar for a dust on top of a cake, pie, or whatever else you're cooking
•It's guaranteed not to rust. Built out of quality stainless steel and guaranteed not to rust
•Product care: Hand wash only. Do not place in dishwasher
•This product is guaranteed for life. Your purchase is backed by our 100-percent Lifetime Guarantee, so you can buy with confidence
•It's proudly Still made in the USA, even after all these years. Buy American and save jobs
",
                Price =79.98M
            }
        };

        #endregion //Data Lists
    }
}