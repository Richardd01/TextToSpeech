using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.IO;

namespace text_to_speech
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer reader;
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            reader = new SpeechSynthesizer();
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            textBox1.ScrollBars = ScrollBars.Both;
        }

        //Spreek tekst
        private void button1_Click(object sender, EventArgs e)
        {
            reader.Dispose();
            if (textBox1.Text != "")
            {

                reader = new SpeechSynthesizer();
                reader.SpeakAsync(textBox1.Text);
                label2.Text = "Spreekt";
                button2.Enabled = true;
                button4.Enabled = true;
                reader.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(reader_SpeakCompleted);
            }
            else
            {
                MessageBox.Show("Voer tekst in", "Bericht", MessageBoxButtons.OK);
            }
        }

        void reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            label2.Text = "Met niets bezig";
        }

        //Pauze
        private void button2_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                if (reader.State == SynthesizerState.Speaking)
                {
                    reader.Pause();
                    label2.Text = "Gepauzeerd";
                    button3.Enabled = true;

                }
            }
        }

        //Ga door
        private void button3_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                if (reader.State == SynthesizerState.Paused)
                {
                    reader.Resume();
                    label2.Text = "Spreekt";
                }
                button3.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                reader.Dispose();
                label2.Text = "Met niets bezig";
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text =  File.ReadAllText(openFileDialog1.FileName.ToString());

        }

   

     

     
    }
}
