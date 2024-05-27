namespace AttendanceTracker.FormsUI
{
    partial class FrmAdmin
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
            btnSyncDbs = new Button();
            btnSyncSQLJSON = new Button();
            btnChangeDb = new Button();
            btnSyncMongoJSON = new Button();
            cmbDbOption = new ComboBox();
            SuspendLayout();
            // 
            // btnSyncDbs
            // 
            btnSyncDbs.Location = new Point(55, 45);
            btnSyncDbs.Name = "btnSyncDbs";
            btnSyncDbs.Size = new Size(99, 25);
            btnSyncDbs.TabIndex = 1;
            btnSyncDbs.Text = "SyncDatabases";
            btnSyncDbs.UseVisualStyleBackColor = true;
            btnSyncDbs.Click += btnSyncDbs_Click;
            // 
            // btnSyncSQLJSON
            // 
            btnSyncSQLJSON.Location = new Point(55, 87);
            btnSyncSQLJSON.Name = "btnSyncSQLJSON";
            btnSyncSQLJSON.Size = new Size(157, 23);
            btnSyncSQLJSON.TabIndex = 2;
            btnSyncSQLJSON.Text = "Sync SQL from JSON";
            btnSyncSQLJSON.UseVisualStyleBackColor = true;
            btnSyncSQLJSON.Click += btnSyncSQLJSON_Click;
            // 
            // btnChangeDb
            // 
            btnChangeDb.Location = new Point(55, 169);
            btnChangeDb.Name = "btnChangeDb";
            btnChangeDb.Size = new Size(90, 23);
            btnChangeDb.TabIndex = 3;
            btnChangeDb.Text = "ChangeDb";
            btnChangeDb.UseVisualStyleBackColor = true;
            btnChangeDb.Click += btnChangeDb_Click;
            // 
            // btnSyncMongoJSON
            // 
            btnSyncMongoJSON.Location = new Point(55, 128);
            btnSyncMongoJSON.Name = "btnSyncMongoJSON";
            btnSyncMongoJSON.Size = new Size(157, 23);
            btnSyncMongoJSON.TabIndex = 4;
            btnSyncMongoJSON.Text = "Sync Mongo from JSON";
            btnSyncMongoJSON.UseVisualStyleBackColor = true;
            btnSyncMongoJSON.Click += btnSyncMongoJSON_Click;
            // 
            // cmbDbOption
            // 
            cmbDbOption.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDbOption.FormattingEnabled = true;
            cmbDbOption.Location = new Point(172, 169);
            cmbDbOption.Name = "cmbDbOption";
            cmbDbOption.Size = new Size(121, 23);
            cmbDbOption.TabIndex = 5;
            // 
            // FrmAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cmbDbOption);
            Controls.Add(btnSyncMongoJSON);
            Controls.Add(btnChangeDb);
            Controls.Add(btnSyncSQLJSON);
            Controls.Add(btnSyncDbs);
            Name = "FrmAdmin";
            Text = "FrmAdmin";
            Load += FrmAdmin_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button btnSyncDbs;
        private Button btnSyncSQLJSON;
        private Button btnChangeDb;
        private Button btnSyncMongoJSON;
        private ComboBox cmbDbOption;
    }
}