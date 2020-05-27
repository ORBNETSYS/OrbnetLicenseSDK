using DemoLicenseExample;
using OrbnetLicenseSDK;
using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoLicenseValidator
{
    public partial class LicenseValidator : Form
    {
        private License License;
        //Must be same as in validator
        private static readonly string _testLicenseClientName = "ORBNET SYSTEMS LTD";             

        public LicenseValidator()
        {
            InitializeComponent();
            //SDK Initializtion. Paste your product key here to activate. 
            Validator.InitLicenseValidator(Guid.Empty.ToString());

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
        }

        private void Button_Load_Click(object sender, EventArgs e)
        {
            string path;
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {                         
                try
                {
                    path = file.FileName;
                    License = DemoLicense.GetLicenseFromFile(path);
                    propertyGrid1.SelectedObject = License;
                  
                    return;
                }
                catch { }    
            }
        }

        private void Button_ValidateLicense_Click(object sender, EventArgs e)
        {
            try
            {
                if (License == null) return;

                Validator.ValidateLicense(License);

                SaveFileDialog savefile = new SaveFileDialog();
                // set a default file name
                savefile.FileName = $"License{DemoLicense.LicenseFileExtention}";
                // set filters - this can be done in properties as well
                savefile.Filter = $"{DemoLicense.LicenseFileDescription} files (*{DemoLicense.LicenseFileExtention})|*{DemoLicense.LicenseFileExtention}|All files (*.*)|*.*";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    License.SaveLicense(savefile.FileName);
                }
                propertyGrid1.SelectedObject = License;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
        }

        /// <summary>
        /// This could be used to create a test license. It would have to be run on the local host where the license would end up so that the Machine Id match.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CreateTestLicense_Click(object sender, EventArgs e)
        {
            var License = new DemoLicense();
            License.ClientName = _testLicenseClientName;
            License.ClientId = Guid.NewGuid().ToString();
            License.DaysAllowed = 30;          
            License.GenerateMachineId();          
            License.Hash = string.Empty;

            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = $"License{DemoLicense.LicenseFileExtention}r";
            // set filters - this can be done in properties as well
            savefile.Filter = $"{DemoLicense.LicenseFileDescription} Request files (*{DemoLicense.LicenseFileExtention}r)|*{DemoLicense.LicenseFileExtention}r|All files (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                License.SaveLicense(savefile.FileName);
            }
        }

        private void button_CheckHash_Click(object sender, EventArgs e)
        {
            if (License == null) return;   

            if (Validator.CheckLicense(License, License.MachineId))
            {
                MessageBox.Show("License hash is VALID");               
            }
            else
            {
                MessageBox.Show("License hash is NOT VALID");
            }
            return;          
        }

        private void button_SetDays_Click(object sender, EventArgs e)
        {
            var t = new TextboxInput();
            t.ShowDialog();
            try
            {
                License.DaysAllowed = int.Parse(t.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var t = new TextboxInput();
            t.ShowDialog();
            try
            {
                License.ClientNumber = long.Parse(t.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }       

        private void Button_DaysLeft_Click(object sender, EventArgs e)
        {
            if (License == null) return;           
            MessageBox.Show(Validator.GetTimeLeftReadableString(License));
        }
    }
}
