using System.Collections.Generic;

namespace Huffman
{
	public interface IHuffmanTreeBuilder
	{
		Node Build(Dictionary<string, int> characterFrequencies);
	}
}