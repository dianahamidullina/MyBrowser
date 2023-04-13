using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
namespace MyBrowser
{
    public partial class Settings : Form
    {

        public class SettingPar
        {
            public string searchSys { get; set; }

            public string startSys { get; set; }

            public bool saveHist { get; set; }
            public string saveType { get; set; }
            public bool saveDate { get; set; }

        }
        public Settings()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Settings_Load(object sender, EventArgs e)
        {
            string[] hist = File.ReadAllLines("browser/history.txt");
            listBox1.Items.AddRange(hist);
            try
            {
                SettingPar setp = JsonSerializer.Deserialize<SettingPar>(File.ReadAllText("browser/settings.json"));
                comboBox1.Text = setp.searchSys;
                comboBox2.Text = setp.startSys;
                checkBox1.Checked = setp.saveHist;
                comboBox3.Text = setp.saveType;
                checkBox2.Checked = setp.saveDate;
            }
            catch(Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SettingPar settingPar = new SettingPar
            {
                searchSys = comboBox1.Text,
                startSys = comboBox2.Text,
                saveHist = checkBox1.Checked,
                saveType = comboBox3.Text,
                saveDate = checkBox2.Checked,

            };
            string json = JsonSerializer.Serialize(settingPar);
            File.WriteAllText("browser/settings.json", json);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.WriteAllText("browser/history.txt", "");
            listBox1.Items.Clear();
        }
    }
}
