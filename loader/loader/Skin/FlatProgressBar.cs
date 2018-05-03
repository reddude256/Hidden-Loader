using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FlatProgressBar : Control
{
	private int W;

	private int H;

	private int _Value = 0;

	private int _Maximum = 100;

	private Color _BaseColor = Color.FromArgb(45, 47, 49);

	private Color _ProgressColor = Helpers._FlatColor;

	private Color _DarkerProgress = Color.FromArgb(23, 148, 92);

	[Category("Colors")]
	public Color DarkerProgress
	{
		get
		{
			return this._DarkerProgress;
		}
		set
		{
			this._DarkerProgress = value;
		}
	}

	[Category("Control")]
	public int Maximum
	{
		get
		{
			return this._Maximum;
		}
		set
		{
			if (value < this._Value)
			{
				this._Value = value;
			}
			this._Maximum = value;
			base.Invalidate();
		}
	}

	[Category("Colors")]
	public Color ProgressColor
	{
		get
		{
			return this._ProgressColor;
		}
		set
		{
			this._ProgressColor = value;
		}
	}

	[Category("Control")]
	public int Value
	{
		get
		{
			return (this._Value == 0 ? 0 : this._Value);
		}
		set
		{
			if (value > this._Maximum)
			{
				value = this._Maximum;
				base.Invalidate();
			}
			this._Value = value;
			base.Invalidate();
		}
	}

	public FlatProgressBar()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.BackColor = Color.FromArgb(60, 70, 73);
		base.Height = 42;
	}

	protected override void CreateHandle()
	{
		base.CreateHandle();
		base.Height = 42;
	}

	public void Increment(int Amount)
	{
		FlatProgressBar value = this;
		value.Value = value.Value + Amount;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Helpers.B = new Bitmap(base.Width, base.Height);
		Helpers.G = Graphics.FromImage(Helpers.B);
		this.W = base.Width - 1;
		this.H = base.Height - 1;
		Rectangle rectangle = new Rectangle(0, 24, this.W, this.H);
		GraphicsPath graphicsPath = new GraphicsPath();
		GraphicsPath graphicsPath1 = new GraphicsPath();
		GraphicsPath graphicsPath2 = new GraphicsPath();
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.PixelOffsetMode = PixelOffsetMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BackColor);
		int num = Convert.ToInt32((double)this._Value / (double)this._Maximum * (double)base.Width);
		int value = this.Value;
		if (value == 0)
		{
			Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
			Helpers.G.FillRectangle(new SolidBrush(this._ProgressColor), new Rectangle(0, 24, num - 1, this.H - 1));
		}
		else if (value == 100)
		{
			Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
			Helpers.G.FillRectangle(new SolidBrush(this._ProgressColor), new Rectangle(0, 24, num - 1, this.H - 1));
		}
		else
		{
			Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
			graphicsPath.AddRectangle(new Rectangle(0, 24, num - 1, this.H - 1));
			Helpers.G.FillPath(new SolidBrush(this._ProgressColor), graphicsPath);
			HatchBrush hatchBrush = new HatchBrush(HatchStyle.Plaid, this._DarkerProgress, this._ProgressColor);
			Helpers.G.FillRectangle(hatchBrush, new Rectangle(0, 24, num - 1, this.H - 1));
			Rectangle rectangle1 = new Rectangle(num - 18, 0, 34, 16);
			graphicsPath1 = Helpers.RoundRec(rectangle1, 4);
			Helpers.G.FillPath(new SolidBrush(this._BaseColor), graphicsPath1);
			graphicsPath2 = Helpers.DrawArrow(num - 9, 16, true);
			Helpers.G.FillPath(new SolidBrush(this._BaseColor), graphicsPath2);
			Graphics g = Helpers.G;
			int value1 = this.Value;
			g.DrawString(value1.ToString(), new System.Drawing.Font("Segoe UI", 10f), new SolidBrush(this._ProgressColor), new Rectangle(num - 11, -2, this.W, this.H), Helpers.NearSF);
		}
		base.OnPaint(e);
		Helpers.G.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
		Helpers.B.Dispose();
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		base.Height = 42;
	}
}