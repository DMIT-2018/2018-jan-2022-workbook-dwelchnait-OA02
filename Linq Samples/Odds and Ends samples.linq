<Query Kind="Program">
  <Connection>
    <ID>ac19c5d1-a507-4043-bafb-b647e09ebfb9</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//conversion .ToList()
	
	//display all albums and their tracks. Display the album title,
	//artist name and album tracks. For each track show the song name,
	//and play time in seconds. Show only albums with 25 or more tracks.
	
	List<AlbumTracks> albumlist = Albums
					.Where(a => a.Tracks.Count( ) >= 25)
					.Select(a => new AlbumTracks
							{
								Title = a.Title,
								Artist = a.Artist.Name,
						        Songs = a.Tracks
										.Select(tr => new SongItem
												{
													Song = tr.Name,
													Playtime = tr.Milliseconds / 1000.0
												})
										.ToList()
							})
					  .ToList()
					//.Dump()
					;
	//typically if the albumlist was a var variable in your BLL method
	//AND the method return datatype was a List<T>, one could, on the
	//return statement do: return albumlist.ToList(); (saw in 1517 course)
	
	//Using .FirstOrDefault()
	//great for looks that you expect 0, 1 or more instances returned
	
	//Find the first ablum of Deep Purple
	
	string artistparam = "Deep Purple";
	var resultsFOD = Albums
					.Where(a => a.Artist.Name.Equals(artistparam))
					.Select(a => a)
					.OrderBy(a => a.ReleaseYear)
					.FirstOrDefault()
					//.Dump()
					;
	//if (resultsFOD != null)
	//	resultsFOD.Dump();
	//else
	//	Console.WriteLine($"No albums found for {artistparam}");
	//	
	//Using SingleOrDefault
	//expecting at most a single instance being return
	
	//Find the album by the albumid
	int albumid = 1;
	var resultsSOD = Albums	
					.Where(a => a.AlbumId == albumid)
					.Select(a => a)
					.SingleOrDefault()
					;
	//if (resultsSOD != null)
	//	resultsSOD.Dump();
	//else
	//	Console.WriteLine($"No album found for id of {albumid}");
	//	
	
	//Distinct()
	//removes duplicate reported lines
	
	//Obtain a list of customer countries.
	var resultsDistinct = Customers
							.OrderBy(c => c.Country)
							.Select(c => c.Country)
							.Distinct()
							//.Dump()
							;
							
	//.Take() and .Skip()
	//in 1517, when you wanted to use your paginator
	//		the query method was to return ONLY the 
	//		needed records to display
	//a) you passed in the pagesize and pagenumber
	//b) the query was executed, returning all rows
	//c) set your out parameter to the .Count of rows
	//d) calculated the number of rows to skip (pagenumber - 1) * pagesize
	//e) on the return statement, against your collection
	//		you used a .Skip and .Take
	//     return variablename.Skip(rowsSkipped).Take(pagesize).ToList();
	
	//Any and All
	//There are 25 Genres on file
	
	//show genres that have tracks which are not on any playlist
	Genres
		.Where(g => g.Tracks.Any(tr => tr.PlaylistTracks.Count() == 0))
		.Select(g => g)
		//.Dump()
		;

	//show genres that have all tracks appearing at least once on a playlist
	Genres
		.Where(g => g.Tracks.All(tr => tr.PlaylistTracks.Count() > 0))
		.Select(g => g)
		//.Dump()
		;

	//there maybe times that using a !Any() -> All(!relationship)
	//and a !All() -> Any(!relationship)
	
	//Using All and Any in comparing 2 complex collections
	//If your collection is NOT a complex record (list of integers, or strings) there
	//	is a Linq method called .Except that can be used to solve your query
	
	//https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-6.0
	//https://dotnettutorials.net/lesson/linq-except-method/
	
	//Compare the track collection of 2 people using All and Any
	//create a small anonymous collection for two person
	// Roberto Almeida(AlmeidaR) and Michelle Brooks(BrooksM)
	
	var almeida = PlaylistTracks
					.Where(x => x.Playlist.UserName.Equals("AlmeidaR"))
					.Select(x => new
						{
							song = x.Track.Name,
							genre = x.Track.Genre.Name,
							id = x.TrackId,
							artist = x.Track.Album.Artist.Name
						})
					.Distinct()
					.OrderBy(x => x.song)
					//.Dump() //110
					;


	var brooks = PlaylistTracks
					.Where(x => x.Playlist.UserName.Equals("BrooksM"))
					.Select(x => new
					{
						song = x.Track.Name,
						genre = x.Track.Genre.Name,
						id = x.TrackId,
						artist = x.Track.Album.Artist.Name
					})
					.Distinct()
					.OrderBy(x => x.song)
					//.Dump() //88
					;
					
	//List the tracks that BOTH Roberto and Michelle like
	//	compare 2 collections together (ListA and ListB)
	//	assume ListA is Roberto and ListB is Michelle
	//	ListA is the collection you wish to report on
	//  ListB is what you wish to compare ListA to (no reporting)
	
	almeida 
		.Where (rob => brooks.Any(mic => mic.id == rob.id))
		.OrderBy(rob => rob.song)
		//.Dump()
		;


	brooks
		.Where(mic => almeida.Any(rob => mic.id == rob.id))
		.OrderBy(mic => mic.song)
		//.Dump()
		;

	//What songs does Roberto like but not Michelle 
	almeida
		.Where(rob => !brooks.Any(mic => mic.id == rob.id))
		.OrderBy(rob => rob.song)
		//.Dump()
		;

	almeida
			.Where(rob => brooks.All(mic => mic.id != rob.id))
			.OrderBy(rob => rob.song)
			//.Dump()
			;
	//What songs does Michelle like but not Roberto 
	brooks
		.Where(mic => !almeida.Any(rob => rob.id == mic.id))
		.OrderBy(mic => mic.song)
		//.Dump()
		;
		
	//Unions
	//since Linq is converted into Sql one would expect that the
	//		Sql Union rule must be the same in Linq
	//purpose: concatenating multiple resullts into one collection
	//syntax (queryA).Union(queryB)[.Union(query...)]
	//rules:
	//	number of columns the same
	//	column datatypes must match
	//	ordering should be done as a method after the last Union
	
	//List the stats (count, cost, average track length) of Albums on Tracks
	//NOTE: for cost and average, one will need an instance in tracks to do
	//			the aggregation
	
	//concern: What is the Album does not have an recorded tracks
	
	//Albums with no tracks on the database will have a count, however
	//		cost and average length will be 0 (no instances to aggregate)
	
	//solution: create two queries; one handling no tracks and one handling
	//				albums with tracks then UNION the two results
	
	//NOTE: IF you are hard coding numeric fields, the query with the hard
	//			coded value MUST be the first query
	
	//queryA would be Albums with no tracks (hard code cost and average)
	//queryB would be Albums with tracks(cost and avaerage will be calculated)
	
	(Albums
		.Where(x => x.Tracks.Count( ) == 0)
		.Select(x => new
				{
					title = x.Title,
					totalTracks = x.Tracks.Count(),
					totalCost = 0.00m,
					averagelength = 0.00
				}))
		.Union(Albums
			.Where(x => x.Tracks.Count() > 0)
			.Select(x => new
			{
				title = x.Title,
				totalTracks = x.Tracks.Count(),
				totalCost = x.Tracks.Sum(t => t.UnitPrice),
				averagelength = x.Tracks.Average(t => t.Milliseconds)
			}))
		.OrderBy(c => c.totalTracks)
		.Dump()
		;

}

// You can define other methods, fields, classes and namespaces here

public class SongItem
{
	public string Song{get;set;}
	public double Playtime{get;set;}
}

public class AlbumTracks
{
	public string Title{get;set;}
	public string Artist{get;set;}
	public List<SongItem> Songs{get;set;}
}