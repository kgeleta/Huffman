using System;
using System.Text;

namespace Huffman
{
	public class Node : IComparable<Node>
	{
		public string Character;
		public int Frequency { get; }
		public Node Left { get; }
		public Node Right { get; }
		
		private Node(string character, int frequency, Node left, Node right)
		{
			frequency = frequency > 0
				? frequency
				: throw new InvalidOperationException($"Frequency must be a positive number");
			
			Character = character;
			Frequency = frequency;
			Left = left;
			Right = right;
		}

		public static Node CreateLeaf(string character, int frequency)
		{
			return new Node(character, frequency, null, null);
		}

		public static Node CreateInternalNode(Node left, Node right)
		{
			return new Node(null, left.Frequency + right.Frequency, left, right);
		}

		public override string ToString()
		{
			return this.Print(new StringBuilder(), true, new StringBuilder()).ToString();
		}

		public int CompareTo(Node other)
		{
			if (this.Frequency < other.Frequency)
			{
				return 1;
			}
			else if (this.Frequency > other.Frequency)
			{
				return -1;
			}
			else
			{
				return 0;
			}
		}

		private StringBuilder Print(StringBuilder prefix, bool isTail, StringBuilder sb)
		{
			Right?.Print(new StringBuilder().Append(prefix).Append(isTail ? "│   " : "    "), false, sb);
			sb.Append(prefix).Append(isTail ? "└── " : "┌── ").Append(this.PrintValue()).Append("\n");
			Left?.Print(new StringBuilder().Append(prefix).Append(isTail ? "    " : "│   "), true, sb);

			return sb;
		}

		private string PrintValue()
		{
			return this.Character != null ? $"'{this.Character}':{this.Frequency}" : $"{this.Frequency}";
		}
	}
}