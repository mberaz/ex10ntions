using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E10netions
{
	public static class Utility
    {
        #region split 

        public static List<string> Split(string text, string delimiter)
        {
            return text.Split(delimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static string[] MultiSplit(string text, string delimiter)
        {
            Regex regex = new Regex(delimiter);
            var parts = regex.Split(text);
            return parts;
        }
        public static List<t> SplitToList<t>(string str, string delimiter)
        {
            var charArray = delimiter.ToCharArray();
            var list = str.Split(charArray, StringSplitOptions.RemoveEmptyEntries).Select(s => (t)Convert.ChangeType(s, typeof(t))).ToList();
            return list;
        }

		
        #endregion

        #region enums
        
      public static Dictionary<int, string> ToNamesDictionary<T>()
	{
		Dictionary<int, string> retVal = new Dictionary<int, string>();
		var type = typeof(T);
		foreach (var itemType in Enum.GetValues(type))
		{
			retVal[(int)itemType] = Enum.GetName(type, itemType);
		}

		return retVal;
	}
	public static string GetEnumName<T>(int value)
	{
		return Enum.GetName(typeof(T), value);
	}

	public static Dictionary<int, string> ToDescriptionDictionary<T>()
	{
		Dictionary<int, string> retVal = new Dictionary<int, string>();
		var enumerationType = typeof(T);
		var descriptionType = typeof(DescriptionAttribute);
		foreach (var currentItem in Enum.GetValues(enumerationType))
		{
			var descAttribute = (DescriptionAttribute[])enumerationType.GetField(currentItem.ToString()).GetCustomAttributes(descriptionType, false);
			string description = descAttribute.Length > 0 ? descAttribute[0].Description : currentItem.ToString();
			retVal.Add((int)currentItem, description);
		}
		return retVal;
	}

	public static string GetEnumDescription<T>(int value)
	{
		var enumerationType = typeof(T);
		var currentItem = Enum.GetName(enumerationType, value);
		var descAttribute = (DescriptionAttribute[])enumerationType.GetField(currentItem.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
		return descAttribute.Length > 0 ? descAttribute[0].Description : currentItem.ToString();
	}
		
		 public static T GetEnumFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }
        #endregion

        #region convertion
        
        public static double ConvertMetersToMiles(double meters)
        {
            return (meters / 1609.344);
        }

        #endregion

        #region params parse
      
        public static int? ParseInt(string intString)
		{
			try
			{
				return int.Parse(intString);
			}
			catch
			{
				return null;
			}
		}

		public static long? ParseLong(string longString)
		{
			try
			{
				return long.Parse(longString);
			}
			catch
			{
				return null;
			}
		}

		public static double? ParseDouble(string doubleString)
		{
			try
			{
				return double.Parse(doubleString);
			}
			catch
			{
				return null;
			}
		}

		public static bool? ParseNullableBool(string boolString)
		{
			try
			{
				return bool.Parse(boolString);
			}
			catch
			{
				return null;
			}
		}

		public static bool ParseBool(string boolString)
		{
			if (string.IsNullOrEmpty(boolString))
			{
				return false;
			}
			else
			{
				return true;
			}
		}//(non empty=true, empty =false)

		public static bool ParseRegularBool(string boolString)
		{
			try
			{
				if (string.IsNullOrEmpty(boolString))
				{
					return false;
				}
				else
				{

					return bool.Parse(boolString);
				}
			}
			catch
			{
				return false;
			}

		}// true or false (null =false)

		public static DateTime? ParseDate(string dateString, string dateFormat)
		{
			CultureInfo provider = CultureInfo.InvariantCulture;
			try
			{
				return DateTime.ParseExact(dateString, dateFormat, provider);
			}
			catch
			{
				return null;
			}
		}

		public static DateTime ParseDateExact(string dateString, string dateFormat)
		{
			CultureInfo provider = CultureInfo.InvariantCulture;

			return DateTime.ParseExact(dateString, dateFormat, provider);
		}

        #endregion

        #region date time

        public static DateTime GetStartOfMonth(DateTime date)
        {
            var newDate = new DateTime(date.Year, date.Month, 1);
            return newDate;
        }

        public static DateTime GetEndOfMonth(DateTime date)
		{
			var daysOfTheMonth = DateTime.DaysInMonth(date.Year, date.Month);
			var newDate = new DateTime(date.Year, date.Month, daysOfTheMonth);
			return newDate;
		}

        public static DateTime GetStartOfNextMonth(DateTime date)
        {
            date = date.AddMonths(1);
            DateTime newDate = new DateTime(date.Year, date.Month, 1);
            return newDate;
        }

        public static DateTime FindNextEndDate(int type, DateTime date)
        {
            if (type == 1)
            {
                return date.AddDays(1);
            }
            else if (type == 2)//end of week +7 days
            {
                int dayOfWeek = (int)date.DayOfWeek;
                dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
                var addDays = (7 - dayOfWeek) + 7;
                return date.AddDays(addDays);
            }
            else//end of month,+ 30 or 31 days
            {
                var year = date.Year;
                var month = date.Month;

                var remainingDays = DateTime.DaysInMonth(year, month) - date.Day;

                year = month == 12 ? year + 1 : year;
                month = month == 12 ? month = 1 : month + 1;

                remainingDays = remainingDays + DateTime.DaysInMonth(year, month);

                return date.AddDays(remainingDays);

            }
        }

		public static DateTime GetStartOfWeek(DateTime date)
		{
			//for weeks that start on monday

			int dayOfWeek = (int)date.DayOfWeek;
			dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
			DateTime startOfWeek = date.AddDays(1 - dayOfWeek);

			return new DateTime(startOfWeek.Year, startOfWeek.Month, startOfWeek.Day);
		}
		
		public DateTime EndOfWeek(DateTime userTodayInUtc, int weekStartsOn)
{
	var start = StartOfWeek(userTodayInUtc, weekStartsOn);
	return start.AddDays(7).AddSeconds(-1) ;
}

public DateTime StartOfWeek(DateTime dt, int startOfWeek)
{
	int diff = (int)dt.DayOfWeek - startOfWeek;
	if (diff < 0)
	{
		diff += 7;
	}
	return dt.AddDays(-1 * diff).Date;
}

		public static DateTime GetStartOfWeekSunday(DateTime date)
		{
			int dayOfWeek = (int)date.DayOfWeek;
			DateTime startOfWeek = date.AddDays(-dayOfWeek);
			return new DateTime(startOfWeek.Year, startOfWeek.Month, startOfWeek.Day);
		}

		public static DateTime GetEndOfWeek(DateTime date)
		{
			int dayOfWeek = (int)date.DayOfWeek;

			DateTime EndOfWeek = date.AddDays((7 - dayOfWeek) + 1);//add another day to skip saturday and get to sunday
			return new DateTime(EndOfWeek.Year, EndOfWeek.Month, EndOfWeek.Day);
		}

		public static DateTime GetEndOfWeekSunday(DateTime date)
		{
			int dayOfWeek = (int)date.DayOfWeek;

			DateTime EndOfWeek = date.AddDays(7 - dayOfWeek);
			return new DateTime(EndOfWeek.Year, EndOfWeek.Month, EndOfWeek.Day);
		}

		public static DateTime GetStartOfDay(DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day);
		}

		public static DateTime GetEndOfDay(DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
		}

		private static int NumberOfElapsedMonths(DateTime date1, DateTime date2)
		{
			int months = ((date1.Year - date2.Year) * 12) + date1.Month - date2.Month;
			return months;
		}

        public static DateTime DatePickerToDatetime(string date)
        {
            var dateArray = date.Split(new char[] { '.', '/', '\\' });
            return new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[0]), int.Parse(dateArray[1]));
        }

        public static DateTime SupportChartToDatetime(string date)
        {
            var dateArray = date.Split(new char[] { '.', '/', '\\' });
            return new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[1]), int.Parse(dateArray[0]));
        }

        #endregion

        #region validetion 
        
        public static bool IsEmail(string email)
		{
			const string MatchEmailPattern =
						@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
				 + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
							[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
				 + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
							[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
				 + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


			if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
			else return false;
		}

		public static bool IsPhoneNumber(string number)
		{
			var res = onlyNumbers(number);
			return (res.Length == 10);
		}

		public static string onlyNumbers(string input)
		{
			string numbers = "0123456789";
			string newstr = string.Empty;
			for (int i = 0; i < input.Length; i++)
			{
				if (numbers.Contains(input[i])) newstr += input[i];
			}
			return newstr;
		}

        public static bool IsInteger(String input)
        {
            try
            {
                int.Parse(input);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region files

        public static string UseDirectory(string folderName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "/" + folderName);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            return directoryInfo.FullName + "//";
        }

        public static string CreateFolder(string folderName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            return directoryInfo.FullName + "//";
        }

        public static string GetCSVsParserPath()
        {
            return AppSetting("CSVParser");
        }
        #endregion

        public static string StringForURL(string input)
		{
			if (input.Length > 100) input = input.Substring(0, 100);
			input = input.ToLower();
			string AllowedCharacters = "אבגדהוזחטיכלמנסעפצקרשתןםךףץabcdefghijklmnopqrstuvwxyz1234567890-_";
			string outstr = string.Empty;
			//for (int i = 0; i < AllowedCharacters.Length; i++)
			for (int i = 0; i < input.Length; i++)
			{
				if (AllowedCharacters.IndexOf(input[i]) == -1)
				{
					outstr += "-";
				}
				else
				{
					outstr += input[i];
				}
			}
			outstr = outstr.Replace("--", "-").Replace("--", "-").Trim(new char[] { '-' }).Trim();
			return outstr;
		}

		public static string ShortString(string input, int chars)
		{
			if (input.Length > chars)
			{
				return input.Substring(0, chars - 3) + "...";
			}
			return input;
		}

        public static string AppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

		public static bool SendMail(MailMessage msg)
		{
			try
			{
				System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
				string msrv = AppSetting("MailServer");
				
					smtpClient.Host = msrv;
					smtpClient.UseDefaultCredentials = false;
					smtpClient.Credentials = new System.Net.NetworkCredential(AppSetting("MailUN"), AppSetting("MailPass"));
					smtpClient.Timeout = 200000;
					smtpClient.Port = Convert.ToInt32(AppSetting("MailPort"));
                    smtpClient.Send(msg);
	
			}
			catch
			{ }
			return true;
		}

        public static int RandomNumber(int from, int to)
        {
            Random random = new Random();
            int randomNumber = random.Next(from, to);
            return randomNumber;
        }

        public static string GeneratePassword()
        {
            return Guid.NewGuid().ToString("d").Substring(1, 7);
        }

        public static string GetConfigConnectionString(string contxtName)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[contxtName].ConnectionString;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\"");
            var parts = regex.Split(connectionString);
            connectionString = parts[1];
            return connectionString;
        }
		
		
        public static string PostDataToURL(string szUrl, string szData)
        {
            //Setup the web request  
            string szResult = string.Empty;
            WebRequest Request = WebRequest.Create(szUrl);
            Request.Timeout = 30000;
            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";

            //Set the POST data in a buffer  
            byte[] PostBuffer;
            try
            {
                // replacing " " with "+" according to Http post RPC  
                szData = szData.Replace(" ", "+");
                //Specify the length of the buffer
                PostBuffer = Encoding.UTF8.GetBytes(szData);
                Request.ContentLength = PostBuffer.Length;
                //Open up a request stream  
                Stream RequestStream = Request.GetRequestStream();
                //Write the POST data  
                RequestStream.Write(PostBuffer, 0, PostBuffer.Length);
                //Close the stream  
                RequestStream.Close();
                //Create the Response object  
                WebResponse Response;
                Response = Request.GetResponse();
                //Create the reader for the response  
                StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);
                //Read the response  
                szResult = sr.ReadToEnd();
                //Close the reader, and response  
                sr.Close();
                Response.Close();
                return szResult;
            }
            catch
            {
                return szResult;
            }
        }

        public static void SaveFile(string tempPath, string fileName, Stream InputStream)
        {
            var stream = InputStream;
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            if (System.IO.File.Exists(tempPath + fileName))
            {
                System.IO.File.Delete(tempPath + fileName);
            }
            using (FileStream fileStream = new FileStream(tempPath + fileName, FileMode.Create))
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    fileStream.WriteByte(buffer[i]);
                }

                fileStream.Seek(0, SeekOrigin.Begin);
            }
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static void SaveBmpToJpg(Bitmap image, string path)
        {
            ImageCodecInfo jgpEncoder = Common.GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            image.Save(path, jgpEncoder, myEncoderParameters);
        }
		
		  public static string HTTPGet(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                string data = reader.ReadToEnd();

                reader.Close();
                stream.Close();

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return null;
        }
		
		//using Newtonsoft.Json;
		 public static  t CloneObject<t>(t source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(t);
            }

            return JsonConvert.DeserializeObject<t>(JsonConvert.SerializeObject(source));

        }
    }
}
