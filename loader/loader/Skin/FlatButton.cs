using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FlatButton : Control
{
	private int W;

	private int H;

	private bool _Rounded = false;

	private MouseState State = MouseState.None;

	private Color _BaseColor = Helpers._FlatColor;

	private Color _TextColor = Color.FromArgb(243, 243, 243);

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

	[Category("Options")]
	public bool Rounded
	{
		get
		{
			return this._Rounded;
		}
		set
		{
			this._Rounded = value;
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

	public FlatButton()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		base.Size = new System.Drawing.Size(106, 32);
		this.BackColor = Color.Transparent;
		this.Font = new System.Drawing.Font("Segoe UI", 12f);
		this.Cursor = Cursors.Hand;
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.State = MouseState.Down;
		base.Invalidate();
	}

	protected override void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.State = MouseState.Over;
		base.Invalidate();
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.State = MouseState.None;
		base.Invalidate();
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.State = MouseState.Over;
		base.Invalidate();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Helpers.B = new Bitmap(base.Width, base.Height);
		Helpers.G = Graphics.FromImage(Helpers.B);
		this.W = base.Width - 1;
		this.H = base.Height - 1;
		GraphicsPath graphicsPath = new GraphicsPath();
		Rectangle rectangle = new Rectangle(0, 0, this.W, this.H);
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.PixelOffsetMode = PixelOffsetMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BackColor);
		switch (this.State)
		{
			case MouseState.None:
			{
				if (!this.Rounded)
				{
					Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
					Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rectangle, Helpers.CenterSF);
				}
				else
				{
					graphicsPath = Helpers.RoundRec(rectangle, 6);
					Helpers.G.FillPath(new SolidBrush(this._BaseColor), graphicsPath);
					Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rectangle, Helpers.CenterSF);
				}
				break;
			}
			case MouseState.Over:
			{
				if (!this.Rounded)
				{
					Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
					Helpers.G.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.White)), rectangle);
					Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rectangle, Helpers.CenterSF);
				}
				else
				{
					graphicsPath = Helpers.RoundRec(rectangle, 6);
					Helpers.G.FillPath(new SolidBrush(this._BaseColor), graphicsPath);
					Helpers.G.FillPath(new SolidBrush(Color.FromArgb(20, Color.White)), graphicsPath);
					Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rectangle, Helpers.CenterSF);
				}
				break;
			}
			case MouseState.Down:
			{
				if (!this.Rounded)
				{
					Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
					Helpers.G.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.Black)), rectangle);
					Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rectangle, Helpers.CenterSF);
				}
				else
				{
					graphicsPath = Helpers.RoundRec(rectangle, 6);
					Helpers.G.FillPath(new SolidBrush(this._BaseColor), graphicsPath);
					Helpers.G.FillPath(new SolidBrush(Color.FromArgb(20, Color.Black)), graphicsPath);
					Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), rectangle, Helpers.CenterSF);
				}
				break;
			}
		}
		base.OnPaint(e);
		Helpers.G.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
		Helpers.B.Dispose();
	}
}