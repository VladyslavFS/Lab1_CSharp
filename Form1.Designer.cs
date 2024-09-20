namespace Lab1
{
    partial class Form1
    {
        private Panel panel1;
        private Button button1;
        private TextBox textBox1;
        private Label label1;

        private void InitializeComponent()
        {
            panel1 = new Panel();
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 376);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(675, 395);
            button1.Name = "button1";
            button1.Size = new Size(113, 43);
            button1.TabIndex = 1;
            button1.Text = "Очистити";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 406);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += SetRadius;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(262, 406);
            label1.Name = "label1";
            label1.Size = new Size(107, 15);
            label1.TabIndex = 3;
            label1.Text = "Площа перетину: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 386);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 5;
            label2.Text = "Радіус кола: ";
            // 
            // Form1
            // 
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Coordinate System";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label2;
    }
}
