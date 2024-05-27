using AttendanceTracker.Domain;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace AttendanceTracker.FormsUI
{
    public partial class FrmProfessor : Form
    {
        private Professor professor;
        HttpClient client;
        private BindingList<Subject> subjects;
        private BindingList<ClassRoom> classrooms;

        public FrmProfessor(Professor prof, HttpClient httpClient)
        {
            InitializeComponent();
            professor = prof;
            client = httpClient;
        }

        private async void FrmProfessor_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadClassRooms();
                await LoadSubjects();
                cmbSubjects.DataSource = subjects;
                cmbSubjects.DisplayMember = "Name";
                cmbClassrooms.DataSource = classrooms;
                cmbClassrooms.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadClassRooms()
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync(
                       "https://localhost:7146/Classroom");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(await response.Content.ReadAsStringAsync());
                    return;
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<ClassRoom>>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                classrooms = new BindingList<ClassRoom>(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadSubjects()
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync(
                        "https://localhost:7146/Subject");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(await response.Content.ReadAsStringAsync());
                    return;
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<Subject>>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                subjects = new BindingList<Subject>(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnAddLesson_Click(object sender, EventArgs e)
        {
            try
            {
                var subject = (Subject)cmbSubjects.SelectedItem;
                var classRoom = (ClassRoom)cmbClassrooms.SelectedItem;

                using StringContent jsonContent = new(JsonSerializer.Serialize(
             new {subjectId=subject.SubjectId,classRoomId=classRoom.ClassRoomId,professorId=professor.ProfessorId,time=System.DateTime.Now }),
             Encoding.UTF8,
             "application/json");

                using HttpResponseMessage response = await client.PostAsync(
                "https://localhost:7146/Lesson",
                jsonContent);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(await response.Content.ReadAsStringAsync());
                    return;
                }
                txtCode.Text = await response.Content.ReadAsStringAsync();
                txtCode.Visible = true;
                MessageBox.Show("Lesson created");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
