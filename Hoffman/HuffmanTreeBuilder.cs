using System;
using System.Collections.Generic;

namespace Huffman
{
	public class HuffmanTreeBuilder : IHuffmanTreeBuilder
	{
		public Node Build(Dictionary<string, int> characterFrequencies)
		{
			characterFrequencies =
				characterFrequencies ?? throw new ArgumentNullException(nameof(characterFrequencies));
			if (characterFrequencies.Count < 2)
			{
				throw new ArgumentException("At least 2 characters are necessary to build huffman tree");
			}

			PriorityQueue<Node> nodes = CreateLeafs(characterFrequencies);

			while (nodes.Count != 1)
			{
				Node internalNode = Node.CreateInternalNode(nodes.Dequeue(), nodes.Dequeue());
				nodes.Enqueue(internalNode);
			}

			return nodes.Dequeue();
		}

		private static PriorityQueue<Node> CreateLeafs(Dictionary<string, int> characterFrequencies)
		{
			PriorityQueue<Node> queue = new PriorityQueue<Node>(characterFrequencies.Count, true);
			foreach (KeyValuePair<string, int> pair in characterFrequencies)
			{
				queue.Enqueue(Node.CreateLeaf(pair.Key, pair.Value));
			}

			return queue;
		}
	}
}