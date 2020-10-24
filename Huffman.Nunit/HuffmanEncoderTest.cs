using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace Huffman.Nunit
{
	[TestFixture]
	public class HuffmanEncoderTest
	{
		private readonly Mock<ICharacterSplitter> characterSplitterMock = new Mock<ICharacterSplitter>();
		private readonly Mock<ICharacterCounter> characterCounterMock = new Mock<ICharacterCounter>();
		private readonly Mock<IHuffmanTreeBuilder> treeBuilderMock = new Mock<IHuffmanTreeBuilder>();
		private readonly Mock<IHuffmanCodeGenerator> codeGeneratorMock = new Mock<IHuffmanCodeGenerator>();

		private readonly List<string> expectedCharacters = new List<string> { "e", "l", "e", "v", "e", "n", " ", "e", "l", "v", "e", "s" };
		private readonly Dictionary<string, int> expectedCharacterFrequencies = new Dictionary<string, int>
		{
			{"e", 5},
			{"l", 2},
			{"v", 2},
			{"n", 2},
			{"s", 1},
			{" ", 4},
		};
		private readonly Node expectedTree = Node.CreateLeaf("A", 20);
		private readonly Dictionary<string, string> expectedCodeMapping = new Dictionary<string, string>
		{
			{"e", "000"},
			{"l", "111"},
			{"v", "222"},
			{"n", "333"},
			{"s", "444"},
			{" ", "555"},
		};

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.characterSplitterMock.Setup(x => x.SplitIntoCharactersBySize(It.IsAny<string>(), It.IsAny<int>()))
				.Returns(this.expectedCharacters);
			this.characterCounterMock.Setup(x => x.CountCharacterOccurrences(It.IsAny<IEnumerable<string>>()))
				.Returns(this.expectedCharacterFrequencies);
			this.treeBuilderMock.Setup(x => x.Build(It.IsAny<Dictionary<string, int>>()))
				.Returns(this.expectedTree);
			codeGeneratorMock.Setup(x => x.GenerateCharacterCodeMapping(It.IsAny<Node>()))
				.Returns(this.expectedCodeMapping);
		}

		[Test]
		public void ShouldEncodeText()
		{
			// Arrange
			const string Text = "eleven elves";
			const int CharacterSize = 1;
			const string ExpectedEncodedText = "000111000222000333555000111222000444";

			HuffmanEncoder encoder = new HuffmanEncoder(this.characterSplitterMock.Object, this.characterCounterMock.Object, this.treeBuilderMock.Object, this.codeGeneratorMock.Object);

			// Act
			string actualEncodedText = encoder.Encode(Text, CharacterSize, out var actualCodeMapping);

			// Assert
			this.characterSplitterMock.Verify(x => x.SplitIntoCharactersBySize(Text, CharacterSize), Times.Once);
			this.characterCounterMock.Verify(x => x.CountCharacterOccurrences(this.expectedCharacters), Times.Once);
			this.treeBuilderMock.Verify(x => x.Build(this.expectedCharacterFrequencies), Times.Once);
			this.codeGeneratorMock.Verify(x => x.GenerateCharacterCodeMapping(this.expectedTree), Times.Once);

			Assert.That(actualCodeMapping, Is.EquivalentTo(this.expectedCodeMapping));
			Assert.That(actualEncodedText, Is.EqualTo(ExpectedEncodedText));
		}
	}
}