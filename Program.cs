using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace sudokoAlgorithm
{
    class Program
    {
   
        static void Main(string[] args)
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
                new int[] {1,9,1, 1,9,1, 1,9,1},
                new int[] {1,1,1, 1,1,9, 1,1,1},
                new int[] {1,3,2, 4,3,6, 7,8,9},
                new int[] {1,6,3, 4,3,6, 7,8,79},
                new int[] {1,0,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,1,3,3},
                new int[] {4,2,4,1},
                new int[] {1,2,3,4},
                       new int[] {4,2,4,4},

            };


            Console.WriteLine(validateSoduko3(goodSudoku1));
            Console.WriteLine(validateSoduko3(goodSudoku2));
            Console.WriteLine(validateSoduko3(goodSudoku3));
            Console.WriteLine(validateSoduko3(badSudoku1));
            Console.WriteLine(validateSoduko3(badSudoku2));


       }




        static bool validateSoduko3(int[][] puzzle)
        {

            int N = puzzle.Length;
            double sqrtN = Math.Sqrt(N);
            if (N > 0 && sqrtN % 1 > 0)
            {
                return false;
            }
            List<int?> validcolumns = new List<int?>();
            List<int?> validrows = new List<int?>();
            List<int[]> validBoxes = new List<int[]>();
            int ni = 0;
            int lastboxesInrow =-1;
            double boxPercent = 100 / sqrtN;
            for (int i = 0; i < puzzle.Length; i++)
            {
                int puzzleRowN = puzzle[i].Length;
                double rowsqrtN = Math.Sqrt(puzzleRowN);

                if (N > 0 && rowsqrtN % 1 > 0)
                {
                    return false;
                }
                if(!puzzle[i].Contains(N))
                {
                    return false;
                }
                if (!validrows.Contains(i))
                {
                    validrows.Add(i);
                }
                for (int j = 0; j < puzzle[i].Length; j++)
                {
                    if (puzzle[i][j] == N   )
                    {
                        double curCOlPerc = Math.Round((double)j / N * 100, 3);

                        if (((ni  <= i ||  ( ni%sqrtN !=0 && j <= (ni % sqrtN) * sqrtN)) && (int)(Math.Floor(curCOlPerc / (double)boxPercent)) != lastboxesInrow) 
                              )
                        {

                            ni++;
                            lastboxesInrow = (int)(Math.Floor(curCOlPerc / (double)boxPercent));
                            int[] plot = new int[] {  i/ (int)sqrtN, lastboxesInrow };
                            if (!validBoxes.Contains(plot)) { 
                                validBoxes.Add(plot);
                            }
                        }


                        if (!validcolumns.Contains(j))
                        {
                            validcolumns.Add(j);
                        }


                    }
        


                }

            }

            validcolumns = validcolumns.Where(c => c != null).ToList();
            validrows = validrows.Where(r => r != null).ToList();
            if (validcolumns.Count != N || validrows.Count != N || validBoxes.Count != N)
            {
                return false;
            }
            return true;
        }

    }
}
