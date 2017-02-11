using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CDS.Model.Dto;
using Newtonsoft.Json.Linq;
using Task = System.Threading.Tasks.Task;

namespace Ex10ntions
{
    public static class Extensions
    {
      
        #region bool

        /// <summary>
        /// return true if the bool? has a TRUE value, false if NULL of FALSE
        /// </summary>
        /// <param name="val">a nullable bool</param>
        /// <returns>boolean value </returns>
        public static bool IsTrue(this bool? val)
        {
            return val.HasValue && val.Value;
        }
        /// <summary>
        /// returns true if the bool? has a FALSE valuse, false if NULL or True
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsFalse(this bool? val)
        {
            return val.HasValue && !val.Value;
        }

        #endregion bool

        #region lists

        public static DataTable ToDataTable<T>(this List<T> list, string columnName)
        {
            DataTable table = new DataTable();

            //1. add column
            table.Columns.Add(columnName, typeof(int));
            //2. add rows
            foreach (T item in list)
            {
                table.Rows.Add(item);
            }

            return table;
        }

        /// <summary>
        /// creates a list of T, and adds the object to this list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> ToSingleMemberList<T>(this T obj)
        {
            var list = new List<T> { obj };
            return list;
        }

        /// <summary>
        /// like List.AddRange but returns the list, so it will be more fluid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldList"></param>
        /// <param name="newList"></param>
        /// <returns></returns>
        public static List<T> AddList<T>(this List<T> oldList, IEnumerable<T> newList)
        {
            oldList.AddRange(newList);
            return oldList;
        }

        /// <summary>
        /// addes several items to a list and returns the list, so it will be more fluid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldList"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> AddItems<T>(this List<T> oldList, params T[] list)
        {
            oldList.AddRange(list);
            return oldList;
        }

        /// <summary>
        /// checks if an item is in a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsIn<T>(this T source, params T[] list) => list.Contains(source);
        /// <summary>
        /// checks if an item is in a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsIn<T>(this T source, List<T> list) => list.Contains(source);

        public static bool IsEmptyList<T>(this List<T> list) => list == null || list.Count == 0;

        public static List<T> RemoveEmptyItems<T>(this IEnumerable<T> list)=>list.Where(l => typeof(T) == typeof(string)? !string.IsNullOrEmpty(l as string) : l != null).ToList();      
       
        /// <summary>
        ///join a list of string s to a single string separated by a delimiter
        /// </summary>
        /// <param name="array"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> array, string delimiter) => string.Join(delimiter, array);

        public static IEnumerable<T> DistinctBy<T, TO>(this IEnumerable<T> source, Func<T, TO> func) => source.GroupBy(func).Select(g => g.First());

        public static bool HasItems<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }
        #endregion lists

        #region Enum

        /// <summary>
        /// parser a string to a value of a given enum
        /// </summary>
        /// <typeparam name="T">the type of enum</typeparam>
        /// <param name="inp">string to parse to enum</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string inp)
        {
            object enumObject = Enum.Parse(typeof(T), inp.CapitalizeFirstLetter());

            return (T)Convert.ChangeType(enumObject, typeof(T));
        }

        public static T? ToNullableEnum<T>(this string inp) where T : struct
        {
            if (inp.IsEmpty())
            {
                return null;
            }

            object enumObject = Enum.Parse(typeof(T), inp.CapitalizeFirstLetter());

            return (T)Convert.ChangeType(enumObject, typeof(T));
        }

        /// <summary>
        /// converts an object to an enum
        /// </summary>
        /// <typeparam name="T">the type of enum</typeparam>
        /// <param name="inp">object to transform to enum</param>
        /// <returns></returns>
        public static T ToEnum<T>(this object inp)
        {
            //if object is number
            if (inp is int)
            {
                return (T)Enum.ToObject(typeof(T), Convert.ToInt32(inp));
            }

            //string
            object enumObject = Enum.Parse(typeof(T), inp.ToString().CapitalizeFirstLetter());
            return (T)Convert.ChangeType(enumObject, typeof(T));
        }

        public static T? ToNullableEnum<T>(this object inp) where T : struct
        {
            if (inp==null || inp.ToString().IsEmpty())
            {
                return null;
            }

            //if object is number
            if (inp is int)
            {
                return (T)Enum.ToObject(typeof(T), Convert.ToInt32(inp));
            }

            //string
            object enumObject = Enum.Parse(typeof(T), inp.ToString().CapitalizeFirstLetter());
            return (T)Convert.ChangeType(enumObject, typeof(T));
        }

        /// <summary>
        /// returns the name and value of and enums type as a dictionary
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<string, int> ToEnumDictionary(this Type enumType)
                => Enum.GetValues(enumType).Cast<int>().ToDictionary(e => Enum.GetName(enumType, e), e => e);

        /// <summary>
        /// return a dictionary of a enum name to enum name and value
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<string, EnumDto> ToEnumDtoDictionary(this Type enumType)
            => enumType.ToEnumDictionary().ToDictionary(e => e.Key, e => new EnumDto { Value = e.Value, Description = e.Key.InsertSpaceBeforeCapitalLetters() });

        #endregion Enum

