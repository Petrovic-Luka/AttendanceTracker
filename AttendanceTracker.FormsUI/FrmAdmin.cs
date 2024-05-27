using AttendanceTracker.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AttendanceTracker.FormsUI
{
    public partial class FrmAdmin : Form
    {
        private Professor professor;
        HttpClient client;

        public FrmAdmin(Professor prof, HttpClient httpClient)
        {
            InitializeComponent();
            professor = prof;
            client = httpClient;
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            cmbDbOption.Items.Clear();
            cmbDbOption.Items.Add("JSON");
            cmbDbOption.Items.Add("SQL");
            cmbDbOption.Items.Add("Mongo");
            cmbDbOption.SelectedIndex = 0;
        }

        private async void btnChangeDb_Click(object sender, EventArgs e)
        {
            var temp = cmbDbOption.SelectedItem;

            try
            {
                using HttpResponseMessage response = await client.PostAsync(
                      $"https://localhost:7146/Admin/SetDb?database={(string)temp}", null);
                MessageBox.Show(await response.Content.ReadAsStringAsync());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnSyncDbs_Click(object sender, EventArgs e)
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync(
                      "https://localhost:7146/Admin/SyncDbLessons");
                MessageBox.Show(await response.Content.ReadAsStringAsync());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnSyncSQLJSON_Click(object sender, EventArgs e)
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync(
                      "https://localhost:7146/Admin/UpdateSqlFromJson");
                MessageBox.Show(await response.Content.ReadAsStringAsync());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnSyncMongoJSON_Click(object sender, EventArgs e)
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync(
                      "https://localhost:7146/Admin/UpdateMongoFromJson");
                MessageBox.Show(await response.Content.ReadAsStringAsync());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
