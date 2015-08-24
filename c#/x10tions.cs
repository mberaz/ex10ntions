using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace x10tions
{
    public static class x10tions
    {
        #region string

        #region convert

        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        public static int? ToNullableInt(this string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return null;
            }
        }

        public static double ToDouble(this string value)
        {
            return double.Parse(value);
        }

        public static double? ToNullableDouble(this string value)
        {
            try
            {
                return double.Parse(value);
            }
            catch
            {
                return null;
            }
        }

        public static long ToLong(this string value)
        {
            return long.Parse(value);
        }

        public static long? ToNullableLong(this string value)
        {
            try
            {
                return long.Parse(value);
            }
            catch
            {
                return null;
            }
        }

        public static bool ToBool(this string value)
        {
            return bool.Parse(value);
        }

        public static bool? ToNullableBool(this string value)
        {
            try
            {
                return bool.Parse(value);
            }
            catch
            {
                return null;
            }
        }

        public static bool ToRegularBool(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return false;
                }
                else
                {

                    return bool.Parse(value);
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }


        public static DateTime? ToNullableDate(this string value, string dateFormat)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            try
            {
                return DateTime.ParseExact(value, dateFormat, provider);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime ToExactDate(this string value, string dateFormat)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;

            return DateTime.ParseExact(value, dateFormat, provider);
        }

        public static string RemovePX(this string input)
        {
            return input.Replace("px", "");
        }
        #endregion

        #region split

        public static List<t> SplitToList<t>(this string str, string delimiter)
        {
            var charArray = delimiter.ToCharArray();
            var type = typeof(t);
            var list = str.Split(charArray, StringSplitOptions.RemoveEmptyEntries).Select(s => (t)Convert.ChangeType(s, type)).ToList();
            return list;
        }

        public static List<string> RegexSplit(this string value, string delimiter)
        {
            Regex regex = new Regex(delimiter);
            var parts = regex.Split(value);
            return parts.ToList();
        }

        public static List<string> SplitNonEmpty(this string value, string delimiter)
        {
            return value.Split(delimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static List<string> SplitIntoParts(this string value, int partLength)
        {
            var result = new List<string>();
            int partIndex = 0;
            int length = value.Length;
            while (length > 0)
            {
                var tempPartLength = length >= partLength ? partLength : length;
                var part = value.Substring(partIndex * partLength, tempPartLength);
                result.Add(part);
                partIndex++;
                length -= partLength;
            }
            return result;
        }

        #endregion

        #region validate

        public static bool IsEmail(this string value)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(value);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool IsInteger(this string value)
        {
            try
            {
                int.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static string onlyNumbers(this string value)
        {
            string numbers = "0123456789";
            string newstr = string.Empty;
            for (int i = 0; i < value.Length; i++)
            {
                if (numbers.Contains(value[i])) newstr += value[i];
            }
            return newstr;
        }

        public static bool IsIsraelyIdentityNumber(this string value)
        {
            bool allCharactersInStringAreDigits = value.All(char.IsDigit);

            int numberOfChars = value.Length;
            if (numberOfChars == 9)
            {
                return true;
            }
            return false;
        }

        public static bool IsHebrew(this string input)
        {
            return input.Any(c => (c >= 0x0580 && c <= 0x05ff));
        }
        #endregion

        public static string Shorten(this string value, int charsNumber)
        {
            if (value.Length > charsNumber)
            {
                return value.Substring(0, charsNumber - 3) + "...";
            }
            return value;
        }

        public static string ReverseString(this string value)
        {
            char[] charArray = value.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string CombineWith(this string input, string suffix, string separator = " ")
        {
            if (string.IsNullOrEmpty(input))
            {
                if (string.IsNullOrEmpty(suffix))
                {
                    return string.Empty;
                }
                else
                {
                    return suffix;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(suffix))
                {
                    return input;
                }
                else
                {
                    return string.Format("{0}{1}{2}", input, separator, suffix);
                }
            }
        }

        public static string InsertDellimiterBeforeCapitalLetters(this string input, string delimiter = " ")
        {
            return string.Concat(input.Select(x => Char.IsUpper(x) ? delimiter + x : x.ToString())).TrimStart(delimiter.ToCharArray());
        }

        public static string Capitalize(this string input)
        {
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        public static string InsertLowerDellimiterBeforeCapitalLetters(this string input, string delimiter = " ")
        {
            return string.Concat(input.Select(x => Char.IsUpper(x) ? (delimiter + x.ToString().ToLower()) : x.ToString())).TrimStart(delimiter.ToCharArray());
        }

        public static string DecodeURL(this string input)
        {
            string temp = "";
            input = input.Replace("%20", "-").Replace("%7C", "_");
            temp = HttpUtility.UrlDecode(input);
            temp = input.Replace("|", "_").Replace("\"", "'").Replace(":", "-");

            return temp;
        }

        public static bool ISLegalInFileSystem(this string input)
        {
            List<string> notLegal = new List<string> { "<", ">", ":", "\"", "/", "\\", "|", "?", "*" };

            bool contains = input.Any(c => notLegal.Contains(c.ToString()));
            return !contains;
        }

        public static string RevereIfHebrew(this string input)
        {
            return input.IsHebrew() ? input.ReverseString() : input;
        }

        public static string ToCamelCase(this string input, bool capitaleFirst = false)
        {
            var parts = input.Split(' ');
            List<string> newList = new List<string>();

            if (capitaleFirst)
            {
                newList.Add(parts[0].Capitalize());
            }
            else
            {
                newList.Add(parts[0]);
            }

            foreach (var item in parts.Skip(1))
            {
                newList.Add(item.Capitalize());
            }


            return string.Join("", newList);
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }


        #endregion

        #region datetime

        public static DateTime StartOfMonth(this DateTime date)
        {
            var newDate = new DateTime(date.Year, date.Month, 1);
            return newDate;
        }

        public static DateTime EndOfMonth(this DateTime date)
        {
            var daysOfTheMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var newDate = new DateTime(date.Year, date.Month, daysOfTheMonth);
            return newDate;
        }

        public static DateTime StartOfNextMonth(this DateTime date)
        {
            date = date.AddMonths(1);
            DateTime newDate = new DateTime(date.Year, date.Month, 1);
            return newDate;
        }

        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            int diff = date.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return date.AddDays(-1 * diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            DateTime lastDayInWeek = date.Date;
            while (lastDayInWeek.DayOfWeek != startOfWeek)
            {
                lastDayInWeek = lastDayInWeek.AddDays(1);
            }

            return lastDayInWeek;
        }

        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        public static int NumberOfElapsedMonths(this DateTime dateStart, DateTime dateEnd)
        {
            int months = ((dateStart.Year - dateEnd.Year) * 12) + dateStart.Month - dateEnd.Month;
            return months;
        }

        public static bool IsLastDayOfTheMonth(this DateTime dateTime)
        {
            return dateTime == new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
        }

        public static bool IsFirstDayOfTheMonth(this DateTime dateTime)
        {
            return dateTime.Day == 1;
        }

        public static Boolean IsBetween(this DateTime dt, DateTime startDate, DateTime endDate, Boolean compareTime = false)
        {
            return compareTime ?
               dt >= startDate && dt <= endDate :
               dt.Date >= startDate.Date && dt.Date <= endDate.Date;
        }

        #endregion

        #region int
        /// <summary>
        /// if the value of a int is negative , zero will be returned
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int NonNegative(this int val)
        {
            return val < 0 ? 0 : val;
        }

        public static TimeSpan Hours(this int i)
        {
            return new TimeSpan(0, i, 0, 0);
        }

        public static TimeSpan Minutes(this int i)
        {
            return new TimeSpan(0, 0, i, 0);
        }

        public static TimeSpan secondes(this int i)
        {
            return new TimeSpan(0, 0, 0, i);
        }

        #endregion

        #region MailMessage

        public static bool Send(this System.Net.Mail.MailMessage msg)
        {
            try
            {
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                string msrv = System.Configuration.ConfigurationManager.AppSettings["MailServer"];

                smtpClient.Host = msrv;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["MailUN"], System.Configuration.ConfigurationManager.AppSettings["MailPass"]);
                smtpClient.Timeout = 200000;
                smtpClient.Port = System.Configuration.ConfigurationManager.AppSettings["MailPort"].ToInt();
                smtpClient.Send(msg);

            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool SendMail(this System.Net.Mail.MailMessage msg, string serverName, int port, string userName, string password)
        {
            try
            {
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();

                smtpClient.Host = serverName;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
                smtpClient.Timeout = 200000;
                smtpClient.Port = port;
                smtpClient.Send(msg);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region list

        public static IList<T> Clone<T>(this IList<T> listToClone)
        {
            return listToClone.ToList();
        }

        public static string StringJoin<T>(this IList<T> list, string delimiter)
        {
            var temp = list.Select(l => l.ToString()).ToArray();
            return string.Join(delimiter, temp);
        }

        #endregion

        #region files

        public static void CreateFile(this string input)
        {
            if (System.IO.File.Exists(input))
            {
                System.IO.File.Delete(input);
            }

            using (FileStream fs = new FileStream(input, FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("");
                    w.Flush();
                }
            }
        }

        public static void CreateFolder(this string input)
        {
            if (!System.IO.Directory.Exists(input))
            {
                System.IO.Directory.CreateDirectory(input);
            }
        }
        #endregion

        public static string ToDescription(this Enum en) //ext method
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        public static string GetDescription(this  Type EnumerationType, int val)
        {
            // = typeof(en);
            var AllItems = Enum.GetValues(EnumerationType);
            foreach (var CurrentItem in AllItems)
            {
                DescriptionAttribute[] DescAttribute = (DescriptionAttribute[])EnumerationType.GetField(CurrentItem.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                string Description = DescAttribute.Length > 0 ? DescAttribute[0].Description : CurrentItem.ToString();

                if ((int)CurrentItem == val)
                {
                    return Description;
                }
            }

            return "";
        }


        public static Dictionary<int, string> ToDictionary(this Type EnumerationType)
        {
            // Type EnumerationType = en.GetType();
            Dictionary<int, string> RetVal = new Dictionary<int, string>();
            var AllItems = Enum.GetValues(EnumerationType);
            foreach (var CurrentItem in AllItems)
            {
                DescriptionAttribute[] DescAttribute = (DescriptionAttribute[])EnumerationType.GetField(CurrentItem.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                string Description = DescAttribute.Length > 0 ? DescAttribute[0].Description : CurrentItem.ToString();
                RetVal.Add((int)CurrentItem, Description);
            }
            return RetVal;
        }
    }
}
