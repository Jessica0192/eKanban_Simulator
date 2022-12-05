
namespace Workstation_Simulation
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.employeeCombo = new System.Windows.Forms.ComboBox();
            this.workstationCombo = new System.Windows.Forms.ComboBox();
            this.runBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // employeeCombo
            // 
            this.employeeCombo.FormattingEnabled = true;
            this.employeeCombo.Location = new System.Drawing.Point(396, 158);
            this.employeeCombo.Name = "employeeCombo";
            this.employeeCombo.Size = new System.Drawing.Size(237, 23);
            this.employeeCombo.TabIndex = 0;
            this.employeeCombo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.employeeCombo_MouseClick);
            // 
            // workstationCombo
            // 
            this.workstationCombo.FormattingEnabled = true;
            this.workstationCombo.Location = new System.Drawing.Point(396, 195);
            this.workstationCombo.Name = "workstationCombo";
            this.workstationCombo.Size = new System.Drawing.Size(237, 23);
            this.workstationCombo.TabIndex = 1;
            this.workstationCombo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.workstationCombo_MouseClick);
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(396, 236);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(84, 23);
            this.runBtn.TabIndex = 2;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.RunBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(396, 265);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(84, 23);
            this.stopBtn.TabIndex = 3;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(296, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Employee";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Workstation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gulim", 30F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(203, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(553, 50);
            this.label3.TabIndex = 6;
            this.label3.Text = "Workstation Simulator";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(13, 13);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(46, 15);
            this.timeLabel.TabIndex = 7;
            this.timeLabel.Text = "0:0:0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 450);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.workstationCombo);
            this.Controls.Add(this.employeeCombo);
            this.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "Form1";
            this.Text = "Workstation Simulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox employeeCombo;
        private System.Windows.Forms.ComboBox workstationCombo;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label timeLabel;
    }
}

