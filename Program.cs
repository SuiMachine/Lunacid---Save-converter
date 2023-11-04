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

							var readText = new StreamReader(filePath);
							var xmlSerializer = new XmlSerializer(typeof(PlayerData));
							PlayerData playerData = (PlayerData)xmlSerializer.Deserialize(readText);
							readText.Close();

							var binaryFormatter = new BinaryFormatter();
							if(File.Exists(outputPath))
							{
								if (!File.Exists(outputPath + "_backup"))
									File.Copy(outputPath, outputPath + "_backup", true);
								File.Delete(outputPath);
							}

							FileStream fileStream = new FileStream(outputPath, FileMode.Create);
							binaryFormatter.Serialize(fileStream, playerData);
							fileStream.Close();

							Console.WriteLine("Converted to MIDNIGHT binary format!");
						}
						else if (filePath.EndsWith(".MIDNIGHT", StringComparison.InvariantCultureIgnoreCase))
						{
							var outputPath = filePath.Remove(filePath.IndexOf(".MIDNIGHT", StringComparison.InvariantCultureIgnoreCase)) + ".xml";
							var binaryFormatter = new BinaryFormatter();
							FileStream fileStream = new FileStream(filePath, FileMode.Open);
							PlayerData playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);
							fileStream.Close();

							var xmlSerializer = new XmlSerializer(typeof(PlayerData));

							if (File.Exists(outputPath))
								File.Delete(outputPath);

							var outputStream = File.Create(outputPath);
							xmlSerializer.Serialize(outputStream, playerData);
							outputStream.Close();

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
			catch(Exception ex)
			{
				Console.WriteLine("Error: " + ex.ToString());
			}

			Console.ReadLine();

		}
	}
}
