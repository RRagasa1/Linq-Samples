<Query Kind="Expression">
  <Connection>
    <ID>5f5b2913-56b0-4104-b162-dfbbaa78f132</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

/*
Linq expressions, statements, programs are written using C# syntax
*/

//query syntax list all records from entity
from x in Artists
select x

//method syntax list all records from entity
Artists.Select(x => x)

//sort Albums by title
from x in Albums
orderby x.Title
select x

//sort Albums by release year(most current), then title within the release year
from x in Albums
orderby x.ReleaseYear descending, x.Title 
select x

//list all albums belonging to artist
//the select is obtaining a subset of attributes from the choosen tables
//the new {} is called an anonymous data set
//anonymous datasets are IOrderedQueryable<>
from x in Albums
select new{
			x.Artist.Name, 
			x.Title
		}

//list all albums belonging to artist where a condition exists
//find albums released in a particular year
from x in Albums
where x.ReleaseYear == 2008
orderby x.Artist.Name, x.Title
select new{
			x.Artist.Name, 
			x.Title
		}
