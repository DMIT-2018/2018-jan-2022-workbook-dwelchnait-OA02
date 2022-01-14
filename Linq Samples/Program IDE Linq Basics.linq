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
	//Program IDE
	//you can have multiple queries written in this IDE evironment
	//this environment works "like" a console application

	//this allows one to pre-test complete components that can
	//	be moved directly into your backend application (class library)

	//IMPORTANT: 
	//queries in this environment MUST be written using the
	//	C# language grammar for a statement. This means that
	//	each statement must end in a semi-colon
	//results MUST be placed in a receiving variable
	//to display the results, use the Linqpad method .Dump() (NOT a C# method)


	//Query
	var paramyear = 1990; //image this is a BindProperty variable on web page
	List<Albums> resultsq = GetAllQ(paramyear);
	resultsq.Dump(); //image this is the return statement in a method

	//Method
	List<Albums> resultsm = GetAllM(paramyear);
	resultsm.Dump();
}

// You can define other methods, fields, classes and namespaces here

//imagine this is a method in your BLL service

	public List<Albums> GetAllQ(int paramyear)
	{
		var resultsq = from x in Albums
					   where x.ReleaseYear == paramyear
					   select x;
		return resultsq.ToList();
	}

public List<Albums> GetAllM(int paramyear)
{
	var resultsm =  Albums
		.Where(a => a.ReleaseYear == paramyear)
		.Select(a => a);
	return resultsm.ToList();
}


