<Query Kind="Statements">
  <Connection>
    <ID>5f5b2913-56b0-4104-b162-dfbbaa78f132</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//a statement has a receiving variable which is set by the results of a query
//when you need to use multiple steps to solve a problem, switch your language choice 
//to either Statement(s) or Program
//The most popular Media type for the tracks
//use MAX()


var maxCount = (from x in MediaTypes
				select x.Tracks.Count()).Max();
//to display the contents of a variable in LinqPad, you use the method .Dump();
maxCount.Dump();

//to filter data, you can use the where clause
//uses a previously created variable value in a following statement
var mediaTypeCounts = from x in MediaTypes
				where x.Tracks.Count() == maxCount 
				select new {
							Name = x.Name,
							TrackCount = x.Tracks.Count()
							};
							
mediaTypeCounts.Dump();

//can the set of statements above be written as one complete query - possibly
//in this example, maxcount could be exchanged for the query that actually created the value in the first place
//this substitution query is a nested query
//The nested query needs its own instance identifier, in this case 'y' as 'x' is already being used.
var mediaTypeCountsNested = from x in MediaTypes
				where x.Tracks.Count() == (from y in MediaTypes
										select y.Tracks.Count()).Max()
				select new {
							Name = x.Name,
							TrackCount = x.Tracks.Count()
							};
							
mediaTypeCountsNested.Dump();

//using a method syntax to determine the count value for the where expression
//this demonstrates that queries can be constructed using both query syntax and method syntax
var mediaTypeCountsMethod = from x in MediaTypes
				where x.Tracks.Count() == MediaTypes.Select (y => y.Tracks.Count()).Max()
				select new {
							Name = x.Name,
							TrackCount = x.Tracks.Count()
							};
							
mediaTypeCountsMethod.Dump();