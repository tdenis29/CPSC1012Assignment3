///// <summary>
///// </summary>

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Windows.Markup;
using System.Text.RegularExpressions;
using System.Globalization;

/// Assignment 3
/// 
/// Author: 
/// Date: 
/// Purpose: Allows user to enter/save/load/edit/view daily sales values
///          from a file. Allows and displays simple data analysis
///          (mean/max/min/graph) of sales values for a given month.
internal class Program
{
    private static void Main(string[] args)
    {
        string mainMenuChoice;
        string analysisMenuChoice;
        bool displayMainMenu = true;
        bool displayAnalysisMenu;
        bool quit;
        // TODO: declare a constant to represent the max size of the sales
        // and dates arrays. The arrays must be large enough to store
        // sales for an entire month.
        const int daysOfMonth = 31;

        // TODO: create a double array named 'sales', use the max size constant you declared
        // above to specify the physical size of the array.
        double[] sales = new double[daysOfMonth];


        // TODO: create a string array named 'dates', use the max size constant you declared
        // above to specify the physical size of the array.
        string[] dates = new string[daysOfMonth];



        string month;
        string year;
        string filename;
        int count = 0;
        bool proceed;
        double mean;
        double largest;
        double smallest;

        DisplayProgramIntro();

        // TODO: call the DisplayMainMenu method
		DisplayMainMenu();

        while (displayMainMenu)
        {
            mainMenuChoice = Prompt("Enter MAIN MENU option ('D' to display menu): ").ToUpper();
            Console.WriteLine();
    

            //MAIN MENU Switch statement
            switch (mainMenuChoice)
            {
                case "N": //[N]ew Daily Sales Entry

                    proceed = NewEntryDisclaimer();

                    if (proceed)
                    {
                        // TODO: uncomment the following and call the EnterSales method below
                        count = EnterSales(sales, dates);
                        Console.WriteLine();
                        Console.WriteLine($"Entries completed. {count} records in temporary memory.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Cancelling new data entry. Returning to MAIN MENU.");
                    }
                    break;
                case "S": //[S]ave Entries to File
                    if (count == 0)
                    {
                        Console.WriteLine("Sorry, LOAD data or enter NEW data before SAVING.");
                    }
                    else
                    {
                        proceed = SaveEntryDisclaimer();

                        if (proceed)
                        {
                            filename = PromptForFilename();
                            // TODO: call the SaveSalesFile method here
                            SaveSalesFile(sales, dates, filename, count);

                        }
                        else
                        {
                            Console.WriteLine("Cancelling save operation. Returning to MAIN MENU.");
                        }
                    }
                    break;
                case "E": //[E]dit Sales Entries
                    if (count == 0)
                    {
                        Console.WriteLine("Sorry, LOAD data or enter NEW data before EDITING.");
                    }
                    else
                    {
                        proceed = EditEntryDisclaimer();

                        if (proceed)
                        {
                            // TODO: call the EditEntries method here

                        }
                        else
                        {
                            Console.WriteLine("Cancelling EDIT operation. Returning to MAIN MENU.");
                        }
                    }
                    break;
                case "L": //[L]oad Sales File
                    proceed = LoadEntryDisclaimer();
                    if (proceed)
                    {
                        filename = Prompt("Enter name of file to load: ");
                        // TODO: uncomment the following and call the LoadSalesFile method below
                        count = LoadSalesFile(filename, sales, dates);
                        Console.WriteLine($"{count} records were loaded.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Cancelling LOAD operation. Returning to MAIN MENU.");
                    }
                    break;
                case "V":
                    if (count == 0)
                    {
                        Console.WriteLine("Sorry, LOAD data or enter NEW data before VIEWING.");
                    }
                    else
                    {
                        DisplayEntries(sales, dates, count);

                    }
                    break;
                case "M": //[M]onthly Statistics
                    if (count == 0)
                    {
                        Console.WriteLine("Sorry, LOAD data or enter NEW data before ANALYSIS.");
                    }
                    else
                    {
                        displayAnalysisMenu = true;
                        while (displayAnalysisMenu)
                        {
                            // TODO: call the DisplayAnalysisMenu here
							DisplayAnalysisMenu();

                            analysisMenuChoice = Prompt("Enter ANALYSIS sub-menu option: ").ToUpper();
                            Console.WriteLine();
                            Console.WriteLine($"{analysisMenuChoice}");
                            switch (analysisMenuChoice)
                            {
                                case "A": //[A]verage Sales
                                          // TODO: uncomment the following and call the Mean method below
                                          mean = MeanAverageSales(sales, count);
                                          month = dates[0].Substring(0, 3);
                                          year = dates[0].Substring(7, 4);
                                          Console.WriteLine($"The mean sales for {month} {year} is: {mean:C}");
                                          Console.WriteLine();
                                    break;
                                case "H": //[H]ighest Sales
                                          // TODO: uncomment the following and call the Largest method below
                                          largest = Largest(sales, count);
                                          month = dates[0].Substring(0, 3);
                                          year = dates[0].Substring(7, 4);
                                          Console.WriteLine($"The largest sales for {month} {year} is: {largest:C}");
                                          Console.WriteLine();
                                    break;
                                case "L": //[L]owest Sales
                                          // TODO: uncomment the following and call the Smallest method below
                                          smallest = LowestSales(sales, count);
                                          month = dates[0].Substring(0, 3);
                                          year = dates[0].Substring(7, 4);
                                          Console.WriteLine($"The smallest sales for {month} {year} is: {smallest:C}");
                                          Console.WriteLine();
                                    break;
                                case "G": //[G]raph Sales
                                          // TODO: call the DisplayChart method below
                                            DisplaySalesChart(sales, dates, count);
                                            Prompt("Press <enter> to continue...");
                                            break;
                                case "R": //[R]eturn to MAIN MENU
                                    displayAnalysisMenu = false;
                                    break;
                                default: //invalid entry. Reprompt.
                                    Console.WriteLine("Invalid reponse. Enter one of the letters to choose a submenu option.");
                                    break;
                            }
                        }
                    }
                    break;
                case "D":
					DisplayMainMenu();
                    break;
                case "Q": //[Q]uit Program
                    quit = Prompt("Are you sure you want to quit (y/N)? ").ToLower().Equals("y");
                    Console.WriteLine();
                    if (quit)
                    {
                        displayMainMenu = false;
                    }
                    break;
                default: //invalid entry. Reprompt.
                    Console.WriteLine("Invalid reponse. Enter one of the letters to choose a menu option.");
                    break;
            }
        }

        DisplayProgramOutro();
     

        // TODO: create the Prompt method
		static string Prompt(string prompt)
		{
			string userString;
			Console.Write(prompt);
			try
			{
				userString = Console.ReadLine();
			}
			catch
			{
				throw new FormatException();
			}
			return userString;
	
		}

        // TODO: create the PromptDouble method
        // The method must always return a double and should not crash the program.
		static double PromptDouble(){
			string userInput;
			double userDouble = 0.0;
			bool exit = false;

			while(!exit){
				Console.WriteLine("Please enter a double value.");
				try{
					userInput = Console.ReadLine();
					userDouble = double.Parse(userInput);
					exit = true;
				}
				catch (FormatException ex){
					Console.WriteLine(ex.Message);
				}
			}
            return userDouble;
        }


        // TODO: create the DisplayMainMenu method
        // the menu must consist of the following options:
		static void DisplayMainMenu()
		{
			Console.WriteLine("[N]ew Daily Sales Entry");
			Console.WriteLine("[S]ave Entries to File");
			Console.WriteLine("[E]dit Sales Entries");
			Console.WriteLine("[L]oad Sales File");
			Console.WriteLine("[V]iew Entered/Loaded Sales");
			Console.WriteLine("[M]onthly Statistics");
			Console.WriteLine("[D]isplay Main Menu");
			Console.WriteLine("[Q]uit Program");
     
		}


        // TODO: create the DisplayAnalysisMenu method
        // the menu must consist of the following options:
        //
        // [A]verage Sales
        // [H]ighest Sales
        // [L]owest Sales
        // [G]raph Sales
        // [R]eturn to MAIN MENU
		static void DisplayAnalysisMenu(){
			Console.WriteLine("[A]verage Sales");
			Console.WriteLine("[H]ighest Sales");
			Console.WriteLine("[L]owest Sales");
			Console.WriteLine("G]raph Sales");
			Console.WriteLine("[R]eturn to MAIN MENU");
		}
        // TODO: create the Largest method
		static double Largest(double[] sales, int countEntries){
			double calcLarge = 0.0;
			int indexOfLargest = 0;
			
            for(int i = 0; i < countEntries; i++){
            	//assign first value as largest to compare
				if(i == 0){
					calcLarge = sales[i];
				} 
				// if another value is 
				if(sales[i] > calcLarge){
					calcLarge = sales[i];
				}
            }
			for(int i = 0; i < countEntries; i++){
				if(sales[i] == calcLarge){
					indexOfLargest = i;
				}
			}
			return sales[indexOfLargest];
		}

        // TODO: create the Smallest method
        static double LowestSales(double[] sales, int countOfEntries){
            int indexOfMin = 0;
            double min = 0;

            for(int i = 0; i < countOfEntries; i++){
                //beginning of array assign first value to min to compare other values
                if(i == 0){
                    min = sales[i];
                }
                //if while looping found a value lower than currentMin assign it to min
                if(sales[i] < min){
                    min = sales[i];
                }
            }

            for(int i = 0; i < countOfEntries; i++){
                if(sales[i] == min){
                    indexOfMin = i;
                    //found indexOfMin break out of loop
                    break;
                }
            }
            return sales[indexOfMin];
        }


        // TODO: create the Mean method
        double MeanAverageSales(double[] sales, int countOfEntries){
            double totalSum = 0.0;
            double mean = 0.0;

            for(int i = 0; i < countOfEntries; i++){
                totalSum += sales[i];
            }
            mean = totalSum / countOfEntries;

            return mean;
        }


        // ++++++++++++++++++++++++++++++++++++ Difficulty 2 ++++++++++++++++++++++++++++++++++++


        // TODO: create the DisplayEntries method
			void DisplayEntries(double[] sales, string[] dates, int countOfEntries){
                Console.WriteLine(String.Format(" {0,0}  {1,24} ", "Dates", "Sales Value"));
                Console.Write("------------");
				Console.Write("        " + "------------");
				Console.WriteLine();
				for(int i = 0; i < countOfEntries; i++){
                    Console.WriteLine(String.Format(" {0,5}  {1,10} ",dates[i] ,sales[i]));
                }
        }


        // TODO: create the EnterSales method
        int EnterSales(double[] sales, string[] dates){
                //insure user input has no numerical characters or special
                string dailySalesTest = @"^[0-9]+";
                string numericalTest = @"^[^\d]+$";
                string yearTest = @"^(19|20)\d{2}$";

                string dailySales = "";
                string[] dailySalesArray = new string[31];

                string monthlySaleString = "";
                string month = "";
                string userYear = "0";

                bool monthMatch = false;
                int count = 0;

                // get month first
                Console.Write("Enter Month as MMM: ");
                do{
                    try{
                       month = Console.ReadLine();
                       //numerical test if fails throw format exception
                       if(Regex.IsMatch(month, numericalTest)){
                            monthMatch = true;
                            //spacing
                            Console.WriteLine();
                       } else {
                            throw new FormatException("Error: Please only enter alphabetic characters for the month.");
                            //spacing
                       }
                    }
                    catch (FormatException ex){
                        Console.WriteLine(ex.Message);
                    }
                } while(!monthMatch);
                //END GET MONTH



                Console.Write("Please enter year. (eg. YYYY): ");

                do{
                    try{
                    userYear = Console.ReadLine();
                        if(!Regex.IsMatch(userYear, yearTest)){
                            throw new FormatException("Please enter a year between 1900 and 2100");
                        } else {
                            Console.WriteLine($"{userYear} is confirmed.");
                            break;
                        }
                    } catch(FormatException ex){
                        Console.WriteLine(ex.Message);
                    }
                } while(userYear != "0");

                
                //add values into array here 
                for(int i = 0; i < 30; i++){
                    Console.WriteLine($"Please enter daily sales for day {i + 1}.");
                    try{
                        dailySales = Console.ReadLine();
                        if(double.Parse(dailySales) == -1){
                            break;
                        }
                        if(Regex.IsMatch(dailySales, dailySalesTest)){
                            dailySalesArray[i] = dailySales;
                            count++;
                        }
                        if(!Regex.IsMatch(dailySales, dailySalesTest)){
                            throw new FormatException("Please only enter digits for daily sales.");
                        }
                    } catch(FormatException ex){
                        Console.WriteLine(ex.Message);
                    }
                 monthlySaleString = $"{month}-0{i + 1}-{userYear}";
                 dates[i] = monthlySaleString;
                 sales[i] = double.Parse(dailySales);
                }
            return count;    
        }

        // int LoadSalesFile(string filename, double[] sales, string[] dates) -->
        // loads the records from a file (filename) into the associative arrays used by the program; 
        //returns the record count (i.e. how many days of data were loaded) [difficulty 2]
        // TODO: create the LoadSalesFile method
        static int LoadSalesFile(string fileName, double[] sales, string[] dates){
            int count = 0;
           
            try{
                if(File.Exists(fileName)){
                    // using (StreamReader sr = new StreamReader(fileName)){
                    // sr.ReadLine();
                    // string[] lines = File.ReadAllLines(fileName);
                    using (StreamReader reader = new StreamReader(fileName)){
                     // Read and discard the header line
                    reader.ReadLine();

                    // Read the rest of the file
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Process each line as needed
                        string[] values = line.Split(',');
                        sales[count] = double.Parse(values[1]);
                        dates[count] =  values[0];
                        count++;
                    }
                    }

                } else {
                    throw new FileNotFoundException("File not found in this directory:");
                }
            } catch(FileNotFoundException ex){
                Console.WriteLine(ex.Message + $" {fileName}" );
            }
            return count;
        }