        #region String
        public static string NormalizeAttributeName(this string attributeName, string entityName)
        {
            if (string.IsNullOrEmpty(entityName))
                return attributeName;

            return attributeName.StartsWith($"{entityName}.") ? attributeName : $"{entityName}.{attributeName}";
        }
        public static string SimpleAttributeName(this string attributeName, string entityName) => attributeName.Replace($"{entityName}.", string.Empty);

        /// <summary>
        /// hash the input string using SHA256c
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        

        public static string GetUniqueGuid(int count = 6)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }

        public static string GetRandomAlphaNumericString(int length)
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;
        }

        /// <summary>
        /// transforms every letter that has a space before it to a capital letter
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string InsertSpaceBeforeCapitalLetters(this string input) => string.Concat(input.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');

        /// <summary>
        /// Capitalize the first letter in a given string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CapitalizeFirstLetter(this string text) => text.First().ToString().ToUpper() + text.Substring(1).ToLower();

        public static string SurroundWithApostrophes(this string text) => "'" + text + "'";

        public static bool IsEmpty(this string val) => string.IsNullOrEmpty(val);

        public static bool IsNotEmpty(this string val) => !string.IsNullOrEmpty(val);

        public static string FormatWith(this string s, params object[] args) => string.Format(s, args);

        public static string Replace(this string template, IDictionary<string, string> kvp)
        {
            var sb = new StringBuilder(template);

            foreach (var kv in kvp)
            {
                sb.Replace(kv.Key, kv.Value);
            }

            return sb.ToString();
        }

        public static T ToJson<T>(this string str) => JObject.Parse(str).ToObject<T>();

        
        //public static string Remove(this string str, string stringToRemove) => str.Replace(stringToRemove, "");
        /// <summary>
        /// removes a list of strings from a string
        /// </summary>
        /// <param name="str">the original string we want to remove things from</param>
        /// <param name="args">several string that we want to remove from the original string</param>
        /// <returns></returns>
        public static string Remove(this string str, params string[] args) => args.Aggregate(str, (accumulator, item) => accumulator.Replace(item, ""));

        /// <summary>
        /// finds a string between to strings
        /// </summary>
        /// <param name="value"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string Between(this string value, string a, string b)
        {
            var posA = value.IndexOf(a);
            var posB = value.LastIndexOf(b);
            if (posA == -1 || posB == -1)
            {
                return string.Empty;
            }

            var adjustedPosA = posA + a.Length;
            return adjustedPosA >= posB ? string.Empty : value.Substring(adjustedPosA, posB - adjustedPosA);
        }

		 public static T ReadConfigXml<T>(string path)
        {
            var reader = new XmlSerializer(typeof(T));
            return (T)reader.Deserialize(new StreamReader(path));
        }
		
		public static T ReadConfigJson<T>(string path)
        {
            return JObject.Parse( File.ReadAllText(path)).ToObject<T>();
        }
 

        #endregion String

        #region object
        /// <summary>
        /// deep copies an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Clone<T>(this T obj) => JObject.FromObject(obj).ToObject<T>();

        public static T? ToNullable<T>(this object obj) where T : struct
        {
            if (obj == null)
            {
                return null;
            }
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static bool ToBoolean(this object obj) => Convert.ToBoolean(obj);
        public static int ToInt(this object obj) => Convert.ToInt32(obj);
        public static DateTime ToDateTimeOrDefault(this object obj) => Convert.ToDateTime(obj);


        public static bool IsDBNull(this IDataReader @this, string name)
        {
            return @this.IsDBNull(@this.GetOrdinal(name));
        }

        #endregion object

        #region Tasks

        /// <summary>
        /// returns a list of task based of the suplied lambda expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="source">the list of objects that task will be applyed to them</param>
        /// <param name="func">the lambda expression that will be applyed to each object</param>
        /// <returns></returns>
        public static List<Task<TO>> ToTask<T, TO>(this IEnumerable<T> source, Func<T, TO> func) => source.Select(item => Task.Run(() => func(item))).ToList();

        /// <summary>
        /// converts several actions to a list of tasks
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Task> ToTaskList(params Action[] list) => new List<Task>(list.Select(Task.Run));

        /// <summary>
        /// waiting for a list of tasks to complete (NOT AWAITABLE!)
        /// </summary>
        /// <typeparam name="TO"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TO[] WhenAll<TO>(this IEnumerable<Task<TO>> source) => Task.WhenAll(source).Result;

        /// <summary>
        /// waiting for a list of tasks to complete (AWAITABLE!)
        /// </summary>
        /// <typeparam name="TO"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static async Task<TO[]> WhenAllAsync<TO>(this IEnumerable<Task<TO>> source) => await Task.WhenAll(source);
        /// <summary>
        /// waiting for a list of tasks to complete (NOT AWAITABLE!)
        /// </summary>
        /// <param name="list"></param>
        public static void WhenAll(this List<Task> list) => Task.WhenAll(list).Wait();
        /// <summary>
        /// waiting for a list of tasks to complete (AWAITABLE!)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static async Task WhenAllAsync(this List<Task> list) => await Task.WhenAll(list);

        #endregion Tasks       
    }
}
