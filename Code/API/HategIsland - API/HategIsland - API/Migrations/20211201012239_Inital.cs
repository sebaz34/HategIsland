using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HategIsland___API.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DinosaurSpecies",
                columns: table => new
                {
                    DinosaurSpeciesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AOH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseHealth = table.Column<int>(type: "int", nullable: false),
                    BaseStamina = table.Column<int>(type: "int", nullable: false),
                    BaseHunger = table.Column<int>(type: "int", nullable: false),
                    BaseThirst = table.Column<int>(type: "int", nullable: false),
                    HistoricalBlurb = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DinosaurSpecies", x => x.DinosaurSpeciesID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseDuration = table.Column<int>(type: "int", nullable: false),
                    BaseReward = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnlockedLocations = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerID);
                    table.ForeignKey(
                        name: "FK_Players_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    BattleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InitiatingPlayerID = table.Column<int>(type: "int", nullable: false),
                    ReceivingPlayerID = table.Column<int>(type: "int", nullable: false),
                    CurrentPlayerTurn = table.Column<int>(type: "int", nullable: false),
                    InitiatingPlayerPack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivingPlayerPack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Winner = table.Column<int>(type: "int", nullable: true),
                    PlayerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.BattleID);
                    table.ForeignKey(
                        name: "FK_Battles_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dinosaurs",
                columns: table => new
                {
                    PackedDinosaurID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    DinosaurSpeciesID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Traits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dinosaurs", x => x.PackedDinosaurID);
                    table.ForeignKey(
                        name: "FK_Dinosaurs_DinosaurSpecies_DinosaurSpeciesID",
                        column: x => x.DinosaurSpeciesID,
                        principalTable: "DinosaurSpecies",
                        principalColumn: "DinosaurSpeciesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dinosaurs_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    InventoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    Money = table.Column<int>(type: "int", nullable: false),
                    HerbFood = table.Column<int>(type: "int", nullable: false),
                    CarnFood = table.Column<int>(type: "int", nullable: false),
                    Medicine = table.Column<int>(type: "int", nullable: false),
                    Herb = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.InventoryID);
                    table.ForeignKey(
                        name: "FK_Inventories_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationVisits",
                columns: table => new
                {
                    LocationVisitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    DinosaurID = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionReward = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationVisits", x => x.LocationVisitID);
                    table.ForeignKey(
                        name: "FK_LocationVisits_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationVisits_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DinosaurSpecies",
                columns: new[] { "DinosaurSpeciesID", "AOH", "BaseHealth", "BaseHunger", "BaseStamina", "BaseThirst", "Diet", "HistoricalBlurb", "Size", "Species" },
                values: new object[,]
                {
                    { 1, "", 0, 100, 0, 100, "", "", "", "" },
                    { 24, "Land", 75, 100, 30, 100, "Carnivore", "A close relative of the Dilophosaurus, the Dracovenator measures up to 7m in length and features a protuding dual horn on its head. This dinosaur is difficult to tame and is often overcome by instinctual impulses.", "Medium", "Dracovenator" },
                    { 25, "Land", 75, 100, 30, 100, "Carnivore", "Ozraptor: def \"Australian Thief\". This dinosaur, native to the area known today as Australia, lived during the middle Jurassic Period. This keen predator measures up to 3m in length and can reach speeds of 40km/hr. Although a solitary hunter, it is able to work with others of its kind to reach a shared goal.", "Medium", "Ozraptor" },
                    { 26, "Land", 75, 100, 30, 100, "Carnivore", "During the late Triassic Period in South America, the Herrerasaurus roamed hunting for its prey. This vicious carnivore can grow up to 6m in length and weighing nearly 400kg. They are a fully bipedal creature which allows them to reach fast speeds to track and kill its prey.", "Medium", "Herrerasaurus" },
                    { 27, "Land", 75, 100, 30, 100, "Carnivore", "This dinosaur native to the Japanese landmass is known for its terrifying bloodlust. Living during the early Cretaceous Period, this dinosaur has been witnessed to show a keen skill for hunting its prey. It takes keen interest in vertibrates smaller than its average size of 4.5m long.", "Medium", "Fukuiraptor" },
                    { 28, "Land", 100, 100, 30, 100, "Herbivore", "The Muttaburrasaurus was alive during the early Cretaceous Period in the grassy plains of Australia. This fasinating creature is most discernable by its pointed snout bone protuding upwards. This hollow cavity allows it to make deafining social calls but also provides the creature a remarkable sense of smell, being able to distinguish predators from over 5kms away. Measuring on average 8m in length and nearly 3 tonnes, these big herbivores are a wonderful sight to see.", "Large", "Muttaburrasaurus" },
                    { 29, "Land", 100, 100, 30, 100, "Herbivore", "Its name preceeds the creature, the famous Brachiosaurus, is a delight for the eyes. This huge herbivore can grow anywhere from 10m to 20m in length and weighs in at an average of 38 metric tonnes. Its long neck distinguishes it from most other dinosaurs but youll be able to hear its distinictive social call from kilometers away. Utilising the air sacs throughout its neck to create the low rumble it uses to communicate.", "Large", "Brachiosaurus" },
                    { 30, "Land", 100, 100, 30, 100, "Herbivore", "Able to walk on all 4's but run on its two powerful back legs, the Edmontosaurus is a fascinating herbivore. Living during the late Cretacarous throughout the American Landmass, this dinosaur has been noted to migrate and prefers costal areas. Its most distingusihing feature is its bent neck which allows the Edmontosaurus to participate in jousting like behaviours as a social activity to determine dominance.", "Large", "Edmontosaurus" },
                    { 31, "Land", 100, 100, 30, 100, "Herbivore", "Nice dinosaur you have there, it'd be a shame if mine were bigger - oh wait… The Sauroposeidon is possibly the largest dinosaur ever to have walked the earth. Its head can reach a mind boggling height of 18m above the ground (that’s around the height of a six story building) and weighing a gargantuan weight of 60 tonnes, this creature has been considered by some academics as: \"kinda big I guess\". What makes this creature truly remarkable however is just how much food it can consume as well as the sheer variety of sustience it is willing to consume.", "Large", "Sauroposeidon" },
                    { 32, "Land", 100, 100, 30, 100, "Carnivore", "A ferocious predator, Ceratosaurus was alive during the late Jurassic Period. Its most distinctive features, the protuding snout bones, are used as fearsome weapons in blunt force attacks as well as in social engagements for dominance. This creature engages is constant grooming to ensure that the horns are kept strong and ready.", "Large", "Ceratosaurus" },
                    { 33, "Land", 100, 100, 30, 100, "Carnivore", "Native to the land that is now India, the Majungasaurus hunted for prey during the late Cretacaous Period. Don't be mistaken by its comparatively short and blunt snout, this dinosaur can and will eat you if given the opportunity. Its diet has beennoted to be so varied that it can and often will engage in cannibalism by hunting and eating others of its own kind.", "Large", "Majungasaurus" },
                    { 34, "Land", 100, 100, 30, 100, "Carnivore", "Often mistaken for a fan or sail boat, the Spinosaurus is actully neither. This dinosaur lived during the late Cretaceous Period and can grow to sizes upwards of 15m long. Along the spine of the dinosaur is its distinctive sail used for heat regulation, intimidation of prey, and balance in and out of the water. Spinosaurus is similar in its behaviour to that of a crocodile, spending a large portion of its time in the water but also able to hunt on land. Its diet consistes of terristal and aquatic animals but it particularly enjoys eating people that make fun of its spine.", "Large", "Spinosaurus" },
                    { 35, "Land", 100, 100, 30, 100, "Carnivore", "An unexpectedly social creature, the Allosaurus lived and hunted in packs during the late Jurassic Period. This predator works together to herd prey into small confined areas and then slowly weakens the animal over an extended period of time. This hunting behaviour has been regarded as highly advanced amongst dinosaurs and should not be underestimated.", "Large", "Allosaurus" },
                    { 36, "Land", 100, 100, 30, 100, "Carnivore", "Ruler of the dinosaurs, the Tyrannosaurus has been portrayed throughout culture as a fearsome predator that roamed North America during the late Cretaceous Period. The reality of this dinosaur is much more terrifying, able to view and track prey accuratley from a distance of 6km (humans can only see 1.6km). This creature can track scents through all terrain types over extremely long distances rivally that of the vulture. The Tyrannosaurus is also able to hear low-frequency noises with astonishing accuracy and its skin is highly sensitive to temperature fluctuations around providing it the ability to track smaller close prey outside of its other senses. This dinsoaur is arguably the most advanced hunting creature ever to roam the planet.", "Large", "Tyrannosaurus" },
                    { 37, "Water", 50, 100, 30, 100, "Carnivore", "During the late Triassic Period, of the coast of modern day California, these large fish-like reptiles hunted small fish and other marine creatures. Whilst generally unremarkable, the Californosaurus is a highly efficent and fast swimmer. This creature has been found to reach speeds of 35km/hr underwater. It utilises is sonar sense to communicate with others of its kind and is generally a social creature.", "Small", "Californosaurus" },
                    { 38, "Water", 75, 100, 30, 100, "Carnivore", "Growing to an average length of 7m, the Platypteryguis navigated the waters around modren day Australia during the Cretaceous Period. This creture boasts a long snout and extremelly powerful tail. Originally thought to only eat marine animals, it has been found to hunt birds and pterosaurs flying low to the water. Utilising its powerful tail to launch itself up to 10m in the air, this stealthy hunter can strike at any unsuspecting flying prey.", "Medium", "Platypterygius" },
                    { 39, "Water", 75, 100, 30, 100, "Carnivore", "Named after the famous sword, this swordfish like ichthyosaur can boast a 1.5m long snout at full maturity. The Excalibosaurus lurked in the early Jurassic Period waters of modern day England. Originally thought to be used for attacking and hunting purposes, the creatures snout is primarially used for coralling schools of fish with others of its kind. Working as a group, they will lead the fish towards the shallows and then utilise the limited space to eat their prey.", "Medium", "Excalibosaurus" },
                    { 40, "Water", 75, 100, 30, 100, "Carnivore", "With its long slender neck and four flippers helping it to glide through the water seemingly efforlessly, the Attenborosaurus is a marvel to watch. This aquatic reptile was found in the early Jurassic Period in the waters around modern day England. Whilst it might seem calm in the water, it truly is a master of deception as this predator can lock on to its prey and is able to rapidly mobilise to hunt it down. Some have noted the level of intelligence displayed at times by this creature indicating that is capable of problem solving and reasoning to obtain its goals.", "Medium", "Attenborosaurus" },
                    { 23, "Land", 75, 100, 30, 100, "Herbivore", "With its notable crest extending backward from its head, the Parasaurolophus lived during the late Cretaceous Period. A herbivore grazing amongst the ground level fauna as well as shorter trees and tall shrubs. This dinosaur moves in herds utilising its hollow crest to communicate with others of its kind.", "Medium", "Parasaurolophus" },
                    { 22, "Land", 75, 100, 30, 100, "Herbivore", "With its two horns protuding from its head, the Triceratops lived during the late Cretaceous Period in North America. This distinct looking dinosaur has been known to grow up to 9m long and weighing 12 metric tonnes. Its horns are used for both social and defensive purposes. The Triceratops enjoys co-habitating with other dinosaurs and has been observed to act defensively for the benefit of other dinosaurs.", "Medium", "Triceratops" },
                    { 21, "Land", 75, 100, 30, 100, "Herbivore", "A living tank: The Ankylosaurus was a heavily armoured dinosaur with heavy bone plates over its body and an offensive club on the end of its tail. This dinosaur could grow to 6-8m and weighed from 4.8 to 8 metric tonnes. Whilst a social creature, the Ankylosaurus preferes to be amongst its kind and has difficulty working with others.", "Medium", "Ankylosaurus" },
                    { 20, "Land", 75, 100, 30, 100, "Herbivore", "If dinosaurs could be considered stubborn, the Pachycephalosaurus would be the most hard headed of them all, literally. This medium dinosaur features a skull with an extensively thick roof used for defensive and social purposes. Its diet is also unique amongst dinosaurs in that whilst it is a herbivore, it doesnt eat fibrous materials but prefers to eat seeds, leaves, and fruit.", "Medium", "Pachycephalosaurus" },
                    { 2, "Air", 50, 100, 30, 100, "Carnivore", "A small flying reptile, the Pterodactylus was alive during the late Jurassic Period around Europe and Africa. It is a generalist carnivore that feeds on a variety of invertebrates and vertebrates. Its body is roughly the size of a house cat with a wingspan around 1.04m.", "Small", "Pterodactylus" },
                    { 3, "Air", 50, 100, 30, 100, "Carnivore", "A small flying reptile, the Dimorphodon was alive during the early Jurassic Period. Inhabiting the European Continent, this pterosaur had a body length from 1m to 1.5m. Whilst able to fly, its aerodynamics do not support long distance flight and it relies on short hops. It is a fantastic climber. Whilst classified as a carnivore its diet is more closer to a scavengers diet, eating anything from plants and insects to fish and small rodents.", "Small", "Dimorphodon" },
                    { 4, "Air", 50, 100, 30, 100, "Carnivore", "A tiny flying dinosaur that lived during the late Jurassic Period. This bat like creature hunted mostly insects and smaller prey. Its large eyes give it phenomnial eyesight.", "Small", "Anurognathus" },
                    { 5, "Air", 75, 100, 30, 100, "Carnivore", "A medium flying reptile, the Kryptodrakon is one of the oldest known pterodactyloid pterosaurs living from the middle to late Jurassic Period. Inhabiting what is now known as modern day China, this dinosaur has a wingspan around 1.5m. The crest on its head is used for speech and calls.", "Medium", "Kryptodrakon" },
                    { 6, "Air", 75, 100, 30, 100, "Herbivore", "This medium flying reptile can grow a wingspan of around 2.5m. Its most prominent feature is its mouth where a series of bristle-like teeth line its lower jaw. It is theorised that its diet consisted partly of crustaceans and as a result, like the flamingo, its feathered skin was most likely a pinkish hue.", "Medium", "Pterodaustro" },
                    { 7, "Air", 75, 100, 30, 100, "Carnivore", "This medium sized flying reptile can grow a wingspan of around 4m. Its arrowhead like skull allows it to glide through the air with ease. It lived during the late Cretaceous Period in the area known today as Hungary.", "Medium", "Bakonydraco" },
                    { 8, "Air", 100, 100, 30, 100, "Carnivore", "This large flying dinosaur lived during the late Cretaceous Period within North America. It has been known as one of the largest flying animals to have ever existed. Its wingspan can range from 11m up to 20m. When standing on its legs it can reach a height of 3m. This dinosaur primarlly eats small vertebrates and can be teritorial in its hunting areas.", "Large", "Quetzalcoatlus" },
                    { 9, "Air", 100, 100, 30, 100, "Carnivore", "The Tupandactylus was a large pterosaur that lived in the early Cretaceous Period. Its wingspan can be up to 5m across and its most noticable feature is its large crainial crest. This crest, composed partly of bone and soft tissue, is used to signal to others as well as a means of determining wing speed and other flight measurements.", "Large", "Tupandactylus" },
                    { 41, "Water", 100, 100, 30, 100, "Carnivore", "Growing up to 18m in length, this late Cretaceous reptile is a ferocious and intimidating creature. Hunting other marine reptiles and sharks this creature was a dominate force in the water. It has a powerful ability to see far distances within water and able to accuratly distinguish prey on the surface of the water. A monumental creature and highly effective underwater killing machine.", "Large", "Mosasaurus" },
                    { 10, "Air", 100, 100, 30, 100, "Carnivore", "From the late Cretaceous Period, Geosternbergia is among the largest of the pterosaurs with its wingspan ranging from 3m to 6m. Its extended beak holds no teeth, an unusual trait for most flying dinosaurs.", "Large", "Geosternbergia" },
                    { 12, "Land", 50, 100, 30, 100, "Herbivore", "The Pisanosaurus lived in Northwestern Argentina during the later Triassic Period. This small feathered herbivore reaches 1m in length and weighs around 5-10kg. Its diet consistes primarilly of ferns, conifers, and other leafy greens but have been noted to enjoy insects as well.", "Small", "Pisanosaurus" },
                    { 13, "Land", 50, 100, 30, 100, "Herbivore", "In the Arizona plains during the early Jurassic Period this small armored herbivore ran amuck. Its name translating in latin to \"little - shielded lizard\" provides detail for the several hundred bone seams running along its back. This creature is very social and works well together in packs.", "Small", "Scutellosaurus" },
                    { 14, "Land", 50, 100, 30, 100, "Herbivore", "Part weasel, part chicken: the Fruitaden grows to 75cm in length and weighs around 0.75kg. This very small dinosaur is oppourtinistic in its diet, eating whatever it can find but prefers to eat plant materials. Historically found during the late Jurassic Period in Colorado.", "Small", "Fruitadens" },
                    { 15, "Land", 50, 100, 30, 100, "Herbivore", "This furry creature from the late Jurassic Period in China is most noticable due to its feathered back spine. Living most of its life on the ground, the Tianyulong has been found to burrow underground and nest.", "Small", "Tianyulong" },
                    { 16, "Land", 50, 100, 30, 100, "Carnivore", "During the late Triassic Period in North America the Camposaurus hunted small vertibrates, insects, and fish. These tiny creatures scavenged whatever they can find but eat primarilly meat.", "Small", "Camposaurus" },
                    { 17, "Land", 50, 100, 30, 100, "Carnivore", "Perhaps the smallest dinosaur known, the Juravenator grew up to 75cm long and lived during the late Jurassic Period. This tiny creature has a fine coat of feathers as well as scales, and behaves very similarly to ducks.", "Small", "Juravenator" },
                    { 18, "Land", 50, 100, 30, 100, "Carnivore", "Using its slender snout and powerful arms to eat insects, the Albertonykus was found throughout North American during the late Cretaceous Period. Originally thought to only eat insects, we have since discovered that it can also hunt small vertibrates with its extremelly powerful limbs.", "Small", "Albertonykus" },
                    { 19, "Land", 75, 100, 30, 100, "Herbivore", "The famous Stegosaurus lived during the late Jurassic Period. With its distinctive kite shaped plates along its spine and spikes protruding from its tail; this magnificent dinosaur has been prominent throughout pop culture. The Stegosaurus is a pack herbiovre and its diet consistets of low lying fauna like ferns and shrubs.", "Medium", "Stegosaurus" },
                    { 11, "Air", 100, 100, 30, 100, "Carnivore", "Native to Brazil, the Tropeognathus is one of the largest pterosaurs with a wing span of 8m and upwards. Its most noticable feature is the crest on their snout. This spoon like structure allows the creature to fish prey from deeper in the water than other winged counterparts.", "Large", "Tropeognathus" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Password", "Username" },
                values: new object[] { 1, "tester", "tester", "tester" });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerID", "PlayerName", "UnlockedLocations", "UserID" },
                values: new object[] { 1, "DinoFan1", "1", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Battles_PlayerID",
                table: "Battles",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Dinosaurs_DinosaurSpeciesID",
                table: "Dinosaurs",
                column: "DinosaurSpeciesID");

            migrationBuilder.CreateIndex(
                name: "IX_Dinosaurs_PlayerID",
                table: "Dinosaurs",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_PlayerID",
                table: "Inventories",
                column: "PlayerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationVisits_LocationID",
                table: "LocationVisits",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_LocationVisits_PlayerID",
                table: "LocationVisits",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserID",
                table: "Players",
                column: "UserID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Battles");

            migrationBuilder.DropTable(
                name: "Dinosaurs");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "LocationVisits");

            migrationBuilder.DropTable(
                name: "DinosaurSpecies");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