        // TODO: create the SaveSalesFile method
        static void SaveSalesFile(double[] sales, string[] dates, string fileName, int countOfEntries){
            try{
                if(File.Exists(fileName)){
                    using (StreamWriter sw = new StreamWriter(fileName)){
                    //print headers first 
                    sw.WriteLine("Dates, Sales");
                
                    for(int i = 0; i < countOfEntries; i++){
                        sw.WriteLine($"{dates[i]}" + "," + $"{sales[i]}");
                    }
                }
                } else {
                    File.Create(fileName).Close();
                    using (StreamWriter sw = new StreamWriter(fileName)){
                     sw.WriteLine("Dates, Sales");
                    for(int i = 0; i < countOfEntries; i++){
                        sw.WriteLine($"{dates[i]}" + "," + $"{sales[i]}");
                    }
                }
            }
            } catch(IOException ex){
                Console.Write(ex.Message);
            }
    }  

        // ++++++++++++++++++++++++++++++++++++ Difficulty 3 ++++++++++++++++++++++++++++++++++++

        // TODO: create the EditEntries method


        // ++++++++++++++++++++++++++++++++++++ Difficulty 4 ++++++++++++++++++++++++++++++++++++

        // TODO: create the DisplaySalesChart method
    void DisplaySalesChart(double[] sales, string[] dates, int countOfEntries)
        {
            Console.WriteLine("--Daily Sales--");
            double salesMax = sales.Max();
            int runningSalesMax = (((int)salesMax + 4) / 5) * 5; // Round up to the nearest multiple of 5

            for (int i = runningSalesMax; i >= 0; i -= 5)
            {
                string rowString = "";	
                if(i == salesMax){
                    rowString = $"{i}   |";
                } else if (i < salesMax){
                    rowString = i < 10 ? $"0{i}     |" : $"{i}     |";
                }
                // Handle leading zeros for alignment
                //rowString += " |"; // Add the separator after the scale label

                for (int j = 0; j < countOfEntries; j++)
                {
                    if (sales[j] != i)
                    {    
                        rowString += "            ".PadLeft(6);
                    
                    }
                    else
                    {
                        rowString += sales[j].ToString().PadLeft(3); 
                    }
                }

                Console.WriteLine(rowString); // Print the current row of the chart
                if (i == 0) break; // Exit the loop when the scale reaches 0
            }

            // Print the separator line
            Console.WriteLine(new string('-', 4 * sales.Length));
            // Print the header row for days
            Console.Write("Days |");
            for(int i = 0; i < countOfEntries; i++){
                //   string date = "MAR-01-2024";
                //   string day = date.Substring(4, 2);  
                  Console.Write($"{i + 1}".PadLeft(6) + "|"); // Print each day with padding
            } 
            
            
            Console.WriteLine(); // New line at the end of the chart
}

// Example usage:



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // ++++++++++++++++++++++++++++++++++++ Additional Provided Methods ++++++++++++++++++++++++++++++++++++
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        // NOTE: Many of the following methods depend on the Prompt method and will operate correctly once
        // that method has been implemented.

