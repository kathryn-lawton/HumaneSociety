using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
	class FileReader
	{
		
		public FileReader()
		{
		}

		public static void ReadFile()
		{
			using (StreamReader reader = new StreamReader(@"C:\Users\klawt\source\repos\HumaneSocietyStarter\animals.csv"))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');

					var valueStrings = values.ToArray();

					Animal animal = new Animal(valueStrings);

					Query.AddAnimal(animal);
				}
			}
		}

		public static void InitalizeBreedDatabase()
		{
			var breeds = new Breed[]
			{
				new Breed(){
					pattern = "spotted",
					breed1 = "goldendoodle",
					catagory = 1
				},
				new Breed()
				{
					pattern = "straight",
					breed1 = "husky",
					catagory = 1
				}
			};

			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			db.Breeds.InsertAllOnSubmit(breeds);
			db.SubmitChanges();
		}
	}
}
