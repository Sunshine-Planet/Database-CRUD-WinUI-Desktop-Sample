
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
    public sealed partial class SelectPage : Page
    {
        private static string selectedPrimaryKey = "";
        private static bool lineIsSelected = false;
        private static string selectSql = "";
        //private static bool isWriten = false;
        public SelectPage()
        {
            this.InitializeComponent();
        }
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ActionPage));
        }
        private void KeyWordBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if(comboBox.SelectedIndex == 0)
            {
                //KeyWordPanel.Children.RemoveAt(1);
                if(KeyWordPanel.Children.Count > 1)
                {
                    //pageTitle.Text = "东西没删掉";
                    for(int i = KeyWordPanel.Children.Count; i > 1; i--)
                    {
                        KeyWordPanel.Children.RemoveAt(i - 1);
                    }
                }
                selectSql = "select * from Ticket order by 编号";
            }else if(comboBox.SelectedIndex == 1)
            {
                if(KeyWordPanel.Children.Count > 1)
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

            }else if(comboBox.SelectedIndex == 2)
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
            else if(comboBox.SelectedIndex == 3)
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(lineIsSelected == true)
            {
                string constr = "Server=localhost; Port=3306; Database=trafficpunishment; Uid=root; Pwd=password;";
                MySqlConnection mycon = new MySqlConnection(constr);
                MySqlCommand mysqlcom = new MySqlCommand("delete from punishment where punishment_number = '"+ selectedPrimaryKey + "';", mycon);
                mycon.Open();
                //Console.WriteLine("连接数据库成功！");
                showInfo.Content = mysqlcom.ExecuteNonQuery();
                mycon.Close();
                lineIsSelected = false;
                //showInfo.Content = App.selectedPrimaryKey;
                InfoBox.Children.Remove(InfoBox.FindName(selectedPrimaryKey) as StackPanel);
            }
        }
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            selectedPrimaryKey = rb.Content as string;
            lineIsSelected = true;
        }

        private void showInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoBox.Children.Clear();
            //InfoBox.Children.Remove(showInfo);

            if (KeyWordBox.SelectedIndex == 1)
            {
                if (!string.IsNullOrEmpty((KeyWordPanel.Children.ElementAt(1) as TextBox).Text))
                {
                    //pageTitle.Text = (KeyWordPanel.Children.ElementAt(1) as TextBox).Text.Trim();
                    selectSql = "select * from Ticket where 编号 = '" + (KeyWordPanel.Children.ElementAt(1) as TextBox).Text.Trim() + "';";

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
            else if (KeyWordBox.SelectedIndex == 2)
            {
                if (!string.IsNullOrEmpty((KeyWordPanel.Children.ElementAt(1) as TextBox).Text))
                {
                    //pageTitle.Text = (KeyWordPanel.Children.ElementAt(1) as TextBox).Text.Trim();
                    selectSql = "select * from Ticket where 违章人 = '"+ (KeyWordPanel.Children.ElementAt(1) as TextBox).Text.Trim() + "' order by 编号";
                    
                    //isWirten = true;
                }
                else { 
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
                    selectSql = "select * from Ticket where 处理交警 = '" + (KeyWordPanel.Children.ElementAt(1) as TextBox).Text.Trim() + "' order by 编号";

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
            else if(KeyWordBox.SelectedIndex == 4)
            {
                selectSql = "select * from ticket where 违章日期 between '" +
                (KeyWordPanel.Children.ElementAt(1) as CalendarDatePicker).Date.Value.ToString("yyyy-MM-dd") + "' and '" +
                (KeyWordPanel.Children.ElementAt(3) as CalendarDatePicker).Date.Value.ToString("yyyy-MM-dd") + "' " +
                "order by 编号;";
            }

            {
                StackPanel infoTitlePanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };
                infoTitlePanel.Name = "Title";
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
                    Text = "驾驶执照号"
                };
                textBlock3.Width = 120;
                TextBlock textBlock4 = new TextBlock()
                {
                    Text = "地址"
                };
                textBlock4.Width = 100;
                TextBlock textBlock5 = new TextBlock()
                {
                    Text = "邮编"
                };
                textBlock5.Width = 80;
                TextBlock textBlock6 = new TextBlock()
                {
                    Text = "电话"
                };
                textBlock6.Width = 110;
                TextBlock textBlock7 = new TextBlock()
                {
                    Text = "机动车牌照号"
                };
                textBlock7.Width = 100;
                TextBlock textBlock8 = new TextBlock()
                {
                    Text = "型号"
                };
                textBlock8.Width = 80;
                TextBlock textBlock9 = new TextBlock()
                {
                    Text = "制造商"
                };
                textBlock9.Width = 80;
                TextBlock textBlock10 = new TextBlock()
                {
                    Text = "生产日期"
                };
                textBlock10.Width = 150;
                TextBlock textBlock11 = new TextBlock()
                {
                    Text = "违章日期"
                };
                textBlock11.Width = 150;
                TextBlock textBlock12 = new TextBlock()
                {
                    Text = "违章时间"
                };
                textBlock12.Width = 100;
                TextBlock textBlock13 = new TextBlock()
                {
                    Text = "违章地点"
                };
                textBlock13.Width = 100;
                TextBlock textBlock14 = new TextBlock()
                {
                    Text = "违章记载"
                };
                textBlock14.Width = 120;
                TextBlock textBlock15 = new TextBlock()
                {
                    Text = "处罚方式"
                };
                textBlock15.Width = 70;
                TextBlock textBlock16 = new TextBlock()
                {
                    Text = "处理交警"
                };
                textBlock16.Width = 70;
                TextBlock textBlock17 = new TextBlock()
                {
                    Text = "交警编号"
                };
                textBlock17.Width = 70;
                TextBlock textBlock18 = new TextBlock()
                {
                    Text = "被处罚人签字"
                };
                textBlock18.Width = 100;

                InfoBox.Children.Add(infoTitlePanel);
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
                infoTitlePanel.Children.Add(textBlock11);
                infoTitlePanel.Children.Add(textBlock12);
                infoTitlePanel.Children.Add(textBlock13);
                infoTitlePanel.Children.Add(textBlock14);
                infoTitlePanel.Children.Add(textBlock15);
                infoTitlePanel.Children.Add(textBlock16);
                infoTitlePanel.Children.Add(textBlock17);
                infoTitlePanel.Children.Add(textBlock18);
            }

            //string result = "";
            string M_str_sqlcon = "server=localhost;user id=root;password=password;database=trafficpunishment";
            MySqlConnection mysqlcon = new MySqlConnection(M_str_sqlcon);
            MySqlCommand mysqlcom = new MySqlCommand(selectSql, mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                StackPanel stackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };

                stackPanel.Name = mysqlread.GetString(0);
                InfoBox.Children.Add(stackPanel);
                //RadioButtons radioButtons = new RadioButtons();
                //radioButtons.Name = "OnlyOne";
                RadioButton radioButton = new RadioButton
                {
                    GroupName = "OnlyOne",
                    Content = mysqlread.GetString(0),
                    Width = 120
                };
                radioButton.Checked += new RoutedEventHandler(HandleCheck);
                //radioButtons.Items.Add(radioButton);
                TextBox textBox1 = new TextBox
                {
                    Text = mysqlread.GetString(1)
                };
                textBox1.Width = 60;
                TextBox textBox2 = new TextBox
                {
                    Text = mysqlread.GetString(2)
                };
                textBox2.Width = 120;
                TextBox textBox3 = new TextBox
                {
                    Text = mysqlread.GetString(3)
                };
                textBox3.Width = 100;
                TextBox textBox4 = new TextBox
                {
                    Text = mysqlread.GetString(4)
                };
                textBox4.Width = 80;
                TextBox textBox5 = new TextBox
                {
                    Text = mysqlread.GetString(5)
                };
                textBox5.Width = 110;
                TextBox textBox6 = new TextBox
                {
                    Text = mysqlread.GetString(6)
                };
                textBox6.Width = 100;
                TextBox textBox7 = new TextBox
                {
                    Text = mysqlread.GetString(7)
                };
                textBox7.Width = 80;
                TextBox textBox8 = new TextBox
                {
                    Text = mysqlread.GetString(8)
                };
                textBox8.Width = 80;
                TextBox textBox9 = new TextBox
                {
                    Text = mysqlread.GetDateTime(9).ToString("d")
                };
                textBox9.Width = 150;
                TextBox textBox10 = new TextBox
                {
                    Text = mysqlread.GetDateTime(10).ToString("d")
                };
                textBox10.Width = 150;
                TextBox textBox11 = new TextBox
                {
                    Text = mysqlread.GetTimeSpan(11) +""
                };
                textBox11.Width = 100;
                TextBox textBox12 = new TextBox
                {
                    Text = mysqlread.GetString(12)
                };
                textBox12.Width = 100;
                TextBox textBox13 = new TextBox
                {
                    Text = mysqlread.GetString(13)
                };
                textBox13.Width = 120;
                TextBox textBox14 = new TextBox
                {
                    Text = mysqlread.GetString(14)
                };
                textBox14.Width = 50;
                TextBox textBox15 = new TextBox
                {
                    Text = mysqlread.GetString(15)
                };
                textBox15.Width = 50;
                TextBox textBox16 = new TextBox
                {
                    Text = mysqlread.GetString(16)
                };
                textBox16.Width = 50;
                TextBox textBox17 = new TextBox
                {
                    Text = mysqlread.GetString(17)
                };
                textBox17.Width = 50;

                stackPanel.Children.Add(radioButton);
                stackPanel.Children.Add(textBox1);
                stackPanel.Children.Add(textBox2);
                stackPanel.Children.Add(textBox3);
                stackPanel.Children.Add(textBox4);
                stackPanel.Children.Add(textBox5);
                stackPanel.Children.Add(textBox6);
                stackPanel.Children.Add(textBox7);
                stackPanel.Children.Add(textBox8);
                stackPanel.Children.Add(textBox9);
                stackPanel.Children.Add(textBox10);
                stackPanel.Children.Add(textBox11);
                stackPanel.Children.Add(textBox12);
                stackPanel.Children.Add(textBox13);
                stackPanel.Children.Add(textBox14);
                stackPanel.Children.Add(textBox15);
                stackPanel.Children.Add(textBox16);
                stackPanel.Children.Add(textBox17);
                

            }
            mysqlcon.Close();
                //TextBlock textBlock = new TextBlock();
                //textBlock.Text = result;
                //textBlock.FontSize = 24;
                //textBlock.FontStyle = Windows.UI.Text.FontStyle.Italic;
                //textBlock.CharacterSpacing = 200;
                //textBlock.Width = 300;
                // Add the TextBox to the visual tree.
                //InfoBox.Children.Add(textBlock);
                //resultBox.Text = result;
            
        }


    }
}
