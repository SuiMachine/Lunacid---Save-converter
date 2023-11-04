using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Lunacid_SaveConverter
{
	public static class Converter
	{
		public static void ConvertToBinaryFormat<T>(string sourceFile, string targetFile, string backupfile)
		{
			var readText = new StreamReader(sourceFile);
			var xmlSerializer = new XmlSerializer(typeof(T));
			T playerData = (T)xmlSerializer.Deserialize(readText);
			readText.Close();

			var binaryFormatter = new BinaryFormatter();
			if (File.Exists(targetFile))
			{
				if (!File.Exists(backupfile))
					File.Copy(targetFile, backupfile, true);
				File.Delete(targetFile);
			}

			FileStream fileStream = new FileStream(targetFile, FileMode.Create);
			binaryFormatter.Serialize(fileStream, playerData);
			fileStream.Close();
		}

		public static void ConvertToXmlFormat<T>(string sourceFile, string targetFile)
		{
			var binaryFormatter = new BinaryFormatter();
			FileStream fileStream = new FileStream(sourceFile, FileMode.Open);
			T playerData = (T)binaryFormatter.Deserialize(fileStream);
			fileStream.Close();

			var xmlSerializer = new XmlSerializer(typeof(T));

			if (File.Exists(targetFile))
				File.Delete(targetFile);

			var outputStream = File.Create(targetFile);
			xmlSerializer.Serialize(outputStream, playerData);
			outputStream.Close();
		}
	}
}
