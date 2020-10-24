using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Huffman.Nunit
{
	[TestFixture]
	public class HuffmanTreeBuilderTest
	{
		private readonly IHuffmanTreeBuilder builder = new HuffmanTreeBuilder();

		private static IEnumerable<Dictionary<string, int>> InvalidCharacterFrequenciesSource =>
			new[]
			{
				new Dictionary<string, int>(),
				new Dictionary<string, int> {{"a", 12}},
			};

		[Test]
		public void ShouldThrowWhenCharacterFrequencyIsNull()
		{
			Assert.Throws<ArgumentNullException>(() => this.builder.Build(null));
		}

		[TestCaseSource(nameof(InvalidCharacterFrequenciesSource))]
		public void ShouldThrowWhenInvalidCharacterFrequencies(Dictionary<string, int> characterFrequencies)
		{
			Assert.Throws<ArgumentException>(() => this.builder.Build(characterFrequencies));
		}

		[Test]
		public void ShouldBuildOptimalTree()
		{
			// Arrange
			Dictionary<string, int> characterFrequencies = new Dictionary<string, int>
			{
				{"f", 5},
				{"e", 9},
				{"c", 12},
				{"b", 13},
				{"d", 16},
				{"a", 45},
			};

			// Act
			Node root = this.builder.Build(characterFrequencies);

			// Assert
			Assert.That(root.Character, Is.Null);
			Assert.That(root.Frequency, Is.EqualTo(100));

			Assert.That(root.Left.Character, Is.EqualTo("a"));
			Assert.That(root.Left.Frequency, Is.EqualTo(45));

			Assert.That(root.Right.Character, Is.Null);
			Assert.That(root.Right.Frequency, Is.EqualTo(55));

			Assert.That(root.Right.Left.Character, Is.Null);
			Assert.That(root.Right.Left.Frequency, Is.EqualTo(25));

			Assert.That(root.Right.Right.Character, Is.Null);
			Assert.That(root.Right.Right.Frequency, Is.EqualTo(30));

			Assert.That(root.Right.Left.Left.Character, Is.EqualTo("c"));
			Assert.That(root.Right.Left.Left.Frequency, Is.EqualTo(12));

			Assert.That(root.Right.Left.Right.Character, Is.EqualTo("b"));
			Assert.That(root.Right.Left.Right.Frequency, Is.EqualTo(13));

			Assert.That(root.Right.Right.Left.Character, Is.Null);
			Assert.That(root.Right.Right.Left.Frequency, Is.EqualTo(14));

			Assert.That(root.Right.Right.Right.Character, Is.EqualTo("d"));
			Assert.That(root.Right.Right.Right.Frequency, Is.EqualTo(16));

			Assert.That(root.Right.Right.Left.Left.Character, Is.EqualTo("f"));
			Assert.That(root.Right.Right.Left.Left.Frequency, Is.EqualTo(5));

			Assert.That(root.Right.Right.Left.Right.Character, Is.EqualTo("e"));
			Assert.That(root.Right.Right.Left.Right.Frequency, Is.EqualTo(9));
		}
	}
}