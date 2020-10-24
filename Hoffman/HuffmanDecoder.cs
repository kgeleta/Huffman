using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huffman
{
	public class HuffmanDecoder
	{
		public string Decode(string text, Dictionary<string, string> characterCodes)
		{
			StringBuilder decodedText = new StringBuilder();
			StringBuilder currentCharacter = new StringBuilder();

			Dictionary<string, string> codeCharacters = this.ToCodeCharacters(characterCodes);

			foreach (char codeCharacter in text)
			{
				currentCharacter.Append(codeCharacter);
				if (codeCharacters.ContainsKey(currentCharacter.ToString()))
				{
					decodedText.Append(codeCharacters[currentCharacter.ToString()]);
					currentCharacter.Clear();
				}
			}

			return decodedText.ToString();
		}

		private Dictionary<string, string> ToCodeCharacters(Dictionary<string, string> characterCodes)
		{
			return characterCodes.ToDictionary(pair => pair.Value, pair => pair.Key);
		}
	}
}