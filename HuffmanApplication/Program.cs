using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Huffman;
using Newtonsoft.Json;

namespace HuffmanApplication
{
	class Program
	{
		private static readonly HuffmanEncoder HuffmanEncoder = new HuffmanEncoder(new CharacterSplitter(), new CharacterCounter(), new HuffmanTreeBuilder(), new HuffmanCodeGenerator());
		private static readonly HuffmanDecoder HuffmanDecoder = new HuffmanDecoder();

		static void Main(string[] args)
		{
			if (args.Length != 4)
			{
				DisplayUsageMessage();
				return;
			}

			try
			{
				Direction direction = ParseDirection(args[0]);
				int characterLength = ParseCharacterLength(args[1]);
				string inputPath = args[2];
				string outputPath = args[3];

				switch (direction)
				{
					case Direction.Encoding:
						Encode(inputPath, outputPath, characterLength);
						return;
					case Direction.Decoding:
						Decode(inputPath, outputPath);
						return;
				}
			}
			catch (Exception e)
			{
				DisplayMessageInColor(ConsoleColor.Red, e.Message);
				Environment.Exit(1);
			}
		}

		private static void DisplayUsageMessage()
		{
			DisplayMessageInColor(ConsoleColor.Red,
				"\nUsage: program.exe <direction> <character_length> <input_file_path> <output_file_path>",
				"Example for encoding: program.exe -e 1 C:\\input.txt C:\\output.txt",
				"Example for decoding: program.exe -d 1 C:\\input.txt C:\\output.txt");
		}

		private static void DisplayMessageInColor(ConsoleColor color, params string[] messages)
		{
			ConsoleColor initialColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			foreach (string message in messages)
			{
				Console.WriteLine(message);
			}
			Console.ForegroundColor = initialColor;
		}

		private static Direction ParseDirection(string directionString)
		{
			if (directionString == "-e" || directionString == "e")
			{
				return Direction.Encoding;
			}
			if (directionString == "-d" || directionString == "d")
			{
				return Direction.Decoding;
			}

			throw new ArgumentException($"Unrecognized direction: '{directionString}'. Use '-e' for encoding and '-d' for decoding.");
		}

		private static int ParseCharacterLength(string characterLengthString)
		{
			if (int.TryParse(characterLengthString, out var characterLength))
			{
				return characterLength;
			}
			
			throw new ArgumentException($"Incorrect character length specified: '{characterLengthString}'. Character length should be a positive integer value.");
		}

		private static void Encode(string inputPath, string outputPath, int characterLength)
		{
			string textToEncode = File.ReadAllText(inputPath);

			string encodedText = HuffmanEncoder.Encode(textToEncode, characterLength, out var mapping);
			string mappingJson = JsonConvert.SerializeObject(mapping);

			StringBuilder builder = new StringBuilder();
			builder.AppendLine(mappingJson);
			builder.Append(encodedText);

			File.WriteAllText(outputPath, builder.ToString());

			int inputLength = textToEncode.Length * 8;
			int compressedLength = encodedText.Length + (mappingJson.Length * 8);

			DisplayMessageInColor(ConsoleColor.Green, $"Compression rate: {CalculateCompressionRate(inputLength, compressedLength),0:P2}");
		}

		private static float CalculateCompressionRate(float inputLength, float compressedLength)
		{
			return (inputLength - compressedLength) / inputLength;
		}

		private static void Decode(string inputPath, string outputPath)
		{
			string text = File.ReadAllText(inputPath);

			int metadataEnd = text.LastIndexOf('}') + 1;
			string metadataJson = text.Substring(0, metadataEnd);
			string textToDecode = text.Substring(metadataEnd).Trim();

			Dictionary<string, string> characterCodes = JsonConvert.DeserializeObject<Dictionary<string, string>>(metadataJson);

			string decodedText = HuffmanDecoder.Decode(textToDecode, characterCodes);

			File.WriteAllText(outputPath, decodedText);
		}
	}
}
