using Microsoft.Win32;
using OrbnetLicenseSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DemoLicenseExample
{
    [Serializable]
    public sealed class DemoLicense : OrbnetLicenseSDK.License
    {
        //Checks the validity of the license every 5 minutes
        private static System.Timers.Timer _LicenseCheckTimer;

        /// <summary>
        /// The name of your company. This is used to create keys in the registry and files in the AppData folder 
        /// </summary>
        public static readonly string CompanyName = "ORBNET SYSTEMS LTD";
        /// <summary>
        /// The name of your applicaiton. This is used to create keys in the registry and files in the AppData folder 
        /// </summary>
        public static readonly string ApplicationName = "OrbnetLicenseSDK";      
        /// <summary>
        /// The name of your applicaiton. This is used to create keys in the registry and files in the AppData folder 
        /// </summary>
        public static readonly string RegistryKey = "NotTheInstallDateInUnixFormatWinkWink";
        /// <summary>
        /// Used as a description when saving the license.
        /// </summary>
        public static readonly string LicenseFileDescription = "Orbnet License";
        /// <summary>
        /// The license file extension.
        /// </summary>
        public static readonly string LicenseFileExtention = ".orblic";
        /// <summary>
        /// This will store your license in the local applications folder. Check the Debug Output when starting to see the file path. 
        /// </summary>
        public static readonly string LicenseFilePath = $@"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Path.Combine(Path.Combine(CompanyName, ApplicationName)))}\license{LicenseFileExtention}";

        /// <summary>
        /// Signals when the license has been terminated after x days.
        /// </summary>
        public static event LicenseTrialTerminatedArgs LicenseTrialTerminated;       
        public delegate void LicenseTrialTerminatedArgs(string info);
        /// <summary>
        /// Direct these events to your logger.
        /// </summary>
        public static event LogEntryArgs LicenseLogEntry;
        public delegate void LogEntryArgs(string log);

        //Use component models to make all your variables appear easilly in .NET property grids.
        [DisplayName("a. Demo bool value"), Category("2. Extra Variables"), Description("A bool value to use in your license software.")]
        public bool DemoBoolValue { get; set; }
        [DisplayName("a. Demo int value"), Category("2. Extra Variables"), Description("A integer value to use in your license software.")]
        public int DemoIntValue { get; set; }
        [DisplayName("b. Demo float value"), Category("2. Extra Variables"), Description("A float value to use in your license software.")]
        public float DemoFloatValue { get; set; }      

        [DisplayName("a. Demo Enum"), Category("3. Choice Variables")]
        public DemoEnum DemoEnumChoice { get; set; }
        public enum DemoEnum 
        { 
            One,
            Two,
            Three,
            Four,
        }

        [DisplayName("a. Demo List of strings"), Category("4. List Variables")]
        public List<string> DemoListOfStrings { get; set; }       

        #region Registry Access

        /// <summary>
        /// Tries to get the unix timestamp of when the inital license file was created.
        /// Looks in the local machiune registry first, than in the user registry.
        /// If no registry value is found, adds an entry to the registry to store the unix timestamp of the creation of the license.
        /// This is used to make sure the trial does not reset when re-installing the software or deleting the license.
        /// You can hide this information in several locations and cross-check them for added security.
        /// </summary>
        /// <returns></returns>
        public static long GetUnixTimestampOfLicenseCreation() 
        {
            long unixTimestamp = -1;
            try
            {
                var val = ReadFromGlobalRegistry(RegistryKey);
                if (val == null)
                {
                    WriteLog($"Key not found in HKEY_LOCAL_MACHINE, searching in HKEY_CURRENT_USER");
                    val = ReadFromUserRegistry(RegistryKey);
                    if (val == null) 
                    {
                        WriteLog($"Key not found in HKEY_CURRENT_USER either, this is the first creation of the license on this machine (unless the registry keys where deleted)");
                    }
                }
                
                if (val != null)
                {
                    long unix = long.Parse($"{val}");
                    WriteLog($"Registry value found: {unix}");
                    unixTimestamp = unix;
                }
                else 
                {
                    Random random = new Random();                    
                    //No registry values found, lets create one.
                    unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(); //this is a decoy :)
                    //First we try to add the key in the global local machine registry. This required admin rights
                    WriteToGlobalRegistry("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
                    WriteToGlobalRegistry("ApplicationParam", LongRandom(1400000000000, 1600000000000, random));
                    WriteToGlobalRegistry(RegistryKey, unixTimestamp);                
                    //Now we add it in the current user registry if admin rights where not provided
                    WriteToUserRegistry("InstallDate", DateTime.Now.ToString("yyyyMMdd")); //this is a decoy :)
                    WriteToUserRegistry("ApplicationParam", LongRandom(1400000000000, 1600000000000, random));
                    WriteToUserRegistry(RegistryKey, unixTimestamp);
                    WriteLog($"Created new registry keys!");
                }
            }
            catch (Exception ex) 
            {
                WriteLog($"{ex}");
            }
            return unixTimestamp;
        }

        /// <summary>
        /// Write a value to a current user registry key. Automatically tries to create the key if it does not exist.
        /// Does not require admin rights.
        /// </summary>
        /// <param name="registryKey"></param>
        /// <param name="value"></param>
        public static bool WriteToUserRegistry(string registryKey, object value) 
        {           
            string keyPath = $@"Software\{Path.Combine(CompanyName, ApplicationName)}";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath, true) ?? Registry.CurrentUser.CreateSubKey(keyPath))
            {
                if (key == null)
                {
                    WriteLog($"Registry key path not found: {keyPath}");
                    return false;
                }

                try
                {     
                    key.SetValue(registryKey, value);
                    return true;
                }
                catch (Exception e)
                {
                    WriteLog(e.ToString());
                    return false;
                }
                finally
                {
                    if (key != null)
                    {
                        key.Close();
                    }
                }
            }           
        }

        public static object ReadFromUserRegistry(string registryKey) 
        {
            try
            {
                string keyPath = $@"Software\{Path.Combine(CompanyName, ApplicationName)}";
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath, true))
                {
                    if (key == null)
                    {
                        WriteLog($"Registry key path not found: {keyPath}");
                        return null;
                    }

                    try
                    {
                        return key.GetValue(registryKey);
                    }
                    catch (Exception e)
                    {
                        WriteLog(e.ToString());
                        throw e;
                    }
                    finally
                    {
                        if (key != null)
                        {
                            key.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog($"{ex}");
            }
            return null;
        }

        /// <summary>
        /// Requires application to run with Administrator rights!
        /// Write a value to a local machine registry key. Automatically tries to create the key if it does not exist.
        /// </summary>
        public static bool WriteToGlobalRegistry(string registryKey, object value)
        {
            string keyPath = $@"SOFTWARE\{Path.Combine(CompanyName, ApplicationName)}";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true) ?? Registry.CurrentUser.CreateSubKey(keyPath))
            {
                if (key == null)
                {
                    WriteLog($"Registry key path not found: {keyPath}");
                    return false;
                }

                try
                {
                    key.SetValue(registryKey, value);
                    return true;
                }
                catch (Exception e)
                {
                    WriteLog(e.ToString());                   
                }
                finally
                {
                    if (key != null)
                    {
                        key.Close();
                    }
                }
            }
            return false;
        }

        public static object ReadFromGlobalRegistry(string registryKey)
        {
            try
            {
                string keyPath = $@"SOFTWARE\{Path.Combine(CompanyName, ApplicationName)}";
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true))
                {
                    if (key == null)
                    {
                        WriteLog($"Registry key path not found: {keyPath}");
                        return null;
                    }

                    try
                    {
                        return key.GetValue(registryKey);
                    }
                    catch (Exception e)
                    {
                        WriteLog(e.ToString());
                        throw e;
                    }
                    finally
                    {
                        if (key != null)
                        {
                            key.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog($"{ex}");
            }
            return null;
        }

        private static long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        #endregion

        public enum LicenseStatus 
        { 
            NotValid,
            TrialStillActive,
            TrialTerminated,
            ValidNoTimeRestriction,
        }

        /// <summary>
        /// Returns true if the license is valid or if the license still has an ongoing trial period.
        /// </summary>
        /// <returns></returns>
        public LicenseStatus CheckLicense()
        {
            try
            {              

                if (Validator.CheckLicense(this, Validator.GetUniqueMachineId()))
                {
                    if (DaysAllowed == -1)//Unlimited
                    {
                        if (_LicenseCheckTimer != null)
                        {
                            _LicenseCheckTimer.Dispose();
                            _LicenseCheckTimer = null;
                        }
                        WriteLog($"Your license is valid with no time restrictions.");                       
                        return LicenseStatus.ValidNoTimeRestriction;
                    }
                }
                else
                {
                    if (DaysAllowed == -1)//License is not valid so days allowed should not be -1
                    {
                        WriteLog($"Your license is not valid.\n" +
                            $"If you have upgraded to a new version of this software, the old license may not be compatible with the new features.\n" +                            
                            $"Send it to us so we can re-activate your license. Any new features may not be free*");
                        return LicenseStatus.NotValid;
                    }
                }

                //License is valid but days are counted
                var daysLeft = Validator.GetDaysLeft(this);
                if (daysLeft <= 0)
                {
                    WriteLog($"Your trial has expired.");
                    return LicenseStatus.TrialTerminated;
                }
                else
                {
                    WriteLog($"You have {daysLeft} days left until the plugins stop working.");
                    if (_LicenseCheckTimer == null)
                    {
                        _LicenseCheckTimer = new System.Timers.Timer()
                        {
                            Interval = 1000 * 60 * 5, //Check the license every 5 minutes
                            AutoReset = true,
                            Enabled = true,
                        };
                        _LicenseCheckTimer.Elapsed +=  LicenseCheckTimer_Elapsed;
                    }
                    return LicenseStatus.TrialStillActive;
                }
            }
            catch (Exception ex)
            {
                WriteLog($"License error: {ex}");
                return LicenseStatus.NotValid;
            }
        }

        private static void WriteLog(string log)
        {
            Debug.WriteLine(log);
            LicenseLogEntry?.Invoke(log);
        }

        private void LicenseCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int daysLeft = Validator.GetDaysLeft(this);
            if (CheckLicense() == LicenseStatus.TrialTerminated)
            {
                LicenseTrialTerminated?.Invoke($"TRIAL TERMINATED AFTER {daysLeft} DAYS!");
                _LicenseCheckTimer?.Stop();
            }
            WriteLog($"Your trial has {daysLeft} left.");
        }

        public override string Serialize()
        {
            var serializer = new XmlSerializer(typeof(DemoLicense));

            TextWriter writer = new StringWriter();
            serializer.Serialize(writer, this);

            return writer.ToString();
        }

        private static DemoLicense Deserialize(string settings)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(settings)) return null;
                var serializer = new XmlSerializer(typeof(DemoLicense));

                TextReader reader = new StringReader(settings);

                return (DemoLicense)serializer.Deserialize(reader);
            }
            catch
            {
                return null;
            }
        }

        public static DemoLicense GetLicense(string filepath)
        {
            try
            {
                if (!File.Exists(filepath))
                {
                    throw new IOException("File does not exits");
                }
                return Deserialize(Validator.Decrypt(File.ReadAllText(filepath)));
            }
            catch
            {
                return null;
            }
        }
    }
}
