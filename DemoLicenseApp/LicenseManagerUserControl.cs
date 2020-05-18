using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using System.IO;
using DemoLicenseExample;
using OrbnetLicenseSDK;

namespace DemoLicenseApp
{
    public partial class LicenseManagerUserControl : UserControl
    {
        public DemoLicense License { get; private set; }
        public LicenseManagerUserControl(DemoLicense license)
        {
            InitializeComponent();
            License = license;
            PropertyGrid_License.SelectedObject = License;
        }

        private void Btn_Export_Click(object sender, EventArgs e)
        {
            if (License == null)
            {
                License = DemoLicense.GetLicense(DemoLicense.LicenseFilePath);
            }

            if (License == null)
            {
                MessageBox.Show("Nothing to export!");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();

            dlg.FileName = $"License{DemoLicense.LicenseFileExtention}";
            dlg.Filter = $"{DemoLicense.LicenseFileDescription} request files (*{DemoLicense.LicenseFileExtention}r)|*{DemoLicense.LicenseFileExtention}r|All files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var result = dlg.ShowDialog();
            // Process save file dialog box results
            if (result == DialogResult.OK)
            {                // Save document
                License.SaveLicense(dlg.FileName);
            }
        }

        private void Btn_Import_Click(object sender, EventArgs e)
        {
            string pathToFile = "";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = $"{DemoLicense.LicenseFileDescription} files (*{DemoLicense.LicenseFileExtention})|*{DemoLicense.LicenseFileExtention}|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                pathToFile = dlg.FileName;
            }
            if (File.Exists(pathToFile))// only executes if the file at pathtofile exists//you need to add the using System.IO reference at the top of te code to use this
            {
                try
                {
                    License = DemoLicense.GetLicense(pathToFile);

                    if (Validator.CheckLicense(License, Validator.GetUniqueMachineId()))
                    {
                        MessageBox.Show("License is valid.");
                        License.SaveLicense(DemoLicense.LicenseFilePath);
                        PropertyGrid_License.SelectedObject = License;
                        MessageBox.Show($"License Activated!\nSaved to: {DemoLicense.LicenseFilePath}");
                    }
                    else
                    {
                        throw new Exception("License not valid.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " Something went wrong, sorry.");
                }
            }

        }
    }
}
