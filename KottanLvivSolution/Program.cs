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
            if(GetCreditCardVendor(number) == "Unknown")
                return "The number of creditcart is wron";
            int num = Convert.ToInt32(number.Substring(6, number.Length-7));
            num++;
            string nums = num.ToString();
            int a = nums.Length;
            if (nums.Length != number.Length - 7)
            {
                for (int i = 0; i <= (number.Length - 8 - a); i++)
                    nums = nums.Insert(0, "0");
            }
            string num1 = number.Substring(0, 6) + nums;
            int a1 = num1.Length;
            int b = number.Length;
            if (num1.Length + 1 != number.Length)
                return "No more card numbers available for this vendor";
            int sum = 0;
            if (number.Length % 2 == 0)
            {
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
            }
            else
            {
                for (int i = 1; i <= num1.Length; i++)
                {
                    var n = num1[i - 1] - 48;
                    if (i % 2 == 0)
                    {
                        if ((n * 2) > 9)
                            sum += (n * 2) - 9;
                        else
                            sum += n * 2;
                    }
                    else
                        sum += n;
                }
            }
            if (sum % 10 == 0)
                num1 += '0';
            else
                num1 += (10 - sum % 10).ToString();
            return num1;
        }

        static bool IsCreditCardNumberValid(string number)
        {
            int sum = 0;
            if (number.Length % 2 == 0)
            {
                for (int i = 1; i <= number.Length; i++)
                {
                    var n = number[i - 1] - 48;
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
            }
            else
            {
                for (int i = 1; i <= number.Length; i++)
                {
                    var n = number[i - 1] - 48;
                    if (i % 2 == 0)
                    {
                        if ((n * 2) > 9)
                            sum += (n * 2) - 9;
                        else
                            sum += n * 2;
                    }
                    else
                        sum += n;
                }
            }
            if (sum % 10 == 0)
                return true;
            return false;
        }

        static string GetCreditCardVendor(string number)
        {
            if(!IsCreditCardNumberValid(number))
                return "Unknown";
            int four = Convert.ToInt32(number.Substring(0, 4));
            int two = Convert.ToInt32(number.Substring(0, 2));
            if (((four>=3528) && (four <= 3589)) && (number.Length == 16))
                    return "JCB";
            if (number.StartsWith("4") &&((number.Length == 13) || (number.Length == 16) ||(number.Length == 19)))
                return "Visa";
            if (((two >= 51) && (two <= 55)) && (number.Length == 16))
                return "MasterCard";
            if (((four >= 2221) && (four <= 2770)) && (number.Length == 16))
                return "MasterCard, not Active";
            if (((two >= 56) && (two <= 69) || (two == 50)) &&((number.Length >= 12) && (number.Length <= 19)))
                return "Maestro";
            if (((two == 34) || (two == 37)) && (number.Length == 15))
                return "American Express";
            return "Unknown";
        }
        
    }
}
