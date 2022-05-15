/*
 *		Solution to Övning 2 - Flow Control
 *		by Andreas Åkerberg
 */

using Ovning2_FlowControl.Helpers;

namespace Ovning2_FlowControl
{
	internal class Application
	{
		private bool quit = false;	// Value of true exists the whole application

		// Ticket types stored with corresponding age limits

		private Dictionary<Range, TicketType> ticketTypes;
		private Range standardPriceRange = new(20, 64);
		private Range youthPriceRange = new(0, 19);
		private Range pensionerPriceRange = new(65, new Index(int.MaxValue));

		public Application()
		{
			// Init ticket types
			ticketTypes = new()
			{
				{ standardPriceRange, new TicketType("Standardpris", 120) },
				{ youthPriceRange, new TicketType("Ungdomspris", 80) },
				{ pensionerPriceRange, new TicketType("Pensionärspris", 90) }
			};
		}


		public void StartApplication()
		{
			Console.WriteLine("~ Övning 2 - Flow Control ~");

			while (!quit)   // Primary top level loop
			{
				ShowMainMenu();
				string menuChoice = Util.AskForString(">");
				InterpretMenuChoice(menuChoice);
			}

			Console.WriteLine("\nAvslutar program");
		}


		private void ShowMainMenu()
		{
			Console.WriteLine(@$" 
			Huvudmeny
			=========
			{MenuHelper.Close}. Stäng program
			{MenuHelper.YouthOrPensioner}. Ungdom eller persionär
			{MenuHelper.RepeatTen}. Upprepa tio gånger
			{MenuHelper.ThirdWord}. Det tredje ordet
			".Replace("\t", ""));
		}

		private void ShowSubMenu1()
		{
			Console.WriteLine(@$" 
			Menyval 1
			=========
			{MenuHelper.MainMenu}. Huvudmeny
			{MenuHelper.TicketPriceForAge}. Biljettpris för ålder
			{MenuHelper.CalcForGroup}. Beräkna för sällskap
			".Replace("\t", ""));
		}

		private void InterpretMenuChoice(string menuChoice)
		{
			switch (menuChoice)
			{
				case MenuHelper.Close:
					quit = true;		// Exit whole application
					break;
				case MenuHelper.YouthOrPensioner:
					DoSubMenu1Loop();	// Enter a nested menu loop for SubMenu1
										// inside the existing main menu loop
					break;
				case MenuHelper.RepeatTen:
					DoRepeatTen();
					break;
				case MenuHelper.ThirdWord:
					DoThirdWord();
					break;
				default:
					Console.WriteLine("\nFelaktig input");
					break;
			}
		}

		private void DoSubMenu1Loop()
		{
			bool backToMainMenu = false;

			while (!backToMainMenu) // Enter nested menu loop inside the existing
			{                       // main menu loop
				ShowSubMenu1();
				string subMenu1Choice = Util.AskForString(">");
				InterpretSubMenu1Choice(subMenu1Choice, ref backToMainMenu);
			}
		}

		private void DoRepeatTen()	// Menu option 2
		{
			Console.WriteLine("\nHär anges ett ord för att sedan upprepa det tio gånger på en rad. Ange ett ord:\n");
			var input = Util.AskForString(">");
			Console.WriteLine();
			for (var i = 0; i < 10; i++)
			{
				Console.Write($"{i + 1}. {input} ");
			}
			Console.WriteLine();
		}

		private void DoThirdWord()	// Menu option 3
		{
			string[] words = new string[0];

			while (words.Length < 3)
			{
				Console.WriteLine("\nAnge en mening med minst tre ord:\n");
				var inputString = Util.AskForString(">");
				words = inputString.Split(' ');
				if (words.Length < 3)
					Console.WriteLine("\nDen angivna meningen innehåller färre än tre ord");
			}

			Console.WriteLine($"\nDet tredje ordet i meningen är: {words[2]}");
		}

		private void InterpretSubMenu1Choice(string menuChoice, ref bool backToMainMenu)
		{
			switch (menuChoice)
			{
				case MenuHelper.MainMenu:
					backToMainMenu = true;	// Exit submenu1-loop and move to main menu loop
					break;
				case MenuHelper.TicketPriceForAge:
					DoTicketPriceForAge();
					break;
				case MenuHelper.CalcForGroup:
					DoCalcCostForGroup();
					break;
				default:
					Console.WriteLine("\nFelaktig input");
					break;
			}
		}

		private void DoTicketPriceForAge()	// Prints the ticket price for the specified age
		{
			uint age = Util.AskForUInt("\nÅlder:\n\n>");
			Console.WriteLine();
			PrintTicketPriceInfo(age);
		}

		private void DoCalcCostForGroup()	// Prints number of persons in group and total
		{									// tickets price
			Console.WriteLine();
			uint numPersons = Util.AskForUInt("Antal personer i sällskap:\n\n>");
			uint totalCost = 0;
			for (var i = 0; i < numPersons; i++)
			{
				uint agePerson = Util.AskForUInt($"\nÅlder för person {i+1}:\n\n>");

				if (agePerson < 20)
				{
					totalCost += 80;
				}
				else if (agePerson > 64)
				{
					totalCost += 90;
				}
				else
				{
					totalCost += 120;
				}
			}

			Console.WriteLine($"\nAntal personer i sällskap: {numPersons}");
			Console.WriteLine($"\nTotalkostnad för hela sällskapet: {totalCost} SEK");
		}

		private void PrintTicketPriceInfo(uint age)	// Finds the corresponding ticket type
		{											// and prints the ticket type label
			foreach (var ticketType in ticketTypes)	// and ticket price
			{
				var ticketAgeRange = ticketType.Key;

				if (age >= ticketAgeRange.Start.Value && age <= ticketAgeRange.End.Value)
					Console.WriteLine(ticketType.Value);
			}
		}
	}
}
