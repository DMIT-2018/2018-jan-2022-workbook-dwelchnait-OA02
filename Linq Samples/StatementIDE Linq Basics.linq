<Query Kind="Statements">
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

//Statement IDE

//you can have multiple queries written in this IDE environment
//you can execute a query individually by highlighting
//	the desired query first
//BY DEFAULT executing the file in this environment will execute
//	ALL queries (statements) top to bottom

//IMPORTANT: Query syntax
//queries in this environment MUST be written using the
//	C# language grammar for a statement. This means that
//	each statement must end in a semi-colon
//results MUST be placed in a receiving variable
//to display the results, use the Linqpad method .Dump() (NOT a C# method)

//It apppears that Method syntax 
//  does NOT need a semi-colon on the query if run individually
//  does NOT need results placed in a receiving variable
//  does need the .Dump() method to display results

//Find all albums released in 2000.
//Display the entire album record

//Query
var paramyear = 1990; //image this is a parameter on a method signature
var resultsq = from x in Albums
				where x.ReleaseYear == paramyear
				select x;
resultsq.Dump(); //image this is the return statement in a method

//Method
Albums
	.Where(a => a.ReleaseYear == paramyear)
	.Select(a => a)
	.Dump();
