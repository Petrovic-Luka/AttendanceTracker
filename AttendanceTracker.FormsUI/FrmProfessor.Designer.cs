namespace AttendanceTracker.FormsUI
{
    partial class FrmProfessor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblName = new Label();
            cmbSubjects = new ComboBox();
            cmbClassrooms = new ComboBox();
            lblSubject = new Label();
            lblClassroom = new Label();
            btnAddLesson = new Button();
            lblTitle = new Label();
            txtCode = new TextBox();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(49, 41);
            lblName.Name = "lblName";
            lblName.Size = new Size(91, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Professor Name";
            // 
            // cmbSubjects
            // 
            cmbSubjects.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSubjects.FormattingEnabled = true;
            cmbSubjects.Location = new Point(49, 106);
            cmbSubjects.Name = "cmbSubjects";
            cmbSubjects.Size = new Size(121, 23);
            cmbSubjects.TabIndex = 1;
            // 
            // cmbClassrooms
            // 
            cmbClassrooms.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClassrooms.FormattingEnabled = true;
            cmbClassrooms.Location = new Point(197, 106);
            cmbClassrooms.Name = "cmbClassrooms";
            cmbClassrooms.Size = new Size(121, 23);
            cmbClassrooms.TabIndex = 2;
            // 
            // lblSubject
            // 
            lblSubject.AutoSize = true;
            lblSubject.Location = new Point(49, 79);
            lblSubject.Name = "lblSubject";
            lblSubject.Size = new Size(46, 15);
            lblSubject.TabIndex = 3;
            lblSubject.Text = "Subject";
            // 
            // lblClassroom
            // 
            lblClassroom.AutoSize = true;
            lblClassroom.Location = new Point(197, 79);
            lblClassroom.Name = "lblClassroom";
            lblClassroom.Size = new Size(63, 15);
            lblClassroom.TabIndex = 4;
            lblClassroom.Text = "Classroom";
            // 
            // btnAddLesson
            // 
            btnAddLesson.Location = new Point(49, 160);
            btnAddLesson.Name = "btnAddLesson";
            btnAddLesson.Size = new Size(112, 23);
            btnAddLesson.TabIndex = 5;
            btnAddLesson.Text = "Add Lesson";
            btnAddLesson.UseVisualStyleBackColor = true;
            btnAddLesson.Click += btnAddLesson_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(49, 222);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(72, 15);
            lblTitle.TabIndex = 6;
            lblTitle.Text = "Lesson code";
            // 
            // txtCode
            // 
            txtCode.BorderStyle = BorderStyle.None;
            txtCode.Location = new Point(49, 255);
            txtCode.Name = "txtCode";
            txtCode.ReadOnly = true;
            txtCode.Size = new Size(250, 16);
            txtCode.TabIndex = 8;
            txtCode.Visible = false;
            // 
            // FrmProfessor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtCode);
            Controls.Add(lblTitle);
            Controls.Add(btnAddLesson);
            Controls.Add(lblClassroom);
            Controls.Add(lblSubject);
            Controls.Add(cmbClassrooms);
            Controls.Add(cmbSubjects);
            Controls.Add(lblName);
            Name = "FrmProfessor";
            Text = "FrmProfessor";
            Load += FrmProfessor_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private ComboBox cmbSubjects;
        private ComboBox cmbClassrooms;
        private Label lblSubject;
        private Label lblClassroom;
        private Button btnAddLesson;
        private Label lblTitle;
        private TextBox txtCode;
    }
}