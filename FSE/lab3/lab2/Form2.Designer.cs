namespace lab2
{
    partial class Form2
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
            trackBar1 = new TrackBar();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(12, 27);
            label1.Name = "label1";
            label1.Size = new Size(383, 25);
            label1.TabIndex = 2;
            label1.Text = "Оберіть кількість новин для завантаження";
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(12, 73);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(240, 45);
            trackBar1.TabIndex = 3;
            trackBar1.Value = 2;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button1.Location = new Point(258, 73);
            button1.Name = "button1";
            button1.Size = new Size(131, 35);
            button1.TabIndex = 4;
            button1.Text = "Завантажити";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(412, 130);
            Controls.Add(button1);
            Controls.Add(trackBar1);
            Controls.Add(label1);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TrackBar trackBar1;
        private Button button1;
    }
}