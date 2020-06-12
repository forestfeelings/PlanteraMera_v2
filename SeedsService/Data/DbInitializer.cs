using SeedsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedsService.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SeedDbContext context)
        {
            // Kollar om databasen är skapad
            context.Database.EnsureCreated();

            // Kolla om det redan finns data
            if (context.Seeds.Any())
            {
                return; // Databasen har matats
            }

            // Skapa matning av data
            var seeds = new Seed[]
            {
                new Seed()
                {
                    Name = "Double Standard",
                    LatinName = "Zea mays var. saccharata",
                    BotanicalFamily = "Gräs - Poaceae",
                    DaysToDevelop = 73,
                    Annuality = "Ettårig",
                    Type = "Sockermajs",
                    Description = "En tidig, öppet pollinerad, tvåfärgad sockermajs. Den gror bättre i sval jord än de flesta sorter och klarar av mindre idealiskt majsväder. Kolvarna varierar lite, men är oftast kring 17 cm långa med 12-14 rader gula och vita korn. Ibland förekommer någon helgul kolv. Double Standard är en fin sort som är godare och sprödare än genomsnittet av standardsorterna. Sparar du mogna frön själv, kan du få en helvit, tidig majs om du sår endast vita korn. En portion innehåller ca. 30 frön.",
                    HeightCm = 140,
                    Price = 35,
                    IsBeginnerSeed = true
                },
                new Seed()
                {
                    Name = "King of the North",
                    LatinName = "	Capsicum annuum",
                    BotanicalFamily = "Potatisfamiljen - Solanaceae",
                    DaysToDevelop = 60,
                    Annuality = "Flerårig",
                    Type = "Paprika",
                    Description = "Typisk köpepaprika med knubbiga, breda och relativt korta frukter. Nästan alla är fyrrummiga, någon enstaka frukt är trerummig. Köttet är tjockt och saftigt. King är inte den allra tidigaste sorten, men tål sämre paprikaväder bättre än de flesta. Plantan är stadig och blir på friland 40-50 cm hög. Något högre i växthus. King of the North är en fin standardpaprika för våra breddgrader. En portion innehåller ca. 15 frön.",
                    HeightCm = 50,
                    Price = 35,
                    IsBeginnerSeed = false
                },
                new Seed()
                {
                    Name = "Santa Fé",
                    LatinName = "Capsicum annuum",
                    BotanicalFamily = "Potatisfamiljen - Solanaceae",
                    DaysToDevelop = 62,
                    Annuality = "Flerårig",
                    Type = "Chilipeppar",
                    Description = "Växer som en kompakt, stabil planta på ca. 30-35 cm. Utmärkt i krukor. De ger ovanligt mycket frukt som från gulgrönt snabbt övergår i orange och sedan rött. De knubbiga frukterna är koniska, 7-9 cm. långa och ca. 4 cm. i diameter vid fästet. Köttet är saftigt och fruktigt och starkt, men långt ifrån den intensiva hetta som finns hos t.ex. Habanero. Santa Fé växer snabbt och ger bra även på friland under normala somrar. En portion innehåller ca. 15 frön.",
                    HeightCm = 35,
                    Price = 35,
                    IsBeginnerSeed = false
                },
                new Seed()
                {
                    Name = "Muncher",
                    LatinName = "Cucumis sativus",
                    BotanicalFamily = "Gurkväxter - Cucurbitaceae",
                    DaysToDevelop = 48,
                    Annuality = "Ettårig",
                    Type = "Gurka",
                    Description = "Delikat, helt slät, mörkgrön, bitterfri salladsgurka färdig att njutas först av alla. Den är en modern variant av s.k. Beit Alpha-gurka. En gurktyp som är mycket vanlig framför allt i Mellanöstern. Plantan är rankande, men drar inte iväg så långt. Frukten, som skördas vid 13-15 cm., är spröd som ett äpple och mycket god både färsk och inlagd. För inläggning skördas frukten oftast vid 8-12 cm. En portion innehåller ca. 10 frön.",
                    HeightCm = 30,
                    Price = 35,
                    IsBeginnerSeed = false
                },
                new Seed()
                {
                    Name = "Fänkål",
                    LatinName = "Foeniculum vulgare var. dulce",
                    BotanicalFamily = "Flockblomstriga - Apiaceae",
                    DaysToDevelop = 60,
                    Annuality = "Ettårig",
                    Type = "Fänkål",
                    Description = "Fänkålen är en växt som används brett och högt i hela världen. Den har odlats sedan minst 2000 år tillbaka och betraktas som en av de äldsta kulturväxterna, om inte för matlagningen så för sina medicinska egenskaper och tillämpningsområden. En portion innehåller ca. 150 frön.",
                    HeightCm = 60,
                    Price = 35,
                    IsBeginnerSeed = false
                },
                new Seed()
                {
                    Name = "Extra mosskrusig",
                    LatinName = "Petroselinum crispum",
                    BotanicalFamily = "Flockblomstriga - Apiaceae",
                    DaysToDevelop = 77,
                    Annuality = "Tvåårig",
                    Type = "Persilja",
                    Description = "Intensivt grön persilja med de krusigaste bladen. Den står länge på hösten utan att förlora sin färg och friskhet. Extra mosskrusig är en gammal pålitlig sort som ger bra, är vacker och lättskördad. Omnämnd redan 1837. En portion innehåller ca. 700 frön.",
                    HeightCm = 50,
                    Price = 35,
                    IsBeginnerSeed = false
                },
                new Seed()
                {
                    Name = "New England Pie",
                    LatinName = "Cucurbita pepo",
                    BotanicalFamily = "Gurkväxter - Cucurbitaceae",
                    DaysToDevelop = 100,
                    Annuality = "Ettårig",
                    Type = "Pumpa",
                    Description = "Är en vacker orange pumpa från USA, som introducerades där före 1863. Sedan dess har den varit en av de mest odlade och älskade sorterna. Frukterna är tillplattat runda, väger 1,5-3 kg och har ett ganska tjockt, gott, kraftigt orange kött. Perfekt för pumpkin pie. Skalet är grönfläckigt innan frukten mognat. Men några veckor inomhus gör det helt orange. New England Pie kallas också Small Sugar. En portion innehåller ca 8-10 frön.",
                    HeightCm = 60,
                    Price = 35,
                    IsBeginnerSeed = false
                },
                new Seed()
                {
                    Name = "Dark Fog",
                    LatinName = "Cucurbita pepo",
                    BotanicalFamily = "Gurkväxter - Cucurbitaceae",
                    DaysToDevelop = 52,
                    Annuality = "Ettårig",
                    Type = "Zucchini",
                    Description = "Är en utmärkt, långsmal, mörkgrön zucchini. Frukten är skinande blank och har lite ljusare prickar i det mörka. Dark Fog är en selektion av gamla pålitliga Black Beauty och skördas både som ung courgette och som större för ungsbakning. En lättodlad och givande sort från Italien. En portion innehåller 8-10 frön.",
                    HeightCm = 60,
                    Price = 35,
                    IsBeginnerSeed = true
                },
                new Seed()
                {
                    Name = "Marmande",
                    LatinName = "Solanum lycopersicum",
                    BotanicalFamily = "Potatisväxter - Solanaceae",
                    DaysToDevelop = 64,
                    Annuality = "Ettårig",
                    Type = "Tomat",
                    Description = "Fransk bifftomat som ger stora, upp till 200 g, lite oregelbundna, åsiga, köttiga frukter med utsökt smak. Plantan är buskig, men kan bli 70 cm hög och behöver då stöd. Marmande är frisk och odlas oftast på friland. Resistent mot Fusarium och Verticillium sjukdomar. En portion innehåller ca. 20 frön.",
                    HeightCm = 70,
                    Price = 35,
                    IsBeginnerSeed = true
                }
            };

            // Lägg till datamatning till kontexten
            foreach (Seed seed in seeds)
            {
                context.Seeds.Add(seed);
            }

            context.SaveChanges();
        }
    }
}
