using System.Collections.Generic;
using NUnit.Framework;

namespace Huffman.Nunit
{
	[TestFixture]
	public class HuffmanCodeGeneratorTest
	{
		private readonly IHuffmanCodeGenerator huffmanCodeGenerator = new HuffmanCodeGenerator();

		[Test]
		public void ShouldGenerateCorrectCodes()
		{
			// Arrange
			Node leafA = Node.CreateLeaf("A", 12);
			Node leafB = Node.CreateLeaf("B", 13);
			Node leafC = Node.CreateLeaf("C", 65);
			Node leafD = Node.CreateLeaf("D", 76);
			Node leafE = Node.CreateLeaf("E", 43);

			Node internal1 = Node.CreateInternalNode(leafA, leafB);
			Node internal2 = Node.CreateInternalNode(internal1, leafE);
			Node internal3 = Node.CreateInternalNode(leafC, internal2);

			Node root = Node.CreateInternalNode(internal3, leafD);

			Dictionary<string, string> expected = new Dictionary<string, string>
			{
				{ "A", "0100" },
				{ "B", "0101" },
				{ "C", "00" },
				{ "D", "1" },
				{ "E", "011" },
			};

			// Act
			Dictionary<string, string> actual = this.huffmanCodeGenerator.GenerateCharacterCodeMapping(root);

			// Assert
			Assert.That(actual, Is.EquivalentTo(expected));
		}
	}
}