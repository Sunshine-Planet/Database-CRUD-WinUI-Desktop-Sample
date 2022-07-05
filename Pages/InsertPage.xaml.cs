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
    public sealed partial class InsertPage : Page
    {
        private readonly List<string> officerList = new();

        public InsertPage()
        {
            this.InitializeComponent();
            MySqlConnection mysqlcon = new MySqlConnection(App.constr);
            MySqlCommand mysqlcom = new MySqlCommand("select officer_name from officer", mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                officerList.Add(mysqlread.GetString(0));
            }
            mysqlcon.Close();

            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            stackPanel.Name = "Title";

            InfoBox.Children.Add(stackPanel);

            //if (firstLine == true)
            //{
            TextBlock textBlock1 = new TextBlock()
            {
                Text = "编号"
            };
            textBlock1.Width = 120;
            TextBlock textBlock2 = new TextBlock()
            {
                Text = "违章人"
            };
            textBlock2.Width = 60;
            TextBlock textBlock3 = new TextBlock()
            {
                Text = "违章人身份证号"
            };
            textBlock3.Width = 130;
            TextBlock textBlock4 = new TextBlock()
            {
                Text = "处理交警"
            };
            textBlock4.Width = 100;
            TextBlock textBlock5 = new TextBlock()
            {
                Text = "违章日期"
            };
            textBlock5.Width = 110;
            TextBlock textBlock6 = new TextBlock()
            {
                Text = "违章时间(小时/分种)"
            };
            textBlock6.Width = 230;
            TextBlock textBlock7 = new TextBlock()
            {
                Text = "违章地点"
            };
            textBlock7.Width = 100;
            TextBlock textBlock8 = new TextBlock()
            {
                Text = "违章记载"
            };
            textBlock8.Width = 120;
            TextBlock textBlock9 = new TextBlock()
            {
                Text = "处罚方式"
            };
            textBlock9.Width = 70;

            TextBlock textBlock10 = new TextBlock()
            {
                Text = "被处罚人签字"
            };
            textBlock10.Width = 100;

            //firstLine = false;
            stackPanel.Children.Add(textBlock1);
            stackPanel.Children.Add(textBlock2);
            stackPanel.Children.Add(textBlock3);
            stackPanel.Children.Add(textBlock4);
            stackPanel.Children.Add(textBlock5);
            stackPanel.Children.Add(textBlock6);
            stackPanel.Children.Add(textBlock7);
            stackPanel.Children.Add(textBlock8);
            stackPanel.Children.Add(textBlock9);
            stackPanel.Children.Add(textBlock10);
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ActionPage));

        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string id = System.Text.RegularExpressions.Regex.Replace(button.Name, @"^Passing:", "");
            //showInfo.Text = InfoBox.Children.Count() + "";
            showInfo.Text = "已添加罚单, 编号: " + id;
            //showInfo.Text = ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(11) as TimePicker).Time.ToString();
            /*
            Info.Text =
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(1) as TextBox).Text + "_" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(2) as TextBox).Text + "_" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(3) as ComboBox).SelectedItem + "_" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(4) as CalendarDatePicker).Date.Value.ToString("yyyy-MM-dd") + "_" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(5) as TimePicker).Time.ToString() + "_" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(6) as TextBox).Text + "_" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(7) as TextBox).Text + "_" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(8) as ComboBox).SelectedItem + "_" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(9) as TextBox).Text;
            */
            string sql = "insert into punishment" +
                "(punishment_mastername, punishment_masterid, punishment_number, punishment_officer, punishment_date, punishment_time, punishment_location, punishment_reason, punishment_method, punishment_sign)" + 
                " values ('" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(1) as TextBox).Text + "', '" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(2) as TextBox).Text + "', '" +
                id + "', '" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(3) as ComboBox).SelectedItem + "', '" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(4) as CalendarDatePicker).Date.Value.ToString("yyyy-MM-dd") + "', '" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(5) as TimePicker).Time.ToString() + "', '" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(6) as TextBox).Text + "', '" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(7) as TextBox).Text + "', '" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(8) as ComboBox).SelectedItem + "', '" +
                ((InfoBox.FindName(id) as StackPanel).Children.ElementAt(9) as TextBox).Text +"');";
            //Info.Text = sql;

            MySqlConnection mysqlcon = new(App.constr);
            MySqlCommand mysqlcom = new MySqlCommand(sql, mysqlcon);
            mysqlcon.Open();
            mysqlcom.ExecuteNonQuery();
            mysqlcon.Close();
            InfoBox.Children.Remove(InfoBox.FindName(id) as StackPanel);//获取了数据之后再删, 不然 StackPanel 返回 null , 你说这怪谁, 你自己先把他删掉的
        }
        private void GenerateIdButton_Click(object sender, RoutedEventArgs e)
        {
            string id;
            Random rd = new();
            List<string> list = new List<string>();
            MySqlConnection mysqlcon = new(App.constr);
            MySqlCommand mysqlcom = new("select punishment_number from punishment", mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                list.Add(mysqlread.GetString(0));
            }
            //showInfo.Text = id;
            mysqlcon.Close();
            do
            {
                //id = "TZ11111";
                id = "TZ" + rd.Next(10000, 100000);
            } while (list.Contains(id));
            //showInfo.Text = "id 不重复" + id;


            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            stackPanel.Name = id;
            //showInfo.Text = stackPanel.Name;
            InfoBox.Children.Add(stackPanel);

            TextBlock textBlock = new();
            textBlock.Width = 120;
            textBlock.Text = id;
            TextBox textBox1 = new();
            textBox1.Width = 60;
            TextBox textBox2 = new();
            textBox2.Width = 120;
            ComboBox comboBox3 = new()
            {
                Width = 100
            };
            for (int i = 0; i < officerList.Count; i++)
            {
                comboBox3.Items.Add(officerList[i].ToString());
            }
            comboBox3.SelectedIndex = 0;
            //CalendarDatePicker calendarDatePicker9 = new CalendarDatePicker();
            //calendarDatePicker9.PlaceholderText = "生产日期";
            //calendarDatePicker9.Date = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            CalendarDatePicker calendarDatePicker4 = new()
            {
                PlaceholderText = "违章日期",
                Date = new DateTimeOffset(DateTime.Now)
            };
            TimePicker timePicker5 = new()
            {
                ClockIdentifier = "24HourClock"
            };
            TimeSpan nowTime = new();
            nowTime = DateTime.Now.TimeOfDay;
            timePicker5.Time = nowTime;
            TextBox textBox6 = new();
            textBox6.Width = 100;
            TextBox textBox7 = new();
            textBox7.Width = 120;
            ComboBox comboBox8 = new();
            comboBox8.Width = 70;
            for(int i = 1; i <= 7; i++)
            {
                comboBox8.Items.Add(i.ToString());
            }
            comboBox8.SelectedIndex = 0;
            TextBox textBox9 = new();
            textBox9.Width = 100;


            stackPanel.Children.Add(textBlock);
            stackPanel.Children.Add(textBox1);
            stackPanel.Children.Add(textBox2);
            stackPanel.Children.Add(comboBox3);
            stackPanel.Children.Add(calendarDatePicker4);
            stackPanel.Children.Add(timePicker5);
            stackPanel.Children.Add(textBox6);
            stackPanel.Children.Add(textBox7);
            stackPanel.Children.Add(comboBox8);
            stackPanel.Children.Add(textBox9);

            Button button = new Button
            {
                Name = "Passing:" + id,
                Margin = new Thickness(50, 0, 0, 0)
            };
            button.Click += new RoutedEventHandler(Insert_Click);
            button.Content = "提交";
            //button.HorizontalAlignment = HorizontalAlignment.Center;
            //button.VerticalAlignment = VerticalAlignment.Bottom;
            stackPanel.Children.Add(button);
        }
    }
}
