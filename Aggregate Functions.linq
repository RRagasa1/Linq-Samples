<Query Kind="Expression">
  <Connection>
    <ID>5f5b2913-56b0-4104-b162-dfbbaa78f132</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//list of all customers supported by the employee 'Jane Peacock'
//List lastname, firstname, city, state, phone and e-mail in alphabetical order


//this sample requires a subset of the entity record
//the data needs to be filtered for specific select thus a Where is needed
//Using the navigation name on Customer, one can access the associate Employee record
//REMINDER: this is c# syntax and thus appropriate methods can be used .Equals()
from x in Customers
where x.SupportRepIdEmployee.FirstName.Equals("Jane") &&
	x.SupportRepIdEmployee.LastName.Equals("Peacock")
orderby x.LastName, x.FirstName
select new {
			Name = x.FirstName + " " + x.LastName,
			City = x.City,
			State = x.State,
			Phone = x.Phone,
			Email = x.Email
			}
			
//List all the Albums and the total number of tracks for that Album
//List albums alphabetically

//for aggregates, it is best to consider doing parent->child direction.
//aggregates are used against collections (multiple records)
//Count() counts the number of instances of the collection referenced
from x in Albums
orderby x.Title
select new{
			Album = x.Title,
			NoOfTracks = x.Tracks.Count(),
			}

//List all the Albums and the total number of tracks for that Album
//List albums alphabetically
//find the total price of each set of album tracks
//null error could occur if a collection is empty for specific aggregate(s) such as Sum()
	//thus you may need to filter (where) the certain records from your query
//Sum() totals a specific field/expression, thus you will likely need to use a delegate to indicate the collection instance attribute to be used.
//example: SUM( y=> y.UnitPrice * y.Quantity)
from x in Albums
orderby x.Title
where x.Tracks.Count() > 0
select new{
			Album = x.Title,
			NoOfTracks = x.Tracks.Count(),
			TotalPrice = x.Tracks.Sum(y => y.UnitPrice)
			}
			

//find the average lenth of the album tracks in seconds
//Average() averages a specific field/expression, thus you will likely need to use a delegate to indicate the collection instance attribute to be used.
from x in Albums
orderby x.Title
where x.Tracks.Count() > 0
select new{
			Album = x.Title,
			NoOfTracks = x.Tracks.Count(),
			TotalPrice = x.Tracks.Sum(y => y.UnitPrice),
			AverageLengthOfTracksInSecondsA = x.Tracks.Average(y => y.Milliseconds)/1000,//complete average (decimal)
			AverageLengthOfTracksInSecondsB = x.Tracks.Average(y => y.Milliseconds/1000)//average per track (int)
			}
			
//The most popular Media type for the tracks
//use MAX()
