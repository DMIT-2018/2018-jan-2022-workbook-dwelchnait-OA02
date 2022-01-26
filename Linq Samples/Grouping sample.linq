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

//Grouping

//when you create a group it builds two (2) components
//	a) Key component (group by criteria values)
//		reference this component using the groupname.Key[.Property]
//  (property < - > column < - > field < - > attribute < - > value)
//	b) the data of the group (instances of the original collection)

//ways to group
//a) by a single property (column,field, attribute,value)	groupname.Key
//b) by a set of properties (anonymous key set)				groupname.Key.PropertyName
//c) by using an entity (x.navproperty) ** try to avoid **	groupname.Key.PropertyName

//concept processing
//start with a "pile" of data (original collection)
//specify the grouping criteria (value(s))
//result of the group operation will be to "place the data into smaller piles"
//	the piles are dependant on the grouping criteria value(s)
//	the grouping criteria (property (ies)) become the Key
//	the individual instances are the data in the smaller piles
//	the entire individual instance of the original collection is placed in the
//		smaller pile
//Manipulation of each of the "smaller piles" is now possible with your linq commands


//grouping is different then Ordering
//Ordering is the re-sequencing of a collection for display
//Groupind re-organizes a collection into separate, usually smaller,
//	collections for processing

//Display albums by ReleaseYear
//	this request does NOT need grouping
//	this request is an re-sequencing (ordering) of output (OrderBy)
//	this affects display only
Albums
	.OrderBy(a => a.ReleaseYear)

//Display albums grouped by ReleaseYear
//	NOT one display of albums but displays of album for a specified value (ReleaseYear)
//	explicit request to breakup the display into desired "piles" (collection)
Albums
	.GroupBy(a => a.ReleaseYear)
	
//query syntax
from a in Albums
group a by a.ReleaseYear

//processing on the created groups of the GroupBy method
Albums
	.GroupBy(a => a.ReleaseYear)	//this method returns a collection of "mini-collections"
	.Select(eachgPile => new
		{
			Year = eachgPile.Key,	//Key component	
			NumberofAlbums = eachgPile.Count() //processing of "mini-collection" data
		}) //the Select is processing each mini-collection one at a time


//query syntax
//using this syntax you MUST specify the name you wish to use to refer to the
//	grouped (mini-collections) collection
//AFTER coding your group command you MUST (are restricted to) use the name you
//	have given your group collection
from a in Albums
//orderby a.ReleaseYear : would be valid because "a" is in context
//orderby eachgPile.Key	: would not be valid because grouping not specified yet
group a by a.ReleaseYear into eachgPile
//orderby a.ReleaseYear :would be invalid because "a" is out of context, group name is eachgPile
//orderby eachgPile.Key :would be valid because eachgPile is currently in context and Key has your year
select new
{
	Year = eachgPile.Key,   //Key component	
	NumberofAlbums = eachgPile.Count() //processing of "mini-collection" data
}

//use a multiple set of criteria (properties) to form the group
//also, include a nested query to report on the "mini-collection" (small piles) of the grouped data

//Display albums grouped by ReleaseLabel then ReleaseYear. Display the
// ReleaseYear and the number of albums. List only the years with
// 2 or more albums release.
//For each album display the title, year of release and count of tracks.

//original collection (large pile of data)?: Albums
//filtering cannot be decided until the groups are created
//grouping: ReleaseLabel, ReleaseYear (anonymous key set: object)
//now filtering can be done on the group: group.Count >= 2
//report the year and number of albums, nested query to report details per album: title, year, # of tracks

Albums
	.GroupBy(a => new { a.ReleaseLabel, a.ReleaseYear}) //creating anonymous key set
	.Where(eachgPile => eachgPile.Count() >= 2)
	.OrderBy(eachgPile => eachgPile.Key.ReleaseLabel)
	.Select(eachgPile => new 
		{
			Label = eachgPile.Key.ReleaseLabel,
			Year = eachgPile.Key.ReleaseYear,
			NumberofAlbums = eachgPile.Count(),
		    AlbumsGroupItems = eachgPile  //small pile (mini collection)
								.Select(eachgPileInstance => new 
									{
										Title = eachgPileInstance.Title,
										Year = eachgPileInstance.ReleaseYear,
										NumberofTracks = eachgPileInstance.Tracks.Count()
									})
		})
	







