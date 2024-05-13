namespace AttendanceTracker.FormsUI
{
    partial class FrmStudent
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
            txtCode = new TextBox();
            lblCode = new Label();
            btnAttendance = new Button();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(40, 35);
            lblName.Name = "lblName";
            lblName.Size = new Size(83, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Student Name";
            // 
            // txtCode
            // 
            txtCode.Location = new Point(40, 118);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(282, 23);
            txtCode.TabIndex = 1;
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Location = new Point(40, 89);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(77, 15);
            lblCode.TabIndex = 2;
            lblCode.Text = "Lecture Code";
            // 
            // btnAttendance
            // 
            btnAttendance.Location = new Point(40, 171);
            btnAttendance.Name = "btnAttendance";
            btnAttendance.Size = new Size(116, 23);
            btnAttendance.TabIndex = 3;
            btnAttendance.Text = "Mark Attendance";
            btnAttendance.UseVisualStyleBackColor = true;
            btnAttendance.Click += btnAttendance_Click;
            // 
            // FrmStudent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAttendance);
            Controls.Add(lblCode);
            Controls.Add(txtCode);
            Controls.Add(lblName);
            Name = "FrmStudent";
            Text = "FrmStudent";
            Load += FrmStudent_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private TextBox txtCode;
        private Label lblCode;
        private Button btnAttendance;
    }
}