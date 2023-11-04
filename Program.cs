using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Lunacid_SaveConverter
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				if (args.Length > 0)
				{
					//var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };


					if (File.Exists(args[0]))
					{
						var filePath = args[0];
						if (filePath.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase))
						{

							var outputPath = filePath.Remove(filePath.IndexOf(".xml", StringComparison.InvariantCultureIgnoreCase)) + ".MIDNIGHT";
							var backupPath = outputPath + "_backup";

							if (filePath.EndsWith("SETTINGS.xml", StringComparison.InvariantCultureIgnoreCase))
								Converter.ConvertToBinaryFormat<SystemData>(filePath, outputPath, backupPath);
							else
								Converter.ConvertToBinaryFormat<PlayerData>(filePath, outputPath, backupPath);

							Console.WriteLine("Converted to MIDNIGHT binary format!");
						}
						else if (filePath.EndsWith(".MIDNIGHT", StringComparison.InvariantCultureIgnoreCase))
						{
							var outputPath = filePath.Remove(filePath.IndexOf(".MIDNIGHT", StringComparison.InvariantCultureIgnoreCase)) + ".xml";
							if (filePath.EndsWith("SETTINGS.MIDNIGHT", StringComparison.InvariantCultureIgnoreCase))
								Converter.ConvertToXmlFormat<SystemData>(filePath, outputPath);
							else
								Converter.ConvertToXmlFormat<PlayerData>(filePath, outputPath);

							Console.WriteLine("Converted to xml!");
						}
						else
						{
							Console.WriteLine("Unknown format!");
						}
					}
				}
				else
				{
					Console.WriteLine("Drag and drop a file onto exe!");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.ToString());
			}

			Console.ReadLine();

		}
	}
}
