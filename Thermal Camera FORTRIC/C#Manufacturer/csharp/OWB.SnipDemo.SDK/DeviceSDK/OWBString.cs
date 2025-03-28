using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;

namespace OWB.SnipDemo.SDK
{
    public static class OWBString
    {
        public static string BytesToString(byte[] input, Encoding encode)
        {
            if (input == null)
            {
                return string.Empty;
            }

            int length = input.Length;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 0)
                {
                    length = i;
                    break;
                }
            }
            return encode.GetString(input, 0, length);
        }

        public static byte[] StringToBytes(string input, int size, Encoding encode)
        {
            if (input != null)
            {
                byte[] tmp = encode.GetBytes(input);
                return tmp;
            }

            return new byte[1];
        }

        public static string BytesToHexString(byte[] input, string format, string separator)
        {
            if (input == null)
            {
                return string.Empty;
            }

            string result = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (i == 0)
                {
                    result += input[i].ToString(format);
                }
                else
                {
                    result += separator + input[i].ToString(format);
                }
            }
            return result;
        }

        public static byte[] HexStringToBytes(string input, string separator)
        {
            if (input == null)
            {
                return new byte[0];
            }

            List<byte> result = new List<byte>();
            if (string.IsNullOrEmpty(separator))
            {
                for (int i = 0; i < input.Length; i = i + 2)
                {
                    result.Add(byte.Parse(input.Substring(i, 2), NumberStyles.AllowHexSpecifier));
                }
            }
            else
            {
                string[] splits = input.Split(separator.ToCharArray());
                for (int i = 0; i < splits.Length; i++)
                {
                    result.Add(byte.Parse(splits[i], NumberStyles.AllowHexSpecifier));
                }
            }

            return result.ToArray();
        }

        public static string GenerateMD5(string input)
        {
            string output = string.Empty;
            MD5 md5 = new MD5CryptoServiceProvider();
            output = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(input)), 4, 8).Replace("-", "");
            return output;
        }
    }
}
