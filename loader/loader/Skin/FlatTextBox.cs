using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

[DefaultEvent("TextChanged")]
internal class FlatTextBox : Control
{
	private int W;

	private int H;

	private TextBox TB;

	private HorizontalAlignment _TextAlign = HorizontalAlignment.Left;

	private int _MaxLength = 32767;

	private bool _ReadOnly;

	private bool _UseSystemPasswordChar;

	private bool _Multiline;

	private Color _BaseColor = Color.FromArgb(45, 47, 49);

	private Color _TextColor = Color.FromArgb(192, 192, 192);

	private Color _BorderColor = Helpers._FlatColor;

	[Category("Options")]
	public override System.Drawing.Font Font
	{
		get
		{
			return base.Font;
		}
		set
		{
			base.Font = value;
			if (this.TB != null)
			{
				this.TB.Font = value;
				this.TB.Location = new Point(3, 5);
				this.TB.Width = base.Width - 6;
				if (!this._Multiline)
				{
					base.Height = this.TB.Height + 11;
				}
			}
		}
	}

	public override Color ForeColor
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

	[Category("Options")]
	public int MaxLength
	{
		get
		{
			return this._MaxLength;
		}
		set
		{
			this._MaxLength = value;
			if (this.TB != null)
			{
				this.TB.MaxLength = value;
			}
		}
	}

	[Category("Options")]
	public bool Multiline
	{
		get
		{
			return this._Multiline;
		}
		set
		{
			this._Multiline = value;
			if (this.TB != null)
			{
				this.TB.Multiline = value;
				if (!value)
				{
					base.Height = this.TB.Height + 11;
				}
				else
				{
					this.TB.Height = base.Height - 11;
				}
			}
		}
	}

	[Category("Options")]
	public bool ReadOnly
	{
		get
		{
			return this._ReadOnly;
		}
		set
		{
			this._ReadOnly = value;
			if (this.TB != null)
			{
				this.TB.ReadOnly = value;
			}
		}
	}

	[Category("Options")]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = value;
			if (this.TB != null)
			{
				this.TB.Text = value;
			}
		}
	}

	[Category("Options")]
	public HorizontalAlignment TextAlign
	{
		get
		{
			return this._TextAlign;
		}
		set
		{
			this._TextAlign = value;
			if (this.TB != null)
			{
				this.TB.TextAlign = value;
			}
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

	[Category("Options")]
	public bool UseSystemPasswordChar
	{
		get
		{
			return this._UseSystemPasswordChar;
		}
		set
		{
			this._UseSystemPasswordChar = value;
			if (this.TB != null)
			{
				this.TB.UseSystemPasswordChar = value;
			}
		}
	}

	public FlatTextBox()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.BackColor = Color.Transparent;
		this.TB = new TextBox()
		{
			Font = new System.Drawing.Font("Segoe UI", 10f),
			Text = this.Text,
			BackColor = this._BaseColor,
			ForeColor = this._TextColor,
			MaxLength = this._MaxLength,
			Multiline = this._Multiline,
			ReadOnly = this._ReadOnly,
			UseSystemPasswordChar = this._UseSystemPasswordChar,
			BorderStyle = BorderStyle.None,
			Location = new Point(5, 5),
			Width = base.Width - 10,
			Cursor = Cursors.IBeam
		};
		if (!this._Multiline)
		{
			base.Height = this.TB.Height + 11;
		}
		else
		{
			this.TB.Height = base.Height - 11;
		}
		this.TB.TextChanged += new EventHandler(this.OnBaseTextChanged);
		this.TB.KeyDown += new KeyEventHandler(this.OnBaseKeyDown);
	}

	private void OnBaseKeyDown(object s, KeyEventArgs e)
	{
		if ((!e.Control ? false : e.KeyCode == Keys.A))
		{
			this.TB.SelectAll();
			e.SuppressKeyPress = true;
		}
		if ((!e.Control ? false : e.KeyCode == Keys.C))
		{
			this.TB.Copy();
			e.SuppressKeyPress = true;
		}
	}

	private void OnBaseTextChanged(object s, EventArgs e)
	{
		this.Text = this.TB.Text;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();
		if (!base.Controls.Contains(this.TB))
		{
			base.Controls.Add(this.TB);
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
		this.TB.Focus();
		base.Invalidate();
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		base.Invalidate();
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.TB.Focus();
		base.Invalidate();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Helpers.B = new Bitmap(base.Width, base.Height);
		Helpers.G = Graphics.FromImage(Helpers.B);
		this.W = base.Width - 1;
		this.H = base.Height - 1;
		Rectangle rectangle = new Rectangle(0, 0, this.W, this.H);
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.PixelOffsetMode = PixelOffsetMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BackColor);
		this.TB.BackColor = this._BaseColor;
		this.TB.ForeColor = this._TextColor;
		Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
		base.OnPaint(e);
		Helpers.G.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
		Helpers.B.Dispose();
	}

	protected override void OnResize(EventArgs e)
	{
		this.TB.Location = new Point(5, 5);
		this.TB.Width = base.Width - 10;
		if (!this._Multiline)
		{
			base.Height = this.TB.Height + 11;
		}
		else
		{
			this.TB.Height = base.Height - 11;
		}
		base.OnResize(e);
	}
}