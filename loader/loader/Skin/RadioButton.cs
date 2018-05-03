using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;

[DefaultEvent("CheckedChanged")]
internal class RadioButton : Control
{
	private MouseState State = MouseState.None;

	private int W;

	private int H;

	private RadioButton._Options O;

	private bool _Checked;

	private Color _BaseColor = Color.FromArgb(45, 47, 49);

	private Color _BorderColor = Helpers._FlatColor;

	private Color _TextColor = Color.FromArgb(243, 243, 243);

	public bool Checked
	{
		get
		{
			return this._Checked;
		}
		set
		{
			this._Checked = value;
			this.InvalidateControls();
			if (this.CheckedChanged != null)
			{
				this.CheckedChanged(this);
			}
			base.Invalidate();
		}
	}

	[Category("Options")]
	public RadioButton._Options Options
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

	public RadioButton()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.Cursor = Cursors.Hand;
		base.Size = new System.Drawing.Size(100, 22);
		this.BackColor = Color.FromArgb(60, 70, 73);
		this.Font = new System.Drawing.Font("Segoe UI", 10f);
	}

	private void InvalidateControls()
	{
		if ((!base.IsHandleCreated ? false : this._Checked))
		{
			foreach (Control control in base.Parent.Controls)
			{
				if ((control == this ? false : control is RadioButton))
				{
					((RadioButton)control).Checked = false;
					base.Invalidate();
				}
			}
		}
	}

	protected override void OnClick(EventArgs e)
	{
		if (!this._Checked)
		{
			this.Checked = true;
		}
		base.OnClick(e);
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();
		this.InvalidateControls();
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
		Rectangle rectangle1 = new Rectangle(4, 6, this.H - 12, this.H - 12);
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BackColor);
		switch (this.O)
		{
			case RadioButton._Options.Style1:
			{
				Helpers.G.FillEllipse(new SolidBrush(this._BaseColor), rectangle);
				switch (this.State)
				{
					case MouseState.Over:
					{
						Helpers.G.DrawEllipse(new Pen(this._BorderColor), rectangle);
						break;
					}
					case MouseState.Down:
					{
						Helpers.G.DrawEllipse(new Pen(this._BorderColor), rectangle);
						break;
					}
				}
				if (this.Checked)
				{
					Helpers.G.FillEllipse(new SolidBrush(this._BorderColor), rectangle1);
				}
				break;
			}
			case RadioButton._Options.Style2:
			{
				Helpers.G.FillEllipse(new SolidBrush(this._BaseColor), rectangle);
				switch (this.State)
				{
					case MouseState.Over:
					{
						Helpers.G.DrawEllipse(new Pen(this._BorderColor), rectangle);
						Helpers.G.FillEllipse(new SolidBrush(Color.FromArgb(118, 213, 170)), rectangle);
						break;
					}
					case MouseState.Down:
					{
						Helpers.G.DrawEllipse(new Pen(this._BorderColor), rectangle);
						Helpers.G.FillEllipse(new SolidBrush(Color.FromArgb(118, 213, 170)), rectangle);
						break;
					}
				}
				if (this.Checked)
				{
					Helpers.G.FillEllipse(new SolidBrush(this._BorderColor), rectangle1);
				}
				break;
			}
		}
		Helpers.G.DrawString(this.Text, this.Font, new SolidBrush(this._TextColor), new Rectangle(20, 2, this.W, this.H), Helpers.NearSF);
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

	public event RadioButton.CheckedChangedEventHandler CheckedChanged;

	[Flags]
	public enum _Options
	{
		Style1,
		Style2
	}

	public delegate void CheckedChangedEventHandler(object sender);
}