using System;
using System.Collections.Generic;
using System.Linq;

class HuffmanNode
{
    public char Character { get; set; }
    public int Frequency { get; set; }
    public HuffmanNode Left { get; set; }
    public HuffmanNode Right { get; set; }
}

class Huffman
{
    public static Dictionary<char, string> Compress(string text)
    {
        Dictionary<char, int> frequencies = text
            .GroupBy(c => c)
            .ToDictionary(g => g.Key, g => g.Count());

        List<HuffmanNode> nodes = frequencies
            .Select(pair => new HuffmanNode
            {
                Character = pair.Key,
                Frequency = pair.Value
            })
            .ToList();

        while (nodes.Count > 1)
        {
            List<HuffmanNode> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList();

            if (orderedNodes.Count >= 2)
            {
                List<HuffmanNode> taken = orderedNodes.Take(2).ToList();

                HuffmanNode parent = new HuffmanNode
                {
                    Character = '*',
                    Frequency = taken[0].Frequency + taken[1].Frequency,
                    Left = taken[0],
                    Right = taken[1]
                };

                nodes.Remove(taken[0]);
                nodes.Remove(taken[1]);
                nodes.Add(parent);
            }
        }

        HuffmanNode root = nodes.FirstOrDefault();

        Dictionary<char, string> codes = new Dictionary<char, string>();
        Encode(root, "", codes);

        return codes;
    }

    private static void Encode(HuffmanNode node, string code, Dictionary<char, string> codes)
    {
        if (node != null)
        {
            if (node.Left == null && node.Right == null)
            {
                codes[node.Character] = code;
            }
            else
            {
                Encode(node.Left, code + "0", codes);
                Encode(node.Right, code + "1", codes);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        string text = "Huffman kódolás teszt";

        Dictionary<char, string> codes = Huffman.Compress(text);

        Console.WriteLine("Huffman-kódolás:");
        foreach (var pair in codes)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }
}
