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
    public partial class StartMenu : Form
    {
        const string savePath = "C:\\AnimeList\\list.txt";
        const string savePathWatched = "C:\\AnimeList\\done.txt";
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
        public StartMenu()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));

            //Read the current items from the savelist and put it in the textbox
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
    
            listBox1.Items.Add(textBox1.Text);
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Save to the list
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(savePath);
            foreach (var item in listBox1.Items)
            {
                SaveFile.WriteLine(item.ToString());
            }
            SaveFile.ToString();
            SaveFile.Close();

            MessageBox.Show("List has been updated");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Random Anime
            Random r = new Random();
            int index = r.Next(list.Count);
            string randomString = list[index];

            textBox2.Text = randomString;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Next Scene
            this.Hide();
            Übersicht form2 = new Übersicht();
            form2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Delete selected item
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(listBox1);
            selectedItems = listBox1.SelectedItems;

            if (listBox1.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                    listBox1.Items.Remove(selectedItems[i]);
            }

            //Save the current items from the listbox to the savefile
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(savePath);
            foreach (var item in listBox1.Items)
            {
                SaveFile.WriteLine(item.ToString());
            }
            SaveFile.ToString();
            SaveFile.Close();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

            this.Hide();
            TopList form3 = new TopList();
            form3.Show();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            var selectedItemsWatched = listBox1.SelectedItem;

           // System.IO.StreamWriter SaveFileWatched = new System.IO.StreamWriter(savePathWatched);

            using (StreamWriter SaveFileWatched = File.AppendText(savePathWatched))
            {
                SaveFileWatched.WriteLine(selectedItemsWatched.ToString());

                SaveFileWatched.ToString();
                SaveFileWatched.Close();
            }

            

            listBox1.Items.Remove(selectedItemsWatched);

            MessageBox.Show("Anime has been added to the Top List and removed from the Watchlist");


            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(savePath);
            foreach (var item in listBox1.Items)
            {
                SaveFile.WriteLine(item.ToString());
            }
            SaveFile.ToString();
            SaveFile.Close();


        }
    }
}
