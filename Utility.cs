using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace OpenAPI_Call
{

    public static class StringExtensions
    {
        // not used
        public static string ReplaceCrLfWithNewLine(this string str)
        {
            if (!string.IsNullOrEmpty(str))
                return str.Replace("\\n", Environment.NewLine);
            return str;
        }

        public static string compact(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.Replace("  ", " ").Replace(". ", ".").Replace(", ", ",");
            }
            return str;
        }

        public static string getTolkensAndCompact(this string str, out double tolkenCount)
        {
            if (!string.IsNullOrEmpty(str))
            {
                // Split the sentence into words using whitespace as a delimiter
                string[] words = str.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                tolkenCount = words.Length * .75;
                return str.compact();
            }
            tolkenCount = 0;
            return str;
        }
    }


   


    public static class Utility
    {

        
        
        
        
        // not used, extension method above used instead
        public static double wordCount(string sentence)
        {
            // Split the sentence into words using whitespace as a delimiter
            string[] words = sentence.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Return the number of words in the sentence
                return words.Length;
        }

        public static string PrintJsonPropertiesAndValues(string json)
        {
            JToken token = JToken.Parse(json);
            StringBuilder sb = new StringBuilder();
            PrintJsonPropertiesAndValues(token, sb, "");
            return sb.ToString();
        }

        private static void PrintJsonPropertiesAndValues(JToken token, StringBuilder sb, string indent)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (var property in token.Children<JProperty>())
                    {
                        sb.AppendLine(indent + "Property: " + property.Name);
                        sb.AppendLine(indent + "Value: " + property.Value);
                        PrintJsonPropertiesAndValues(property.Value, sb, indent + "  ");
                    }
                    break;

                case JTokenType.Array:
                    foreach (var item in token.Children())
                    {
                        PrintJsonPropertiesAndValues(item, sb, indent + "  ");
                    }
                    break;

                default:
                    sb.AppendLine(indent + "Value: " + token);
                    break;
            }
        }




    }
}
