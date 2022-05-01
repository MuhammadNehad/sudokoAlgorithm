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
                      new int[] {1,16,3,4, 16,6,7,8, 16,1,1,1, 1,16,1,1},
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
            //check if puzzle root is integer
            int N = puzzle.Length;
            double sqrtN = Math.Sqrt(N);
            if (N > 0 && sqrtN % 1 > 0)
            {
                return false;
            }

            //lists to store valid columns , rows and boxes
            List<int?> validcolumns = new List<int?>();
            List<int?> validrows = new List<int?>();
            List<int[]> validBoxes = new List<int[]>();


            //ni is increment number of boxes to use in some conditions
            // lastboxesInrow is to store latest check box position to avoid dublicating boxes
            // boxPercent is to check space ratio that box takes from row
            int ni = 0;        
            int lastboxesInrow =-1;
            double boxPercent = 100 / sqrtN;

            // start iterate over puzzle

            for (int i = 0; i < puzzle.Length; i++)
            {

                // check if rows length root is integer (same of puzzle length)
                int puzzleRowN = puzzle[i].Length;
                if (puzzleRowN != N)
                {
                    return false;
                }

                //check if row contains N number
                if(!puzzle[i].Contains(N))
                {
                    return false;
                }

                //iterate over row to check columns cells

                for (int j = 0; j < puzzle[i].Length; j++)
                {
                    //check if column val equals N

                    if (puzzle[i][j] == N   )
                    {
                        //get the space ratio this column in to check which box it belongs to
                        double curCOlPerc = Math.Round((double)j / N * 100, 3);

                        // conditions to avoid dublications  ni for boxes increment sqrtn for square root of N , i for row position
                        // j for column position in row ,curCOlPerc for column space ratio ,boxPercent for one box space ratio ,and lastboxesinrow for latest checked box
                        if (((ni  <= i ||  ( ni%sqrtN !=0 && j <= (ni % sqrtN) * sqrtN)) && (int)(Math.Floor(curCOlPerc / (double)boxPercent)) != lastboxesInrow) 
                              )
                        {

                            ni++;
                            lastboxesInrow = (int)(Math.Floor(curCOlPerc / (double)boxPercent));

                            // set latest checked valid box and  ratio of i to N root to avoid dublication in adding to list

                            int[] plot = new int[] {  i/ (int)sqrtN, lastboxesInrow };

                            //condition to find if plot is added 
                            if (!validBoxes.Contains(plot)) { 
                                // adding valid plot
                                validBoxes.Add(plot);
                            }
                        }



                    }
        


                }

            }


            //get not null columns and rows and check if any of lists count less not same of N
            if (validBoxes.Count != N)
            {
                return false;
            }
            return true;
        }

    }
}
