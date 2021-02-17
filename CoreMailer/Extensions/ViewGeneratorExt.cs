using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreMailer.Extensions
{
    public static class ViewGeneratorExt
    {
        internal static string GetLayout(this string fileName)
        {
            var content = new StringBuilder();

            var fileWithPath = Path.GetFullPath(fileName);
            try
            {
                using var htmlReader = new StreamReader(fileWithPath);
                string lineStr;
                while ((lineStr = htmlReader.ReadLine()) != null)
                {
                    content.Append(lineStr);
                }
            }
            catch (Exception e)
            {
                throw;
            }


            return content.ToString();
        }
        public static string GetHtmlEmailContent<T>(this T model, string fileName) where T : class
        {
            var content = new StringBuilder();
            var modelType = model.GetType();
            var properties = modelType.GetProperties().Where(x => (x.MemberType & MemberTypes.Property) != 0)
                .Select(x => x.Name).ToList();

            var fileWithPath = Path.GetFullPath(fileName);
            try
            {
                using var htmlReader = new StreamReader(fileWithPath);

                string lineStr;
                while ((lineStr = htmlReader.ReadLine()) != null)
                {
                    foreach (var property in properties)
                    {
                        var value = modelType.GetProperty(property)?.GetValue(model)?.ToString();
                        lineStr = Regex.Replace(lineStr, @"(?<![\w]){" + property + @"}(?![\w])", value ?? "");
                    }
                    content.Append(lineStr);
                }
            }
            catch (Exception e)
            {
                throw;
            }


            return content.ToString();
        }
        public static string GetTextEmailContent<T>(this T model, string message) where T : class
        {
            var content = new StringBuilder();
            var modelType = model.GetType();
            var properties = modelType.GetProperties().Where(x => (x.MemberType & MemberTypes.Property) != 0)
                .Select(x => x.Name).ToList();
            try
            {
                var _message = message;


                foreach (var property in properties)
                {
                    var value = modelType.GetProperty(property)?.GetValue(model)?.ToString();
                    _message = Regex.Replace(_message, @"(?<![\w]){" + property + @"}(?![\w])", value ?? "");
                }

                content.Append(_message);

            }
            catch (Exception e)
            {
                throw;
            }


            return content.ToString();
        }
    }
}
