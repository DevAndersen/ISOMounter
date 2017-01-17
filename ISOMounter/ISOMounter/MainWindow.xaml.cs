using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace ISOMounter
{
    public partial class MainWindow : Window
    {
        public string selectedFile = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ISO Files (*.iso)|*.iso|All Files (*.*)|*.*";
            try
            {
                if (ofd.ShowDialog() == true)
                {
                    lblFile.Content = "Selected ISO: " + ofd.FileName;
                    selectedFile = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMount_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFile.Length == 0)
            {
                MessageBox.Show("Please select an ISO file to mount first.", "No file selected", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            try
            {
                using (RunspaceInvoke invoker = new RunspaceInvoke())
                {
                    invoker.Invoke("mount-diskimage -imagepath \"" + selectedFile + "\"");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDismount_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFile.Length == 0)
            {
                MessageBox.Show("Please select an ISO file to dismount first.", "No file selected", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            try
            {
                using (RunspaceInvoke invoker = new RunspaceInvoke())
                {
                    invoker.Invoke("dismount-diskimage -imagepath \"" + selectedFile + "\"");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
