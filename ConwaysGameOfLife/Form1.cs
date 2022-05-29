namespace ConwaysGameOfLife;

using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;

public partial class Form1 : Form
{
    private readonly Timer _drawTimer = new();
    private readonly Matrix _matrix = new();
    private readonly bool[] _bools = new bool[400*400];
    private const int width = 400;

    public Form1()
    {
        InitializeComponent();
        DoubleBuffered = true;
        Paint += Form1_Paint;
        _drawTimer.Interval = 16;
        _drawTimer.Enabled = true;
        _drawTimer.Tick += DrawTimer_Tick;
        SizeChanged += Form1_SizeChanged;

        var rnd = new Random();
        for (int i = 0; i < _bools.Length; i++)
        {
            _bools[i] = rnd.Next(2) == 1;
        }

        UpdateMatronx();
    }

    private void Form1_SizeChanged(object? sender, EventArgs e)
    {
        UpdateMatronx();
    }

    private void UpdateMatronx()
    {
        var playingArea = new Rectangle(0, 0, 400, 400);
        int clientWidth = ClientRectangle.Width;
        int clientHeight = ClientRectangle.Height;
        float ratioX = clientWidth / (float)playingArea.Width;
        float ratioY = clientHeight / (float)playingArea.Height;
        float ratio = Math.Min(ratioX, ratioY);
        _matrix.Reset();
        _matrix.Translate((ClientRectangle.Width - playingArea.Width * ratio) / 2f,
            (ClientRectangle.Height - playingArea.Height * ratio) / 2f);
        _matrix.Scale(ratio, ratio);
    }

    private void Form1_Paint(object? sender, PaintEventArgs e)
    {
        e.Graphics.Transform = _matrix;
        for (int i = 0; i < _bools.Length; i++)
        {
            e.Graphics.FillRectangle(_bools[i] ? Brushes.CornflowerBlue : Brushes.OldLace, i % width, i / width, 1, 1);
        }
    }

    private void DrawTimer_Tick(object? sender, EventArgs e)
    {
        Invalidate();
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }
}