using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FlatClose : Control
{
	private MouseState State = MouseState.None;

	private int x;

	private Color _BaseColor = Color.FromArgb(168, 35, 35);

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

	public FlatClose()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.BackColor = Color.White;
		base.Size = new System.Drawing.Size(18, 18);
		this.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		this.Font = new System.Drawing.Font("Marlett", 10f);
	}

	protected override void OnClick(EventArgs e)
	{
		base.OnClick(e);
		Environment.Exit(0);
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

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		this.x = e.X;
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
		Bitmap bitmap = new Bitmap(base.Width, base.Height);
		Graphics graphic = Graphics.FromImage(bitmap);
		Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height);
		graphic.SmoothingMode = SmoothingMode.HighQuality;
		graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
		graphic.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		graphic.Clear(this.BackColor);
		graphic.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
		graphic.DrawString("r", this.Font, new SolidBrush(this.TextColor), new Rectangle(0, 0, base.Width, base.Height), Helpers.CenterSF);
		switch (this.State)
		{
			case MouseState.Over:
			{
				graphic.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.White)), rectangle);
				break;
			}
			case MouseState.Down:
			{
				graphic.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rectangle);
				break;
			}
		}
		base.OnPaint(e);
		graphic.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		base.Size = new System.Drawing.Size(18, 18);
	}
}