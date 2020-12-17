using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DS
{
    public static class DataSource
    {
        public static List<User> Users;
        public static List<Bus> Buses;
        public static List<Line> Lines;
        public static List<Station> Stations;
        public static List<LineStation> LineStations;
		public static List<TwoFollowingStations> FollowingStations;
        public static List<DrivingBus> DrivingBuses;
		public static List<DrivingLine> DrivingLines;
        public static string ManagingCode = "123456";

        static DataSource()
        {
			Stations = new List<Station>()
			{
				new Station
				{
					ID = 38831,
					Name = "BarLev/BenYehudaSchool",
					Latitude = 32.183921,
					Longitude = 34.917806
				},

				new Station
				{
					ID = 38832,
					Name = "Herzl/BiluJunction",
					Latitude = 31.870034,
					Longitude = 34.819541
				},

				new Station
				{
					ID = 38833,
					Name = "Thesurge/fishermen",
					Latitude = 31.984553,
					Longitude = 34.782828
				},

				new Station
				{
					ID = 38834,
					Name = "Fried/TheSixDays",
					Latitude = 31.88855,
					Longitude = 34.790904
				},

				new Station
				{
					ID = 38836,
					Name = "A.LodCentral/Download",
					Latitude = 31.956392,
					Longitude = 34.898098
				},

				new Station
				{
					ID = 38837,
					Name = "HannahAvrech/Vulcani",
					Latitude = 31.892166,
					Longitude = 34.796071
				},

				new Station
				{
					ID = 38838,
					Name = "Herzl/MosheSharet",
					Latitude = 31.857565,
					Longitude = 34.824106
				},

				new Station
				{
					ID = 38839,
					Name = "Theboys/EliCohen",
					Latitude = 31.862305,
					Longitude = 34.821857
				},

				new Station
				{
					ID = 38840,
					Name = "Weizmann/TheBoys",
					Latitude = 31.865085,
					Longitude = 34.822237
				},

				new Station
				{
					ID = 38841,
					Name = "Theiris/anemone",
					Latitude = 31.865222,
					Longitude = 34.818957
				},

				new Station
				{
					ID = 38842,
					Name = "Theanemone/daffodil",
					Latitude = 31.867597,
					Longitude = 34.818392
				},

				new Station
				{
					ID = 38844,
					Name = "EliCohen/GhettoFighters",
					Latitude = 31.86244,
					Longitude = 34.827023
				},

				new Station
				{
					ID = 38845,
					Name = "Shabazi/Shabbatbrothers",
					Latitude = 31.863501,
					Longitude = 34.828702
				},

				new Station
				{
					ID = 38846,
					Name = "Shabazi/Weizmann",
					Latitude = 31.865348,
					Longitude = 34.827102
				},

				new Station
				{
					ID = 38847,
					Name = "HaimBarLev/YitzhakRabinBoulevard",
					Latitude = 31.977409,
					Longitude = 34.763896
				},

				new Station
				{
					ID = 38848,
					Name = "LevHasharonMentalHealthCenter",
					Latitude = 32.300345,
					Longitude = 34.912708
				},

				new Station
				{
					ID = 38849,
					Name = "LevHasharonMentalHealthCenter",
					Latitude = 32.301347,
					Longitude = 34.912602
				},

				new Station
				{
					ID = 38852,
					Name = "Holtzman/Science",
					Latitude = 31.914255,
					Longitude = 34.807944
				},

				new Station
				{
					ID = 38854,
					Name = "ZrifinCamp/Club",
					Latitude = 31.963668,
					Longitude = 34.836363
				},

				new Station
				{
					ID = 38855,
					Name = "Herzl/Golani",
					Latitude = 31.856115,
					Longitude = 34.825249
				},

				new Station
				{
					ID = 38856,
					Name = "Rotem/Deganiot",
					Latitude = 31.874963,
					Longitude = 34.81249
				},

				new Station
				{
					ID = 38859,
					Name = "Theprairie",
					Latitude = 32.300035,
					Longitude = 34.910842
				},

				new Station
				{
					ID = 38860,
					Name = "Introductiontothevine/Slopeofthefig",
					Latitude = 32.305234,
					Longitude = 34.948647
				},

				new Station
				{
					ID = 38861,
					Name = "Introductiontothevine/extension",
					Latitude = 32.304022,
					Longitude = 34.943393
				},

				new Station
				{
					ID = 38862,
					Name = "Theextensiona",
					Latitude = 32.302957,
					Longitude = 34.940529
				},

				new Station
				{
					ID = 38863,
					Name = "Theextensionb",
					Latitude = 32.300264,
					Longitude = 34.939512
				},

				new Station
				{
					ID = 38864,
					Name = "Theextension/veterans",
					Latitude = 32.298171,
					Longitude = 34.938705
				},

				new Station
				{
					ID = 38865,
					Name = "Airports/AliyahAuthority",
					Latitude = 31.990876,
					Longitude = 34.8976
				},

				new Station
				{
					ID = 38866,
					Name = "Wing/Cypress",
					Latitude = 31.998767,
					Longitude = 34.879725
				},

				new Station
				{
					ID = 38867,
					Name = "Thegang/DovHoz",
					Latitude = 31.883019,
					Longitude = 34.818708
				},

				new Station
				{
					ID = 38869,
					Name = "BeitHalevie",
					Latitude = 32.349776,
					Longitude = 34.926837
				},

				new Station
				{
					ID = 38870,
					Name = "First/Route5700",
					Latitude = 32.352953,
					Longitude = 34.899465
				},

				new Station
				{
					ID = 38872,
					Name = "ThegeniusBenIshChai/Ceylon",
					Latitude = 31.897286,
					Longitude = 34.775083
				},

				new Station
				{
					ID = 38873,
					Name = "Okashi/LeviEshkol",
					Latitude = 31.883941,
					Longitude = 34.807039
				},

				new Station
				{
					ID = 38875,
					Name = "Restandestate/YehudaGorodiski",
					Latitude = 31.896762,
					Longitude = 34.816752
				},

				new Station
				{
					ID = 38876,
					Name = "Gorodsky/YechielPaldi",
					Latitude = 31.898463,
					Longitude = 34.823461
				},

				new Station
				{
					ID = 38877,
					Name = "DerechMenachemBegin/YaakovHazan",
					Latitude = 32.076535,
					Longitude = 34.904907
				},

				new Station
				{
					ID = 38878,
					Name = "ThroughthePark/RabbiNeria",
					Latitude = 32.299994,
					Longitude = 34.878765
				},

				new Station
				{
					ID = 38879,
					Name = "Thefig/vine",
					Latitude = 31.865457,
					Longitude = 34.859437
				},

				new Station
				{
					ID = 38880,
					Name = "Thefig/oak",
					Latitude = 31.866772,
					Longitude = 34.864555
				},

				new Station
				{
					ID = 38881,
					Name = "ThroughtheFlowers/Jasmine",
					Latitude = 31.809325,
					Longitude = 34.784347
				},

				new Station
				{
					ID = 38883,
					Name = "YitzhakRabin/PinchasSapir",
					Latitude = 31.80037,
					Longitude = 34.778239
				},

				new Station
				{
					ID = 38884,
					Name = "MenachemBegin/YitzhakRabin",
					Latitude = 31.799224,
					Longitude = 34.782985
				},

				new Station
				{
					ID = 38885,
					Name = "HaimHerzog/Dolev",
					Latitude = 31.800334,
					Longitude = 34.785069
				},

				new Station
				{
					ID = 38886,
					Name = "Shades/CedarSchool",
					Latitude = 31.802319,
					Longitude = 34.786735
				},

				new Station
				{
					ID = 38887,
					Name = "Throughthetrees/oak",
					Latitude = 31.804595,
					Longitude = 34.786623
				},

				new Station
				{
					ID = 38888,
					Name = "ThroughtheTrees/MenachemBegin",
					Latitude = 31.805041,
					Longitude = 34.785098
				},

				new Station
				{
					ID = 38889,
					Name = "Independence/Weizmann",
					Latitude = 31.816751,
					Longitude = 34.782252
				},

				new Station
				{
					ID = 38890,
					Name = "Weizmann/TheMagicRug",
					Latitude = 31.816579,
					Longitude = 34.779753
				},

				new Station
				{
					ID = 38891,
					Name = "Tzala/Coral",
					Latitude = 31.801182,
					Longitude = 34.787199
				},

				new Station
				{
					ID = 38892,
					Name = "Hatzav/TzalaGarden",
					Latitude = 31.802279,
					Longitude = 34.786055
				},

				new Station
				{
					ID = 38893,
					Name = "Pines/Levinson",
					Latitude = 31.814676,
					Longitude = 34.777574
				},

				new Station
				{
					ID = 38894,
					Name = "Feinberg/Schachwitz",
					Latitude = 31.813285,
					Longitude = 34.775928
				},

				new Station
				{
					ID = 38895,
					Name = "BenGurion/Fox",
					Latitude = 31.806959,
					Longitude = 34.773504
				},

				new Station
				{
					ID = 38898,
					Name = "LeviEshkol/RabbiDavidIsrael",
					Latitude = 31.884187,
					Longitude = 34.805494
				},

				new Station
				{
					ID = 38899,
					Name = "Lily/Oppenheimer",
					Latitude = 31.910118,
					Longitude = 34.805809
				},

				new Station
				{
					ID = 38901,
					Name = "RabbiDavidIsrael/ArieDolcin",
					Latitude = 31.882474,
					Longitude = 34.80506
				},

				new Station
				{
					ID = 38903,
					Name = "Kronenberg/Crimson",
					Latitude = 31.878667,
					Longitude = 34.81138
				},

				new Station
				{
					ID = 38904,
					Name = "YaakovFreiman/BenjaminShmotkin",
					Latitude = 31.975479,
					Longitude = 34.813355
				},

				new Station
				{
					ID = 38905,
					Name = "Anielewicz/ShalomAsh",
					Latitude = 31.982177,
					Longitude = 34.789445
				},

				new Station
				{
					ID = 38906,
					Name = "Nehemiah/TheCemetery",
					Latitude = 31.948552,
					Longitude = 34.822422
				},

				new Station
				{
					ID = 38907,
					Name = "YehudaHalevi/Yohanantheshoemaker",
					Latitude = 31.967732,
					Longitude = 34.816339
				},

				new Station
				{
					ID = 38908,
					Name = "Thedefense/Hish",
					Latitude = 31.893823,
					Longitude = 34.824617
				},

				new Station
				{
					ID = 38909,
					Name = "KiryatEkron/Road411",
					Latitude = 31.854169,
					Longitude = 34.824714
				},

				new Station
				{
					ID = 38910,
					Name = "RatJunction/Road411",
					Latitude = 31.811907,
					Longitude = 34.900793
				},

				new Station
				{
					ID = 38911,
					Name = "Greenspan/YigalAlon",
					Latitude = 31.956842,
					Longitude = 34.814839
				},

				new Station
				{
					ID = 38912,
					Name = "TheGuardian/Fathers",
					Latitude = 31.959821,
					Longitude = 34.814747
				},

				new Station
				{
					ID = 38913,
					Name = "MosheSharet/YaakovKenner",
					Latitude = 31.992306,
					Longitude = 34.75691
				},

				new Station
				{
					ID = 38914,
					Name = "Thefishermen/surge",
					Latitude = 31.98497,
					Longitude = 34.78262
				},

				new Station
				{
					ID = 38916,
					Name = "JosephBurg/BeaconsIsaac",
					Latitude = 31.968049,
					Longitude = 34.818099
				},
			};			
        }       
	}
}