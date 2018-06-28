using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
		
        public static Client GetClient(string userName, string password)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var resultClient = db.Clients.Where(c => c.userName == userName && c.pass == password).FirstOrDefault();
			
			return resultClient;
		}

		public static void RunEmployeeQueries(Employee employee, string update)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var employeeUpdate = db.Employees.Where(e => e.ID == employee.ID).First();
			employeeUpdate = employee;
			db.SubmitChanges();
		}

		public static IQueryable<ClientAnimalJunction> GetPendingAdoptions()
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var adoptions = db.ClientAnimalJunctions.Where(a => a.approvalStatus.ToLower() == "pending");
			return adoptions;
		}

		public static IQueryable<ClientAnimalJunction> GetUserAdoptionStatus(Client client)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var pendingAdoptions = db.ClientAnimalJunctions.Where(p => p.Client1 == client).Select(p => p);
			return pendingAdoptions;
		}

		public static Animal GetAnimalByID(int iD)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var animal = db.Animals.Where(a => a.ID == iD).FirstOrDefault();
			return animal;
		}

		public static void Adopt(Animal animal, Client client)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			ClientAnimalJunction application = new ClientAnimalJunction();
			application.animal = animal.ID;
			application.client = client.ID;
			application.approvalStatus = "pending";
			db.ClientAnimalJunctions.InsertOnSubmit(application);

			var animalUpdate = db.Animals.Where(a => a.ID == animal.ID).First();
			animalUpdate.adoptionStatus = "pending";

			db.SubmitChanges();
		}

		public static IQueryable<Client> RetrieveClients()
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var clients = db.Clients.Select(c => c);
				return clients;		
		}
		public static IQueryable<USState> GetStates()
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var states = db.USStates.Select(s => s);
			return states;
		}

		public static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();

			UserAddress userAddress = new UserAddress();
			userAddress.addessLine1 = streetAddress;
			userAddress.addressLine2 = null;
			userAddress.zipcode = zipCode;
			userAddress.USState = Query.GetStates().Where(s => s.ID == state).First();
			db.UserAddresses.InsertOnSubmit(userAddress);

			Client client = new Client();
			client.firstName = firstName;
			client.lastName = lastName;
			client.userName = username;
			client.pass = password;
			client.email = email;
			client.UserAddress1 = userAddress;
			db.Clients.InsertOnSubmit(client);

			db.SubmitChanges();			
		}

		public static void UpdateClient(Client client)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var clientUpdate = (from c in db.Clients where c.ID == client.ID select c).First();

			clientUpdate.email = client.email;
			clientUpdate.firstName = client.firstName;
			clientUpdate.lastName = client.lastName;
			clientUpdate.homeSize = client.homeSize;
			clientUpdate.kids = client.kids;
			clientUpdate.pass = client.pass;
			clientUpdate.userAddress = client.userAddress;
			clientUpdate.userName = client.userName;
			clientUpdate.income = client.income;

			db.SubmitChanges();
		}

		public static void UpdateUsername(Client client)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var usernameUpdate = (from c in db.Clients where c.ID == client.ID select c).First();
			usernameUpdate.userName = client.userName;
			db.SubmitChanges();
		}

		public static void UpdateEmail(Client client)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var emailUpdate = (from c in db.Clients where c.ID == client.ID select c).First();
			emailUpdate.email = client.email;
			db.SubmitChanges();
		}

		public static void UpdateAddress(Client client)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var addressUpdate = (from c in db.Clients where c.ID == client.ID select c).First();
			addressUpdate.userAddress = client.userAddress;
			db.SubmitChanges();
		}

		public static void UpdateFirstName(Client client)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var firstNameUpdate = (from c in db.Clients where c.ID == client.ID select c).First();
			firstNameUpdate.firstName = client.firstName;
			db.SubmitChanges();
		}

		public static void UpdateLastName(Client client)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var lastNameUpdate = (from c in db.Clients where c.ID == client.ID select c).First();
			lastNameUpdate.lastName = client.lastName;
			db.SubmitChanges();
		}

		public static void UpdateAdoption(bool status, ClientAnimalJunction clientAnimalJunction)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var update = (from u in db.ClientAnimalJunctions where u.client == clientAnimalJunction.Client1.ID && u.animal == clientAnimalJunction.Animal1.ID select u).First();
			update.approvalStatus = status ? "approved" : "denied";

			db.SubmitChanges();
		}

		public static IQueryable<AnimalShotJunction> GetShots(Animal animal)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var shots = db.AnimalShotJunctions.Where(s => s.Animal.ID == animal.ID);
			return shots;
		}

		public static Shot GetShot(int shotId)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var shot = db.Shots.Where(s => s.ID == shotId).First();
			return shot;
		}

		public static void UpdateShot(int shotId, Animal animal)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var updateShot = db.AnimalShotJunctions.Where(s => s.Animal_ID == animal.ID && s.Shot_ID == shotId).First();
			updateShot.dateRecieved = DateTime.Now;

			db.SubmitChanges();
		}

		public static void AddShot(Shot shot, Animal animal)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			AnimalShotJunction shotUpdate = new AnimalShotJunction();
			shotUpdate.Animal_ID = animal.ID;
			shotUpdate.dateRecieved = DateTime.Now;
			shotUpdate.Shot_ID = shot.ID;

			db.AnimalShotJunctions.InsertOnSubmit(shotUpdate);
			db.SubmitChanges();
		}

		public static IQueryable<Shot> GetAllShots()
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			return db.Shots;
		}

		public static void EnterUpdate(Animal animal, Dictionary<int,string> updates)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			//loop through keys
		}

		public static void RemoveAnimal(Animal animal)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			db.Animals.DeleteOnSubmit(animal);
			db.SubmitChanges();
		}

		//public static int GetBreed()
		//{
		//	HumaneSocietyDataContext db = new HumaneSocietyDataContext();
		//	Breed breed = new Breed();
		//	Catagory catagory = new Catagory();
		//	var update = db.Animals.Where(u => u.Breed1.Catagory1.ID == catagory.ID).ToList().First().ID;
		//	return update;
		//}

		public static Breed[] GetBreeds()
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var breeds = db.Breeds.ToArray();
			return breeds;
		}

		//public static int GetDiet()
		//{
		//	HumaneSocietyDataContext db = new HumaneSocietyDataContext();
		//	Breed breed = new Breed();
		//	DietPlan diet = new DietPlan();
		//	var update = db.Animals.Where(u => u.DietPlan.ID == diet.ID).ToList().First().ID;
		//	return update;
		//}

		public static DietPlan[] GetDietPlans()
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var diets = db.DietPlans.ToArray();
			return diets;
		}

		//public static int GetLocation()
		//{
		//	HumaneSocietyDataContext db = new HumaneSocietyDataContext();
		//	var update = db.Animals.Where(u => u.Room.ID == 0).ToList().First().ID;
		//	return update;
		//}

		public static Room[] GetRooms()
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var rooms = db.Rooms.ToArray();
			return rooms;
		}

		public static void AddAnimal(Animal animal)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			db.Animals.InsertOnSubmit(animal);
			db.SubmitChanges();
		}

		public static Employee EmployeeLogin(string userName, string password)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var employeeLogin = db.Employees.Where(e => e.userName == userName && e.pass == password).FirstOrDefault();
			return employeeLogin;
		}

		public static Employee RetrieveEmployeeUser(string email, int employeeNumber)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var employeeUser = db.Employees.Where(e => e.email == email && e.employeeNumber == employeeNumber).FirstOrDefault();
			return employeeUser;
		}

		public static void AddUsernameAndPassword(Employee employee)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			db.Employees.InsertOnSubmit(employee);
			db.SubmitChanges();
			
		}

		public static bool CheckEmployeeUserNameExist(string username)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var userNameExists = db.Employees.Where(u => u.userName == username).FirstOrDefault();
			if (userNameExists != null)
			{
				return true;
			}
			else
			{
				return false;
			}
			
		}

		public static Animal[] GetAvailableAnimals()
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var animals = db.Animals.Where(a => a.adoptionStatus.ToLower() != "adopted").ToArray();
			return animals;
		}
	}
}
