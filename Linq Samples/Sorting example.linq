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

//Sorting

//there is a significant difference between query and method syntax

//query syntax is much like sql
//		orderby field {[ascending]|descending} [, field {[ascending]|descending}...]

//ascending is the default option

//method syntax is a series of individual methods
//start up
//	.OrderBy(x => x.field)    			//ascending
//  .OrderByDescending(x => x.field)	//descending
//after one of these two beginning methods
//if you have any other field(s)
//	.ThenBy(x => x.field)				//ascending
//	.ThenByDescending(x => x.field)		//descending

//Find all albums released in the 90's (1990-1999)
//Order the albums by ascending year and then alphabetically by album title
//Display the entire album record

//if no clear specification on ascending or descending, normal one assumes ascending
//often the ordering phrase may be done with the work "within"
//without the "within" the implied order of your fields are major to minor
//with the "within" the implied order is minor to major for your list of fields
//  (order alphabetically by album title within year)


//query syntax
from x in Albums
orderby x.ReleaseYear, x.Title
where x.ReleaseYear > 1989 && x.ReleaseYear <2000
select x

//method syntax
Albums
	.Where( x =>x.ReleaseYear > 1989 && x.ReleaseYear <2000)
	.OrderBy(x => x.ReleaseYear)
	.ThenBy(x => x.Title)
	.Select(x => x)






