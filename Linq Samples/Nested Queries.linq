<Query Kind="Program">
  <Connection>
    <ID>ac19c5d1-a507-4043-bafb-b647e09ebfb9</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//Nested Queries
	//sometimes referred to as subqueries

	//simply put: it is a query within a query [ ..... ]

	//list all sales support employees showing their
	// fullname (last, first), title, phone
	//For each employee show a list of their customers
	//they support. List the customer fullname (last, first),
	//city and state.

	// Smith, Bob sales support 7801234567 //this is on employee
	//      Kan, Jerry edmonton ab			//this is on customer
	//      Stewant, Iam edmonton ab		//this is on customer
	//      Jest, Shirley U edmonton ab		//this is on customer
	// Kake, Patty sales support supervisor 7801234568
	// Jones, Pat sales support 7801234569
	//      Behold, Lowand edmonton ab
	
	//there appears to be 2 separate lists that need to
	//	be within one final dataset collection
	//one for employees
	//one for customers
	//concern: the list are inter mixed!!!!
	
	//C# point of view in a class defintion
	//A composite class can have a single occurring field AND use of other classes
	//Other classes maybe be a single instance OR collection<T> 
	//List<T>, IEnumerable<T>, IQueryable<T> is a collection with a define datatype of <T>
	//classname
	//  property
	//  property
	//  ....
	//  collection<T> (set of records, still a property)
	
	var results = Employees
					.Where(e => e.Title.Contains("Sales Support"))
					.Select(e => new EmployeeItem
							{
								FullName = e.LastName + ", " + e.FirstName,
								Title = e.Title,
								Phone = e.Phone,
								CustomerList = e.SupportRepCustomers
												//.Where(c => c.SupportRepId == e.EmployeeId)
												.Select(c => new CustomerItem
													{
														FullName = c.LastName + ", " + c.FirstName,
														City = c.City,
														State = c.State
													})
							});  //eos end of statement
	results.Dump();
}

// You can define other methods, fields, classes and namespaces here

public class EmployeeItem
{
	public string FullName {get;set;}
	public string Title {get;set;}
	public string Phone {get;set;}
	public IEnumerable<CustomerItem> CustomerList {get;set;}
}

public class CustomerItem
{
	public string FullName { get; set; }
	public string City { get; set; }
	public string State { get; set; }
}