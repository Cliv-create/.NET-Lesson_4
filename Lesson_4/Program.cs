using System.Text.RegularExpressions;

namespace Lesson_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.Write("Input number to roman numeral: ");
            int user_number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(NumberToRomanNumeral(user_number));

            string pattern = @"(?<=user:).*?(?=\\e)";
            string to_search = @"user:\n123\t\e";

            string result = FindRegex(pattern, to_search);
            if (result != "0")
            {
                Console.WriteLine(result);
            }


            Print2DArray(25, 25, 500, 1000);
            */

            Console.WriteLine(@"Input file path: ");
            string user_path = Console.ReadLine();
            Console.WriteLine("Input search pattern. (?<==  ).* - for numbers pattern: ");
            string search_pattern = Console.ReadLine();

            // ReadAllLines(user_path);
            List<string> result = FindRegexInArray(search_pattern, ReadAllLines(user_path));
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            Console.Write("Input array size: ");
            int user_array_size = Convert.ToInt32(Console.ReadLine());

            string[] random_numerals_array = new string[user_array_size];
            var rand = new Random();
            for (int i = 0; i < random_numerals_array.Length; i++)
            {
                random_numerals_array[i] = result[rand.Next(0, result.Count - 1)];
            }

            foreach (var item in random_numerals_array)
            {
                Console.WriteLine(item);
            }
        }

        static string NumberToRomanNumeral(int number)
        {
            if (number > 4000) number = 2500;
            string[] single_digit = new string[] { " ", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" }; // x - 1 to get number's
            string[] dozen = new string[] { " ", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" }; // Range 1 - 9. "C" for 100
            string[] hundreds = new string[] { " ", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" }; // 1-9. M for 1000
            string[] thousands = new string[] { " ", "M", "MM", "MMM", "MMMM" }; // 1 - 4
            string[] result = new string[] { $"{thousands[FlooredIntDiv(number, 1000) % 10]}", $"{hundreds[FlooredIntDiv(number, 100) % 10]}", $"{dozen[FlooredIntDiv(number, 10) % 10]}", $"{single_digit[number % 10]}" };
            return string.Join("", result);
        }

        static int FlooredIntDiv(int a, int b)
        {
            return (a / b - Convert.ToInt32(((a < 0) ^ (b < 0)) && (a % b != 0)));
        }

        // TODO: Pass file link. Check file existence. Read line by line. Return string array.
        // Create regex pattern. Reuse pattern in a for loop throught string array. Extract numerals into string array. Return string array.
        // Make a array function. Pass string arrray, use Rand to fill array with numerals.

        static string[] ReadAllLines(string file_path)
        {
            if (!File.Exists(file_path))
            {
                Console.WriteLine($"File {file_path} doesn't exist!");
                // Debug.WriteLine($"File {file_path} doesn't exist!");
                return new string[] { "N/A" };
            }

            string[] array = File.ReadAllLines(file_path);
            return array;
        }

        /// <summary>
        /// Searches string for provided regex pattern. Provide string with @ (verbatim identifier) modifier to interpret string literally (string example = @"example").
        /// </summary>
        /// <param name="regex_pattern">Pattern to use when creating regex.</param>
        /// <param name="input">String to be searched.</param>
        /// <returns>String that matched regex pattern. Returns string "0" if nothning was found.</returns>
        static string FindRegex(string regex_pattern, string input)
        {
            Regex pattern = new Regex(regex_pattern);

            Match match = pattern.Match(input);

            if (match.Success)
            {
                return Convert.ToString(match);
                // match = match.NextMatch(); // for while
            }
            else
            {
                return "0";
            }
        }

        static List<string> FindRegexInArray(string regex_pattern, string[] array_input)
        {
            if (regex_pattern == null || regex_pattern == " ") return new List<string> { "N/A" };
            Regex pattern = new Regex(regex_pattern, RegexOptions.Compiled);
            List<string> array_return = new List<string>();
            array_return.Capacity = array_input.Length / 2;
            
            for (int i = 0; i < array_input.Length; i++)
            {
                Match match = pattern.Match(array_input[i]);
                if (match.Success) array_return.Add(match.Value);
            }
            return array_return;
        }

        static bool Print2DArray(int height, int width, int min_value, int max_value)
        {
            var ar = new int[height, width];

            for (int y = 0; y < ar.GetLength(0); y++)
            {
                for (int x = 0; x < ar.GetLength(1); x++)
                {
                    ar[y, x] = new Random().Next(min_value, max_value); // 0-999
                    Console.Write("{0, 4}", ar[y, x]);
                }
                Console.WriteLine();
            }
            return true;
        }

        static bool Print2DArray(int min_value = 0, int max_value = 1000)
        {
            var ar = new int[10, 10];

            for (int y = 0; y < ar.GetLength(0); y++)
            {
                for (int x = 0; x < ar.GetLength(1); x++)
                {
                    ar[y, x] = new Random().Next(min_value, max_value); // 0-999
                    Console.Write("{0, 4}", ar[y, x]);
                }
                Console.WriteLine();
            }
            return true;
        }
    }
}
