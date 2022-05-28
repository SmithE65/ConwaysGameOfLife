using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;

namespace ConwaysGameOfLife
{
    public partial class Form1 : Form
    {
        private readonly Timer _drawTimer = new();
        private readonly Matrix _matrix = new();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Paint += Form1_Paint;
            _drawTimer.Interval = 16;
            _drawTimer.Enabled = true;
            _drawTimer.Tick += DrawTimer_Tick;
            SizeChanged += Form1_SizeChanged;
        }

        private void Form1_SizeChanged(object? sender, EventArgs e)
        {
            Rectangle playingArea = new Rectangle(0, 0, 400, 400);
            int width = this.ClientRectangle.Width;
            int height = this.ClientRectangle.Height;
            float ratioX = width / playingArea.Width;
            float ratioY = width /playingArea.Height;
            float ratio = Math.Min(ratioX, ratioY);
            _matrix.Reset();
            _matrix.Translate((ClientRectangle.Width - playingArea.Width * ratio) / 2f,
                ((ClientRectangle.Height - playingArea.Height * ratio) / 2f));
            _matrix.Scale(ratio, ratio);
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(DateTime.Now.ToString(), this.Font, Brushes.Black, PointF.Empty);

        }

        private void DrawTimer_Tick(object? sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}