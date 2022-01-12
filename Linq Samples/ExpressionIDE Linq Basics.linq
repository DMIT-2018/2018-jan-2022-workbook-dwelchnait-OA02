<Query Kind="Expression">
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

//our code is using C# grammar/syntax

//comments are done with slashes
//hotkey to comment ctrl+k,ctrl+c
//         uncomment ctrl+k,ctrl+u
//alternatively use ctrl + / as a toggle

//Expression IDE
//single linq query statements with out a semi-colon
//you can have multiple statements in your file BUT
//		if you do so, you MUST highlight the statement to execute

//executing: use F5 or the green triangle on the query menu
//  if the query seems to be not ending you can use the red square to terminate

//to toggle your results on and off (visible) use ctrl + r

//Query syntax
//uses a "sql-like" syntax
//view the Student Notes for examples

//from rowinstanceplaceholder in Albums
//select rowinstanceplaceholder

//Method syntax
//uses C# method syntax (OOP language grammar)
//Collection is Albums
//to execute a method on a collection you need to use
//		the access operator (dot operator)
//this results in the returning of an other collection from the method !!***
//method name starts with a capital
//methods contain contents as a delegate
//a delegate describes the action to be done

//Albums
//	.Select(rowinstanceplaceholder => rowinstanceplaceholder)
	
//Find all albums released in 2000.
//Display the entire album record

//Query
from x in Albums
where x.ReleaseYear == 2000
select x

//Method
Albums
	.Where(a => a.ReleaseYear == 2000)
	.Select(a => a)
	
	
	
	
	