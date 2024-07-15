using DVLD_Business;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DVLD_Project
{
    public class Global
    {
        public static User CurUser;

        public static string RegistryPath = @"HKEY_CURRENT_USER\DVLD_Project";

        private string RegistryName = "DVLD_Project";

        public static bool RememberUser(string UserName, string Password)
        {

            try
            {
                string ProjectPath = Directory.GetCurrentDirectory();
                string FilePath = Path.Combine(ProjectPath, "data.txt");

                if (UserName == "" && File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return true;
                }

                string DataToSave = UserName + "#//#" + Password;

                using (StreamWriter Line = new StreamWriter(FilePath))
                {
                    Line.WriteLine(DataToSave);
                    return true;
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool GetStoredCredential(ref string UserName, ref string Password)
        {
            try
            {
                string CurrentDirectoy = Directory.GetCurrentDirectory();
                string FilePath = Path.Combine(CurrentDirectoy, "data.txt");

                if (File.Exists(FilePath))
                {
                    using (StreamReader Reader = new StreamReader(FilePath))
                    {
                        string Line;
                        while ((Line = Reader.ReadLine()) != null)
                        {
                            string[] Result = Line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                            UserName = Result[0];
                            Password = Result[1];
                        }
                        return true;
                    }
                }
                else return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool SaveUserCredentialToRegistry(string UserName, string Password)
        {
            string RegistryName = "User Credential";
            string RegistryValue = UserName + "#//#" + Password;

            if (UserName == "" || Password == "")
            {
                try
                {
                    Registry.SetValue(RegistryPath, RegistryName, "", RegistryValueKind.String);
                }
                catch { return false; }
            }
            else
            {
                try
                {
                    Registry.SetValue(RegistryPath, RegistryName, RegistryValue, RegistryValueKind.String);
                }
                catch { return false; }
            }
            return true;
        }

        public static bool GetStoredCredentialFromRegistry(ref string UserName, ref string Password)
        {
            string RegistryName = "User Credential";
            try
            {

                string Credential = Registry.GetValue(RegistryPath, RegistryName, null) as string;
                if (Credential == null)
                    return false;

                string []Result = Credential.Split(new string[] { "#//#" }, StringSplitOptions.None);

                UserName = Result[0];
                Password = Result[1];
            }
            catch { return false; }
            return true;
        }

    }
}
