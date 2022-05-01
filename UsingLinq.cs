using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokoAlgorithm
{
    class UsingLinq
    {
        static void Main2(string[] args)
        {

            int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };


            int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };
            int[][] goodSudoku3 =  {
                      new int[] {1,9,9,9, 16,9,9,9, 1,16,1,1, 1,1,1,1},
                      new int[] {9,16,9,9, 16,9,9,9, 9,9,9,9, 5,2,16,9},
                      new int[] {9,9,9,9, 9,9,16,9, 9,9,9,16 ,4,11,16,9},
                      new int[] {9,9,16,9, 9,9,16,9, 9,9,9,16 ,6,4,9,9},

                      new int[] {1,16,1,1, 1,16,1,1, 1,1,1,1, 1,1,1,1},
                      new int[] {1,16,1,1, 9,1,1,9, 1,1,1,1, 1,1,1,1},
                      new int[] {1,1,1,1, 1,9,1,1, 1,1,16,1, 1,1,1,1},
                      new int[] {1,3,2,4, 3,16,7,8, 1,16,1,1, 16,1,1,1},

                      new int[] {1,9,3,4, 9,6,7,8, 1,1,1,1,   1,16,1,19},
                      new int[] {1,11,3,4, 16,6,7,8, 1,1,16,1, 1,16,1,1},
                      new int[] {1,16,3,16, 5,6,7,16, 16,1,1,1, 1,16,1,1},
                      new int[] {1,11,3,4, 5,6,7,8, 1,1,1,1, 1,1,1,16},

                      new int[] {1,11,3,4, 5,6,16,8, 1,1,16,1, 16,1,1,1},
                      new int[] {16,11,3,4, 5,6,7,8, 1,1,1,1, 16,1,1,1},
                      new int[] {1,16,3, 4,16,6,7, 8,16,1,1, 1,1,16,1,1},
                      new int[] {1,0,3,4, 5,6,7,8, 1,1,1,1, 1,1,16,1},
                        };


            int[][] badSudoku1 =  {
                new int[] {9,9,9, 9,9,9, 9,9,9},
                new int[] {9,9,9, 9,9,9, 9,9,9},
                new int[] {9,9,9, 9,9,9, 9,9,9},
                new int[] {1,1,1, 1,1,1, 1,1,9},
                new int[] {1,1,1, 1,9,1, 1,9,1},
                new int[] {1,1,1, 1,1,9, 1,1,1},
                new int[] {1,3,2, 4,3,6, 7,8,9},
                new int[] {1,9,3, 4,9,6, 7,8,79},
                new int[] {1,0,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,4,3,3},
                new int[] {7,2,3,1},
                new int[] {1,2,3,1},
                       new int[] {4,2,4,4},

            };

            Console.WriteLine(SodukoLinq(goodSudoku1));
            Console.WriteLine(SodukoLinq(goodSudoku2));
            Console.WriteLine(SodukoLinq(goodSudoku3));
            Console.WriteLine(SodukoLinq(badSudoku1));
            Console.WriteLine(SodukoLinq(badSudoku2));
        }

        static bool SodukoLinq(int[][] puzzle)
        {
            int N = puzzle.Length;
            double sqrtN = Math.Sqrt(N);
            if (N > 0 && sqrtN % 1 > 0)
            {
                return false;
            }

            List<int[]> validBoxes = new List<int[]>();
            int ni = 0;
            int lastboxesInrow = -1;
            int j = 0;
            int i = 0;
            double boxPercent = 100 / sqrtN;
            var validboxes = (from r in puzzle
                              where N > 0 && Math.Sqrt(r.Length) % 1 == 0 && r.Contains(N)
                              select new
                              {
                                  row = r,
                                  i = i++,
                                  j = 0
                              } into newval
                              where newval.row != null
                              from c in newval.row
                              select new
                              {
                                  col = c,
                                  j = j++
                              } into cs
                              where cs.col == N
                              select boxes(ref ni, j - 1, i - 1, (int)sqrtN, N, boxPercent, ref lastboxesInrow)
                                     into vb
                              where vb != null
                              select vb).ToList();
            if (validboxes.Count != N)
            {
                return false;
            }
            return true;
        }

        private static int[] boxes(ref int ni, int j, int i, int sqrtN, int N, double boxPercent, ref int lastboxesInrow)
        {
            double curColPerc = Math.Round((double)j / N * 100, 3);
            int[] plot = null;
            if (
                ((ni <= i || (ni % sqrtN != 0 && j <= (ni % sqrtN) * sqrtN))
                && (int)(Math.Floor(curColPerc / (double)boxPercent)) != lastboxesInrow)
                  )
            {

                ni++;
                lastboxesInrow = (int)(Math.Floor(curColPerc / (double)boxPercent));
                plot = new int[] { i / (int)sqrtN, lastboxesInrow };
            }

            return plot;
        }



    }
}
