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

//Using the Ternary Operator

//   conditions(s) ? true value : false value

//both the true value and false value MUST resolve to a
//	SINGLE piece of data (a single Value)

//compare to the conditional statement

//if (condition(s))
// true path (complex logic)
//else
// false path (complex logic)

//just like the conditional statement which can
//	have nested logic, the true value and false value
//	can have nested ternary operators as long as
//	the final results resolves to a SINGLE value

//List all ablums by release label. Any ablum with
//no label should be indicicated as Unkown. 
//List Title, Label, and Artist Name.

//understand problem
//	collection: albums
//	selective data set: anonymous data set
//  ordering: releaselabel
//  label: either Unknown or label  ******

//design
//  Albums
//	.OrderBy : label
//  .Select( new{})
//	using nav property Artist Name
//   ??? assigning the label (condition ? label : unknown)

//coding and testing
Albums
	
	.Select(a => new 
		{
			Title = a.Title,
			Label = a.ReleaseLabel == null ? "Unknown" : a.ReleaseLabel,
			Artist = a.Artist.Name
		})
	.OrderBy(a => a.Title)

//List all albums showing the Title, Artist name, Year and decade of
//	releases (oldies, 70s, 80s, 90s, or modern)
//Order by decade.










