namespace ConwaysGameOfLife;

using GameOfLife;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;

public partial class Form1 : Form
{
    private readonly Timer _drawTimer = new();
    private readonly Matrix _matrix = new();
    private const int boardSize = 400;

    private readonly ILifeGame _game;
    private readonly Gameboard _gameboard;

    private ulong _ticks = 0;
    private const int ticksToAverage = 60;
    private readonly long[] _tickTimes = new long[ticksToAverage];
    private readonly long[] _drawTimes = new long[ticksToAverage];
    private readonly Stopwatch _stopwatch = new();

    public Form1()
    {
        InitializeComponent();
        DoubleBuffered = true;
        Paint += Form1_Paint;
        _drawTimer.Interval = 16;
        _drawTimer.Enabled = true;
        _drawTimer.Tick += DrawTimer_Tick;
        SizeChanged += Form1_SizeChanged;

        _gameboard = new Gameboard(boardSize, boardSize);
        _game = new LifeGame(_gameboard, LifeGame.IsAlive);

        RandomizeBoard();
        UpdateMatrix();
    }

    private void RandomizeBoard()
    {
        var rnd = new Random();
        for (int i = 0; i < boardSize * boardSize; i++)
        {
            _gameboard.SetAlive(i % boardSize, i / boardSize, rnd.Next(2) == 1);
        }
    }

    private void Form1_SizeChanged(object? sender, EventArgs e)
    {
        UpdateMatrix();
    }

    private void UpdateMatrix()
    {
        var playingArea = new Rectangle(0, 0, boardSize, boardSize);
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
        _stopwatch.Restart();
        e.Graphics.Transform = _matrix;
        var grid = _gameboard.Snapshot();
        for (int i = 0; i < grid.Length; i++)
        {
            e.Graphics.FillRectangle(grid[i] ? Brushes.CornflowerBlue : Brushes.OldLace, i % boardSize, i / boardSize, 1, 1);
        }
        _stopwatch.Stop();
        _drawTimes[_ticks % ticksToAverage] = _stopwatch.ElapsedMilliseconds;

        numTicksLabel.Text = _ticks.ToString();
        tickLabel.Text = $"Tick: {_tickTimes.Average():F2}";
        drawLabel.Text = $"Draw: {_drawTimes.Average():F2}";
    }

    private void DrawTimer_Tick(object? sender, EventArgs e)
    {
        _stopwatch.Restart();
        _game.Tick();
        _stopwatch.Stop();
        _tickTimes[_ticks % ticksToAverage] = _stopwatch.ElapsedMilliseconds;
        Invalidate();
        _ticks++;
    }
}