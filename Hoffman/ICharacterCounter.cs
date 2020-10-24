using System.Collections.Generic;

namespace Huffman
{
	public interface ICharacterCounter
	{
		Dictionary<string, int> CountCharacterOccurrences(IEnumerable<string> characters);
	}
}