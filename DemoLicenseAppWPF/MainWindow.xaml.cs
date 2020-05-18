using DemoLicenseExample;
using Microsoft.Win32;
using OrbnetLicenseSDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoLicenseAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DemoLicense _license;
        private System.Windows.Threading.DispatcherTimer _getTimeleftTimer = new System.Windows.Threading.DispatcherTimer();
        
        public MainWindow()
        {
            InitializeComponent();

            //SDK Initializtion. Paste your product key here to activate. 
            string key = Guid.Empty.ToString();            
            if (Validator.InitLicenseValidator(key) == Validator.LicenseTier.Invalid)
            {
                WriteLog($"Product key is not valid.");
                return;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //Tier II & Tier III PRODUCT KEYS ONLY
            //
            //Set a different Master Key for each different kind of product license
            //Automatically sets the encryption key and initialization vector using the Master Key
            //
            //Validator.SetMasterKey(Guid.Parse("139f19ff-69cb-4ba6-b5ec-0d846d3685f0"));
            //--------------------------------------------------------------------------------------------------
            //Set a different encryption key and initialization vector if you do not want to use the MasterKey.     
            //
            //Validator.SetEncryptionKey("djdh47fh56dt34tigliserti3er456tz");
            //Validator.SetEncryptionIV("sjf467edrt90nchr");
            ////////////////////////////////////////////////////////////////////////////////////////////////////


            DemoLicense.LicenseTrialTerminated += DemoLicense_LicenseTrialTerminated;
            DemoLicense.LicenseLogEntry += DemoLicense_LicenseLogEntry;

            _getTimeleftTimer.Tick += GetTimeLeftTimer_Tick;
            _getTimeleftTimer.Interval = new TimeSpan(0, 0, 1);
            _getTimeleftTimer.Start();

            try
            {
                WriteLog($"Attempting to load license from {DemoLicense.LicenseFilePath}");
                _license = DemoLicense.GetLicense(DemoLicense.LicenseFilePath);                

                if (_license != null)
                {
                    var licenseStatus = _license.CheckLicense();
                    if (licenseStatus == DemoLicense.LicenseStatus.ValidNoTimeRestriction)
                    {
                        Lb_TimeLeft.Content = $"LICENSE IS VALID WITH NO TIME RESTRICTIONS";
                        _getTimeleftTimer.Stop();
                    }
                    else if (licenseStatus == DemoLicense.LicenseStatus.TrialStillActive)
                    {
                        WriteLog($"Your license has not been validated, you are running a trial of {_license.DaysAllowed} days.");
                    } 
                    else if (licenseStatus == DemoLicense.LicenseStatus.NotValid || licenseStatus == DemoLicense.LicenseStatus.TrialTerminated)
                    {
                        WriteLog($"License needs validation.");
                    }                                    
                }                
            }
            catch(Exception ex)
            {
                WriteLog($"{ex}");
            }            
        }

        private void DemoLicense_LicenseLogEntry(string log)
        {
            WriteLog(log);
        }

        private void DemoLicense_LicenseTrialTerminated(string info)
        {
            WriteLog(info);
            _getTimeleftTimer.Stop();
            Lb_TimeLeft.Content = info;
        }

        private void WriteLog(string log) 
        {
            RTB_Logs.AppendText(log+Environment.NewLine);
        }

        private void GetTimeLeftTimer_Tick(object sender, EventArgs e)
        {
            _license = DemoLicense.GetLicense(DemoLicense.LicenseFilePath);
            if (_license == null)
            {
                Lb_TimeLeft.Content = "PLEASE CREATE A LICENSE FILE";
            }
            else 
            {  
                Lb_TimeLeft.Content = $"Time left: {Validator.GetTimeLeftReadableString(_license)}";                                               
            }
        }

        private void CreateLicense_Click(object sender, RoutedEventArgs e)
        {          
            try
            {
                var lic = DemoLicense.GetLicense(DemoLicense.LicenseFilePath);
                if (lic == null)
                {
                    //Get timestamp from registry to avoid re-starting the trial
                    long unixTimeMillis = DemoLicense.GetUnixTimestampOfLicenseCreation();

                    lic = new DemoLicense()
                    {
                        //ORBNNET License SDK mandatory fields
                        ClientName = "<Enter your company name here>",
                        DaysAllowed = 30,
                        MachineId = Validator.GetUniqueMachineId(),
                        ClientId = Guid.NewGuid().ToString(),
                        ClientNumber = unixTimeMillis,
                        //DemoLicense custom fields
                        //Use these to limit features in your application.
                        DemoBoolValue = true,
                        DemoIntValue = 42,
                        DemoFloatValue = 1.618034f,
                        DemoEnumChoice = DemoLicense.DemoEnum.Three,
                        DemoListOfStrings = new List<string>() {
                                                                "192.168.1.1",
                                                                "192.168.1.2",
                                                                "192.168.1.3",
                                                                },
                    };
                    lic.SaveLicense(DemoLicense.LicenseFilePath);
                    _license = lic;
                    MessageBox.Show($"A 30 day license has been activated! Please enter your company name into the license, export it and get it activated for permanent use. Note: If you are upgrading from an older version your trial will not be reset.");
                }
                else 
                {
                    WriteLog($"License already exists in {DemoLicense.LicenseFilePath}. A new license will only be created if the existing license is deleted.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error generating license file!: " + ex.ToString());
            }            
        }

        private void ManageLicense_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var lic = DemoLicense.GetLicense(DemoLicense.LicenseFilePath);
                if (lic == null)
                {
                    MessageBox.Show("Error! No license found. Please create a license first.");
                    return;
                }
                
                ChangeLicenseDialog diag = new ChangeLicenseDialog(lic);
                var host = new Window();
                host.Title = "License Manager";
                host.Width = 460;
                host.Height = 600;
                host.Content = diag;
                host.ShowDialog();
                if (diag.License != null) 
                {
                    _license = diag.License;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
