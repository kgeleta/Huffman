using NUnit.Framework;
using System.Collections.Generic;

namespace Huffman.Nunit
{
	[TestFixture]
	public class CharacterCounterTest
	{
		private readonly ICharacterCounter characterCounter = new CharacterCounter();

		private static IEnumerable<object[]> Source =>
			new[]
			{
				new object[]{ 
					new [] { "a1", "a8", "a4", "a4", "a4", "a4", "a1" }, 
					new Dictionary<string, int>
					{
						{"a1", 2 },
						{"a4", 4 },
						{"a8", 1 },
					}
				},
				new object[]{
					new [] { "bb1", "bb5", "bb7", "bb5", "bb1", "bb1", "bb1", "aa" },
					new Dictionary<string, int>
					{
						{"bb1", 4 },
						{"bb5", 2 },
						{"bb7", 1 },
						{"aa", 1 },
					}
				},
				new object[]{
					new string[0],
					new Dictionary<string, int>()
				},
			};

		[TestCaseSource(nameof(Source))]
		public void ShouldCountCharacters(string[] characters, Dictionary<string, int> expected)
		{
			// Act
			Dictionary<string, int> actual = this.characterCounter.CountCharacterOccurrences(characters);

			// Assert
			Assert.That(actual, Is.EquivalentTo(expected));
		}
	}
}