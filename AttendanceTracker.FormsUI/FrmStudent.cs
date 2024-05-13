using AttendanceTracker.Domain;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace AttendanceTracker.FormsUI
{
    public partial class FrmStudent : Form
    {
        private Student student;
        HttpClient client;

        public FrmStudent(Student student, HttpClient client)
        {
            InitializeComponent();
            this.student = student;
            this.client = client;
        }

        private void FrmStudent_Load(object sender, EventArgs e)
        {
            lblName.Text = student.FullName;
        }

        private async void btnAttendance_Click(object sender, EventArgs e)
        {
            try
            {
                var codeText=txtCode.Text;
                Guid code;
                var result = Guid.TryParse(codeText,out code);
                if(result==false)
                {
                    MessageBox.Show("Format was not correct");
                    return;
                }
                using StringContent jsonContent = new(JsonSerializer.Serialize(
               new { lessonId = code, index = student.Index }),
               Encoding.UTF8,
               "application/json");

                using HttpResponseMessage response = await client.PostAsync(
                "https://localhost:7146/Attends",
                jsonContent);

                MessageBox.Show(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
