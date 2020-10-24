using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Huffman.Nunit.Integration
{
	[TestFixture]
	public class HuffmanCodeTest
	{
		private readonly HuffmanEncoder huffmanEncoder = new HuffmanEncoder(
			new CharacterSplitter(),
			new CharacterCounter(),
			new HuffmanTreeBuilder(),
			new HuffmanCodeGenerator());
		private readonly HuffmanDecoder huffmanDecoder = new HuffmanDecoder();

		private static IEnumerable<object[]> Source =>
			new[]
			{
				new object[]{ "Resources/seneca.txt", 1 },
				new object[]{ "Resources/seneca.txt", 3 },
				new object[]{ "Resources/seneca.txt", 31 },
				new object[]{ "Resources/vuldat.txt", 5 },
				new object[]{ "Resources/vuldat.txt", 51 },
				new object[]{ "Resources/vuldat.txt", 97 },
				new object[]{ "Resources/test.txt", 1 },
				new object[]{ "Resources/test.txt", 2 },
				new object[]{ "Resources/test.txt", 3 },
			};

		[TestCaseSource(nameof(Source))]
		public void ShouldEncodeAndDecodeText(string path, int characterSize)
		{
			// Arrange
			string text = File.ReadAllText(path);

			// Act
			string encodedText = this.huffmanEncoder.Encode(text, characterSize, out var characterCodes);
			string decodedText = this.huffmanDecoder.Decode(encodedText, characterCodes);

			// Assert
			Assert.That(encodedText, Is.Not.Null);
			Assert.That(decodedText, Is.EqualTo(text));
		}
	}
}