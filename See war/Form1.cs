using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace See_war
{
    class coord
    {
        public static int number { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
    class coords
    {
        public static int range { get; set; }
        coord[] coordn = new coord[coord.number];
    }
    public partial class Form1 : Form
    {
        int i,j,j0;
        bool hod;
        int[,] field = new int[12, 12];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            hod = true;
            dataGridView1.Enabled = false;
            dataGridView1.Visible = false;
            dataGridView1.ColumnCount = 10;
            dataGridView1.RowCount = 10;
            dataGridView1.Width = 510;
            dataGridView1.Height = 510;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Height = 50;
                dataGridView1.Columns[i].Width = 50;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    dataGridView1.Rows[i].Cells[j].Style.SelectionBackColor = Color.White;
            }    
            dataGridView1.ClearSelection();
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 12; j++)
                    field[i, j] = 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            using (var soundPlayer = new SoundPlayer(@"c:\Windows\Media\see_war.wav"))
            {
                soundPlayer.Play(); 
            }
            dataGridView1.Enabled = true;
            dataGridView1.Visible = true;
            button1.Enabled = false;
            i = 4;
            j = 1;
            j0 = j;
            label1.Text = "Выберите на поле " + i.ToString() +" корабля длины 1";

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (hod)
            {
                if ((i > 0) && (j0 == j) && field[e.RowIndex + 1, e.ColumnIndex + 1] == 0)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                    field[e.RowIndex + 1, e.ColumnIndex + 1] = 1;
                    i -= 1;
                    label1.Text = "Выберите на поле " + i.ToString() + " корабля длины " + j.ToString();
                    j0 -= 1;
                }
                else if ((i >= 0) && (j0 < j) && (j0 > 0))
                {
                    if (field[e.RowIndex + 1, e.ColumnIndex + 1] == 0)
                    {
                        if (((field[e.RowIndex, e.ColumnIndex + 1] == 1) && (e.RowIndex > 0)) || ((field[e.RowIndex + 2, e.ColumnIndex + 1] == 1) && (e.RowIndex <= 8)) || ((field[e.RowIndex + 1, e.ColumnIndex + 2] == 1) && (e.ColumnIndex <= 8)) || ((field[e.RowIndex + 1, e.ColumnIndex] == 1) && (e.ColumnIndex > 0)))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                            field[e.RowIndex + 1, e.ColumnIndex + 1] = 1;
                            j0 -= 1;
                        }
                    }
                }
                if (j < 5)
                {
                    if ((i == 0) && (j0 == 0))
                    {
                        j += 1;
                        i = 5 - j;
                        if ((i > 0) || (j < 5))
                            label1.Text = "Выберите на поле " + i.ToString() + " корабля длины " + j.ToString();
                        else label1.Text = "";
                    }
                    if (j0 == 0)
                    {
                        j0 = j;
                    }
                }
                else
                {
                    hod = false;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                        }
                    }
                    label1.Text = "Начинайте угадывание положения вражеских кораблей";
                }
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
                if (field[e.RowIndex + 1, e.ColumnIndex + 1] == 1)
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Black;
                else dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Blue;
            }
        }
    }
}
