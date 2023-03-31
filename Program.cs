namespace NumberWaysCuttingPizza
{
    internal class Program
    {

        public class NumberWaysCuttingPizza
        {
            public int Ways(string[] pizza, int k)
            {
                int n = pizza.Length;
                int m = pizza[0].Length;
                int[,] apples = new int[n + 1, m + 1];
                int[,,] dp = new int[k, n, m];
                for(int r = n - 1; r >= 0; --r)
                {
                    for(int c = m - 1; c >= 0; --c)
                    {
                        apples[r, c] = (pizza[r][c] == 'A' ? 1 : 0) + apples[r + 1, c] + apples[r, c + 1] - apples[r + 1, c + 1];
                        dp[0, r, c] = apples[r, c] > 0 ? 1 : 0;
                    }
                }

                int modulo = 1000000007;
                for(int remain = 1; remain < k; ++remain)
                {
                    for(int r = 0; r < n; ++r)
                    {
                        for(int c = 0; c < m; ++c)
                        {
                            for(int nextR = r + 1; nextR < n; ++nextR)
                            {
                                if (apples[r, c] - apples[nextR, c] > 0)
                                {
                                    dp[remain, r, c] += dp[remain - 1, nextR, c];
                                    dp[remain, r, c] %= modulo;
                                }
                            }

                            for(int nextC = c + 1; nextC < m; ++nextC)
                            {
                                if (apples[r, c] - apples[r, nextC] > 0)
                                {
                                    dp[remain, r, c] += dp[remain - 1, r, nextC];
                                    dp[remain, r, c] %= modulo;
                                }
                            }
                        }
                    }
                }
                return dp[k - 1, 0, 0];
            }
        }

        static void Main(string[] args)
        {
            NumberWaysCuttingPizza numberWaysCuttingPizza = new();
            Console.WriteLine(numberWaysCuttingPizza.Ways(new string[] { "A..", "AAA", "..." }, 3));
            Console.WriteLine(numberWaysCuttingPizza.Ways(new string[] { "A..", "AA.", "..." }, 3));
            Console.WriteLine(numberWaysCuttingPizza.Ways(new string[] { "A..", "A..", "..." }, 1));
        }
    }
}