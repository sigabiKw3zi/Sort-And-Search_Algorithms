using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace PRTAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Title = "Sort and Search Algorithms";


            Console.WriteLine("Search and Sort Algorithms");
            Console.WriteLine();

            bool valid;

            string FilePath = "AssignementData.txt";
            StreamReader myReader = new StreamReader(FilePath); // Reads information from the text file



            List<int> lines = new List<int>(); //Reads text File into a list
            int[] arr = new int[lines.Count];


            int NumberToFind = FindNumber("Which number are you searching for?: "); //calls the method that propmts the user 
            int temp; //Reads every row in the text file 


            while (!myReader.EndOfStream)
            {

                temp = int.Parse(myReader.ReadLine());
                lines.Add(temp); // Loads read data into the list row by row.
            }

            myReader.Close();

            arr = Display(lines); // calls the Display return method 
            Console.WriteLine();
            do // Runs should a user enter an invalid option 
            {
                valid = true;

                Console.Write("Which Sort Algorithm would you like to use? \nBubble (Enter B)\nInsertion (Enter I)\nQuick (Enter Q)\nPlease enter your option: ");
                char SortOption = char.Parse(Console.ReadLine()); // Prompts and gets the user Sort Algorithm Option.

                if (SortOption == 'B' || SortOption == 'b') //Calls a method depending on User input
                    BubbleSort(arr);
                else if (SortOption == 'I' || SortOption == 'i')
                    InsertionSort(arr);
                else if (SortOption == 'Q' || SortOption == 'q')
                {
                    QuickSort(arr, 0, arr.Length - 1);

                }
                else // Caters for incase there is an invalid input from the user
                {
                    Console.WriteLine();
                    Console.WriteLine("You have entered an invalid option, Please enter a B, I or Q");
                    valid = false;
                    Console.WriteLine();
                }
            }
            while (valid == false);
            Console.WriteLine();
            do
            {
                valid = true;

                Console.Write("Which search algorithm would you like to use? \nLinear (Enter L)\nBinary (Enter B)\nPlease enter your option: ");
                char SearchOption = char.Parse(Console.ReadLine());

                if (SearchOption == 'l' || SearchOption == 'L')
                {
                    int indexFound = LinearSearch(arr, NumberToFind); // Calls the Linear Search Method 

                    Console.WriteLine();

                    if (indexFound == -1)//determines wether the number being searched exists in the file data 
                        Console.WriteLine("The number {0} was not found in the file", NumberToFind);
                    else
                        Console.WriteLine("The number {0} was found at index {1} in the file ", NumberToFind, indexFound);
                }
                else if (SearchOption == 'b' || SearchOption == 'B')
                {
                    int indexFound = BinarySearch(arr, NumberToFind); //Calls the Binary Search method 

                    if (indexFound == -1) //determines wether the number being searched exists in the file data
                        Console.WriteLine("{0} was not found in the array", NumberToFind);
                    else
                        Console.WriteLine("{0} was found in the array at index {1}", NumberToFind, indexFound);
                }
                else // Caters for incase there is an invalid input from the user
                {
                    Console.WriteLine();
                    Console.WriteLine("You have entered an invalid option, Please enter B or L");
                    valid = false;
                    Console.WriteLine();
                }
            } while (valid == false);


            Console.ReadLine();
        }
        static int FindNumber(string question)
        {
            int number = 0;
            bool valid;

            do
            {
                try
                {
                    valid = true;
                    Console.Write(question);//Prints out the prompt
                    number = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException) //handles an exception where a user might enter anything besides numerical data
                {
                    Console.WriteLine("Please only enter numerical data");
                    valid = false;
                }
            } while (valid == false);
            return number;
        }
        static void BubbleSort(int[] array)
        {
            int temp;

            for (int k = 0; k < array.Length; k++)
            {
                for (int i = 1; i < array.Length; i++)
                {
                    if (array[i - 1] > array[i])
                    {
                        temp = array[i - 1];
                        array[i - 1] = array[i];
                        array[i] = temp;
                    }

                }

            }


        }
        static void InsertionSort(int[] array)
        {

            int temp;

            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int k = i + 1; k > 0; k--)
                {
                    if (array[k - 1] > array[k])
                    {
                        temp = array[k - 1];
                        array[k - 1] = array[k];
                        array[k] = temp;
                    }
                }

            }

        }
        static void QuickSort(int[] array, int begin, int end)
        {

            int i;
            if (begin < end)
            {
                i = Partition(array, begin, end);
                QuickSort(array, begin, i - 1);//Recursive method that checks the Left hand side.
                QuickSort(array, i + 1, end);//Recursive method that checks the Right hand Side.

            }

        }

        static int Partition(int[] arr, int begin, int end)
        {
            int temp;
            int pivot = arr[end];
            int i = begin - 1;


            for (int j = begin; j <= end - 1; j++) //Loop responsible for the actual sorting 
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }

            }
            temp = arr[i + 1]; //Swap first and last value .
            arr[i + 1] = arr[end];
            arr[end] = temp;

            return i + 1; //returns and determines what our new pivot is .


        }
        static int[] Display(List<int> Lines)
        {
            int[] Arr = new int[Lines.Count];

            int x = 0;

            foreach (int Value in Lines)
            {
                Arr[x] = Value;
                x++;
            }


            return Arr;
        }

        static int LinearSearch(int[] Lines, int Number)
        {
            Console.WriteLine();
            int IndexFound = -1;

            for (int i = 0; i < Lines.Length; i++)
            {
                if (Number == Lines[i])
                {
                    IndexFound = i;
                    break;
                }
            }
            return IndexFound;
        }
        static int BinarySearch(int[] Lines, int Number)
        {
            Console.WriteLine();
            int First, Last, Middle, IndexFound = 0;
            bool Found;

            First = 0;

            Last = Lines.Length - 1;


            Found = false;

            while (Found == false && First <= Last)
            {
                Middle = (First + Last) / 2;

                if (Number == Lines[Middle])
                {
                    Found = true;
                    IndexFound = Middle;
                }
                else
                    if (Lines[Middle] > Number)
                    Last = Middle - 1;
                else if (Lines[Middle] < Number)
                    First = Middle + 1;
            }

            if (Found == false)
                IndexFound = -1;


            return IndexFound;
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //            string filePath = @"C: \Users\mbasa\OneDrive\Documents\ONT\PRTAssignment\PRTAssignment\bin\Debug\AssignementData.txt";

        //            StreamReader myReader = new StreamReader(filePath);

        //            List<string> lines = new List<string>();

        //            string SearchOption;

        //            Console.WriteLine("Which Search Option would you prefern Linear (L) or Binary (B): ");

        //            SearchOption = myReader.ReadLine();

        //            int NumToFind = GetNumber("Which number are you looking for?: ", 1, 100);

        //            string currentRow = myReader.ReadLine();

        //            while (!myReader.EndOfStream)
        //            {
        //                if (currentRow == NumToFind.ToString())
        //                {
        //                    Console.WriteLine("Number was found");
        //                    break;
        //                }
        //                currentRow = myReader.ReadLine();
        //            }

        //            while(!myReader.EndOfStream)
        //            {
        //                lines.Add(myReader.ReadLine());
        //                currentRow = myReader.ReadLine();
        //            }

        //            int indexFound = SequentialSearch(lines, NumToFind);

        //            if (indexFound == -1)
        //            {
        //                Console.WriteLine("The number {0} was not found in this file", NumToFind);
        //            }
        //            else
        //            {
        //                Console.WriteLine("The number was {0} was found at index {1} in the file ", NumToFind, indexFound);
        //            }

        //            int size = 50, min = 1, max = 100;
        //            int[] array = new int[size];
        //            array = FillArray(size, min, max);

        //            //int indexFound = BinarySearch(array, NumToFind);


        //            //Console.WriteLine("Unsorted Array");

        //            //DisplayArray(array);

        //            //Console.WriteLine("The array was sorted using Bubble sort");

        //            //BubbleSort(array);
        //        }

        //        static int LinearSearch(int[] Lines, int Numnber)
        //        {
        //            Console.WriteLine();
        //            int indexFound = -1;

        //            for (int i = 0; i < Lines.Length; i++)
        //            {
        //                if(Number == Lines[i])
        //                {
        //                    indexFound = i;
        //                    break;
        //                }
        //            }
        //            return indexFound;
        //        }

        //        static int SequentialSearch(List<string> lines, int numToFind)
        //        {
        //            int indexFound = -1;

        //            for (int i = 0; i < lines.Count; i++)
        //            {
        //                if(numToFind.ToString() == lines[i])
        //                {
        //                    indexFound = i;
        //                    break;
        //                }
        //            }
        //            return indexFound;
        //        }

        //        static int[] FillArray(int size, int min, int max)
        //        {
        //            int[] array = new int[size];
        //            int temp;
        //            bool found;

        //            Random random = new Random();

        //            for (int i = 0; i < size; i++)
        //            {
        //                do
        //                {
        //                    temp = random.Next(min, max + 1);
        //                    found = false;

        //                    for (int j = 0; j < temp; j++)
        //                    {
        //                        found = true;
        //                        j = i;
        //                    }

        //                    if (found == false)
        //                        array[i] = temp;
        //                } while (found == true);

        //            }
        //            Array.Sort(array);
        //            return array;
        //        }

        //        static int GetNumber(string prompt, int lowest, int highest)
        //        {
        //            int number = 0;
        //            bool valid;

        //            do
        //            {
        //                try
        //                {
        //                    valid = true;
        //                    Console.Write(prompt);
        //                    number = int.Parse(Console.ReadLine());
        //                    if (number < lowest || number > highest)
        //                    {
        //                        Console.WriteLine("Invalid number entered. Number should be between {0} & {1}.", lowest, highest);
        //                        valid = false;
        //                    }
        //                }
        //                catch (System.FormatException ex)
        //                {
        //                    Console.WriteLine("Invalid number entered. Please enter numerical data.");
        //                    valid = false;
        //                }
        //            }while (valid == false);
        //            return number;
        //        }
                    //BinarySearch
        //        static int BinarySearch(int[] array, int NumberToFind)
        //        {
        //            int first, last, middle = 0;
        //            bool found;
        //            int indexFound;
        //            first = 0;
        //            last = array.Length - 1;
        //            found = false;

        //            while(found == false && first <= last)
        //            {
        //                middle = (first + last) / 2;   
        //                if (NumberToFind == array[middle])
        //                    found = true;
        //                else
        //                {
        //                    if(NumberToFind > array[middle])
        //                        first = middle + 1;
        //                    else
        //                        last = middle - 1;
        //                }

        //            }

        //            if (found == true)
        //                indexFound = middle;
        //            else
        //                indexFound = -1; 
        //            return indexFound;
        //       }


        //        static void DisplayArray(int[] array)
        //        {
        //            for (int i = 0; i < array.Length; i++)
        //            {
        //                Console.Write("[{0}]{1},", i, array[i]);
        //            }

        //            Console.WriteLine("[{0}]{1},", array.Length - 1, array[array.Length - 1]);

        //        }

                    // BubbleSort
        //    //    static void BubbleSort(int[] array)
        //    //    {
        //    //        int temp;

        //    //        for (int x = 0; x < array.Length; x++)
        //    //        {
        //    //            for (int i = 1; i < array.Length; i++)
        //    //            {
        //    //                if (array[i - 1] > array[i])
        //    //                {
        //    //                    temp = array[i - 1];
        //    //                    array[i - 1] = array[i];
        //    //                    array[i] = temp;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //    //Insertion
        //    //    static void InsertionSort(int[] array)
        //    //    {
        //    //        int temp;

        //    //        for (int i = 0; i < array.Length - 1; i++)
        //    //        {
        //    //            for (int x = i + 1; x > 0; x--)
        //    //            {
        //    //                if (array[x - 1] > array[x])
        //    //                {
        //    //                    temp = array[x - 1];
        //    //                    array[x - 1] = array[x];
        //    //                    array[x] = temp;
        //    //                }
        //    //            }
        //    //        }
        //    //    }


        //    //    //QuickSort
        //    //    static void QuickSort(int[] array, int start, int end)
        //    //    {
        //    //        int i;

        //    //        if (start < end)
        //    //        {
        //    //            i = Partition(array, start, end);
        //    //            QuickSort(array, start, i - 1);
        //    //            QuickSort(array, i + 1, end);
        //    //        }
        //    //    }

        //    //    static int Partition(int[] array, int start, int end)
        //    //    {
        //    //        int temp;
        //    //        int pivot = array[end];
        //    //        int i = start - 1;

        //    //        for (int x = start; x <= end - 1; x++)
        //    //        {
        //    //            if (array[x] <= pivot)
        //    //            {
        //    //                i++;
        //    //                temp = array[i];
        //    //                array[i] = array[x];
        //    //                array[x] = temp;
        //    //            }
        //    //        }
        //    //        temp = array[i + 1];
        //    //        array[i + 1] = array[end];
        //    //        array[end] = temp;
        //    //        return i + 1;
        //    //    }
    }
    }
