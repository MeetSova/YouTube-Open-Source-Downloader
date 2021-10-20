using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace YT_OS_Downloader
{
    public partial class Form1 : Form
    {
        string[] plik;
        List<string> listafilmow = new List<string>();
        string file;
        public Form1()
        {
            InitializeComponent();
            textBox2.Text = Environment.GetEnvironmentVariable("userprofile").ToString()+"\\Downloads";
        }

        private void Download_Click(object sender, EventArgs e)
        {
            string link = textBox1.Text.ToString();
            //MessageBox.Show(textBox2.Text);
            //Console.WriteLine("cd \"" + textBox2.Text + "\"");
            //Console.WriteLine("youtube-dl "+link);
            //string strCmdLine = "/C youtube-dl --output \""+ textBox2.Text + "\"\\%(title)s.%(ext)s "+ link;

            //System.Diagnostics.Process.Start("CMD.exe", strCmdLine);
            string argum = "";
            if (MP3.Checked)
            {
                argum = "-f bestaudio --extract-audio --audio-format mp3 ";
            }
            else if (MP4.Checked)
            {
                argum = "-f \"bestvideo[ext=mp4]+bestaudio[ext=m4a]/mp4\" ";
            }
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //cmd.StartInfo.CreateNoWindow = true;
            //cmd.StartInfo.ArgumentList.Add();
            cmd.StartInfo.Arguments = "/c yt-dlp " + argum + "--output \"" + textBox2.Text + "\\%(title)s.%(ext)s\" " + link;
            //MessageBox.Show("/c youtube-dl " + argum + "--output \"" + textBox2.Text + "\\%(title)s.%(ext)s\" " + link);

            cmd.Start();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Browse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox2.Text = fbd.SelectedPath.ToString();
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            string buf;
            
            if (result == DialogResult.OK) // Test result.
            {
                int sec;
                int min;
                string kg;
                file = openFileDialog1.FileName;
                textBox3.Text = file;
                string text = System.IO.File.ReadAllText(file);
                plik = text.Split(',', '\n', '?');
                int j = 1;
                for(int i=1; i<plik.Length; i += 3)
                {
                    
                    
                    /*
                    if(plik[i].Length == 1)
                    {
                        buf = "000" + plik[i];
                        plik[i] = buf;
                    }
                    else if(plik[i].Length == 2)
                    {
                        buf = "00" + plik[i];
                        plik[i] = buf;
                    }
                    else if (plik[i].Length == 3)
                    {
                        buf = "0" + plik[i];
                        plik[i] = buf;
                    }*/
                    if (i != 1)
                    {
                        richTextBox1.Text += j;
                        richTextBox1.Text += "      |     ";
                        richTextBox1.Text += plik[i - 1];
                        listafilmow.Add(plik[i - 1]);
                        richTextBox1.Text += "      |     ";
                        plik[i] = plik[i].TrimStart('t', '=');
                        plik[i + 1] = plik[i + 1].TrimEnd('\r');
                        double tol1 = (double)numericUpDown1.Value;
                        double secondstostart = double.Parse(plik[i]) - tol1;
                        if (secondstostart < 0) secondstostart = 0;

                        min = int.Parse(plik[i]) / 60;
                        sec = int.Parse(plik[i]) % 60;
                        if (min < 10 && sec < 10) plik[i] = "0"+min.ToString() + ":0" + sec.ToString();
                        else if (min < 10 && sec > 9) plik[i] = "0"+min.ToString() + ":" + sec.ToString();
                        else if (min > 9 && sec < 10) plik[i] = ""+min.ToString() + ":0" + sec.ToString();
                        else plik[i] =  min.ToString() + ":" + sec.ToString();
                        richTextBox1.Text += plik[i];
                        richTextBox1.Text += "      |     ";

                        if (plik[i + 1].Length == 1)
                        {
                            buf = "000" + plik[i + 1]; ;
                            plik[i + 1] = buf;
                        }
                        else if (plik[i + 1].Length == 2)
                        {
                            buf = "00" + plik[i + 1]; ;
                            plik[i + 1] = buf;
                        }
                        else if (plik[i + 1].Length == 3)
                        {
                            buf = "0" + plik[i + 1]; ;
                            plik[i + 1] = buf;
                        }

                        kg = plik[i + 1].Insert(2, ":");
                        plik[i + 1] = kg;
                        richTextBox1.Text += plik[i + 1];
                        richTextBox1.Text += "      |     ";
                        string time = "00:" + plik[i + 1];
                        double secondstoend = TimeSpan.Parse(time).TotalSeconds;
                        double tol2 = (double)numericUpDown2.Value;
                        double timeofmoment = secondstoend - secondstostart + tol2;
                        //richTextBox1.Text += timeofmoment + "\n";
                        

                        min = Convert.ToInt32(timeofmoment) / 60;
                        sec = Convert.ToInt32(timeofmoment) % 60;
                        if (min < 10 && sec < 10) plik[i+1] = "0" + min.ToString() + ":0" + sec.ToString();
                        else if (min < 10 && sec > 9) plik[i+1] = "0" + min.ToString() + ":" + sec.ToString();
                        else if (min > 9 && sec < 10) plik[i+1] = "" + min.ToString() + ":0" + sec.ToString();
                        else plik[i+1] = min.ToString() + ":" + sec.ToString();
                        richTextBox1.Text += plik[i + 1] + "\n";

                        j++;
                        //plik[i + 1] = timeofmoment.ToString();
                    }
                    else
                    {
                        richTextBox1.Text += "nr";
                        richTextBox1.Text += "      |     ";
                        richTextBox1.Text += "Link do momentu";
                        richTextBox1.Text += "      |     ";
                        richTextBox1.Text += "poczatek momentu";
                        richTextBox1.Text += "      |     ";
                        richTextBox1.Text += "koniec momentu";
                        richTextBox1.Text += "      |     ";
                        richTextBox1.Text += "Czas trwania momentu\n";
                    }

                    
                    /*
                    
                    kg = plik[i + 1].Insert(2, ":");
                    plik[i + 1] = kg;
                    richTextBox1.Text += plik[i + 1] + "\n";
                    */
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Wybierz plik aby kontynuować", "Brak pliku", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string[] s = listafilmow.Distinct().ToArray();
                string argum = "-f \"bestvideo[ext=mp4]+bestaudio[ext=m4a]/mp4\" ";
                string[] buf;

                //cmd.StartInfo.CreateNoWindow = true;
                //cmd.StartInfo.ArgumentList.Add();
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "cmd.exe";
                info.RedirectStandardInput = true;
                info.UseShellExecute = false;
                
                p.StartInfo = info;
                p.Start();

                using (StreamWriter sw = p.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        for(int i=0; i<s.Length; i++)
                        {
                            buf = s[i].Split('/');
                            sw.WriteLine("yt-dlp " + argum + "--output \"" + textBox2.Text + "\\" + buf[3] + ".%(ext)s\" " + s[i]);
                        }

                        //sw.WriteLine("mysql -u root -p");
                    }
                }

                /*
                for (int i=0; i<s.Length; i++)
                {
                    //////////////////////
                    Process cmd = new Process();
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    buf = s[i].Split('/');
                    cmd.StartInfo.Arguments = "/c youtube-dl " + argum + "--output \"" + textBox2.Text + "\\" + buf[3] + ".%(ext)s\" " + s[i];
                    
                    //string cos = "/c youtube-dl " + argum + "--output \"" + textBox2.Text + "\\" + buf[3] + ".%(ext)s\" " + s[i];
                    //MessageBox.Show(cos, "fds", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmd.Start();
                    
                    
                    cmd.WaitForExit();
                }*/
                //ffmpeg -ss 00:00:15.00 -i "OUTPUT-OF-FIRST URL" -t 00:00:10.00 -c copy out.mp4
                
                string mpeg;
                /*
                    for (int i = 4; i < plik.Length; i += 3)
                    {
                        Process cmd = new Process();
                        cmd.StartInfo.FileName = "cmd.exe";
                        cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        buf = plik[i - 1].Split('/');
                        cmd.StartInfo.Arguments = "/c " + "ffmpeg -ss 00:" + plik[i] + ".00 -i \"" + textBox2.Text + "\\" + buf[3] + ".mp4\"" + " -t 00:" + plik[i + 1] + ".00 -c copy \"" + textBox2.Text + "\\" + j + ".mp4\"";
                        //mpeg = "/c " + "ffmpeg -ss 00:" + plik[i] + ".00 -i \""+ textBox2.Text + "\\" + buf[3] + ".mp4\"" + " -t 00:" + plik[i + 1] + ".00 -c copy \"" + textBox2.Text + "\\" + j + ".mp4\"";
                        cmd.Start();
                        cmd.WaitForExit();
                        
                    }*/
                //MessageBox.Show("/c youtube-dl " + argum + "--output \"" + textBox2.Text + "\\%(title)s.%(ext)s\" " + link);


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int j = 1;
            string[] buf;

            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;

            p.StartInfo = info;
            p.Start();

            using (StreamWriter sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    for (int i = 4; i < plik.Length; i += 3)
                    {
                        buf = plik[i - 1].Split('/');
                        sw.WriteLine("ffmpeg -ss 00:" + plik[i] + ".00 -i \"" + textBox2.Text + "\\" + buf[3] + ".mp4\"" + " -t 00:" + plik[i + 1] + ".00 -c copy \"" + textBox2.Text + "\\" + j + ".mp4\"");
                        Thread.Sleep(200);
                        j++;
                    }
                }
            }

            /*
            for (int i = 4; i < plik.Length; i += 3)
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                buf = plik[i - 1].Split('/');
                cmd.StartInfo.Arguments = "/c " + "ffmpeg -ss 00:" + plik[i] + ".00 -i \"" + textBox2.Text + "\\" + buf[3] + ".mp4\"" + " -t 00:" + plik[i + 1] + ".00 -c copy \"" + textBox2.Text + "\\" + j + ".mp4\"";
                //mpeg = "/c " + "ffmpeg -ss 00:" + plik[i] + ".00 -i \""+ textBox2.Text + "\\" + buf[3] + ".mp4\"" + " -t 00:" + plik[i + 1] + ".00 -c copy \"" + textBox2.Text + "\\" + j + ".mp4\"";
                cmd.Start();
                cmd.WaitForExit();
                j++;
            }*/
        }
    }
}
