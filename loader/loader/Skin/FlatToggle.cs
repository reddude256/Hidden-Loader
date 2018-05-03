using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;

[DefaultEvent("CheckedChanged")]
internal class FlatToggle : Control
{
	private int W;

	private int H;

	private FlatToggle._Options O;

	private bool _Checked = false;

	private Color BaseColor = Helpers._FlatColor;

	private Color BaseColorRed = Color.FromArgb(220, 85, 96);

	private Color BGColor = Color.FromArgb(84, 85, 86);

	private Color ToggleColor = Color.FromArgb(45, 47, 49);

	private Color TextColor = Color.FromArgb(243, 243, 243);

	[Category("Options")]
	public bool Checked
	{
		get
		{
			return this._Checked;
		}
		set
		{
			this._Checked = value;
		}
	}

	[Category("Options")]
	public FlatToggle._Options Options
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

	public FlatToggle()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.BackColor = Color.Transparent;
		base.Size = new System.Drawing.Size(44, base.Height + 1);
		this.Cursor = Cursors.Hand;
		this.Font = new System.Drawing.Font("Segoe UI", 10f);
		base.Size = new System.Drawing.Size(76, 33);
	}

	protected override void OnClick(EventArgs e)
	{
		base.OnClick(e);
		this._Checked = !this._Checked;
		if (this.CheckedChanged != null)
		{
			this.CheckedChanged(this);
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

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		base.Invalidate();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Helpers.B = new Bitmap(base.Width, base.Height);
		Helpers.G = Graphics.FromImage(Helpers.B);
		this.W = base.Width - 1;
		this.H = base.Height - 1;
		GraphicsPath graphicsPath = new GraphicsPath();
		GraphicsPath graphicsPath1 = new GraphicsPath();
		Rectangle rectangle = new Rectangle(0, 0, this.W, this.H);
		Rectangle rectangle1 = new Rectangle(Convert.ToInt32(this.W / 2), 0, 38, this.H);
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.PixelOffsetMode = PixelOffsetMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BackColor);
		switch (this.O)
		{
			case FlatToggle._Options.Style1:
			{
				graphicsPath = Helpers.RoundRec(rectangle, 6);
				graphicsPath1 = Helpers.RoundRec(rectangle1, 6);
				Helpers.G.FillPath(new SolidBrush(this.BGColor), graphicsPath);
				Helpers.G.FillPath(new SolidBrush(this.ToggleColor), graphicsPath1);
				Helpers.G.DrawString("OFF", this.Font, new SolidBrush(this.BGColor), new Rectangle(19, 1, this.W, this.H), Helpers.CenterSF);
				if (this.Checked)
				{
					graphicsPath = Helpers.RoundRec(rectangle, 6);
					graphicsPath1 = Helpers.RoundRec(new Rectangle(Convert.ToInt32(this.W / 2), 0, 38, this.H), 6);
					Helpers.G.FillPath(new SolidBrush(this.ToggleColor), graphicsPath);
					Helpers.G.FillPath(new SolidBrush(this.BaseColor), graphicsPath1);
					Helpers.G.DrawString("ON", this.Font, new SolidBrush(this.BaseColor), new Rectangle(8, 7, this.W, this.H), Helpers.NearSF);
				}
				break;
			}
			case FlatToggle._Options.Style2:
			{
				graphicsPath = Helpers.RoundRec(rectangle, 6);
				rectangle1 = new Rectangle(4, 4, 36, this.H - 8);
				graphicsPath1 = Helpers.RoundRec(rectangle1, 4);
				Helpers.G.FillPath(new SolidBrush(this.BaseColorRed), graphicsPath);
				Helpers.G.FillPath(new SolidBrush(this.ToggleColor), graphicsPath1);
				Helpers.G.DrawLine(new Pen(this.BGColor), 18, 20, 18, 12);
				Helpers.G.DrawLine(new Pen(this.BGColor), 22, 20, 22, 12);
				Helpers.G.DrawLine(new Pen(this.BGColor), 26, 20, 26, 12);
				Helpers.G.DrawString("r", new System.Drawing.Font("Marlett", 8f), new SolidBrush(this.TextColor), new Rectangle(19, 2, base.Width, base.Height), Helpers.CenterSF);
				if (this.Checked)
				{
					graphicsPath = Helpers.RoundRec(rectangle, 6);
					rectangle1 = new Rectangle(Convert.ToInt32(this.W / 2) - 2, 4, 36, this.H - 8);
					graphicsPath1 = Helpers.RoundRec(rectangle1, 4);
					Helpers.G.FillPath(new SolidBrush(this.BaseColor), graphicsPath);
					Helpers.G.FillPath(new SolidBrush(this.ToggleColor), graphicsPath1);
					Helpers.G.DrawLine(new Pen(this.BGColor), Convert.ToInt32(this.W / 2) + 12, 20, Convert.ToInt32(this.W / 2) + 12, 12);
					Helpers.G.DrawLine(new Pen(this.BGColor), Convert.ToInt32(this.W / 2) + 16, 20, Convert.ToInt32(this.W / 2) + 16, 12);
					Helpers.G.DrawLine(new Pen(this.BGColor), Convert.ToInt32(this.W / 2) + 20, 20, Convert.ToInt32(this.W / 2) + 20, 12);
					Helpers.G.DrawString("�", new System.Drawing.Font("Wingdings", 14f), new SolidBrush(this.TextColor), new Rectangle(8, 7, base.Width, base.Height), Helpers.NearSF);
				}
				break;
			}
			case FlatToggle._Options.Style3:
			{
				graphicsPath = Helpers.RoundRec(rectangle, 16);
				rectangle1 = new Rectangle(this.W - 28, 4, 22, this.H - 8);
				graphicsPath1.AddEllipse(rectangle1);
				Helpers.G.FillPath(new SolidBrush(this.ToggleColor), graphicsPath);
				Helpers.G.FillPath(new SolidBrush(this.BaseColorRed), graphicsPath1);
				Helpers.G.DrawString("OFF", this.Font, new SolidBrush(this.BaseColorRed), new Rectangle(-12, 2, this.W, this.H), Helpers.CenterSF);
				if (this.Checked)
				{
					graphicsPath = Helpers.RoundRec(rectangle, 16);
					rectangle1 = new Rectangle(6, 4, 22, this.H - 8);
					graphicsPath1.Reset();
					graphicsPath1.AddEllipse(rectangle1);
					Helpers.G.FillPath(new SolidBrush(this.ToggleColor), graphicsPath);
					Helpers.G.FillPath(new SolidBrush(this.BaseColor), graphicsPath1);
					Helpers.G.DrawString("ON", this.Font, new SolidBrush(this.BaseColor), new Rectangle(12, 2, this.W, this.H), Helpers.CenterSF);
				}
				break;
			}
			case FlatToggle._Options.Style4:
			{
				if (this.Checked)
				{
				}
				break;
			}
			case FlatToggle._Options.Style5:
			{
				if (this.Checked)
				{
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
		base.Width = 76;
		base.Height = 33;
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		base.Invalidate();
	}

	public event FlatToggle.CheckedChangedEventHandler CheckedChanged;

	[Flags]
	public enum _Options
	{
		Style1,
		Style2,
		Style3,
		Style4,
		Style5
	}

	public delegate void CheckedChangedEventHandler(object sender);
}