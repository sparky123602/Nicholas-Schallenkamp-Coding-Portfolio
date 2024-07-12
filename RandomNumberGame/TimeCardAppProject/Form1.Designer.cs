namespace TimeCardAppProject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            Clock = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            Date = new Label();
            button3 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(144, 184);
            label1.Name = "label1";
            label1.Size = new Size(274, 37);
            label1.TabIndex = 0;
            label1.Text = "Employee First Name:";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 20F);
            label2.Location = new Point(144, 221);
            label2.Name = "label2";
            label2.Size = new Size(279, 37);
            label2.TabIndex = 1;
            label2.Text = "Employee Last Name: ";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 14F);
            textBox1.Location = new Point(439, 191);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(137, 32);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 14F);
            textBox2.Location = new Point(439, 228);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(137, 32);
            textBox2.TabIndex = 3;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 15F);
            button1.Location = new Point(245, 290);
            button1.Name = "button1";
            button1.Size = new Size(116, 44);
            button1.TabIndex = 4;
            button1.Text = "Clock In";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 15F);
            button2.Location = new Point(410, 290);
            button2.Name = "button2";
            button2.Size = new Size(116, 44);
            button2.TabIndex = 5;
            button2.Text = "Clock Out";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Clock
            // 
            Clock.AutoSize = true;
            Clock.Font = new Font("Segoe UI", 30F);
            Clock.Location = new Point(300, 109);
            Clock.Name = "Clock";
            Clock.Size = new Size(199, 54);
            Clock.TabIndex = 6;
            Clock.Text = "HH:mm:ss";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // Date
            // 
            Date.AutoSize = true;
            Date.Font = new Font("Segoe UI", 20F);
            Date.Location = new Point(312, 72);
            Date.Name = "Date";
            Date.Size = new Size(134, 37);
            Date.TabIndex = 7;
            Date.Text = "d, m, yyyy";
            Date.Click += label3_Click;
            // 
            // button3
            // 
            button3.Location = new Point(588, 431);
            button3.Name = "button3";
            button3.Size = new Size(146, 35);
            button3.TabIndex = 8;
            button3.Text = "Go To Employer Page";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(746, 478);
            Controls.Add(button3);
            Controls.Add(Date);
            Controls.Add(Clock);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Button button2;
        private Label Clock;
        private System.Windows.Forms.Timer timer1;
        private Label Date;
        private Button button3;
    }
}
