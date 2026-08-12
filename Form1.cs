using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace WinFormApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PlayGreeting();
            DisplayLogo();
        }

        private void PlayGreeting()
        {
            SoundPlayer player = new SoundPlayer("voice/greeting.wav");
            player.Play();
        }

        private void DisplayLogo()
        {
            pictureBox1.Image = Image.FromFile("images/logo.png");
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}