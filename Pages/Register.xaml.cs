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
    public sealed partial class Register : Page
    {
        public Register()
        {
            this.InitializeComponent();
        }
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Home));
        }
        private void register_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(usernameBox.Text) || string.IsNullOrEmpty(passwordBox.Password) || string.IsNullOrEmpty(repasswordBox.Password))
            {
                text.Text = "请输入用户名或密码";
                return;
            }else if (passwordBox.Password != repasswordBox.Password)
            {
                text.Text = "两次输入密码不相同";
                return ;
            }
            string username = usernameBox.Text;
            string password = passwordBox.Password;
            Random rd = new Random();
            List<string> list = new List<string>();
            string id;
            string M_str_sqlcon = "server=localhost;user id=root;password=password;database=trafficpunishment";
            MySqlConnection mysqlcon = new MySqlConnection(M_str_sqlcon);
            MySqlCommand mysqlcom = new MySqlCommand("select officer_id from officer", mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                list.Add(mysqlread.GetString(0));
            }
            mysqlcon.Close();
            do
            {
                id = rd.Next(100, 1000) + "";
            } while (list.Contains(id));
            //text.Text = "id 没重复" + id;
            string sql = "insert into officer(officer_name, officer_id, officer_pwd) values ('" + username + "','" + id + "','" + password + "')";
            MySqlCommand mysqlcomIns = new MySqlCommand(sql, mysqlcon);
            mysqlcon.Open();
            mysqlcomIns.ExecuteNonQuery();
            mysqlcon.Close();
            this.Frame.Navigate(typeof(Login));
        }
    }
}
