using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace AnimeWatchList2
{
    public partial class Übersicht : Form
    {

        const string savePath = "C:\\AnimeList\\list.txt";
        List<String> list = new List<String>();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public Übersicht()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));

            using (StreamReader sr = new StreamReader("C:\\AnimeList\\list.txt"))
            {
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
                listBox1.Items.AddRange(list.ToArray());

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Next Scene
            this.Hide();
            StartMenu form1 = new StartMenu();
            form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            TopList form3 = new TopList();
            form3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(listBox1);
            selectedItems = listBox1.SelectedItems;

            string animeSavePath = "C:\\AnimeList\\Animes";
            string fileName = System.IO.Path.GetRandomFileName();
            animeSavePath = System.IO.Path.Combine(animeSavePath, fileName);

            if (!System.IO.File.Exists(animeSavePath))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(animeSavePath))
                {
                    for (byte i = 0; i < 100; i++)
                    {
                        fs.WriteByte(i);
                    }
                }
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }
            



            
        }
    }
}
