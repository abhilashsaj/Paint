using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool startPaint = false;
        Graphics g;
        //nullable int for storing Null value
        int? initX = null;
        int? initY = null;
        bool drawSquare = false;
        bool drawRectangle = false;
        bool drawCircle = false;
        Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            bmp = new Bitmap(panel1.ClientSize.Width, panel1.ClientSize.Height);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            
            saveFileDialog1.Title = "Save Image";
            
            saveFileDialog1.DefaultExt = "jpeg";
            saveFileDialog1.Filter = "JPEG files (*.jpeg)|*.jpeg|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                    bmp.Save(saveFileDialog1.FileName, ImageFormat.Png);
                //textBox1.Text = saveFileDialog1.FileName;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPaint)
            {
                //Setting the Pen BackColor and line Width
                Pen p = new Pen(btn_PenColor.BackColor, float.Parse(cmb_PenSize.Text));
                //Drawing the line.
                g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                initX = e.X;
                initY = e.Y;
                /*using (g = Graphics.FromImage(bmp))
                {
                    //Setting the Pen BackColor and line Width
                    Pen p = new Pen(btn_PenColor.BackColor, float.Parse(cmb_PenSize.Text));
                    //Drawing the line.
                    g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                    initX = e.X;
                    initY = e.Y;

                }
                   
                panel1.Invalidate();

                */
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            startPaint = true;
            if (drawSquare)
            {
                //Use Solid Brush for filling the graphic shapes
                SolidBrush sb = new SolidBrush(btn_PenColor.BackColor);
                //setting the width and height same for creating square.
                //Getting the width and Heigt value from Textbox(txt_ShapeSize)
                g.FillRectangle(sb, e.X, e.Y, int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text));
                //setting startPaint and drawSquare value to false for creating one graphic on one click.
                startPaint = false;
                drawSquare = false;
            }
            if (drawRectangle)
            {
                SolidBrush sb = new SolidBrush(btn_PenColor.BackColor);
                //setting the width twice of the height
                g.FillRectangle(sb, e.X, e.Y, 2 * int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text));
                startPaint = false;
                drawRectangle = false;
            }
            if (drawCircle)
            {
                SolidBrush sb = new SolidBrush(btn_PenColor.BackColor);
                g.FillEllipse(sb, e.X, e.Y, int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text));
                startPaint = false;
                drawCircle = false;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btn_PenColor_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                btn_PenColor.BackColor = c.Color;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            startPaint = false;
            initX = null;
            initY = null;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clearing the graphics from the Panel(pnl_Draw)
            g.Clear(panel1.BackColor);
            //Setting the BackColor of pnl_draw and btn_CanvasColor to White on Clicking New under File Menu
            panel1.BackColor = Color.White;
            btn_CanvasColor.BackColor = Color.White;
        }

        private void btn_CanvasColor_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                panel1.BackColor = c.Color;
                btn_CanvasColor.BackColor = c.Color;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            drawSquare = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            drawCircle = true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            drawRectangle = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmp, Point.Empty);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            {
                
                openFileDialog1.Title = "Browse Image Files";



                openFileDialog1.DefaultExt = "jpeg";
                openFileDialog1.Filter = "jpeg files (*.jpeg)|*.jpeg";
                openFileDialog1.FilterIndex = 2;
                

                
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFileName = openFileDialog1.FileName;
                bmp = new Bitmap(sFileName);
                // openFileDialog1.FileName;
            }
        }
    }
}
