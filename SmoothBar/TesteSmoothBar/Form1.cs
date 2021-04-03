using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteSmoothBar
{
    public partial class Form1 : Form
    {
        private int valor;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fogueteBar.Value < 100)
            {
                fogueteBar.Value++;
            }
            else
            {
                timer1.Enabled = false;
                label1.Visible = true;
                label1.Text = "Foguete lançado com sucesso!";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fogueteBar.Value = 0;
            label1.Visible = false;

            timer1.Interval = 1;
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if (!downloadBar.IsRunning())
            {
                label2.Visible = false;
                valor = 0;
                downloadBar.startRun();
                timer2.Interval = 1;
                timer2.Enabled = true;
            }                
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (valor < 400)
            {
                valor++;
            }
            else
            {
                timer2.Enabled = false;
                downloadBar.stopRun();
                label2.Visible = true;
                label2.Text = "Download finalizado com sucesso!";
            }
        }
    }
}
