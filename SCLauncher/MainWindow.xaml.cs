using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
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

using Microsoft.Win32;
using System.Diagnostics;
using System.IO;


namespace SCLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Read App.Config Key/Values
        NameValueCollection appSettings = ConfigurationManager.AppSettings;

        const string userRoot = "HKEY_CURRENT_USER";

        const string SCSMSubkey = @"Software\Microsoft\System Center\2010\Service Manager\Console\User Settings";
        const string SCSMkeyName = userRoot + "\\" + SCSMSubkey;

        const string SCOMSubkey = @"Software\Microsoft\Microsoft Operations Manager\3.0\User Settings";
        const string SCOMkeyName = userRoot + "\\" + SCOMSubkey;

        public MainWindow()
        {
            InitializeComponent();

            // SCSM
            string SCSMEnvDev = Properties.Settings.Default.SCSMDevName; // appSettings["DEV"];
            string SCSMEnvTest = Properties.Settings.Default.SCSMTestName; // appSettings["TEST"];
            string SCSMEnvProd = Properties.Settings.Default.SCSMProdName; // appSettings["PROD"];

            cmbSCSMServer.Items.Add("SCSM_DEV - " + SCSMEnvDev);
            cmbSCSMServer.Items.Add("SCSM_TST - " + SCSMEnvTest);
            cmbSCSMServer.Items.Add("SCSM_PRD - " + SCSMEnvProd);
            cmbSCSMServer.Items.Add("Set Environment at Launch");

            string currentSCSMRegValue = (string)Registry.GetValue(SCSMkeyName, "SDKServiceMachine", SCSMEnvDev);

            // get the current environment
            cmbSCSMServer.SelectedIndex = 1;

            if (currentSCSMRegValue == SCSMEnvDev)
                cmbSCSMServer.SelectedIndex = 0;
            else if (currentSCSMRegValue == SCSMEnvTest)
                cmbSCSMServer.SelectedIndex = 1;
            else if (currentSCSMRegValue == SCSMEnvProd)
                cmbSCSMServer.SelectedIndex = 2;
            else
                cmbSCSMServer.SelectedIndex = 3;

            // SCOM
            string SCOMEnvDev = Properties.Settings.Default.SCOMDevName; // appSettings["DEV"];
            string SCOMEnvTest = Properties.Settings.Default.SCOMTestName; // appSettings["TEST"];
            string SCOMEnvProd = Properties.Settings.Default.SCOMProdName; // appSettings["PROD"];

            cmbSCOMServer.Items.Add("SCOM_DEV - " + SCOMEnvDev);
            cmbSCOMServer.Items.Add("SCOM_TST - " + SCOMEnvTest);
            cmbSCOMServer.Items.Add("SCOM_PRD - " + SCOMEnvProd);
            cmbSCOMServer.Items.Add("Set Environment at Launch");

            string currentSCOMRegValue = (string)Registry.GetValue(SCOMkeyName, "SDKServiceMachine", SCOMEnvDev);

            // get the current environment
            cmbSCOMServer.SelectedIndex = 1;

            if (currentSCOMRegValue == SCOMEnvDev)
                cmbSCOMServer.SelectedIndex = 0;
            else if (currentSCOMRegValue == SCOMEnvTest)
                cmbSCOMServer.SelectedIndex = 1;
            else if (currentSCOMRegValue == SCOMEnvProd)
                cmbSCOMServer.SelectedIndex = 2;
            else
                cmbSCOMServer.SelectedIndex = 3;
        }



        private void btnLaunchSCSM_Click(object sender, RoutedEventArgs e)
        {
            int selectedServer = cmbSCSMServer.SelectedIndex;

            try
            {
                if (selectedServer == -1)
                    MessageBox.Show("Please select an option");
                else
                {

                    string SCSMEnvDev = Properties.Settings.Default.SCSMDevName; // appSettings["DEV"];
                    string SCSMEnvTest = Properties.Settings.Default.SCSMTestName; // appSettings["TEST"];
                    string SCSMEnvProd = Properties.Settings.Default.SCSMProdName; // appSettings["PROD"];

                    switch (selectedServer)
                    {
                        case 0: // DEV - scsm-dev.example.com
                            Registry.SetValue(SCSMkeyName, "SDKServiceMachine", SCSMEnvDev);
                            startSCSM();
                            break;
                        case 1: // TEST - scsm-test.example.com
                            Registry.SetValue(SCSMkeyName, "SDKServiceMachine", SCSMEnvTest);
                            startSCSM();
                            break;
                        case 2: // PROD - scsm-prod.example.com
                            Registry.SetValue(SCSMkeyName, "SDKServiceMachine", SCSMEnvProd);
                            startSCSM();
                            break;
                        case 3: // User Set
                            Registry.SetValue(SCSMkeyName, "SDKServiceMachine", "");
                            startSCSM();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnLaunchSCOM_Click(object sender, RoutedEventArgs e)
        {
            int selectedServer = cmbSCOMServer.SelectedIndex;

            try
            {
                if (selectedServer == -1)
                    MessageBox.Show("Please select an option");
                else
                {

                    string SCOMEnvDev = Properties.Settings.Default.SCOMDevName; // appSettings["DEV"];
                    string SCOMEnvTest = Properties.Settings.Default.SCOMTestName; // appSettings["TEST"];
                    string SCOMEnvProd = Properties.Settings.Default.SCOMProdName; // appSettings["PROD"];

                    switch (selectedServer)
                    {
                        case 0: // DEV - scom-dev.example.com
                            Registry.SetValue(SCOMkeyName, "SDKServiceMachine", SCOMEnvDev);
                            startSCOM();
                            break;
                        case 1: // TEST - scom-test.example.com
                            Registry.SetValue(SCOMkeyName, "SDKServiceMachine", SCOMEnvTest);
                            startSCOM();
                            break;
                        case 2: // PROD - scom-prod.example.com
                            Registry.SetValue(SCOMkeyName, "SDKServiceMachine", SCOMEnvProd);
                            startSCOM();
                            break;
                        case 3: // User Set
                            Registry.SetValue(SCOMkeyName, "SDKServiceMachine", "");
                            startSCOM();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private void startSCSM()
        {

            string programLocation = Properties.Settings.Default.SCSMLocation; // appSettings["ApplicationLocation"];

            Process.Start(programLocation);

            if (chkCloseLauncher.IsChecked == true)
                this.Close();

        }

        private void startSCOM()
        {

            string programLocation1 = Properties.Settings.Default.SCOMLocation1; // appSettings["ApplicationLocation1"];
            string programLocation2 = Properties.Settings.Default.SCOMLocation2; // appSettings["ApplicationLocation2"];

            if (File.Exists(programLocation1))
                Process.Start(programLocation1);
            else
                Process.Start(programLocation2);

            if (chkCloseLauncher.IsChecked == true)
                this.Close();

        }

        private void hypEmailLink_Click(object sender, RoutedEventArgs e)
        {
            // open URL

            Hyperlink source = sender as Hyperlink;

            if (source != null)
            {

                System.Diagnostics.Process.Start(source.NavigateUri.ToString());

            }
        }


    }
}
