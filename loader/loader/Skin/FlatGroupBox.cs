using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FlatGroupBox : ContainerControl
{
	private int W;

	private int H;

	private bool _ShowText = true;

	private Color _BaseColor = Color.FromArgb(60, 70, 73);

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

	public bool ShowText
	{
		get
		{
			return this._ShowText;
		}
		set
		{
			this._ShowText = value;
		}
	}

	public FlatGroupBox()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.BackColor = Color.Transparent;
		base.Size = new System.Drawing.Size(240, 180);
		this.Font = new System.Drawing.Font("Segoe ui", 10f);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Helpers.B = new Bitmap(base.Width, base.Height);
		Helpers.G = Graphics.FromImage(Helpers.B);
		this.W = base.Width - 1;
		this.H = base.Height - 1;
		GraphicsPath graphicsPath = new GraphicsPath();
		GraphicsPath graphicsPath1 = new GraphicsPath();
		GraphicsPath graphicsPath2 = new GraphicsPath();
		Rectangle rectangle = new Rectangle(8, 8, this.W - 16, this.H - 16);
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.PixelOffsetMode = PixelOffsetMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BackColor);
		graphicsPath = Helpers.RoundRec(rectangle, 8);
		Helpers.G.FillPath(new SolidBrush(this._BaseColor), graphicsPath);
		graphicsPath1 = Helpers.DrawArrow(28, 2, false);
		Helpers.G.FillPath(new SolidBrush(this._BaseColor), graphicsPath1);
		graphicsPath2 = Helpers.DrawArrow(28, 8, true);
		Helpers.G.FillPath(new SolidBrush(Color.FromArgb(60, 70, 73)), graphicsPath2);
		if (this.ShowText)
		{
			Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(Helpers._FlatColor), new Rectangle(16, 16, this.W, this.H), Helpers.NearSF);
		}
		base.OnPaint(e);
		Helpers.G.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
		Helpers.B.Dispose();
	}
}