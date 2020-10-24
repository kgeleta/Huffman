using NUnit.Framework;
using System.Collections.Generic;

namespace Huffman.Nunit
{
	[TestFixture]
	public class HuffmanDecoderTest
	{
		private readonly HuffmanDecoder huffmanDecoder = new HuffmanDecoder();

		[Test]
		public void ShouldDecodeText()
		{
			// Arrange
			const string Text = "000101001010111100";
			const string ExpectedDecodedText = "ABBACBBCCCA";

			Dictionary<string, string> characterCodes = new Dictionary<string, string>
			{
				{ "A", "00" },
				{ "B", "01" },
				{ "C", "1" },
			};

			// Act
			string actualDecodedText = this.huffmanDecoder.Decode(Text, characterCodes);

			// Assert
			Assert.That(actualDecodedText, Is.EqualTo(ExpectedDecodedText));
		}
	}
}