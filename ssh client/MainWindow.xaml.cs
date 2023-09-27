using Renci.SshNet; //install ssh.net
using System;
using System.Threading.Tasks; 
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace LinuxServerControlApp
{
    public partial class MainWindow : Window
    {
        private const string ServerIp = "";
        private const string Username = "";
        private const string Password = "";
        

        public MainWindow()
        {
            InitializeComponent();
        }
        


        private void ExecuteCommandButton_Click(object sender, RoutedEventArgs e)
        {
            string command = CommandTextBox.Text; // Получаем команду из текстового поля

            using (var client = new SshClient(ServerIp, Username, Password))
            {
                try
                {
                    client.Connect();

                    if (client.IsConnected)
                    {
                        var commandResult = client.RunCommand(command);
                        string output = commandResult.Result;
                        
                        OutputTextBox.Clear();
                        CommandTextBox.Clear();
                        OutputTextBox.Text = output;
                        
                        Console.WriteLine($"Output: {output}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
                finally
                {
                    if (client.IsConnected)
                    {
                        client.Disconnect();
                    }
                }
            }
        }
    }
}