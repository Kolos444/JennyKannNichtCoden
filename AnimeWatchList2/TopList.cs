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
    public partial class TopList : Form
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
        public TopList()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));

            using (StreamReader sr = new StreamReader("C:\\AnimeList\\done.txt"))
            {
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
                listBox1.Items.AddRange(list.ToArray());

            }
        }

        int selected;

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {

           selected = listBox1.SelectedIndex;
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            
            var index = listBox1.IndexFromPoint(e.X, e.Y);
            if(index == selected)
            {
                return;
            }
            var tmp = listBox1.Items[selected];
            listBox1.Items.RemoveAt(selected);
            for(int i = index; i < listBox1.Items.Count; i++)
            {
                var buffer = listBox1.Items[i];
                listBox1.Items[i] = tmp;
                tmp = buffer;

            }
            listBox1.Items.Add(tmp);

            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(savePathWatched);
            foreach (var item in listBox1.Items)
            {
                SaveFile.WriteLine(item.ToString());
            }
            SaveFile.ToString();
            SaveFile.Close();
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
            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(savePathWatched);
            foreach (var item in listBox1.Items)
            {
                SaveFile.WriteLine(item.ToString());
            }
            SaveFile.ToString();
            SaveFile.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Next Scene
            this.Hide();
            StartMenu form1 = new StartMenu();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Next Scene
            this.Hide();
            Übersicht form2 = new Übersicht();
            form2.Show();
        }
    }
}
