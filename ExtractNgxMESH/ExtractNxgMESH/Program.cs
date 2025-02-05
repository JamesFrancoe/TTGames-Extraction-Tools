using System;
using ExtractHelper;

namespace ExtractNxgMESH
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				ExtractNxgMESH extractNxgMESH = new ExtractNxgMESH();
				extractNxgMESH.ParseArgs(args);
				extractNxgMESH.Extract();
			}
			catch (NotSupportedException ex)
			{
				ColoredConsole.WriteLineError("Not yet surported: " + ex.Message);
			}
			catch (NotImplementedException ex2)
			{
				ColoredConsole.WriteLineError("Not yet implemented: " + ex2.Message);
			}
			catch (Exception ex3)
			{
				ColoredConsole.WriteLineError(ex3.Message);
				ColoredConsole.WriteLineError(ex3.StackTrace);
			}

			Console.WriteLine("Press enter to close...");
			Console.ReadLine();
		}
	}
}
