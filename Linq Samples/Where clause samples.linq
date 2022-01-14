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

//Where clause

//filter clause
//the conditions are setup as you would in C#
//beware that Linqpad may NOT like some C# syntax (DateTime)
//beware that Linq is converted to SQL which may not
//	like certains C# syntax because it could not be converted

//syntax
//query
// where condition [logical operator condition2 ....]
//method
//	notice that the method syntax make use of Lambda expressions
// .Where(Lambda expression)
// .Where(x => condition [logical operator condition2 ...])

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

//Find all albums released in the 90s (1990-1999)
//Display the entire album record
Albums
	.Where(a => a.ReleaseYear >= 1990 
			&& a.ReleaseYear <= 1999)
	.Select(a => a)

//Find all the albums of the artist Queen.
//concern: the artist name is in another table
//			in an Sql Select query you would be using an inner Join
//			in Linq you DO NOT want to specify your joins unless absolutely necessary
//			instead use the "navigational properties" of your entity
//				to generate the relationship

//.Equals() is an exact match of a string (==), in sql = or like 'string'
//.Contains() is a partial string match, in sql like '%string%'
//for numerics use your relative operators (==, >, <, <=, >=<, !=)
Albums
	.Where(a => a.Artist.Name.Equals("Queen"))
	.Select(a => a)


//Find all albums wher the producer (label) is unknow (null)
Albums
	.Where(a => a.ReleaseLabel == null)
	.Select(a => a)