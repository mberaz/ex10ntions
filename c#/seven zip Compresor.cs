
using SevenZip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileonStickyBLL
{
    public static class Compresor
    {
        public static void SevenZIP(List<string> files, string output)
        {
            SevenZipExtractor.SetLibraryPath(ConfigurationManager.AppSettings["7ZIPDLL"]);

            SevenZipCompressor.SetLibraryPath(ConfigurationManager.AppSettings["7ZIPDLL"]);

            SevenZipCompressor compressor = new SevenZipCompressor();
            compressor.ArchiveFormat = OutArchiveFormat.SevenZip;
            compressor.CompressionMode = CompressionMode.Create;
            compressor.TempFolderPath = System.IO.Path.GetTempPath();
            //compressor.VolumeSize = 51200000;//50 MB
            compressor.CompressFiles(output, files.ToArray());
        }

    }
}
