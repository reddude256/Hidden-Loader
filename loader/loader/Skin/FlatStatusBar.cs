using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FlatStatusBar : Control
{
	private int W;

	private int H;

	private bool _ShowTimeDate = false;

	private Color _BaseColor = Color.FromArgb(45, 47, 49);

	private Color _TextColor = Color.White;

	private Color _RectColor = Helpers._FlatColor;

	[Category("Colors")]
	public Color BaseColor
	{
		get
		{
			return this._BaseColor;
		}
		set
		{
			this._BaseColor = value;
		}
	}

	[Category("Colors")]
	public Color RectColor
	{
		get
		{
			return this._RectColor;
		}
		set
		{
			this._RectColor = value;
		}
	}

	public bool ShowTimeDate
	{
		get
		{
			return this._ShowTimeDate;
		}
		set
		{
			this._ShowTimeDate = value;
		}
	}

	[Category("Colors")]
	public Color TextColor
	{
		get
		{
			return this._TextColor;
		}
		set
		{
			this._TextColor = value;
		}
	}

	public FlatStatusBar()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Segoe UI", 8f);
		this.ForeColor = Color.White;
		base.Size = new System.Drawing.Size(base.Width, 20);
	}

	protected override void CreateHandle()
	{
		base.CreateHandle();
		this.Dock = DockStyle.Bottom;
	}

	public string GetTimeDate()
	{
		object[] date = new object[5];
		DateTime now = DateTime.Now;
		date[0] = now.Date;
		date[1] = " ";
		now = DateTime.Now;
		date[2] = now.Hour;
		date[3] = ":";
		now = DateTime.Now;
		date[4] = now.Minute;
		return string.Concat(date);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Helpers.B = new Bitmap(base.Width, base.Height);
		Helpers.G = Graphics.FromImage(Helpers.B);
		this.W = base.Width;
		this.H = base.Height;
		Rectangle rectangle = new Rectangle(0, 0, this.W, this.H);
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.PixelOffsetMode = PixelOffsetMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BaseColor);
		Helpers.G.FillRectangle(new SolidBrush(this.BaseColor), rectangle);
		Helpers.G.DrawString(this.Text, this.Font, Brushes.White, new Rectangle(10, 4, this.W, this.H), Helpers.NearSF);
		Helpers.G.FillRectangle(new SolidBrush(this._RectColor), new Rectangle(4, 4, 4, 14));
		if (this.ShowTimeDate)
		{
			Graphics g = Helpers.G;
			string timeDate = this.GetTimeDate();
			System.Drawing.Font font = this.Font;
			SolidBrush solidBrush = new SolidBrush(this._TextColor);
			RectangleF rectangleF = new Rectangle(-4, 2, this.W, this.H);
			StringFormat stringFormat = new StringFormat()
			{
				Alignment = StringAlignment.Far,
				LineAlignment = StringAlignment.Center
			};
			g.DrawString(timeDate, font, solidBrush, rectangleF, stringFormat);
		}
		base.OnPaint(e);
		Helpers.G.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
		Helpers.B.Dispose();
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		base.Invalidate();
	}
}