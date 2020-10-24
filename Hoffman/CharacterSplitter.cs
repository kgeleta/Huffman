using System;
using System.Collections.Generic;

namespace Huffman
{
	public class CharacterSplitter : ICharacterSplitter
	{
		public List<string> SplitIntoCharactersBySize(string text, int maxCharacterSize)
		{
			List<string> result = new List<string>();

			for (int i = 0; i < text.Length; i += maxCharacterSize)
				result.Add(text.Substring(i, Math.Min(maxCharacterSize, text.Length - i)));

			return result;
		}
	}
}