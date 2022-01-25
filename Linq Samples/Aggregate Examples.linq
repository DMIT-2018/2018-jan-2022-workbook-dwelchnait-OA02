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

//Aggregates
//.Count()			counts the number of istances in a collection
//.Sum(x => ...)	sums (totals) a numeric field (numeric expression) in the collection
//.Min(x => ...) 	finds the minumum value of a collection of values for a field
//.Max(x => ...)	finds the maximum value of a collection of values for a field
//.Average(x => ...) finds the average value of a collection of values for a field

//IMPORTANT !!!!!
//Aggregates work ONLY on a collection of values for a particular field (expression)
//Aggregates DO NOT work on a single row (not the same as a collection with one row)

//syntax
//query
// (from ....
//   ...
//  select expression).aggregate()
//the expression is resoved to a single field value for Sum,Min,Max.Average

//method
//  collection.aggregate(x => expression) Sum,Min,Max.Average
//	NOTE: Count() does NOT use an expression
//	collection.Select(x => expression).aggregate()

//you can use multiple aggregates on a single column
//   collection.Sum(x => expression).Min(x => expression)

//Find the average playing time (length) of tracks in our music collection

//thought process
// average is an aggregate
// what is the collection? : a track is a member of the Tracks table
// what is the expression? : field Milliseconds representing the track length

//query
( from x in Tracks
	select x.Milliseconds).Average()

//method
//Tracks.Average() //Aborts because the collection has multiple fields, some non-numeric
Tracks.Average(t => t.Milliseconds )
Tracks.Select(t => t.Milliseconds).Average()

//List all albums of the 60s showing the album title, artist and various
//aggregates for albums contaning tracks

//for each album show the number of tracks, the longest playing track,
//the shortest playing track, the total price of all tracks and the
//average playing length of the album tracks

//Hint: Albums has two navigation properties
//		Artist points to the single parent record
//		Tracks points to the collection of child records (Tracks) of that album


