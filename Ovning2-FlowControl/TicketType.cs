/*
 *		Solution to Övning 2 - Flow Control
 *		by Andreas Åkerberg
 */

namespace Ovning2_FlowControl
{

	// Data structure for a ticket type

	internal class TicketType
	{
		private string label = string.Empty;
		private uint price = 0;

		public TicketType(string label, uint price)
		{
			this.label = label;
			this.price = price;
		}

		public override string ToString()
		{
			return $"{label}: {price} kr";
		}
	}
}
