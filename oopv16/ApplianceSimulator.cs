using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace oopv16
{


    public partial class ApplianceSimulator : Form
    {
        Lamp A;
        int ShowSpeed = 0;
        bool Working = false;
        bool PowerOn = true;
        bool FirstPlay = true;
        public string MinResCB1, MaxResCB1, MinResCB2, MaxResCB2;


        public ApplianceSimulator()
        {
            InitializeComponent();
        }

        public void setA(Lamp argA)
        {
            A = argA;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        public void ChangeBrightLvl(int brightlvl)
        {
            pictureBox1.BackColor = Color.FromArgb(brightlvl, 249, 244, 100);
        }
       
        public void PrintToTextBox(string textBoxName, string message)
        {
            Control c = (from Control c1 in this.Controls where c1.Name.Equals("textbox" + textBoxName) select c1).FirstOrDefault();
            if (c != null)
            {
                (c as TextBox).Clear();
                (c as TextBox).AppendText(message);
            }
        }

        public void OpModeMassage()
        {
            MessageBox.Show(
        "Прибор находится в рабочем режиме");
        }

        public void BurnOutMassage()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(10);
                pictureBox1.BackColor = Color.FromArgb(200, 249, 244, 100);
                Application.DoEvents();
                Thread.Sleep(10);
                pictureBox1.BackColor = Color.FromArgb(0, 249, 244, 100);
                Application.DoEvents();
            }
            MessageBox.Show(
        "Прибор перегорел");
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            buttonPowerOn.Focus();
            if (FirstPlay == true)
            {
                A.Init(Convert.ToDouble(textboxOutTem.Text) + 273,
                Convert.ToDouble(textboxVoltage.Text),
                Convert.ToDouble(textboxMinRes.Text),
                Convert.ToDouble(textboxMaxRes.Text),
                ShowSpeed,
                Convert.ToInt32(textboxStep.Text));
                A.Update += Application.DoEvents;
                A.OpMode += OpModeMassage;
                A.BurnOut += BurnOutMassage;
                FirstPlay = false;
            }
            if (Working == false)
            {
                buttonPlay.Text = ("PAUSE");
                A.DoStop = false;
                Working = true;
                A.Start(PrintToTextBox, ChangeBrightLvl);

            }
            else
            {
                buttonPlay.Text = ("PLAY");
                A.DoStop = true;
                Working = false;
            }

        }

        private void buttonPowerOn_Click(object sender, EventArgs e)
        {
            if (PowerOn == false)
            {
                buttonPowerOn.Text = ("Прибор включён");
                PowerOn = true;
                A.PowerOn = true;
            }
            else
            {
                buttonPowerOn.Text = ("Прибор выключен");
                PowerOn = false;
                A.PowerOn = false;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            A.Apply(ShowSpeed, Convert.ToInt32(textboxStep.Text));
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            FirstPlay = true;
            A.Reset(Convert.ToDouble(textboxOutTem.Text) + 273,
                Convert.ToDouble(textboxVoltage.Text),
                Convert.ToDouble(textboxMinRes.Text),
                Convert.ToDouble(textboxMaxRes.Text),
                ShowSpeed,
                Convert.ToInt32(textboxStep.Text),
                PrintToTextBox);
        }

        private void checkBoxx01_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxx01.Checked == true)
            {
                checkBoxx01.Checked = true;
                checkBoxx1.Checked = false;
                checkBoxx10.Checked = false;
                checkBoxx100.Checked = false;
                checkBoxx1k.Checked = false;
                ShowSpeed = -1;
                textboxStep.Text = "10";
            }
        }

        private void checkBoxx1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxx1.Checked == true)
            {
                checkBoxx01.Checked = false;
                checkBoxx1.Checked = true;
                checkBoxx10.Checked = false;
                checkBoxx100.Checked = false;
                checkBoxx1k.Checked = false;
                ShowSpeed = 0;
                textboxStep.Text = "1000";
            }
        }

        private void checkBoxx10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxx10.Checked == true)
            {
                checkBoxx01.Checked = false;
                checkBoxx1.Checked = false;
                checkBoxx10.Checked = true;
                checkBoxx100.Checked = false;
                ShowSpeed = 1;
                textboxStep.Text = "1000";
            }
        }

        private void checkBoxx100_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxx100.Checked == true)
            {
                checkBoxx01.Checked = false;
                checkBoxx1.Checked = false;
                checkBoxx10.Checked = false;
                checkBoxx100.Checked = true;
                checkBoxx1k.Checked = false;
                ShowSpeed = 2;
                textboxStep.Text = "1000";
            }
        }

        private void checkBoxx1k_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxx1k.Checked == true)
            {
                checkBoxx01.Checked = false;
                checkBoxx1.Checked = false;
                checkBoxx10.Checked = false;
                checkBoxx100.Checked = false;
                checkBoxx1k.Checked = true;
                ShowSpeed = 3;
                textboxStep.Text = "1000";
            }
        }


        private void checkBoxL1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxL1.Checked == true)
            {
                checkBoxL1.Checked = true;
                checkBoxL2.Checked = false;
                checkBoxCustom.Checked = false;
                textboxMinRes.Text = MinResCB1;
                textboxMaxRes.Text = MaxResCB1;
                textboxMinRes.ReadOnly = true;
                textboxMaxRes.ReadOnly = true;
            }
        }

        private void checkBoxL2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxL2.Checked == true)
            {
                checkBoxL1.Checked = false;
                checkBoxL2.Checked = true;
                checkBoxCustom.Checked = false;
                textboxMinRes.Text = MinResCB2;
                textboxMaxRes.Text = MaxResCB2;
                textboxMinRes.ReadOnly = true;
                textboxMaxRes.ReadOnly = true;
            }
        }

        private void checkBoxCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCustom.Checked == true)
            {
                checkBoxL1.Checked = false;
                checkBoxL2.Checked = false;
                checkBoxCustom.Checked = true;
                textboxMinRes.ReadOnly = false;
                textboxMaxRes.ReadOnly = false;
            }
        }
    }
}
