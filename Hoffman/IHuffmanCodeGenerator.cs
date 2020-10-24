using System.Collections.Generic;

namespace Huffman
{
	public interface IHuffmanCodeGenerator
	{
		Dictionary<string, string> GenerateCharacterCodeMapping(Node root);
	}
}