using System.Collections.Generic;

namespace Huffman
{
	public interface ICharacterSplitter
	{
		List<string> SplitIntoCharactersBySize(string text, int maxCharacterSize);
	}
}