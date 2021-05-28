using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Leetcode
{
    class Program
    {
        static void Main(string[] args)
        {
            //var n1 = new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9)))))));
            //var n2 = new ListNode(9, new ListNode(9, new ListNode(9, new ListNode(9))));
            //Console.WriteLine(AddTwoNumbers(n1, n2));
            //Console.WriteLine(GetSum(-16, -115));
            //LetterCasePermutation("a1b2");
            //TwoSum(new[] { 3, 3 }, 6);
            //TwoSumSecond(new[] {2, 7, 11, 15}, 9);
            //LengthOfLongestSubstring("pwwkew");
            //Convert("AB", 1);
            //Console.WriteLine(MyAtoi(" -"));
        }

        public static int MyAtoi(string str)
        {
            str = str.Trim();
            if (string.IsNullOrEmpty(str) ||
                !(char.IsDigit(str[0]) || str[0] == '+' || str[0] == '-'))
                return 0;

            if ((str[0] == '+' || str[0] == '-')
                && (str.Length > 1 && !char.IsDigit(str[1])
                || str.Length == 1)) return 0;

            var number = new StringBuilder(str[0].ToString());
            for (var i = 1; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                    number.Append(str[i]);
                else break;
            }

            if (int.TryParse(number.ToString(), out var result))
            {
                return result;
            }
            else if (number[0] == '-')
            {
                return int.MinValue;
            }
            else 
            {
                return int.MaxValue;
            }
        }

        public static string Convert(string s, int numRows)
        {
            if (numRows == 1)
                return s;
            var matrix = new string[Math.Min(s.Length, numRows)];
            var row = 0;
            var isChangedRow = false;
            foreach (var c in s)
            {
                matrix[row] += c;
                if (row == 0 || row == numRows - 1)
                    isChangedRow = !isChangedRow;
                row += isChangedRow ? 1 : -1;
            }

            return matrix.Aggregate("", (result, current) => result + current);
        }

        public static int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            var charsAndIndices = new Dictionary<char, int>();
            var startIndex = 0;
            var longLength = 0;
            for (int endIndex = 0; endIndex < s.Length; endIndex++)
            {
                if (charsAndIndices.ContainsKey(s[endIndex]))
                {
                    startIndex = Math.Max(startIndex, charsAndIndices[s[endIndex]] + 1);
                }

                charsAndIndices[s[endIndex]] = endIndex;
                longLength = Math.Max(longLength, endIndex - startIndex + 1);
            }

            return longLength;
        }

        public int[] twoSum(int[] numbers, int target)
        {
            int l = 0, h = numbers.Length - 1, sum;

            while ((sum = numbers[l] + numbers[h]) != target && h != l)
            {
                if (sum > target)
                    h = BinarySearch(numbers, l + 1, h - 1, target - numbers[l]);
                else if (sum < target)
                    l = BinarySearch(numbers, l + 1, h - 1, target - numbers[h]);
            }
            return new[] { l + 1, h + 1 };
        }

        private static int BinarySearch(int[] numbers, int low, int high, int target)
        {
            while (low < high)
            {
                var mid = (low + high) / 2;
                if (target == numbers[mid])
                    return mid;
                else if (target < numbers[mid])
                    high = mid;
                else
                    low = mid + 1;
            }

            return high;
        }

        public static int[] TwoSumSecond(int[] nums, int target)
        {
            var result = new int[2];
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        result[0] = i + 1;
                        result[1] = j + 1;
                        return result;
                    }
                }
            }

            return result;
        }

        public static int[] TwoSum(int[] nums, int target)
        {
            var result = new int[2];
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        result[0] = i;
                        result[1] = j;
                        return result;
                    }
                }
            }

            return result;
        }

        // "a1b24c"
        // "A1b24c"
        // "A1B24c"
        // "A1b24C" ...
        public static IList<string> LetterCasePermutation(string s)
        {
            Permutation(s);
            return res;
        }

        private static List<string> res = new List<string>();
        static void Permutation(string s, int i = 0, string temp = "")
        {
            if (i == s.Length)
            {
                res.Add(temp);
                return;
            }

            Permutation(s, i + 1, temp + s[i]);
            if (char.IsLower(s[i]))
                Permutation(s, i + 1, temp + char.ToUpper(s[i]));
            else if (char.IsUpper(s[i]))
                Permutation(s, i + 1, temp + char.ToLower(s[i]));
        }

        public static int GetSum(int a, int b)
        {
            return b == 0 ? a : GetSum(a ^ b, (a & b) * 2);
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var current = new ListNode();
            var res = current;
            var rem = 0;
            while (l1 != null || l2 != null)
            {
                var n1 = l1?.val ?? 0;
                var n2 = l2?.val ?? 0;
                var sum = n1 + n2 + rem;
                rem = sum / 10;
                current.val = sum % 10;
                if (l1?.next == null && l2?.next == null)
                    break;
                current.next = new ListNode();
                current = current.next;

                l1 = l1?.next;
                l2 = l2?.next;
            }

            if (rem == 1)
                current.next = new ListNode(1);
            return res;
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}
