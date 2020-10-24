using System.Collections.Generic;

namespace Huffman
{
	public class CharacterCounter : ICharacterCounter
	{
		public Dictionary<string, int> CountCharacterOccurrences(IEnumerable<string> characters)
		{
			Dictionary<string, int> characterFrequencies = new Dictionary<string, int>();

			foreach (string character in characters)
			{
				if (!characterFrequencies.ContainsKey(character))
				{
					characterFrequencies[character] = 0;
				}

				characterFrequencies[character]++;
			}

			return characterFrequencies;
		}
	}
}