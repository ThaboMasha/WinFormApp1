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
            SoundPlayer player = new SoundPlayer("C:\\Users\\Student\\Downloads\\Absa ATM 2.wav");
            player.Play();
        }

        private void DisplayLogo()
        {
            pictureBox1.Image = Image.FromFile("C:\\Users\\Student\\Pictures\\Screenshots\\image.png"
                );

            //fill the picture to the rectangle
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            //Position the PictureBox at the top-left
            pictureBox1.Location = new Point(0, 0);
        }


        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}