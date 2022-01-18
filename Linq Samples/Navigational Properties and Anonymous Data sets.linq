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

//Using Navigational Properties and Anonymous data set (collection)

//reference: Student Notes/Demos/eRestaurant/Linq: Query and Method Syntax/Expressions


//Find all albums released in the 90's (1990-1999)
//Order the albums by ascending year and then alphabetically by album title
//Display the Year, Title, Artist Name and Release Label

//concerns: a) not all properties of Album are to be displayed
//			b) the order of the properties are to be displayed
//				in a different sequence then the definition of
//				the properties on the entity
//			c) the artist name is NOT on the Album table but on
//				the Artist table

//solution: use an anonymous data set

//the anonymous instance is defined within the Select by
//	the declared fields (properties) 
//the order of the fields on the instance is defined during
//	the specification of your code

Albums
	.Where(x => x.ReleaseYear > 1989 && x.ReleaseYear < 2000)
	.Select(x => new 
			{
				Year = x.ReleaseYear,
				Title = x.Title,
				Artist = x.Artist.Name,
				Label = x.ReleaseLabel
			})
	.OrderBy(x => x.Year)  //Year is in the anonymous data type define, ReleaseYear is NOT
	.ThenBy(x => x.Title)
	
	
	
	
	
	
	
	
	
	
	
	