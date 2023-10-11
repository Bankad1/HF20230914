using System;

class Knapsack
{
    static int KnapsackDP(int[] weights, int[] values, int capacity)
    {
        int n = weights.Length;
        int[,] dp = new int[n + 1, capacity + 1];

        // Build the knapsack table
        for (int i = 0; i <= n; i++)
        {
            for (int w = 0; w <= capacity; w++)
            {
                if (i == 0 || w == 0)
                {
                    dp[i, w] = 0;
                }
                else if (weights[i - 1] <= w)
                {
                    dp[i, w] = Math.Max(values[i - 1] + dp[i - 1, w - weights[i - 1]], dp[i - 1, w]);
                }
                else
                {
                    dp[i, w] = dp[i - 1, w];
                }
            }
        }

        // The result is stored in dp[n, capacity]
        return dp[n, capacity];
    }

    static void Main()
    {
        int[] weights = { 1, 2, 3, 4, 5 };
        int[] values = { 10, 20, 30, 40, 50 };
        int capacity = 7;

        int maxValue = KnapsackDP(weights, values, capacity);
        Console.WriteLine("Maximum value that can be obtained: " + maxValue);
    }
}
