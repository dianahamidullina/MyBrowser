using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace MyBrowser
{
    public partial class Form1 : Form
    {
        ChromiumWebBrowser chrom;
        public Form1()
        {
            InitializeComponent();
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
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);

            chrom = new ChromiumWebBrowser("https://ya.ru"); // стартовая страница
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
            }));
        
    }

        private void Chrom_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                tabControl1.SelectedTab.Text = e.Title;
               
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
            ChromiumWebBrowser chrome = new ChromiumWebBrowser("https://ya.ru");
            chrom.AddressChanged += Chrom_AddressChanged;
            chrom.TitleChanged += Chrom_TitleChanged;
            tabPage.Controls.Add(chrome);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectTab(tabControl1.TabCount - 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser chrome = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;

            if (chrome != null)
            {
                chrome.Load(textBox1.Text);
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
    }
}
