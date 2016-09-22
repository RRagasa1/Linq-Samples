<Query Kind="Program">
  <Connection>
    <ID>5f5b2913-56b0-4104-b162-dfbbaa78f132</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
	//A list of bill counts for all Waiters
	//This query will create a flat dataset
	//The Columns are native datatypes (ie int, string, double, DateTime...)
	//One is not concerned with the repeated data in a column
	//Instead of using an anonymous datatype (new{...})
		//we wish the use a defined class definition
var bestWaiter = from x in Waiters
				select new WaiterBillCounts{
					Name = x.FirstName + " " + x.LastName,
					TCount = x.Bills.Count()
							};
							
bestWaiter.Dump();

var paramMonth = 5;
var paramYear = 2014;
var waiterBills = from x in Waiters
					where x.LastName.Contains("k")
					orderby x.LastName, x.FirstName
					select new WaiterBills {
							Name = x.LastName +", "+ x.FirstName,
							TotalBillCount = x.Bills.Count(), 
							BillInfo = (from y in x.Bills 
									where y.BillItems.Count() > 0
									y.BillDate.Month = paramMonth 
								&&  y.BillDate.Year = paramYear
									select new BillItemSummary {
										BillID = y.BillID,
										BillDate = y.BillDate,
										TableID = y.TableID,
										Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
									}
										).ToList()
								};		
waiterBills.Dump();
	
}

// Define other methods and classes here
//An example of a POCO class (flat)
public class WaiterBillCounts
{
	//whatever the receiving field on your queryin your Select,
		//there appears a property of that name in this class
	public string Name{get; set;}
	public int TCount{get; set;}
}

public class BillItemSummary
{
	public int BillID{get;set;}
	public DateTime BillDate{get;set;}
	public int? TableID{get;set;}
	public decimal Total{get;set;}
}

//An example of a DTO class (structure/collection)
public class WaiterBills
{
	public string Name{get;set;}
	public int TotalBillCount{get;set;}
	public List<BillItemSummary> BillInfo{get;set;} 
}
