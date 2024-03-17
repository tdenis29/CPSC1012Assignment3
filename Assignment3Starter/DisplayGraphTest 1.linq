<Query Kind="Statements" />

void DrawBarChart(int[] sales,int[] dates)
{

    int dayMax = dates.Length ;
	int salesMax = sales.Max();
	int runningSalesMax = salesMax;
	string whiteSpaceColumn = "       ";
	//    foreach (var date in dates)
//    {
//      Console.WriteLine(new String('█', value * 10 / max) + $" {value}");
//    }
	//loop to make strings? because this loop is running on max sales so wee ned to loop for 750 / 50 = 14
	//to make rows 
	//int count or i can just be the count of columns so that at least is easy
	
	Console.WriteLine("--Daily Sales--");
	for(int i = salesMax; i >= 0; i--){
		if(i == salesMax){
		//if we make the string first then write to the console should work just fine.
		//in the first column we don need the whiteSPaceColumn but if for example its day 2
		// then we can make a string like $"{whiteSpaceColumn}{$456}}"
		//then 3 can be {whitespaceColumn}{whiteSpaceColumn}{$100}
			Console.WriteLine($" {runningSalesMax} | ");
		}
		else if(runningSalesMax == 50){
		    runningSalesMax = runningSalesMax - 50;
			Console.WriteLine($" {runningSalesMax} \t|");
		}
		else if(runningSalesMax == 0){
			Console.WriteLine($" {runningSalesMax} \t|");
		} else {
			runningSalesMax = runningSalesMax - 50;
			Console.WriteLine($" {runningSalesMax} | ");
		}
		if(runningSalesMax <= 0){
		Console.WriteLine("  --------------------------------------------------------------------------------------");
		
		for(int j = 0; j < dayMax; j++){
			if(j == 0){
			Console.Write("Days ");
			}
			Console.Write($"| Day {j + 1} ");
		}
		break;
		}
		
		
		
	}
}

// Example usage:
int[] days = { 1, 2, 3, 4, 5 };
int[] sales = {100, 500, 300, 600, 700};
DrawBarChart(sales, days);