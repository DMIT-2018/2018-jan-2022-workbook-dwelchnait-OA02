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
	//Strongly typed query dataset
	
	//Anonymous dataset from a query does NOT have a permament specified class definition (dynamic)
	//Strongly type query dataset HAS  a permament class definition in its code
	
	//Find all songs that contain a partial string of a track name.
	//List the Album, song (track name), Artist
	
	//image your solution need to mimic a web app query to some BLL service
	
	string partialSongName = "dance"; //mimic your [BindProperty] variable {get;set;}
	List<Songs> results = SongsByPartialName(partialSongName);
	results.Dump(); //mimic your table display on the web page
}

// You can define other methods, fields, classes and namespaces here

//developer defined data type

public class Songs
{
	public string AlbumTitle {get;set;}
	public string Song {get;set;}
	public string Artist {get;set;}
}

List<Songs> SongsByPartialName(string partialSongName)
{
	//to change an Anonymous dataset to a strongly type dataset
	//	add the datatype to the instance creation operator (new)
	

	IEnumerable<Songs> songCollection = Tracks
						.Where(tr => tr.Name.Contains(partialSongName))
						.Select(tr => new Songs
							{
								AlbumTitle = tr.Album.Title,
								
								Song = tr.Name,
								Artist = tr.Album.Artist.Name
							});
	return songCollection.ToList();
}