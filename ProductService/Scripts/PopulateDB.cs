namespace ProductService.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;

    public class PopulateDB
    {
        public class PdpItem
        {
            public PdpItem(
                int productId,
                string productName,
                string splashImgUrl,
                int price,
                int? discountPrice,
                Variant[] variants,
                Image[] imageOptions,
                string productDescription,
                string productBrand,
                string brandDescription,
                string materials,
                string gender)
            {
                this.ProductId = productId;
                this.ProductName = productName;
                this.SplashImgUrl = splashImgUrl;
                this.Price = price;
                this.DiscountPrice = discountPrice;
                this.Variants = variants;
                this.ImageOptions = imageOptions;
                this.ProductDescription = productDescription;
                this.ProductBrand = productBrand;
                this.BrandDescription = brandDescription;
                this.Materials = materials;
                this.Gender = gender;
            }

            public int ProductId { get; set; }

            public string ProductName { get; set; }

            public string SplashImgUrl { get; set; }

            public int Price { get; set; }

            public int? DiscountPrice { get; set; }

            public Variant[] Variants { get; set; }

            public Image[] ImageOptions { get; set; }

            public string ProductDescription { get; set; }

            public string ProductBrand { get; set; }

            public string BrandDescription { get; set; }

            public string Materials { get; set; }

            public string Gender { get; set; }

            public class Variant
            {
                public int VariantId { get; set; }

                public Variant()
                {
                }

                public Variant(int id)
                {
                    this.VariantId = id;
                }
            }

            public class Image
            {
                public Image()
                {
                }

                public Image(string colour, string[] imageList)
                {
                    this.Colour = colour;
                    this.ImageList = imageList;
                }

                public string Colour { get; set; }

                public string[] ImageList { get; set; }
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() != typeof(PdpItem))
                {
                    return false;
                }

                return this.ToString().Equals(obj.ToString());
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

//        public static List<PdpItem> GeneratePdpDbData()
//        {
//            List<PdpItem> pdpCollection = new List<PdpItem>();
//
//            PdpItem pdpItem1 = new PdpItem
//                                   {
//                                       ProductId = 1,
//                                       ProductName = "Warehouse Side Split Roll Neck Jumper",
//                                       SplashImgUrl = "./static/media/prod1-img1.jpg",
//                                       Price = 4600,
//                                       DiscountPrice = null,
//                                       Variants =
//                                           new[]
//                                               {
//                                                   new PdpItem.Variant { VariantId = 1, },
//                                                   new PdpItem.Variant { VariantId = 2, },
//                                                   new PdpItem.Variant { VariantId = 3, },
//                                                   new PdpItem.Variant { VariantId = 4, },
//                                                   new PdpItem.Variant { VariantId = 5, },
//                                                   new PdpItem.Variant { VariantId = 6, }
//                                               },
//                                       ImageOptions =
//                                           new[]
//                                               {
//                                                   new PdpItem.Image
//                                                       {
//                                                           Colour = "Olive marl",
//                                                           ImageList =
//                                                               new[]
//                                                                   {
//                                                                       "../static/media/prod1-img1.jpg",
//                                                                       "../static/media/prod1-img2.jpg",
//                                                                       "../static/media/prod1-img3.jpg",
//                                                                       "../static/media/prod1-img4.jpg",
//                                                                   }
//                                                       }
//                                               },
//                                       ProductDescription =
//                                           "Jumper by Warehouse, Textured knit, Cowl neck, Raglan sleeves, Ribbed trims, Split hem, Regular fit - true to size",
//                                       ProductBrand = "Warehouse",
//                                       BrandDescription =
//                                           "Delivering seasonal trends for the high street, Warehouse offer a collection of directional pieces, with vibrant prints and clean cut tailoring channelling the brand's signature style. Statement party dresses sit alongside classic wardrobe staples, all complemented by a key range of jewellery and accessories, including an exclusive edit of three satchel bags.",
//                                       Materials = "55% Polyamide, 40% Acrylic, 5% Wool",
//                                       Gender = "Women",
//                                   };
//
//            PdpItem pdpItem2 = new PdpItem
//                                   {
//                                       ProductId = 2,
//                                       ProductName =
//                                           "French Connection Checked Lined Harrington Jacket with Borg Collar",
//                                       SplashImgUrl = "./static/media/prod2-img1.jpg",
//                                       Price = 12000,
//                                       DiscountPrice = 6000,
//                                       Variants =
//                                           new[]
//                                               {
//                                                   new PdpItem.Variant { VariantId = 7, },
//                                                   new PdpItem.Variant { VariantId = 8, },
//                                                   new PdpItem.Variant { VariantId = 9, },
//                                                   new PdpItem.Variant { VariantId = 10, },
//                                                   new PdpItem.Variant { VariantId = 11, },
//                                                   new PdpItem.Variant { VariantId = 12, }
//                                               },
//                                       ImageOptions =
//                                           new[]
//                                               {
//                                                   new PdpItem.Image
//                                                       {
//                                                           Colour = "Black",
//                                                           ImageList =
//                                                               new[]
//                                                                   {
//                                                                       "../static/media/prod2-img1.jpg",
//                                                                       "../static/media/prod2-img2.jpg",
//                                                                       "../static/media/prod2-img3.jpg",
//                                                                       "../static/media/prod2-img4.jpg",
//                                                                   }
//                                                       }
//                                               },
//                                       ProductDescription =
//                                           "Jacket by French Connection, Smooth woven fabric, Checked lining, Borg collar, Full zip fastening, Long sleeves, Ribbed cuffs, Functional pockets, Regular fit - true to size, Machine wash, 100% Cotton, Our model wears a size Medium and is 191cm/6'3 tall",
//                                       ProductBrand = "French Connection",
//                                       BrandDescription =
//                                           "Founded in 1972 by Stephen Marks and driven by innovation, French Connection is known for its wearable, intelligent collections and off-beat advertising campaigns. French Connection PLUS sees a fresh crop of jersey and outerwear in a variety of shapes and sizes also added to the mix.",
//                                       Materials = "Body: 100% Cotton",
//                                       Gender = "Men",
//                                   };
//
//            PdpItem pdpItem3 = new PdpItem
//                                   {
//                                       ProductId = 3,
//                                       ProductName = "Adidas Originals Trefoil Hoodie",
//                                       SplashImgUrl = "./static/media/prod3-img1.jpg",
//                                       Price = 5000,
//                                       DiscountPrice = null,
//                                       Variants =
//                                           new[]
//                                               {
//                                                   new PdpItem.Variant { VariantId = 13, },
//                                                   new PdpItem.Variant { VariantId = 14, },
//                                                   new PdpItem.Variant { VariantId = 15, },
//                                                   new PdpItem.Variant { VariantId = 16, },
//                                                   new PdpItem.Variant { VariantId = 17, },
//                                                   new PdpItem.Variant { VariantId = 18, },
//                                                   new PdpItem.Variant { VariantId = 19, },
//                                                   new PdpItem.Variant { VariantId = 20, },
//                                                   new PdpItem.Variant { VariantId = 21, },
//                                                   new PdpItem.Variant { VariantId = 22, },
//                                                   new PdpItem.Variant { VariantId = 23, },
//                                                   new PdpItem.Variant { VariantId = 24, }
//                                               },
//                                       ImageOptions =
//                                           new[]
//                                               {
//                                                   new PdpItem.Image
//                                                       {
//                                                           Colour = "Grey",
//                                                           ImageList =
//                                                               new[]
//                                                                   {
//                                                                       "../static/media/prod3-img1.jpg",
//                                                                       "../static/media/prod3-img2.jpg",
//                                                                       "../static/media/prod3-img3.jpg",
//                                                                       "../static/media/prod3-img4.jpg",
//                                                                   }
//                                                       },
//                                                   new PdpItem.Image
//                                                       {
//                                                           Colour = "Black",
//                                                           ImageList =
//                                                               new[]
//                                                                   {
//                                                                       "../static/media/prod3-img5.jpg",
//                                                                       "../static/media/prod3-img6.jpg",
//                                                                       "../static/media/prod3-img7.jpg",
//                                                                       "../static/media/prod3-img8.jpg",
//                                                                   }
//                                                       }
//                                               },
//                                       ProductDescription =
//                                           "Hoodie by adidas Originals, Supplier code: BS2229, Soft-touch sweat, Drawstring hood, Raglan sleeves, Over-the-head style, Pouch pockets, Ribbed trims, Regular fit - true to size,  Machine wash, Our model wears a size Medium and is 6'1”/185.5 cm tall",
//                                       ProductBrand = "Addidas Originals",
//                                       BrandDescription =
//                                           "With a brand history stretching back over 60 years, adidas Originals draw inspiration from street culture and retro styles to provide fresh vintage inspired menswear. The adidas Originals range incorporates everything from the brands most iconic trainers to new vintage inspired clothes.",
//                                       Materials =
//                                           "BFabric 1: 70% Cotton, 30% Polyester, Fabric 2: 97% Cotton, 3% Elastane, Lining: 100% Cotton.",
//                                       Gender = "Men",
//                                   };
//
//            PdpItem pdpItem4 = new PdpItem
//                                   {
//                                       ProductId = 4,
//                                       ProductName = "ASOS Body With Plunge Neck Long Sleeve And Thong",
//                                       SplashImgUrl = "./static/media/prod4-img1.jpg",
//                                       Price = 1800,
//                                       DiscountPrice = 1050,
//                                       Variants =
//                                           new[]
//                                               {
//                                                   new PdpItem.Variant { VariantId = 25, },
//                                                   new PdpItem.Variant { VariantId = 26, },
//                                                   new PdpItem.Variant { VariantId = 27, },
//                                                   new PdpItem.Variant { VariantId = 28, },
//                                                   new PdpItem.Variant { VariantId = 29, },
//                                                   new PdpItem.Variant { VariantId = 30, }
//                                               },
//                                       ImageOptions =
//                                           new[]
//                                               {
//                                                   new PdpItem.Image
//                                                       {
//                                                           Colour = "Black",
//                                                           ImageList =
//                                                               new[]
//                                                                   {
//                                                                       "../static/media/prod4-img1.jpg",
//                                                                       "../static/media/prod4-img2.jpg",
//                                                                       "../static/media/prod4-img3.jpg",
//                                                                       "../static/media/prod4-img4.jpg",
//                                                                   }
//                                                       }
//                                               },
//                                       ProductDescription =
//                                           "Body by ASOS Collection, Soft-touch jersey, Plunge neck, Thong cut, Press-stud fastening underneath, Close-cut bodycon fit, Machine wash",
//                                       ProductBrand = "ASOS",
//                                       BrandDescription =
//                                           "Score a wardrobe win no matter the dress code with our ASOS Collection own-label collection. From polished prom to the after party, our London-based design team scour the globe to nail your new-season fashion goals with need-right-now dresses, outerwear, shoes and denim in the coolest shapes and fits.",
//                                       Materials = "95% Viscose, 5% Elastane",
//                                       Gender = "Women",
//                                   };
//
//            PdpItem pdpItem5 = new PdpItem
//                                   {
//                                       ProductId = 5,
//                                       ProductName = "All Saints Oversized Zip Biker Jacket in Leather",
//                                       SplashImgUrl = "./static/media/prod5-img1.jpg",
//                                       Price = 42000,
//                                       DiscountPrice = null,
//                                       Variants =
//                                           new[]
//                                               {
//                                                   new PdpItem.Variant { VariantId = 31, },
//                                                   new PdpItem.Variant { VariantId = 32, },
//                                                   new PdpItem.Variant { VariantId = 33, },
//                                                   new PdpItem.Variant { VariantId = 34, }
//                                               },
//                                       ImageOptions =
//                                           new[]
//                                               {
//                                                   new PdpItem.Image
//                                                       {
//                                                           Colour = "Aries white",
//                                                           ImageList =
//                                                               new[]
//                                                                   {
//                                                                       "../static/media/prod5-img1.jpg",
//                                                                       "../static/media/prod5-img2.jpg",
//                                                                       "../static/media/prod5-img3.jpg",
//                                                                       "../static/media/prod5-img4.jpg",
//                                                                   }
//                                                       }
//                                               },
//                                       ProductDescription =
//                                           "Biker jacket by AllSaints, Smooth leather, Fully lined, Notch lapels, Asymmetric zip fastening, Zipped cuffs, Functional pockets, Belted hem, Oversized fit - falls generously over the body",
//                                       ProductBrand = "AllSaints",
//                                       BrandDescription =
//                                           "East London born and bred, AllSaints turned heads in the mid-90s with its investment leather jackets. Today, worn-in denim, draped cuts and classic jersey pieces reflect the laid-back attitude at the heart of the brand.",
//                                       Materials =
//                                           "100% Real Leather, Lining: 62% Viscose, 38% Cotton, Sleeve Lining: 100% Polyester.",
//                                       Gender = "Women",
//                                   };
//
//            pdpCollection.Add(pdpItem1);
//            pdpCollection.Add(pdpItem2);
//            pdpCollection.Add(pdpItem3);
//            pdpCollection.Add(pdpItem4);
//            pdpCollection.Add(pdpItem5);
//
//            return pdpCollection;
//        }
//
        public static async Task CreateDocumentCollection(
            DocumentClient client,
            string documentDbName,
            string collectionName)
        {
            await client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(documentDbName),
                new DocumentCollection { Id = collectionName }).ConfigureAwait(false);
        }

        public static async Task CreatePdpDocumentIfNotExists(
            DocumentClient client,
            string documentDBName,
            string collectionName,
            PdpItem pdpItem)
        {
            try
            {
                await client.ReadDocumentAsync(
                        UriFactory.CreateDocumentUri(documentDBName, collectionName, pdpItem.ProductId.ToString()))
                    .ConfigureAwait(false);
                Console.WriteLine("Found {0}", pdpItem.ProductId);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentAsync(
                        UriFactory.CreateDocumentCollectionUri(documentDBName, collectionName),
                        pdpItem).ConfigureAwait(false);
                    Console.WriteLine("Created PdpItem {0}", pdpItem.ProductId);
                }
                else
                {
                    throw;
                }
            }
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(
                    "Please write the following database information: [DatabaseName] [DatabaseEndpoint] [DatabaseKey] [CollectionName]");
            }

            var DatabaseName = args[0];
            var DatabaseEndpoint = args[1];
            var DatabaseKey = args[2];
            var collectionName = args[3];

            Console.WriteLine("The values you entered are:");
            Console.WriteLine("Database Name: " + DatabaseName);
            Console.WriteLine("Database Endpoint: " + DatabaseEndpoint);
            Console.WriteLine("Database Key: " + DatabaseKey);
            Console.WriteLine("Collection Name: " + collectionName);

            var client = new DocumentClient(new Uri(DatabaseEndpoint), DatabaseKey);
            CreateDocumentCollection(client, DatabaseName, collectionName).Wait();

//            var items = PopulateDB.GeneratePdpDbData();
//            foreach (PdpItem pdpItem in items)
//            {
//                CreatePdpDocumentIfNotExists(client, DatabaseName, collectionName, pdpItem).Wait();
//            }
        }
    }
}