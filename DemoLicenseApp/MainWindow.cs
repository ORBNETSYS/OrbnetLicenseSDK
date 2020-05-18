using DemoLicenseExample;
using OrbnetLicenseSDK;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace DemoLicenseApp
{
    public partial class MainWindow : Form
    {
        DemoLicense _license;

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

            try
            {
                WriteLog($"Attempting to load license from {DemoLicense.LicenseFilePath}");
                _license = DemoLicense.GetLicense(DemoLicense.LicenseFilePath);

                if (_license != null)
                {
                    var licenseStatus = _license.CheckLicense();
                    if (licenseStatus == DemoLicense.LicenseStatus.ValidNoTimeRestriction)
                    {
                        Lb_TimeLeft.Text = $"LICENSE IS VALID WITH NO TIME RESTRICTIONS";
                        GetTimeleftTimer.Stop();
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
            catch (Exception ex)
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
            GetTimeleftTimer.Stop();
            Lb_TimeLeft.Text = info;
        }

        private void WriteLog(string log)
        {
            RTB_Logs.AppendText(log + Environment.NewLine);
        }

        private void Btn_Create_Click(object sender, EventArgs e)
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
                MessageBox.Show($"Error generating license file!: " + ex.ToString());
            }
        }

        private void Btn_Manage_Click(object sender, EventArgs e)
        {
            try
            {
                var lic = DemoLicense.GetLicense(DemoLicense.LicenseFilePath);
                if (lic == null)
                {
                    MessageBox.Show("Error! No license found. Please create a license first.");
                    return;
                }

                var licenseManager = new LicenseManagerUserControl(lic);
                Form window = new Form
                {
                    Text = "License Manager",
                    TopLevel = true,
                    FormBorderStyle = FormBorderStyle.Fixed3D, //Disables user resizing
                    MaximizeBox = false,
                    MinimizeBox = false,
                    ClientSize = licenseManager.Size //size the form to fit the content
                };
                window.Controls.Add(licenseManager);
                licenseManager.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                window.ShowDialog();

                if (licenseManager.License != null)
                {
                    _license = licenseManager.License;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetTimeleftTimer_Tick(object sender, EventArgs e)
        {
            _license = DemoLicense.GetLicense(DemoLicense.LicenseFilePath);
            if (_license == null)
            {
                Lb_TimeLeft.Text = "PLEASE CREATE A LICENSE FILE";
            }
            else
            {
                Lb_TimeLeft.Text = $"Time left: {Validator.GetTimeLeftReadableString(_license)}";
            }
        }
    }
}
