using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FlatAlertBox : Control
{
	private int W;

	private int H;

	private FlatAlertBox._Kind K;

	private string _Text;

	private MouseState State = MouseState.None;

	private int X;

	private Timer T;

	private Color SuccessColor = Color.FromArgb(60, 85, 79);

	private Color SuccessText = Color.FromArgb(35, 169, 110);

	private Color ErrorColor = Color.FromArgb(87, 71, 71);

	private Color ErrorText = Color.FromArgb(254, 142, 122);

	private Color InfoColor = Color.FromArgb(70, 91, 94);

	private Color InfoText = Color.FromArgb(97, 185, 186);

	[Category("Options")]
	public FlatAlertBox._Kind kind
	{
		get
		{
			return this.K;
		}
		set
		{
			this.K = value;
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
			if (this._Text != null)
			{
				this._Text = value;
			}
		}
	}

	[Category("Options")]
	public new bool Visible
	{
		get
		{
			return !base.Visible;
		}
		set
		{
			base.Visible = value;
		}
	}

	public FlatAlertBox()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.BackColor = Color.FromArgb(60, 70, 73);
		base.Size = new System.Drawing.Size(576, 42);
		base.Location = new Point(10, 61);
		this.Font = new System.Drawing.Font("Segoe UI", 10f);
		this.Cursor = Cursors.Hand;
	}

	protected override void OnClick(EventArgs e)
	{
		base.OnClick(e);
		this.Visible = false;
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
		this.X = e.X;
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
		Rectangle rectangle = new Rectangle(0, 0, this.W, this.H);
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BackColor);
		switch (this.K)
		{
			case FlatAlertBox._Kind.Success:
			{
				Helpers.G.FillRectangle(new SolidBrush(this.SuccessColor), rectangle);
				Helpers.G.FillEllipse(new SolidBrush(this.SuccessText), new Rectangle(8, 9, 24, 24));
				Helpers.G.FillEllipse(new SolidBrush(this.SuccessColor), new Rectangle(10, 11, 20, 20));
				Helpers.G.DrawString("�", new System.Drawing.Font("Wingdings", 22f), new SolidBrush(this.SuccessText), new Rectangle(7, 7, this.W, this.H), Helpers.NearSF);
				Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this.SuccessText), new Rectangle(48, 12, this.W, this.H), Helpers.NearSF);
				Helpers.G.FillEllipse(new SolidBrush(Color.FromArgb(35, Color.Black)), new Rectangle(this.W - 30, this.H - 29, 17, 17));
				Helpers.G.DrawString("r", new System.Drawing.Font("Marlett", 8f), new SolidBrush(this.SuccessColor), new Rectangle(this.W - 28, 16, this.W, this.H), Helpers.NearSF);
				if (this.State == MouseState.Over)
				{
					Helpers.G.DrawString("r", new System.Drawing.Font("Marlett", 8f), new SolidBrush(Color.FromArgb(25, Color.White)), new Rectangle(this.W - 28, 16, this.W, this.H), Helpers.NearSF);
				}
				break;
			}
			case FlatAlertBox._Kind.Error:
			{
				Helpers.G.FillRectangle(new SolidBrush(this.ErrorColor), rectangle);
				Helpers.G.FillEllipse(new SolidBrush(this.ErrorText), new Rectangle(8, 9, 24, 24));
				Helpers.G.FillEllipse(new SolidBrush(this.ErrorColor), new Rectangle(10, 11, 20, 20));
				Helpers.G.DrawString("r", new System.Drawing.Font("Marlett", 16f), new SolidBrush(this.ErrorText), new Rectangle(6, 11, this.W, this.H), Helpers.NearSF);
				Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this.ErrorText), new Rectangle(48, 12, this.W, this.H), Helpers.NearSF);
				Helpers.G.FillEllipse(new SolidBrush(Color.FromArgb(35, Color.Black)), new Rectangle(this.W - 32, this.H - 29, 17, 17));
				Helpers.G.DrawString("r", new System.Drawing.Font("Marlett", 8f), new SolidBrush(this.ErrorColor), new Rectangle(this.W - 30, 17, this.W, this.H), Helpers.NearSF);
				if (this.State == MouseState.Over)
				{
					Helpers.G.DrawString("r", new System.Drawing.Font("Marlett", 8f), new SolidBrush(Color.FromArgb(25, Color.White)), new Rectangle(this.W - 30, 15, this.W, this.H), Helpers.NearSF);
				}
				break;
			}
			case FlatAlertBox._Kind.Info:
			{
				Helpers.G.FillRectangle(new SolidBrush(this.InfoColor), rectangle);
				Helpers.G.FillEllipse(new SolidBrush(this.InfoText), new Rectangle(8, 9, 24, 24));
				Helpers.G.FillEllipse(new SolidBrush(this.InfoColor), new Rectangle(10, 11, 20, 20));
				Helpers.G.DrawString("?", new System.Drawing.Font("Segoe UI", 20f, FontStyle.Bold), new SolidBrush(this.InfoText), new Rectangle(12, -4, this.W, this.H), Helpers.NearSF);
				Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this.InfoText), new Rectangle(48, 12, this.W, this.H), Helpers.NearSF);
				Helpers.G.FillEllipse(new SolidBrush(Color.FromArgb(35, Color.Black)), new Rectangle(this.W - 32, this.H - 29, 17, 17));
				Helpers.G.DrawString("r", new System.Drawing.Font("Marlett", 8f), new SolidBrush(this.InfoColor), new Rectangle(this.W - 30, 17, this.W, this.H), Helpers.NearSF);
				if (this.State == MouseState.Over)
				{
					Helpers.G.DrawString("r", new System.Drawing.Font("Marlett", 8f), new SolidBrush(Color.FromArgb(25, Color.White)), new Rectangle(this.W - 30, 17, this.W, this.H), Helpers.NearSF);
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
		base.Height = 42;
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		base.Invalidate();
	}

	public void ShowControl(FlatAlertBox._Kind Kind, string Str, int Interval)
	{
		this.K = Kind;
		this.Text = Str;
		this.Visible = true;
		this.T = new Timer()
		{
			Interval = Interval,
			Enabled = true
		};
	}

	private void T_Tick(object sender, EventArgs e)
	{
		this.Visible = false;
		this.T.Enabled = false;
		this.T.Dispose();
	}

	[Flags]
	public enum _Kind
	{
		Success,
		Error,
		Info
	}
}