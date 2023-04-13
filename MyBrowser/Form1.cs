using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;


namespace MyBrowser
{
    public partial class Form1 : Form
    {
        ChromiumWebBrowser chrom;
        Settings.SettingPar setp;
        string adress;
        public Form1()
        {
            InitializeComponent();
        }
        public void AddHistory(string site)
        {
            if (setp.saveDate)
            {
                if (setp.saveHist)
                {
                    DateTime dateTime = DateTime.UtcNow;
                    File.AppendAllText("browser/history.txt", "\n" + site + "\t" + dateTime.ToString("HH:mm dd.MM.yy"));
                }
                else
                {
                    File.AppendAllText("browser/history.txt", "\n" + site);
                }
            }
        }
        private void update_button_Click(object sender, EventArgs e)
        {

            ChromiumWebBrowser chrome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;

            if (chrome != null && chrome.CanGoBack)
            {
                chrome.Back();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                setp = JsonSerializer.Deserialize<Settings.SettingPar>(File.ReadAllText("browser/settings.json"));
            }
            catch (Exception ex)
            {
                setp = new Settings.SettingPar
                { 
                searchSys = "Yandex",
                startSys = "ya.ru",
                saveHist = true ,
                saveType = "Адрес",
                saveDate = false
                 };
             }
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);

            chrom = new ChromiumWebBrowser("https://" + setp.startSys); // стартовая страница
            chrom.AddressChanged += Chrom_AddressChanged;
            chrom.TitleChanged += Chrom_TitleChanged;

            tabControl1.SelectedTab.Controls.Add(chrom);
        }

        private void Chrom_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                tabControl1.SelectedTab.Text = e.Address;
                textBox1.Text = e.Address;
                adress = e.Address;
            }));
        
    }

        private void Chrom_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {

                tabControl1.SelectedTab.Text = e.Title;
               if(setp.saveType == "Адрес")
                {
                    AddHistory(adress);
                }
                else
                {
                    AddHistory(e.Title);
                }
            }));
        }

      
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TabPage tabPage = new TabPage();
            tabPage.Text = "Новая вкладка";
            ChromiumWebBrowser chrome = new ChromiumWebBrowser("https://" + setp.startSys);
            chrom.AddressChanged += Chrom_AddressChanged;
            chrom.TitleChanged += Chrom_TitleChanged;
            tabPage.Controls.Add(chrome);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectTab(tabControl1.TabCount - 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch( textBox1.Text , @"^http\w*"))
            {
                MessageBox.Show(setp.searchSys);
                ChromiumWebBrowser chrome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;

                if (chrome != null)
                {
                    chrome.Load(textBox1.Text);
                }
            }
            else
            {
                if(setp.searchSys == "Yandex")
                {
                    chrom.Load("https://yandex.ru/search/?text=" + textBox1.Text);
                }
                else if(setp.searchSys == "Google")
                {
                    chrom.Load("https://www.google.ru/search?q" + textBox1.Text);
                }
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ChromiumWebBrowser chrome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;

            if(chrome != null)
            {
                chrome.Reload();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            ChromiumWebBrowser chrome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;

            if (chrome != null  && chrome.CanGoForward)
            {
                chrome.Forward();
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Settings  settings = new Settings();
            settings.Show();
        }
    }
}
