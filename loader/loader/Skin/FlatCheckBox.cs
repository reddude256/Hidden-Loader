using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;

[DefaultEvent("CheckedChanged")]
internal class FlatCheckBox : Control
{
	private int W;

	private int H;

	private MouseState State = MouseState.None;

	private FlatCheckBox._Options O;

	private bool _Checked;

	private Color _BaseColor = Color.FromArgb(45, 47, 49);

	private Color _BorderColor = Helpers._FlatColor;

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
	public Color BorderColor
	{
		get
		{
			return this._BorderColor;
		}
		set
		{
			this._BorderColor = value;
		}
	}

	public bool Checked
	{
		get
		{
			return this._Checked;
		}
		set
		{
			this._Checked = value;
			base.Invalidate();
		}
	}

	[Category("Options")]
	public FlatCheckBox._Options Options
	{
		get
		{
			return this.O;
		}
		set
		{
			this.O = value;
		}
	}

	public FlatCheckBox()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.BackColor = Color.FromArgb(60, 70, 73);
		this.Cursor = Cursors.Hand;
		this.Font = new System.Drawing.Font("Segoe UI", 10f);
		base.Size = new System.Drawing.Size(112, 22);
	}

	protected override void OnClick(EventArgs e)
	{
		this._Checked = !this._Checked;
		if (this.CheckedChanged != null)
		{
			this.CheckedChanged(this);
		}
		base.OnClick(e);
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
		Rectangle rectangle = new Rectangle(0, 2, base.Height - 5, base.Height - 5);
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BackColor);
		switch (this.O)
		{
			case FlatCheckBox._Options.Style1:
			{
				Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
				switch (this.State)
				{
					case MouseState.Over:
					{
						Helpers.G.DrawRectangle(new Pen(this._BorderColor), rectangle);
						break;
					}
					case MouseState.Down:
					{
						Helpers.G.DrawRectangle(new Pen(this._BorderColor), rectangle);
						break;
					}
				}
				if (this.Checked)
				{
					Helpers.G.DrawString("�", new System.Drawing.Font("Wingdings", 18f), new SolidBrush(this._BorderColor), new Rectangle(5, 7, this.H - 9, this.H - 9), Helpers.CenterSF);
				}
				if (!base.Enabled)
				{
					Helpers.G.FillRectangle(new SolidBrush(Color.FromArgb(54, 58, 61)), rectangle);
					Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(Color.FromArgb(140, 142, 143)), new Rectangle(20, 2, this.W, this.H), Helpers.NearSF);
				}
				Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), new Rectangle(20, 2, this.W, this.H), Helpers.NearSF);
				break;
			}
			case FlatCheckBox._Options.Style2:
			{
				Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
				switch (this.State)
				{
					case MouseState.Over:
					{
						Helpers.G.DrawRectangle(new Pen(this._BorderColor), rectangle);
						Helpers.G.FillRectangle(new SolidBrush(Color.FromArgb(118, 213, 170)), rectangle);
						break;
					}
					case MouseState.Down:
					{
						Helpers.G.DrawRectangle(new Pen(this._BorderColor), rectangle);
						Helpers.G.FillRectangle(new SolidBrush(Color.FromArgb(118, 213, 170)), rectangle);
						break;
					}
				}
				if (this.Checked)
				{
					Helpers.G.DrawString("�", new System.Drawing.Font("Wingdings", 18f), new SolidBrush(this._BorderColor), new Rectangle(5, 7, this.H - 9, this.H - 9), Helpers.CenterSF);
				}
				if (!base.Enabled)
				{
					Helpers.G.FillRectangle(new SolidBrush(Color.FromArgb(54, 58, 61)), rectangle);
					Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(Color.FromArgb(48, 119, 91)), new Rectangle(20, 2, this.W, this.H), Helpers.NearSF);
				}
				Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), new Rectangle(20, 2, this.W, this.H), Helpers.NearSF);
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
		base.Height = 22;
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		base.Invalidate();
	}

	public event FlatCheckBox.CheckedChangedEventHandler CheckedChanged;

	[Flags]
	public enum _Options
	{
		Style1,
		Style2
	}

	public delegate void CheckedChangedEventHandler(object sender);
}