using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Huffman.Nunit
{
	[TestFixture]
	public class CharacterSplitterTest
	{
		private readonly ICharacterSplitter characterSplitter = new CharacterSplitter();

		private static IEnumerable<object[]> Source =>
			new[]
			{
				new object[]{ "Resources/seneca.txt", 1 },
				new object[]{ "Resources/seneca.txt", 3 },
				new object[]{ "Resources/seneca.txt", 31 },
				new object[]{ "Resources/vuldat.txt", 5 },
				new object[]{ "Resources/test.txt", 1 },
				new object[]{ "Resources/test.txt", 2 },
				new object[]{ "Resources/test.txt", 3 },
			};

		[TestCaseSource(nameof(Source))]
		public void ShouldHaveCorrectLength(string path, int characterSize)
		{
			// Arrange
			string text = File.ReadAllText(path);
			int expectedLength = text.Length;
		
			// Act
			List<string> characters = this.characterSplitter.SplitIntoCharactersBySize(text, characterSize);
			int actualLength = characters.Sum(character => character.Length);
		
			// Assert
			Assert.That(actualLength, Is.EqualTo(expectedLength));
		}
		
		[TestCaseSource(nameof(Source))]
		public void ShouldContainAllCharacters(string path, int characterSize)
		{
			// Arrange
			string expectedText = File.ReadAllText(path);

			// Act
			List<string> characters = this.characterSplitter.SplitIntoCharactersBySize(expectedText, characterSize);
			string actualText = string.Join(string.Empty, characters);

			// Assert
			Assert.That(actualText, Is.EqualTo(expectedText));
		}
	}
}