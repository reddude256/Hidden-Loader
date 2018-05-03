using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FlatComboBox : ComboBox
{
	private int W;

	private int H;

	private int _StartIndex = 0;

	private int x;

	private int y;

	private Color _BaseColor = Color.FromArgb(25, 27, 29);

	private Color _BGColor = Color.FromArgb(45, 47, 49);

	private Color _HoverColor = Color.FromArgb(35, 168, 109);

	private bool EventsSubscribed = false;

	[Category("Colors")]
	public Color HoverColor
	{
		get
		{
			return this._HoverColor;
		}
		set
		{
			this._HoverColor = value;
		}
	}

	private int StartIndex
	{
		get
		{
			return this._StartIndex;
		}
		set
		{
			this._StartIndex = value;
			try
			{
				base.SelectedIndex = value;
			}
			catch
			{
			}
			base.Invalidate();
		}
	}

	public FlatComboBox()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.BackColor = Color.FromArgb(45, 45, 48);
		this.ForeColor = Color.White;
		base.DropDownStyle = ComboBoxStyle.DropDownList;
		this.Cursor = Cursors.Hand;
		this.StartIndex = 0;
		base.ItemHeight = 18;
		this.Font = new System.Drawing.Font("Segoe UI", 8f, FontStyle.Regular);
		this.SubscribeToEvents();
	}

	public void DrawItem_(object sender, DrawItemEventArgs e)
	{
		if (e.Index >= 0)
		{
			e.DrawBackground();
			e.DrawFocusRectangle();
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
			{
				e.Graphics.FillRectangle(new SolidBrush(this._BaseColor), e.Bounds);
			}
			else
			{
				e.Graphics.FillRectangle(new SolidBrush(this._HoverColor), e.Bounds);
			}
			Graphics graphics = e.Graphics;
			string itemText = base.GetItemText(base.Items[e.Index]);
			System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 8f);
			Brush white = Brushes.White;
			Rectangle bounds = e.Bounds;
			int x = bounds.X + 2;
			bounds = e.Bounds;
			int y = bounds.Y + 2;
			int width = e.Bounds.Width;
			bounds = e.Bounds;
			graphics.DrawString(itemText, font, white, new Rectangle(x, y, width, bounds.Height));
			e.Graphics.Dispose();
		}
	}

	protected override void OnClick(EventArgs e)
	{
		base.OnClick(e);
		base.Invalidate();
	}

	protected override void OnDrawItem(DrawItemEventArgs e)
	{
		base.OnDrawItem(e);
		base.Invalidate();
		if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
		{
			base.Invalidate();
		}
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		base.Invalidate();
	}

	protected override void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		base.Invalidate();
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		base.Invalidate();
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		this.x = e.Location.X;
		this.y = e.Location.Y;
		base.Invalidate();
		if (e.X >= base.Width - 41)
		{
			this.Cursor = Cursors.Hand;
		}
		else
		{
			this.Cursor = Cursors.IBeam;
		}
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		base.Invalidate();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Helpers.B = new Bitmap(base.Width, base.Height);
		Helpers.G = Graphics.FromImage(Helpers.B);
		this.W = base.Width;
		this.H = base.Height;
		Rectangle rectangle = new Rectangle(0, 0, this.W, this.H);
		Rectangle rectangle1 = new Rectangle(Convert.ToInt32(this.W - 40), 0, this.W, this.H);
		GraphicsPath graphicsPath = new GraphicsPath();
		GraphicsPath graphicsPath1 = new GraphicsPath();
		Helpers.G.Clear(Color.FromArgb(45, 45, 48));
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.PixelOffsetMode = PixelOffsetMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.FillRectangle(new SolidBrush(this._BGColor), rectangle);
		graphicsPath.Reset();
		graphicsPath.AddRectangle(rectangle1);
		Helpers.G.SetClip(graphicsPath);
		Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle1);
		Helpers.G.ResetClip();
		Helpers.G.DrawLine(Pens.White, this.W - 10, 6, this.W - 30, 6);
		Helpers.G.DrawLine(Pens.White, this.W - 10, 12, this.W - 30, 12);
		Helpers.G.DrawLine(Pens.White, this.W - 10, 18, this.W - 30, 18);
		Helpers.G.DrawString(this.Text, this.Font, Brushes.White, new Point(4, 6), Helpers.NearSF);
		Helpers.G.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
		Helpers.B.Dispose();
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		base.Height = 18;
	}

	private void SubscribeToEvents()
	{
		if (!this.EventsSubscribed)
		{
			this.EventsSubscribed = true;
			base.DrawItem += new DrawItemEventHandler(this.DrawItem_);
		}
	}
}