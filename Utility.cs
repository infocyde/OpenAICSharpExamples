﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace OpenAPI_Call
{
    public static class Utility
    {


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
