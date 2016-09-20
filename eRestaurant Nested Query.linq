<Query Kind="Statements">
  <Connection>
    <ID>5f5b2913-56b0-4104-b162-dfbbaa78f132</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//find the waiter with the most Bills


//step a) get a list of bill counts by waiter and determine the max
var maxBillCount = (from x in Waiters
				select x.Bills.Count()).Max();
maxBillCount.Dump();

//step b) using the maxbillcount on the where clause, find the waiter that matches the count
var bestWaiter = from x in Waiters
				where x.Bills.Count() == maxBillCount 
				select new {
							Name = x.FirstName + " " + x.LastName
							};
							
bestWaiter.Dump();

//to confirm:
var maxBillCount = (from x in Waiters
				select x.Bills.Count()).Max();
maxBillCount.Dump();

var bestWaiter = from x in Waiters
				//where x.Bills.Count() == maxBillCount 
				select new {
							Name = x.FirstName + " " + x.LastName,
							billCounts = x.Bills.Count()
							};
							
bestWaiter.Dump();

//create a dataset that has an unknown number of records associated with a data value
//a list of all bills associated with the waiter. List all waiters
//the inner nested query uses the associated Bill records of the currently processing Waiter--> x.Collection
var waiterBills = from x in Waiters
					orderby x.LastName, x.FirstName
					select new {
							Name = x.LastName +", "+ x.FirstName,
							TotalBillCount = x.Bills.Count(), 
							BillInfo = (from y in x.Bills 
									where y.BillItems.Count() > 0
									select new {
										BillID = y.BillID,
										BillDate = y.BillDate,
										TableID = y.TableID,
										Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
									}
										)
								};		
waiterBills.Dump();