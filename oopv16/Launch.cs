using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oopv16
{
    public partial class Launch : Form
    {
        Lamp L = new Lamp();
        Heater H = new Heater();

        public Launch()
        {
            InitializeComponent();
        }

        private void Lamp_Click(object sender, EventArgs e)
        {
            ApplianceSimulator Form1 = new ApplianceSimulator();
            Form1.setA(L);
            Form1.Show();
            Form1.pictureBox1.Image = Image.FromFile("lamp.png");
            Form1.pictureBox1.BackColor = Color.FromArgb(0, 249, 244, 100);
            Form1.label2.Text = "Лампа накаливания";
            Form1.MinResCB1 = "95";
            Form1.MaxResCB1 = "1200";
            Form1.MinResCB2 = "65";
            Form1.MaxResCB2 = "805";
            this.Hide();
        }

        private void Heater_Click(object sender, EventArgs e)
        {
            ApplianceSimulator Form2 = new ApplianceSimulator();
            Form2.setA(H);
            Form2.Show();
            Form2.pictureBox1.Image = Image.FromFile("heater.png");
            // L.pictureBox1.BackColor = Color.FromArgb(0, 249, 244, 100);
            Form2.label2.Text = "   Обогреватель   ";
            Form2.label24.Hide();
            Form2.label25.Hide();
            Form2.textboxBrightness.Hide();
            Form2.checkBoxL1.Text = "1000 Вт";
            Form2.checkBoxL2.Text = "800 Вт";
            Form2.label16.Text = "Сопротивление радиатора:";
            Form2.label17.Text = "Ток через радиатор:";
            Form2.label19.Text = "Температура радиатора:";
            Form2.MinResCB1 = "5";
            Form2.MaxResCB1 = "49";
            Form2.MinResCB2 = "18";
            Form2.MaxResCB2 = "61";
            Form2.textboxMinRes.Text = "5";
            Form2.textboxMaxRes.Text = "49";
            this.Hide();
        }
    }
}
