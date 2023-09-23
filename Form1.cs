using System;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq;
namespace MATRIX
{
    public partial class Form1 : Form
    {
        public int m_width;
        public int m_heigth;
        public int m_noofrows;
        public int m_noofcolumns;
        public int m_Xoffset;
        public int m_Yoffset;

        private ManualResetEvent pauseEvent = new ManualResetEvent(true);
        public int m_iCounter = 2;
        public int m_iGridMaxSize = 2;


        public const int DEFAULT_Xoffset = 100;
        public const int DEFAULT_Yoffset = 50;
        public const int DEFAULT_noofrows = 3;
        public const int DEFAULT_noofcolumns = 3;
        public const int DEFAULT_width = 60;
        public const int DEFAULT_heigth = 60;
        public Form1()
        {
            initialize();
            InitializeComponent();
            bThreadStatus = false;


        }

        private void onpaint(object sender, EventArgs e)
        {
            DrawGrid();
        }
        public void initialize()
        {
            m_width = DEFAULT_width;
            m_heigth = DEFAULT_heigth;
            m_noofrows = DEFAULT_noofrows;
            m_noofcolumns = DEFAULT_noofcolumns;
            m_Xoffset = DEFAULT_Xoffset;
            m_Yoffset = DEFAULT_Yoffset;
        }
        private void DrawGrid()
        {
            Graphics boardLayout = this.CreateGraphics();
            Pen layoutPen = new Pen(Color.Green);
            layoutPen.Width = 5;


            int X = DEFAULT_Xoffset;
            int Y = DEFAULT_Yoffset;

            for (int i = 0; i <= m_iCounter; i++)
            {
                boardLayout.DrawLine(layoutPen, X, Y, X + this.m_width * this.m_iCounter, Y);
                Y = Y + m_heigth;
            }

            X = DEFAULT_Xoffset;
            Y = DEFAULT_Yoffset;

            for (int j = 0; j <= m_iCounter; j++)
            {
                boardLayout.DrawLine(layoutPen, X, Y, X, Y + this.m_heigth * this.m_iCounter);
                X = X + this.m_width;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            m_iGridMaxSize = 3;

            this.Refresh();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            m_iGridMaxSize = 4;

            this.Refresh();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            m_iGridMaxSize = 5;

            this.Refresh();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {


            CounterThread = new Thread(new ThreadStart(ThreadCounter));
            CounterThread.Start();
            bThreadStatus = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            pauseEvent.Reset();
        }



        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            pauseEvent.Set();
        }




        public void ThreadCounter()
        {

            try
            {
                while (true)
                {
                    m_iCounter++;

                    if (m_iCounter > m_iGridMaxSize)
                    {
                        m_iCounter = 2;
                    }
                    Invalidate();
                    pauseEvent.WaitOne();
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
            }





        }


    }
}
