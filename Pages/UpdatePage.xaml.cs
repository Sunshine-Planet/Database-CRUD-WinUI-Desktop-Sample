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
    public sealed partial class UpdatePage : Page
    {
        private readonly List<string> officerList = new();
        private string selectedPrimaryKey = "";
        private bool lineIsSelected = false;
        private string selectSql = "";
        public UpdatePage()
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
        }
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ActionPage));
        }
        private void KeyWordBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //更换搜索关键词后取消勾选, 且关闭修改
            if(lineIsSelected == true)
            {
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(0) as RadioButton).IsChecked = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(1) as TextBox).IsReadOnly = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(2) as TextBox).IsReadOnly = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(3) as ComboBox).IsEditable = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(4) as CalendarDatePicker).IsEnabled = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(5) as TimePicker).IsEnabled = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(6) as TextBox).IsReadOnly = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(7) as TextBox).IsReadOnly = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(8) as ComboBox).IsEditable = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(9) as TextBox).IsReadOnly = true;
            }

            //初始化主键值
            selectedPrimaryKey = "";
            //初始化列选择
            lineIsSelected = false;

            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedIndex == 0)
            {
                //KeyWordPanel.Children.RemoveAt(1);
                if (KeyWordPanel.Children.Count > 1)
                {
                    //pageTitle.Text = "东西没删掉";
                    for (int i = KeyWordPanel.Children.Count; i > 1; i--)
                    {
                        KeyWordPanel.Children.RemoveAt(i - 1);
                    }
                }
                selectSql = "select * from punishment order by punishment_number";
            }
            else if (comboBox.SelectedIndex == 1)
            {
                if (KeyWordPanel.Children.Count > 1)
                {
                    for (int i = KeyWordPanel.Children.Count; i > 1; i--)
                    {
                        KeyWordPanel.Children.RemoveAt(i - 1);
                    }
                }
                //pageTitle.Text = KeyWordPanel.Children.Count.ToString();
                TextBox textBox = new();
                textBox.Height = 30;
                textBox.Width = 150;
                KeyWordPanel.Children.Add(textBox);

            }
            else if (comboBox.SelectedIndex == 2)
            {
                if (KeyWordPanel.Children.Count > 1)
                {
                    for (int i = KeyWordPanel.Children.Count; i > 1; i--)
                    {
                        KeyWordPanel.Children.RemoveAt(i - 1);
                    }
                }
                TextBox textBox = new();
                textBox.Height = 30;
                textBox.Width = 150;
                KeyWordPanel.Children.Add(textBox);
            }
            else if (comboBox.SelectedIndex == 3)
            {
                if (KeyWordPanel.Children.Count > 1)
                {
                    for (int i = KeyWordPanel.Children.Count; i > 1; i--)
                    {
                        KeyWordPanel.Children.RemoveAt(i - 1);
                    }
                }
                TextBox textBox = new();
                textBox.Height = 30;
                textBox.Width = 150;
                KeyWordPanel.Children.Add(textBox);
            }
            else if (comboBox.SelectedIndex == 4)
            {
                if (KeyWordPanel.Children.Count > 1)
                {
                    for (int i = KeyWordPanel.Children.Count; i > 1; i--)
                    {
                        KeyWordPanel.Children.RemoveAt(i - 1);
                    }
                }
                CalendarDatePicker beginDate = new CalendarDatePicker();
                beginDate.Date = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
                CalendarDatePicker endDate = new CalendarDatePicker();
                endDate.Date = new DateTimeOffset(DateTime.Now);
                TextBlock textBlock = new TextBlock
                {
                    Text = "至",
                    Width = 50,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                KeyWordPanel.Children.Add(beginDate);
                KeyWordPanel.Children.Add(textBlock);
                KeyWordPanel.Children.Add(endDate);
            }

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (lineIsSelected == true)
            {
                string sql = "update punishment set "+
                    "punishment_mastername = '" + ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(1) as TextBox).Text + "', "+
                    "punishment_masterid = '" + ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(2) as TextBox).Text + "', " +
                    "punishment_officer = '" + ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(3) as ComboBox).SelectedItem + "', " +
                    "punishment_date = '" + ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(4) as CalendarDatePicker).Date.Value.ToString("yyyy-MM-dd") + "', " +
                    "punishment_time = '" + ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(5) as TimePicker).Time.ToString() + "', " +
                    "punishment_location = '" + ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(6) as TextBox).Text + "', " +
                    "punishment_reason = '" + ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(7) as TextBox).Text + "', " +
                    "punishment_method = '" + ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(8) as ComboBox).SelectedItem + "', " +
                    "punishment_sign = '" + ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(9) as TextBox).Text + "' " +
                    "where punishment_number = '" + selectedPrimaryKey + "';";
                MySqlConnection mycon = new MySqlConnection(App.constr);
                MySqlCommand mysqlcom = new MySqlCommand(sql, mycon);
                mycon.Open();
                //Console.WriteLine("连接数据库成功！");
                mysqlcom.ExecuteNonQuery();
                mycon.Close();
                Info.Text = "罚单 (编号: " + selectedPrimaryKey + ") 更新成功";
                
                //lineIsSelected = false;
                //showInfo.Content = sql;

            }
            else
            {
                Info.Text = "请选择待更新罚单";
            }
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!selectedPrimaryKey.Equals(""))
            {
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(1) as TextBox).IsReadOnly = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(2) as TextBox).IsReadOnly = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(3) as ComboBox).IsEditable = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(4) as CalendarDatePicker).IsEnabled = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(5) as TimePicker).IsEnabled = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(6) as TextBox).IsReadOnly = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(7) as TextBox).IsReadOnly = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(8) as ComboBox).IsEditable = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(9) as TextBox).IsReadOnly = true;
                selectedPrimaryKey = rb.Content as string;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(1) as TextBox).IsReadOnly = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(2) as TextBox).IsReadOnly = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(3) as ComboBox).IsEditable = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(4) as CalendarDatePicker).IsEnabled = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(5) as TimePicker).IsEnabled = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(6) as TextBox).IsReadOnly = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(7) as TextBox).IsReadOnly = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(8) as ComboBox).IsEditable = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(9) as TextBox).IsReadOnly = false;
            }
            else
            {
                selectedPrimaryKey = rb.Content as string;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(1) as TextBox).IsReadOnly = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(2) as TextBox).IsReadOnly = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(3) as ComboBox).IsEditable = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(4) as CalendarDatePicker).IsEnabled = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(5) as TimePicker).IsEnabled = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(6) as TextBox).IsReadOnly = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(7) as TextBox).IsReadOnly = false;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(8) as ComboBox).IsEditable = true;
                ((InfoBox.FindName(selectedPrimaryKey) as StackPanel).Children.ElementAt(9) as TextBox).IsReadOnly = false;

            }
            lineIsSelected = true;

        }

        private void showInfo_Click(object sender, RoutedEventArgs e)
        {
            Info.Text = "";
            InfoBox.Children.Clear();
            //InfoBox.Children.Remove(showInfo);

            if (KeyWordBox.SelectedIndex == 1)
            {
                if (!string.IsNullOrEmpty((KeyWordPanel.Children.ElementAt(1) as TextBox).Text))
                {
                    //pageTitle.Text = (KeyWordPanel.Children.ElementAt(1) as TextBox).Text.Trim();
                    selectSql = "select * from punishment where punishment_number = '" + (KeyWordPanel.Children.ElementAt(1) as TextBox).Text.Trim() + "';";

                }
                else
                {
                    InfoBox.Children.Clear();
                    TextBlock tb = new TextBlock();
                    tb.Text = "请输入查询内容";
                    InfoBox.Children.Add(tb);
                    return;
                }
            }
            else if (KeyWordBox.SelectedIndex == 2)
            {
                if (!string.IsNullOrEmpty((KeyWordPanel.Children.ElementAt(1) as TextBox).Text))
                {
                    //pageTitle.Text = (KeyWordPanel.Children.ElementAt(1) as TextBox).Text.Trim();
                    selectSql = "select * from punishment where punishment_mastername = '" + (KeyWordPanel.Children.ElementAt(1) as TextBox).Text.Trim() + "' order by punishment_number";

                    //isWirten = true;
                }
                else
                {
                    InfoBox.Children.Clear();
                    TextBlock tb = new TextBlock();
                    tb.Text = "请输入查询内容";
                    InfoBox.Children.Add(tb);
                    return;
                }
            }
            else if (KeyWordBox.SelectedIndex == 3)
            {
                if (!string.IsNullOrEmpty((KeyWordPanel.Children.ElementAt(1) as TextBox).Text))
                {
                    //pageTitle.Text = (KeyWordPanel.Children.ElementAt(1) as TextBox).Text.Trim();
                    selectSql = "select * from punishment where punishment_officer = '" + (KeyWordPanel.Children.ElementAt(1) as TextBox).Text.Trim() + "' order by punishment_number";

                    //isWirten = true;
                }
                else
                {
                    InfoBox.Children.Clear();
                    TextBlock tb = new TextBlock();
                    tb.Text = "请输入查询内容";
                    InfoBox.Children.Add(tb);
                    InfoBox.Children.Clear();
                    return;
                }
            }
            else if (KeyWordBox.SelectedIndex == 4)
            {
                selectSql = "select * from punishment where punishment_date between '" +
                (KeyWordPanel.Children.ElementAt(1) as CalendarDatePicker).Date.Value.ToString("yyyy-MM-dd") + "' and '" +
                (KeyWordPanel.Children.ElementAt(3) as CalendarDatePicker).Date.Value.ToString("yyyy-MM-dd") + "' " +
                "order by punishment_number;";
            }

            {
                StackPanel infoTitlePanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };
                infoTitlePanel.Name = "Title";

                InfoBox.Children.Add(infoTitlePanel);

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
                infoTitlePanel.Children.Add(textBlock1);
                infoTitlePanel.Children.Add(textBlock2);
                infoTitlePanel.Children.Add(textBlock3);
                infoTitlePanel.Children.Add(textBlock4);
                infoTitlePanel.Children.Add(textBlock5);
                infoTitlePanel.Children.Add(textBlock6);
                infoTitlePanel.Children.Add(textBlock7);
                infoTitlePanel.Children.Add(textBlock8);
                infoTitlePanel.Children.Add(textBlock9);
                infoTitlePanel.Children.Add(textBlock10);
            }

            //string result = "";
            MySqlConnection mysqlcon = new MySqlConnection(App.constr);
            MySqlCommand mysqlcom = new MySqlCommand(selectSql, mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                StackPanel stackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };

                stackPanel.Name = mysqlread.GetString(2);
                InfoBox.Children.Add(stackPanel);
                //RadioButtons radioButtons = new RadioButtons();
                //radioButtons.Name = "OnlyOne";
                RadioButton radioButton = new RadioButton
                {
                    GroupName = "OnlyOne",
                    Content = mysqlread.GetString(2),
                    Width = 120
                };
                radioButton.Checked += new RoutedEventHandler(HandleCheck);
                //radioButtons.Items.Add(radioButton);
                TextBox textBox1 = new();
                textBox1.Text = mysqlread.GetString(0);
                textBox1.IsReadOnly = true;
                textBox1.Width = 60;
                TextBox textBox2 = new();
                textBox2.Text = mysqlread.GetString(1);
                textBox2.IsReadOnly = true;
                textBox2.Width = 120;
                ComboBox comboBox3 = new()
                {
                    Width = 100,
                    IsEditable = false
                };
                for (int i = 0; i < officerList.Count; i++)
                {
                    comboBox3.Items.Add(officerList[i].ToString());
                }
                comboBox3.SelectedItem = mysqlread.GetString(3);

                CalendarDatePicker calendarDatePicker4 = new()
                {
                    PlaceholderText = "违章日期",
                    Date = mysqlread.GetDateTime(4),
                    IsEnabled = false
                };
                TimePicker timePicker5 = new()
                {
                    ClockIdentifier = "24HourClock",
                    IsEnabled = false
                };
                timePicker5.Time = mysqlread.GetTimeSpan(5);
                TextBox textBox6 = new();
                textBox6.Text = mysqlread.GetString(6);
                textBox6.IsReadOnly = true;
                textBox6.Width = 100;
                TextBox textBox7 = new();
                textBox7.Text = mysqlread.GetString(7);
                textBox7.IsReadOnly = true;
                textBox7.Width = 120;
                ComboBox comboBox8 = new()
                {
                    Width = 70,
                    IsEditable = false
                };
                for (int i = 1; i <= 7; i++)
                {
                    comboBox8.Items.Add(i.ToString());
                }
                comboBox8.SelectedItem = mysqlread.GetString(8);
                TextBox textBox9 = new();
                textBox9.Text = mysqlread.GetString(9);
                textBox9.IsReadOnly = true;
                textBox9.Width = 100;


                stackPanel.Children.Add(radioButton);
                stackPanel.Children.Add(textBox1);
                stackPanel.Children.Add(textBox2);
                stackPanel.Children.Add(comboBox3);
                stackPanel.Children.Add(calendarDatePicker4);
                stackPanel.Children.Add(timePicker5);
                stackPanel.Children.Add(textBox6);
                stackPanel.Children.Add(textBox7);
                stackPanel.Children.Add(comboBox8);
                stackPanel.Children.Add(textBox9);


            }
            mysqlcon.Close();

        }
    }
}
