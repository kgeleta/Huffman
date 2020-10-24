using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman
{
	public class HuffmanEncoder
	{
		private readonly ICharacterSplitter characterSplitter;
		private readonly ICharacterCounter characterCounter;
		private readonly IHuffmanTreeBuilder treeBuilder;
		private readonly IHuffmanCodeGenerator codeGenerator;

		public HuffmanEncoder(ICharacterSplitter characterSplitter, ICharacterCounter characterCounter, IHuffmanTreeBuilder treeBuilder,
			IHuffmanCodeGenerator codeGenerator)
		{
			this.characterSplitter = characterSplitter;
			this.characterCounter = characterCounter;
			this.treeBuilder = treeBuilder;
			this.codeGenerator = codeGenerator;
		}

		public string Encode(string text, int characterSize, out Dictionary<string, string> characterCodes)
		{
			List<string> characters = this.characterSplitter.SplitIntoCharactersBySize(text, characterSize);
			Dictionary<string, int> characterFrequencies = this.characterCounter.CountCharacterOccurrences(characters);
			Node huffmanTree = this.treeBuilder.Build(characterFrequencies);
			characterCodes = this.codeGenerator.GenerateCharacterCodeMapping(huffmanTree);

			StringBuilder encodedText = new StringBuilder();

			foreach (string character in characters)
			{
				if (!characterCodes.ContainsKey(character))
				{
					throw new InvalidOperationException($"Could not find code for character: '{character}'");
				}

				encodedText.Append(characterCodes[character]);
			}

			return encodedText.ToString();
		}
	}
}