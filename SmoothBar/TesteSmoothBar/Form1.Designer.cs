
namespace TesteSmoothBar
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fogueteBar = new SmoothBar.SmoothBar();
            this.downloadBar = new SmoothBar.SmoothBar();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Lançar foguete";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(138, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Baixar arquivo";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 338);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // fogueteBar
            // 
            this.fogueteBar.ComecaRodando = false;
            this.fogueteBar.Location = new System.Drawing.Point(12, 54);
            this.fogueteBar.MarqueeAnimationSpeed = 50;
            this.fogueteBar.Maximum = 100;
            this.fogueteBar.Minimum = 0;
            this.fogueteBar.Name = "fogueteBar";
            this.fogueteBar.Orientation = OrientationType.Vertical;
            this.fogueteBar.PercentualRadius = 70;
            this.fogueteBar.ProgressBarColor = System.Drawing.Color.DarkOrange;
            this.fogueteBar.ProgressEspiralColor = System.Drawing.Color.WhiteSmoke;
            this.fogueteBar.Shape = ShapeType.Rectangle;
            this.fogueteBar.Size = new System.Drawing.Size(41, 297);
            this.fogueteBar.TabIndex = 9;
            this.fogueteBar.Value = 0;
            // 
            // downloadBar
            // 
            this.downloadBar.ComecaRodando = false;
            this.downloadBar.Location = new System.Drawing.Point(138, 54);
            this.downloadBar.MarqueeAnimationSpeed = 30;
            this.downloadBar.Maximum = 100;
            this.downloadBar.Minimum = 0;
            this.downloadBar.Name = "downloadBar";
            this.downloadBar.PercentualRadius = 70;
            this.downloadBar.ProgressBarColor = System.Drawing.Color.LimeGreen;
            this.downloadBar.ProgressEspiralColor = System.Drawing.Color.WhiteSmoke;
            this.downloadBar.Size = new System.Drawing.Size(200, 200);
            this.downloadBar.Style = StyleType.Marquee;
            this.downloadBar.TabIndex = 8;
            this.downloadBar.Value = 0;
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 363);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fogueteBar);
            this.Controls.Add(this.downloadBar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private SmoothBar.SmoothBar downloadBar;
        private SmoothBar.SmoothBar fogueteBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label2;
    }
}

