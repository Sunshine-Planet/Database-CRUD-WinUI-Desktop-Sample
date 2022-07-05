using App1.Pages;
using App1.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public LoginInfoViewModels LoginInfo { get; set; }

        public Login()
        {
            this.InitializeComponent();
            LoginInfo = new LoginInfoViewModels();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Home));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //string result = "";
            if (string.IsNullOrEmpty(usernameBox.Text) || string.IsNullOrEmpty(passwordBox.Password))
            {
                text.Text = "请输入用户名或密码";
               // text.Text = LoginInfo.UserName;
                return;
            }
            string username = usernameBox.Text;
            string password = passwordBox.Password;
            MySqlConnection mysqlcon = new MySqlConnection(App.constr);
            MySqlCommand mysqlcom = new MySqlCommand("select officer_name, officer_pwd from officer", mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                if (username == mysqlread.GetString(0) && password == mysqlread.GetString(1))
                {
                    //result += mysqlread.GetString(0) + ":" + mysqlread.GetString(1) + "\n";
                    App.isLogined = true;
                    //text.Text = App.isLogined + ":"+result;
                    this.Frame.Navigate(typeof(ActionPage));
                    break;
                }
            }
            mysqlcon.Close();
            if (!App.isLogined)
            {
                text.Text = "用户名或密码错误";
            }
        }
    }
}