        ///// <summary>
        /// Displays the Program intro.
        ///// </summary>
        static void DisplayProgramIntro()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("=                                      =");
            Console.WriteLine("=            Monthly  Sales            =");
            Console.WriteLine("=                                      =");
            Console.WriteLine("========================================");
            Console.WriteLine();
        }

        ///// <summary>
        /// Displays the Program outro.
        ///// </summary>
        static void DisplayProgramOutro()
        {
            Console.Write("Program terminated. Press ENTER to exit program...");
            Console.ReadLine();
        }

        ///// <summary>
        /// Displays a disclaimer for NEW entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool NewEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.");
            Console.WriteLine("Hint: Select EDIT from the main menu instead, to change individual days.");
            Console.WriteLine("Hint: You'll need to enter data for the whole month.");
            Console.WriteLine();
            response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        ///// <summary>
        /// Displays a disclaimer for SAVE entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool SaveEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: saving to an EXISTING file will overwrite data currently on that file.");
            Console.WriteLine("Hint: Files will be saved to this program's directory by default.");
            Console.WriteLine("Hint: If the file does not yet exist, it will be created.");
            Console.WriteLine();
            response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        ///// <summary>
        /// Displays a disclaimer for EDIT entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool EditEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: editing will overwrite unsaved sales values.");
            Console.WriteLine("Hint: Save to a file before editing.");
            Console.WriteLine();
            response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        ///// <summary>
        /// Displays a disclaimer for LOAD entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool LoadEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.");
            Console.WriteLine("Hint: If you entered New Daily sales entries, save them first!");
            Console.WriteLine();
            response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        ///// <summary>
        /// Displays prompt for a filename, and returns a valid filename. 
        /// Includes exception handling.
        /// </summary>
        /// <returns>User-entered string, representing valid filename (.txt or .csv)</returns>
        static string PromptForFilename()
        {
            string filename = "";
            bool isValidFilename = true;
            const string CsvFileExtension = ".csv";
            const string TxtFileExtension = ".txt";

            do
            {
                filename = Prompt("Enter name of .csv or .txt file to save to (e.g. JAN-2024-sales.csv): ");
                if (filename == "")
                {
                    isValidFilename = false;
                    Console.WriteLine("Please try again. The filename cannot be blank or just spaces.");
                }
                else
                {
                    if (!filename.EndsWith(CsvFileExtension) && !filename.EndsWith(TxtFileExtension)) //if filename does not end with .txt or .csv.
                    {
                        filename = filename + CsvFileExtension; //append .csv to filename
                        Console.WriteLine("It looks like your filename does not end in .csv or .txt, so it will be treated as a .csv file.");
                        isValidFilename = true;
                    }
                    else
                    {
                        Console.WriteLine("It looks like your filename ends in .csv or .txt, which is good!");
                        isValidFilename = true;
                    }
                }
            } while (!isValidFilename);
            return filename;
        }
    }
}