using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FormSkin : ContainerControl
{
	private int W;

	private int H;

	private bool Cap = false;

	private bool _HeaderMaximize = false;

	private Point MousePoint = new Point(0, 0);

	private object MoveHeight = 50;

	private Color _HeaderColor = Color.FromArgb(45, 47, 49);

	private Color _BaseColor = Color.FromArgb(60, 70, 73);

	private Color _BorderColor = Color.FromArgb(53, 58, 60);

	private Color TextColor = Color.FromArgb(234, 234, 234);

	private Color _HeaderLight = Color.FromArgb(171, 171, 172);

	private Color _BaseLight = Color.FromArgb(196, 199, 200);

	public Color TextLight = Color.FromArgb(45, 47, 49);
    private FlatButton flatButton1;

	private bool EventsSubscribed = false;

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

	[Category("Colors")]
	public Color FlatColor
	{
		get
		{
			return Helpers._FlatColor;
		}
		set
		{
			Helpers._FlatColor = value;
		}
	}

	[Category("Colors")]
	public Color HeaderColor
	{
		get
		{
			return this._HeaderColor;
		}
		set
		{
			this._HeaderColor = value;
		}
	}

	[Category("Options")]
	public bool HeaderMaximize
	{
		get
		{
			return this._HeaderMaximize;
		}
		set
		{
			this._HeaderMaximize = value;
		}
	}

	public FormSkin()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.BackColor = Color.White;
		this.Font = new System.Drawing.Font("Segoe UI", 12f);
		this.SubscribeToEvents();
	}

	private void FormSkin_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		bool flag;
		if (this.HeaderMaximize)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Left)
			{
				flag = true;
			}
			else
			{
				Rectangle rectangle = new Rectangle(0, 0, base.Width, Convert.ToInt32(this.MoveHeight));
				flag = !rectangle.Contains(e.Location);
			}
			if (!flag)
			{
				if (base.FindForm().WindowState == FormWindowState.Normal)
				{
					base.FindForm().WindowState = FormWindowState.Maximized;
					base.FindForm().Refresh();
				}
				else if (base.FindForm().WindowState == FormWindowState.Maximized)
				{
					base.FindForm().WindowState = FormWindowState.Normal;
					base.FindForm().Refresh();
				}
			}
		}
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();
		base.ParentForm.FormBorderStyle = FormBorderStyle.None;
		base.ParentForm.AllowTransparency = false;
		base.ParentForm.TransparencyKey = Color.Fuchsia;
		base.ParentForm.FindForm().StartPosition = FormStartPosition.CenterScreen;
		this.Dock = DockStyle.Fill;
		base.Invalidate();
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		bool flag;
		base.OnMouseDown(e);
		if (e.Button != System.Windows.Forms.MouseButtons.Left)
		{
			flag = true;
		}
		else
		{
			Rectangle rectangle = new Rectangle(0, 0, base.Width, Convert.ToInt32(this.MoveHeight));
			flag = !rectangle.Contains(e.Location);
		}
		if (!flag)
		{
			this.Cap = true;
			this.MousePoint = e.Location;
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (this.Cap)
		{
			Control parent = base.Parent;
			Point mousePosition = Control.MousePosition;
			int x = mousePosition.X - this.MousePoint.X;
			mousePosition = Control.MousePosition;
			parent.Location = new Point(x, mousePosition.Y - this.MousePoint.Y);
		}
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.Cap = false;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Helpers.B = new Bitmap(base.Width, base.Height);
		Helpers.G = Graphics.FromImage(Helpers.B);
		this.W = base.Width;
		this.H = base.Height;
		Rectangle rectangle = new Rectangle(0, 0, this.W, this.H);
		Rectangle rectangle1 = new Rectangle(0, 0, this.W, 50);
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.PixelOffsetMode = PixelOffsetMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BackColor);
		Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
		Helpers.G.FillRectangle(new SolidBrush(this._HeaderColor), rectangle1);
		Helpers.G.FillRectangle(new SolidBrush(Color.FromArgb(243, 243, 243)), new Rectangle(8, 16, 4, 18));
		Helpers.G.FillRectangle(new SolidBrush(Helpers._FlatColor), 16, 16, 4, 18);
		Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this.TextColor), new Rectangle(26, 15, this.W, this.H), Helpers.NearSF);
		Helpers.G.DrawRectangle(new Pen(this._BorderColor), rectangle);
		base.OnPaint(e);
		Helpers.G.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
		Helpers.B.Dispose();
	}

	private void SubscribeToEvents()
	{
		if (!this.EventsSubscribed)
		{
			this.EventsSubscribed = true;
			base.MouseDoubleClick += new MouseEventHandler(this.FormSkin_MouseDoubleClick);
		}
	}

    private void InitializeComponent()
    {
            this.flatButton1 = new FlatButton();
            this.SuspendLayout();
            // 
            // flatButton1
            // 
            this.flatButton1.BackColor = System.Drawing.Color.Transparent;
            this.flatButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.flatButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flatButton1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.flatButton1.Location = new System.Drawing.Point(0, 0);
            this.flatButton1.Name = "flatButton1";
            this.flatButton1.Rounded = false;
            this.flatButton1.Size = new System.Drawing.Size(106, 32);
            this.flatButton1.TabIndex = 0;
            this.flatButton1.Text = "flatButton1";
            this.flatButton1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ResumeLayout(false);

    }
}