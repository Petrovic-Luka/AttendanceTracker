namespace AttendanceTracker.FormsUI
{
    partial class FrmLogIn
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
            label1 = new Label();
            label2 = new Label();
            txtMaillAdress = new TextBox();
            txtPassword = new TextBox();
            btnLogIn = new Button();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(346, 113);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 0;
            label1.Text = "MailAdress";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(346, 196);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 1;
            label2.Text = "Password";
            // 
            // txtMaillAdress
            // 
            txtMaillAdress.Location = new Point(329, 143);
            txtMaillAdress.Name = "txtMaillAdress";
            txtMaillAdress.Size = new Size(100, 23);
            txtMaillAdress.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(329, 227);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(100, 23);
            txtPassword.TabIndex = 3;
            // 
            // btnLogIn
            // 
            btnLogIn.Location = new Point(346, 299);
            btnLogIn.Name = "btnLogIn";
            btnLogIn.Size = new Size(75, 23);
            btnLogIn.TabIndex = 4;
            btnLogIn.Text = "LogIn";
            btnLogIn.UseVisualStyleBackColor = true;
            btnLogIn.Click += btnLogIn_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(346, 274);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(75, 19);
            checkBox1.TabIndex = 5;
            checkBox1.Text = "Professor";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // FrmLogIn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBox1);
            Controls.Add(btnLogIn);
            Controls.Add(txtPassword);
            Controls.Add(txtMaillAdress);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FrmLogIn";
            Text = "FrmLogIn";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtMaillAdress;
        private TextBox txtPassword;
        private Button btnLogIn;
        private CheckBox checkBox1;
    }
}