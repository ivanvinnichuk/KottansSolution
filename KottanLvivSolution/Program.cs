using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KottanLvivSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Введіть номер картки: ");
            string c = Console.ReadLine();
            c = c.Replace(" ", "");
            Console.WriteLine("Вендор кредитної картки: " + GetCreditCardVendor(c));
            Console.Write("Коректність введеного номера: ");
            if (IsCreditCardNumberValid(c))
                Console.WriteLine("Valid");
            else
                Console.WriteLine("Not valid");
            Console.WriteLine("Згенерований наступний номер картки: " + GenerateNextCreditCardNumber(c));
            Console.Read();

        }

        static string GenerateNextCreditCardNumber(string number)
        {
            int num = Convert.ToInt32(number.Substring(6, 9));
            num++;
            string nums = num.ToString();
            if (nums.Length != 9)
            {
                for (int i = 0; i <= 9 - nums.Length; i++)
                    nums = nums.Insert(0, "0");
            }
            string num1 = number.Substring(0, 6) + nums;
            int sum = 0;
            for (int i = 1; i <= num1.Length; i++)
            {
                var n = num1[i - 1] - 48;
                if (i % 2 != 0)
                {
                    if ((n * 2) > 9)
                        sum += (n * 2) - 9;
                    else
                        sum += n * 2;
                }
                else
                    sum += n;
            }
            num1 += (10 - sum % 10).ToString();
            num1 = num1.Insert(4, " ");
            num1 = num1.Insert(9, " ");
            num1 = num1.Insert(14, " ");
            return num1;
        }

        static bool IsCreditCardNumberValid(string number)
        {
            int sum = 0;
            for(int i = 1; i<=number.Length; i++)
            {
                var n = number[i-1]-48;
                //int n = Convert.ToInt32((char)s);
                if (i % 2 != 0)
                {
                    if ((n * 2) > 9)
                        sum += (n * 2) - 9;
                    else
                        sum += n * 2;
                }
                else
                    sum += n;
                
            }
            if (sum % 10 == 0)
                return true;
            return false;
        }

        static string GetCreditCardVendor(string number)
        {
            int four = Convert.ToInt32(number.Substring(0, 4));
            int two = Convert.ToInt32(number.Substring(0, 2));
            if ((four>=3528) && (four <= 3589))
                    return "JCB";
            if (number.StartsWith("4"))
                return "Visa";
            if ((two >= 51) && (two <= 55))
                return "MasterCard";
            if (((two >= 56) && (two <= 69)) || (two == 50))
                return "Maestro";
            if ((two == 34) || (two == 37))
                return "American Express";
            return "Unknown";
        }
    }
}
