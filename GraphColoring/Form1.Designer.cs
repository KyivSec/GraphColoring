namespace GraphColoring
{
    partial class GraphColoringInterface
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
            progressBar1 = new ProgressBar();
            panel1 = new Panel();
            comboBox1 = new ComboBox();
            label1 = new Label();
            button3 = new Button();
            button5 = new Button();
            button4 = new Button();
            button2 = new Button();
            label2 = new Label();
            button1 = new Button();
            menuStrip1 = new MenuStrip();
            fIleToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 379);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(776, 15);
            progressBar1.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(12, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 346);
            panel1.TabIndex = 6;
            panel1.Paint += panel1_Paint;
            panel1.MouseClick += panel1_MouseClick;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Greedy", "Backtrack (MRV)", "Backtrack (Power)" });
            comboBox1.Location = new Point(12, 415);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 28);
            comboBox1.TabIndex = 7;
            comboBox1.Text = "Greedy";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 397);
            label1.Name = "label1";
            label1.Size = new Size(79, 20);
            label1.TabIndex = 8;
            label1.Text = "Algorithm:";
            // 
            // button3
            // 
            button3.Location = new Point(532, 415);
            button3.Name = "button3";
            button3.Size = new Size(125, 23);
            button3.TabIndex = 11;
            button3.Text = "Create Point";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button5
            // 
            button5.Location = new Point(663, 415);
            button5.Name = "button5";
            button5.Size = new Size(125, 23);
            button5.TabIndex = 13;
            button5.Text = "Remove Point";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Location = new Point(139, 415);
            button4.Name = "button4";
            button4.Size = new Size(91, 23);
            button4.TabIndex = 14;
            button4.Text = "Color Graph";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click_1;
            // 
            // button2
            // 
            button2.Location = new Point(401, 415);
            button2.Name = "button2";
            button2.Size = new Size(125, 23);
            button2.TabIndex = 16;
            button2.Text = "Create Connection";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(139, 397);
            label2.Name = "label2";
            label2.Size = new Size(81, 20);
            label2.TabIndex = 17;
            label2.Text = "Status: Idle";
            // 
            // button1
            // 
            button1.Location = new Point(236, 414);
            button1.Name = "button1";
            button1.Size = new Size(91, 23);
            button1.TabIndex = 18;
            button1.Text = "Reset Graph";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ResetGraph;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fIleToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 19;
            menuStrip1.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            fIleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem });
            fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            fIleToolStripMenuItem.Size = new Size(46, 24);
            fIleToolStripMenuItem.Text = "FIle";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(224, 26);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += btnSave_Click;
            // 
            // GraphColoringInterface
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button4);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(panel1);
            Controls.Add(progressBar1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "GraphColoringInterface";
            Text = "Graph Coloring Interface";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ProgressBar progressBar1;
        private Panel panel1;
        private ComboBox comboBox1;
        private Label label1;
        private Button button3;
        private Button button5;
        private Button button4;
        private Button button2;
        private Label label2;
        private Button button1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fIleToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
    }
}
