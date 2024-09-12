namespace Lab1
{
    partial class Form1
    {
        private Panel panel1;
        private Button button1;

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.button1 = new Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 376);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(283, 395);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(228, 43);
            this.button1.TabIndex = 1;
            this.button1.Text = "Очистити точки";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Coordinate Grid App";
            this.ResumeLayout(false);
        }
    }
}
