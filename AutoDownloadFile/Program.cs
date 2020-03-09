using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using Ionic.Zip;
using System.IO;
using System.Diagnostics;

namespace AutoDownloadFile
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Download the Files from AXS server.
            DownloadFile();
            //Run Payment.Axs exe to process files data
            //ProcessData();
        }

        private static void ProcessData()
        {
            try
            {
                Process.Start(@"D:\Amol\MyEnglishSchool\ECOS.BAT.PAYMENT.AXS\bin\Debug\ECOS.BAT.PAYMENT.AXS.exe");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void DownloadFile()
        {
            try
            {
                string FTPFileName = "MYENGLISHSCHOOL_" + DateTime.Now.ToString("yyyyMMdd") + ".csv.zip";
                string MoveFileName = "MYENGLISHSCHOOL_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";

                //Validation to check file process already. If file moved in the backup folder means it processed already...
                if (!File.Exists(@"E:\WWW_DATA\AXS\BACKUP\" + MoveFileName + ""))
                {
                    System.Net.WebClient client = new System.Net.WebClient();
                    client.Credentials = new NetworkCredential("crmecinfoftp1234", "nk40SB12!aldo");
                    client.DownloadFile("ftp://crm.ecinfo.co/wwwroot/UploadedFiles/" + FTPFileName + "", @"E:\WWW_DATA\FTPDownloadedFile\" + FTPFileName + "");
                    //Extract .csv file from .zip
                    using (ZipFile archive = new ZipFile(@"E:\WWW_DATA\FTPDownloadedFile\" + FTPFileName + ""))
                    {
                        archive.Password = "6fe37470a41e8";
                        // archive.Encryption = EncryptionAlgorithm.PkzipWeak; // the default: you might need to select the proper value here
                        // archive.StatusMessageTextWriter = Console.Out;
                        archive.ExtractAll(@"E:\WWW_DATA\FTPDownloadedFile\", ExtractExistingFileAction.Throw);
                    }
                    if (File.Exists(@"E:\WWW_DATA\FTPDownloadedFile\" + MoveFileName + ""))
                    {
                        System.IO.File.Move(@"E:\WWW_DATA\FTPDownloadedFile\" + MoveFileName + "", @"E:\WWW_DATA\AXS\" + MoveFileName + "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}