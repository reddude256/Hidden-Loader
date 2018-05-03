using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FlatStickyButton : Control
{
	private int W;

	private int H;

	private MouseState State = MouseState.None;

	private bool _Rounded = false;

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

	private Rectangle Rect
	{
		get
		{
			Rectangle rectangle = new Rectangle(base.Left, base.Top, base.Width, base.Height);
			return rectangle;
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

	public FlatStickyButton()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		base.Size = new System.Drawing.Size(106, 32);
		this.BackColor = Color.Transparent;
		this.Font = new System.Drawing.Font("Segoe UI", 12f);
		this.Cursor = Cursors.Hand;
	}

	private bool[] GetConnectedSides()
	{
		bool[] flagArray = new bool[4];
		foreach (Control control in base.Parent.Controls)
		{
			if (control is FlatStickyButton)
			{
				if ((control == this ? false : this.Rect.IntersectsWith(this.Rect)))
				{
					double num = Math.Atan2((double)(base.Left - control.Left), (double)(base.Top - control.Top)) * 2 / 3.14159265358979;
					if (Math.Truncate(num / 1) == num)
					{
						flagArray[Convert.ToInt32(num + 1)] = true;
					}
				}
				else
				{
					continue;
				}
			}
		}
		return flagArray;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();
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
		this.W = base.Width;
		this.H = base.Height;
		GraphicsPath graphicsPath = new GraphicsPath();
		bool[] connectedSides = this.GetConnectedSides();
		GraphicsPath graphicsPath1 = Helpers.RoundRect(0f, 0f, (float)this.W, (float)this.H, 0.3f, (connectedSides[2] ? false : !connectedSides[1]), (connectedSides[1] ? false : !connectedSides[0]), (connectedSides[3] ? false : !connectedSides[0]), (connectedSides[3] ? false : !connectedSides[2]));
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
					graphicsPath = graphicsPath1;
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
					graphicsPath = graphicsPath1;
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
					graphicsPath = graphicsPath1;
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

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
	}
}