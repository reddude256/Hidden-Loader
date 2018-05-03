using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;

[DefaultEvent("Scroll")]
internal class FlatTrackBar : Control
{
	private int W;

	private int H;

	private int Val;

	private bool Bool;

	private Rectangle Track;

	private Rectangle Knob;

	private FlatTrackBar._Style Style_;

	private int _Minimum;

	private int _Maximum = 10;

	private int _Value;

	private bool _ShowValue = false;

	private Color BaseColor = Color.FromArgb(45, 47, 49);

	private Color _TrackColor = Helpers._FlatColor;

	private Color SliderColor = Color.FromArgb(25, 27, 29);

	private Color _HatchColor = Color.FromArgb(23, 148, 92);

	[Category("Colors")]
	public Color HatchColor
	{
		get
		{
			return this._HatchColor;
		}
		set
		{
			this._HatchColor = value;
		}
	}

	public int Maximum
	{
		get
		{
			return this._Maximum;
		}
		set
		{
			if (value < 0)
			{
			}
			this._Maximum = value;
			if (value < this._Value)
			{
				this._Value = value;
			}
			if (value < this._Minimum)
			{
				this._Minimum = value;
			}
			base.Invalidate();
		}
	}

	public int Minimum
	{
		get
		{
			return 0;
		}
		set
		{
			if (value < 0)
			{
			}
			this._Minimum = value;
			if (value > this._Value)
			{
				this._Value = value;
			}
			if (value > this._Maximum)
			{
				this._Maximum = value;
			}
			base.Invalidate();
		}
	}

	public bool ShowValue
	{
		get
		{
			return this._ShowValue;
		}
		set
		{
			this._ShowValue = value;
		}
	}

	public FlatTrackBar._Style Style
	{
		get
		{
			return this.Style_;
		}
		set
		{
			this.Style_ = value;
		}
	}

	[Category("Colors")]
	public Color TrackColor
	{
		get
		{
			return this._TrackColor;
		}
		set
		{
			this._TrackColor = value;
		}
	}

	public int Value
	{
		get
		{
			return this._Value;
		}
		set
		{
			if (value != this._Value)
			{
				if ((value > this._Maximum ? true : value < this._Minimum))
				{
				}
				this._Value = value;
				base.Invalidate();
				if (this.Scroll != null)
				{
					this.Scroll(this);
				}
			}
		}
	}

	public FlatTrackBar()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		base.Height = 18;
		this.BackColor = Color.FromArgb(60, 70, 73);
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (e.KeyCode == Keys.Subtract)
		{
			if (this.Value != 0)
			{
				FlatTrackBar value = this;
				value.Value = value.Value - 1;
			}
		}
		else if (e.KeyCode == Keys.Add)
		{
			if (this.Value != this._Maximum)
			{
				FlatTrackBar flatTrackBar = this;
				flatTrackBar.Value = flatTrackBar.Value + 1;
			}
		}
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		if (e.Button == System.Windows.Forms.MouseButtons.Left)
		{
			this.Val = Convert.ToInt32((double)(this._Value - this._Minimum) / (double)(this._Maximum - this._Minimum) * (double)(base.Width - 11));
			this.Track = new Rectangle(this.Val, 0, 10, 20);
			this.Bool = this.Track.Contains(e.Location);
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if ((!this.Bool || e.X <= -1 ? false : e.X < base.Width + 1))
		{
			this.Value = this._Minimum + Convert.ToInt32((double)(this._Maximum - this._Minimum) * ((double)e.X / (double)base.Width));
		}
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.Bool = false;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Helpers.B = new Bitmap(base.Width, base.Height);
		Helpers.G = Graphics.FromImage(Helpers.B);
		this.W = base.Width - 1;
		this.H = base.Height - 1;
		Rectangle rectangle = new Rectangle(1, 6, this.W - 2, 8);
		GraphicsPath graphicsPath = new GraphicsPath();
		GraphicsPath graphicsPath1 = new GraphicsPath();
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.PixelOffsetMode = PixelOffsetMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BackColor);
		this.Val = Convert.ToInt32((double)(this._Value - this._Minimum) / (double)(this._Maximum - this._Minimum) * (double)(this.W - 10));
		this.Track = new Rectangle(this.Val, 0, 10, 20);
		this.Knob = new Rectangle(this.Val, 4, 11, 14);
		graphicsPath.AddRectangle(rectangle);
		Helpers.G.SetClip(graphicsPath);
		Helpers.G.FillRectangle(new SolidBrush(this.BaseColor), new Rectangle(0, 7, this.W, 8));
		Helpers.G.FillRectangle(new SolidBrush(this._TrackColor), new Rectangle(0, 7, this.Track.X + this.Track.Width, 8));
		Helpers.G.ResetClip();
		HatchBrush hatchBrush = new HatchBrush(HatchStyle.Plaid, this.HatchColor, this._TrackColor);
		Helpers.G.FillRectangle(hatchBrush, new Rectangle(-10, 7, this.Track.X + this.Track.Width, 8));
		switch (this.Style)
		{
			case FlatTrackBar._Style.Slider:
			{
				graphicsPath1.AddRectangle(this.Track);
				Helpers.G.FillPath(new SolidBrush(this.SliderColor), graphicsPath1);
				break;
			}
			case FlatTrackBar._Style.Knob:
			{
				graphicsPath1.AddEllipse(this.Knob);
				Helpers.G.FillPath(new SolidBrush(this.SliderColor), graphicsPath1);
				break;
			}
		}
		if (this.ShowValue)
		{
			Graphics g = Helpers.G;
			string str = this.Value.ToString();
			System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 8f);
			Brush white = Brushes.White;
			RectangleF rectangleF = new Rectangle(1, 6, this.W, this.H);
			StringFormat stringFormat = new StringFormat()
			{
				Alignment = StringAlignment.Far,
				LineAlignment = StringAlignment.Far
			};
			g.DrawString(str, font, white, rectangleF, stringFormat);
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
		base.Height = 23;
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		base.Invalidate();
	}

	public event FlatTrackBar.ScrollEventHandler Scroll;

	[Flags]
	public enum _Style
	{
		Slider,
		Knob
	}

	public delegate void ScrollEventHandler(object sender);
}