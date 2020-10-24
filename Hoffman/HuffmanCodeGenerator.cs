using System.Collections.Generic;

namespace Huffman
{
	public class HuffmanCodeGenerator : IHuffmanCodeGenerator
	{
		public Dictionary<string, string> GenerateCharacterCodeMapping(Node root)
		{
			Dictionary<string, string> charCodes = new Dictionary<string, string>();

			Queue<KeyValuePair<Node, string>> nodeCodes = new Queue<KeyValuePair<Node, string>>();
			nodeCodes.Enqueue(new KeyValuePair<Node, string>(root, string.Empty));

			while (nodeCodes.Count > 0)
			{
				var current = nodeCodes.Dequeue();
				if (current.Key.Character != null)
				{
					charCodes[current.Key.Character] = current.Value;
				}
				else
				{
					if (current.Key.Left != null)
					{
						nodeCodes.Enqueue(new KeyValuePair<Node, string>(current.Key.Left, current.Value + "0"));
					}

					if (current.Key.Right != null)
					{
						nodeCodes.Enqueue(new KeyValuePair<Node, string>(current.Key.Right, current.Value + "1"));
					}
				}
			}

			return charCodes;
		}
	}
}