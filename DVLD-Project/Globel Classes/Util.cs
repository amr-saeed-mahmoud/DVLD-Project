using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD_Project
{
    public class Util
    {
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if(!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false; 
                }
            }
            return true;
        }

        public static string ReplaceFileNameWithGuid(string FileSource)
        {
            string FileName = FileSource;
            FileInfo file = new FileInfo(FileName);
            string Ex = file.Extension;
            return GenerateGuid() + Ex;
        }

        public static bool CopyFileToImageFolder(ref string sourceFile)
        {
            string DestinationFolder = @"D:\C# project\DVLD-Project\People Images\";
            if(!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string FileDestination = DestinationFolder+ReplaceFileNameWithGuid(sourceFile);

            try
            {
                File.Copy(sourceFile, FileDestination, true);

            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            sourceFile = FileDestination;
            return true;
        }
    }
}
