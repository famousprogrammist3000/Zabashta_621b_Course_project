using Surname_Group_Course_project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zabashta_Group_Course_project
{

    public partial class Form1 : Form
    {
        private bool Mode; // Режим дозволу / заборони введення даних
        private MajorWork MajorObject; 
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tClock_Tick(object sender, EventArgs e)
        {
            tClock.Stop();
            MessageBox.Show("Минуло 25 секунд", "Увага");
            tClock.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            About A = new About(); // створення форми About
            A.tAbout.Start();
            A.ShowDialog(); // відображення діалогового вікна About
            MajorObject = new MajorWork();
            MajorObject.SetTime();
            this.Mode = true;
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            if (Mode)
            {
                tbInput.Enabled = true;
                tClock.Start();
                bStart.Text = "Стоп"; // зміна тексту на кнопці на "Стоп"
                this.Mode = false;
            }
            else
            {
                tbInput.Enabled = false;
                tClock.Stop();
                bStart.Text = "Пуск";// зміна тексту на кнопці на "Пуск"
                this.Mode = true;
                MajorObject.Write(tbInput.Text);// Запис даних у об'єкт
                MajorObject.Task();// Обробка даних
                label1.Text = MajorObject.Read();// Відображення результату
            }
        }


        private void tbInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            tClock.Stop();
            tClock.Start();
            if ((e.KeyChar >= '0') & (e.KeyChar <= '9') | (e.KeyChar == (char)8))
            {
                return;
            }
            else
            {
                tClock.Stop();
                MessageBox.Show("Неправильний символ", "Помилка");
                tClock.Start();
                e.KeyChar = (char)0;
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string s;
            s = (System.DateTime.Now - MajorObject.GetTime()).ToString();
            MessageBox.Show(s, "Час роботи програми");
        }
    }
}
