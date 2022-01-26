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
							.Dump()
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