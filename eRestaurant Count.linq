<Query Kind="Expression">
  <Connection>
    <ID>ab6c59d8-5700-4ba2-899d-fcc25f953479</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from food in Items
	group food by new {food.MenuCategory.Description} into tempdataset
	select new  //DTO's - complex dataset 'MenuCategoryFoodItemsDTO'
		{
		MenuCategoryDescription = tempdataset.Key.Description,
		FoodItems = from x in tempdataset
		select new  //POCOs flat dataset 'FoodItem'
		{
		ItemID = x.ItemID,
		FoodDescription = x.Description,
		CurrentPrice = x.CurrentPrice,
		TimesServed = x.BillItems.Count()
		}
		}
	
	
from food in Items
	orderby food.MenuCategory.Description
	select new //POCOS -flat dataset 'MenuCategoryFoodItemsDPOCO'
		{
		MenuCategoryDescription = food.MenuCategory.Description,
		ItemID = food.ItemID,
		CurrentPrice = food.CurrentPrice,
		TimesServed = food.BillItems.Count()
		}