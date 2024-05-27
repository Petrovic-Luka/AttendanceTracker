using AttendanceTracker.Domain;
using System.Text;
using System.Text.Json;

namespace AttendanceTracker.FormsUI
{
    public partial class FrmLogIn : Form
    {
        HttpClient client;
        public FrmLogIn()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        private async void btnLogIn_Click(object sender, EventArgs e)
        {

            try
            {
                using StringContent jsonContent = new(JsonSerializer.Serialize(
                    new { mailAdress = txtMaillAdress.Text, password = txtPassword.Text, }),
                    Encoding.UTF8,
                    "application/json");

                if (checkBox1.Checked)
                {
                    using HttpResponseMessage response = await client.PostAsync(
                       "https://localhost:7146/User/professor",
                       jsonContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(await response.Content.ReadAsStringAsync());
                        return;
                    }
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<Professor>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (result.ProfessorId == 0)
                    {
                        FrmAdmin frmAdmin = new FrmAdmin(result, client);
                        frmAdmin.ShowDialog();
                    }
                    else
                    {
                        FrmProfessor frm = new FrmProfessor(result, client);
                        frm.ShowDialog();
                    }
                }
                else
                {

                    using HttpResponseMessage response = await client.PostAsync(
                        "https://localhost:7146/User/student",
                        jsonContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(await response.Content.ReadAsStringAsync());
                        return;
                    }
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<Student>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    FrmStudent form = new FrmStudent(result, client);
                    form.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
