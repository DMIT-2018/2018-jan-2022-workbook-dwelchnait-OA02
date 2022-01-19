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

	// Smith, Bob sales support 780123456 //this is on employee
	//      Kan, Jerry edmonton ab			//this is on customer
	//      Stewant, Iam edmonton ab		//this is on customer
	//      Jest, Shirley U edmonton ab		//this is on customer
	//      Behold, Lowand edmonton ab		//this is on customer
	// Smith, Bob sales support 780123456
	//      Kan, Jerry edmonton ab
	//      Stewant, Iam edmonton ab
	//      Jest, Shirley U edmonton ab
	//      Behold, Lowand edmonton ab
	// Smith, Bob sales support 780123456
	//      Kan, Jerry edmonton ab
	//      Stewant, Iam edmonton ab
	//      Jest, Shirley U edmonton ab
	//      Behold, Lowand edmonton ab
	// Smith, Bob sales support 780123456
	//      Kan, Jerry edmonton ab
	//      Stewant, Iam edmonton ab
	//      Jest, Shirley U edmonton ab
	//      Behold, Lowand edmonton ab
	
	//there appears to be 2 separate lists that need to
	//	be within one final dataset collection
	//one for employees
	//one for customers
	//concern: the list are inter mixed!!!!
}

// You can define other methods, fields, classes and namespaces here