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

		}

		public static IQueryable<ClientAnimalJunction> GetPendingAdoptions()
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var adoptions = db.ClientAnimalJunctions.Select(a => a);
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

		public static Client AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			Client client = new Client();
			UserAddress userAddress = new UserAddress();
			USState uSState = new USState();
			client.firstName = firstName;
			client.lastName = lastName;
			client.userName = username;
			client.pass = password;
			client.email = email;
			userAddress.addessLine1 = streetAddress;
			userAddress.zipcode = zipCode;
			
		}

		public static void UpdateClient(Client client)
		{

		}

		public static void UpdateUsername(Client client)
		{

		}

		public static void UpdateEmail(Client client)
		{

		}

		public static void UpdateAddress(Client client)
		{

		}

		public static void UpdateFirstName(Client client)
		{

		}

		public static void UpdateLastName(Client client)
		{

		}

		public static void UpdateAdoption(bool status, ClientAnimalJunction clientAnimalJunction)
		{

		}

		public static IQueryable<AnimalShotJunction> GetShots(Animal animal)
		{
			HumaneSocietyDataContext db = new HumaneSocietyDataContext();
			var shot = db.AnimalShotJunctions.Where(s => s.Animal == animal).Select(s => s);
			return shot;
		}

		public static void UpdateShot(string booster, Animal animal)
		{

		}

		public static void EnterUpdate(Animal animal, Dictionary<int,string> updates)
		{

		}

		public static void RemoveAnimal(Animal animal)
		{

		}

		public static int GetBreed()
		{
			return 0;
		}

		public static int GetDiet()
		{
			return 0;
		}

		public static int GetLocation()
		{
			return 0;
		}

		public static void AddAnimal(Animal animal)
		{

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

		}

		public static bool CheckEmployeeUserNameExist(string username)
		{
			return true;
		}
	}
}
