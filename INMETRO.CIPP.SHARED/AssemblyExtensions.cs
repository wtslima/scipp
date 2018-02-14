using System;
using System.IO;

namespace System.Reflection
{

    public static class AssemblyExtensions
    {

        public static DateTime BuildDate(this Assembly self)
        {

            string filePath = self.Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            var s = default(Stream);

            try
            {
                s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);

            var date = new DateTime(1970, 1, 1, 0, 0, 0);
            date = date.AddSeconds(secondsSince1970);
            date = date.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(date).Hours);
            return date;

        }

    }

}