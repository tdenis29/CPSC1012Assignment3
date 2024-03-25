<Query Kind="Statements" />

int[] days = { 1, 2, 3, 4, 5 };
int[] sales = {100, 500, 300, 600, 700};
DrawBarChart(sales, days);

static string AddSpacing(string inputString, int spacingCount)
    {
        StringBuilder modifiedString = new StringBuilder(inputString);

        // Loop through the original string from the end to the beginning
        for (int i = inputString.Length - 1; i >= 0; i--)
        {
            // Insert spaces at each index minus 1
            modifiedString.Insert(i, new string(' ', spacingCount));
        }

        return modifiedString.ToString();
    }


void DrawBarChart(int[] sales,int[] dates)
{

    int dayMax = dates.Length ;
	int salesMax = sales.Max();
	int runningSalesMax = salesMax;
	string whiteSpaceColumn = "       ";

	string currString = "";
	string withSpacing = "";
   //string whitespaceToAdd = new string(' ', 8); // 8 whitespace characters
   //         string modifiedString = originalString.Insert(index, whitespaceToAdd);
   //         Console.WriteLine(modifiedString); // Output: ex        ample
	
	
	Console.WriteLine("--Daily Sales--");
	for(int i = salesMax; i >= 0; i--){
		if(i == salesMax){
			Console.Write($" {runningSalesMax} | ");
			for(int j = 0; j < sales.Length; j++){
				if(sales[j] == runningSalesMax){
				Console.WriteLine($"${sales[j]}");
			 }
		  }
		}
		else if(runningSalesMax == 50){
		    runningSalesMax = runningSalesMax - 50;
			Console.WriteLine($" {runningSalesMax} |");
		}
		else if(runningSalesMax == 0){
			Console.WriteLine($" {runningSalesMax} |");
		} else {
			runningSalesMax = runningSalesMax - 50;
			currString = $" {runningSalesMax} |";
			SpacingString += AddSpacing(currString, i);
			 }
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
	


// Example usage:
