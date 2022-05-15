/*
 *		Solution to Övning 2 - Flow Control
 *		by Andreas Åkerberg
 */

namespace Ovning2_FlowControl.Helpers
{

	// Global helpers for commonly used functions

	internal static class Util
	{

		// Read a string from console and do validity check

		public static string AskForString(string prompt)
		{
			while (true)
			{
				string input;
				Console.Write($"{prompt} ");
				if ((input = Console.ReadLine()!.Trim()) != string.Empty)
					return input;
				Console.WriteLine("\nDu angav tom input, försök igen:\n");
			}
		}

		// Read a string from console and try and parse to int with validity check

		public static uint AskForUInt(string prompt)
		{
			while (true)
			{
				bool success;
				uint inputInt;
				string input = AskForString(prompt);
				success = uint.TryParse(input, out inputInt);
				if (success)
					return inputInt;
				Console.WriteLine("\nAngiven input är inte ett positivt heltal, försök igen:\n");
			}
		}
	}
}
