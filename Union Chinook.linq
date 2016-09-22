<Query Kind="Expression">
  <Connection>
    <ID>c89dc572-2111-492e-8f1d-19af6b7c4edb</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//syntax style for .Union() is
	//(query).Union(query2).Union(queryN).OrderBy(firstsortfield).ThenBY(othersortfield)
//RULES
	//number of columns are the same from both queries
	//column datatypes from both columns should be the same
	//orderBy, use the name of the anonymous datafields 

//to get both albums with tracks and without tracks
(from x in Albums
where x.Tracks.Count() > 0 
select new{
			Title = x.Title,
			TotalTracksforAlbum = x.Tracks.Count(),
			TotalPriceforAlbumTracks = x.Tracks.Sum(y => y.UnitPrice),
			AverageTrackLengthA = x.Tracks.Average(y => y.Milliseconds)/1000.0,//complete average (decimal)
			AverageTrackLengthB = (int)x.Tracks.Average(y => y.Milliseconds/1000.0)//average per track (int)
			}
).Union(
from x in Albums
where x.Tracks.Count() == 0
select new{
			Title = x.Title,
			TotalTracksforAlbum = 0,
			TotalPriceforAlbumTracks = 0.00m,
			AverageTrackLengthA = 0.00,
			AverageTrackLengthB = 0
			}
).OrderBy(y => y.TotalTracksforAlbum).ThenBy(y => y.Title)