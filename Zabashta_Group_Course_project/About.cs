﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zabashta_Group_Course_project
{
    public partial class About : Form
    {

        public About()
        {
            InitializeComponent();
        }

        private void tAbout_Tick(object sender, EventArgs e)
        {

        }

        private void About_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 1;
                Application.DoEvents();
            }
            timer1.Start();
            if (progressBar1.Value >= 100)
            {
                Application.DoEvents();
                await Task.Delay(1000);
                Close();
            }
        }
    }
}

  

