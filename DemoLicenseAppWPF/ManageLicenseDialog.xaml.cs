
using DemoLicenseExample;
using Microsoft.Win32;
using OrbnetLicenseSDK;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DemoLicenseAppWPF
{

    /// <summary>
    /// Interaction logic for ChangePropertiesDialog.xaml
    /// </summary>
    public partial class ChangeLicenseDialog : UserControl
    {
        public DemoLicense License { get; private set; }

        public ChangeLicenseDialog(DemoLicense lic)
        {
            InitializeComponent();
            License = lic;
            PropertyGrid.SelectedObject = License;
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            string pathToFile = "";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = $"{DemoLicense.LicenseFileDescription} files (*{DemoLicense.LicenseFileExtention})|*{DemoLicense.LicenseFileExtention}|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var result = dlg.ShowDialog();
            if (result == true)
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
                        PropertyGrid.SelectedObject = License;
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

        private void Export_Click(object sender, RoutedEventArgs e)
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
            if (result == true)
            {                // Save document
                License.SaveLicense(dlg.FileName);              
            }           
        }
    }
}
