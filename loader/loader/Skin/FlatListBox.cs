using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FlatListBox : Control
{
	private ListBox ListBx = new ListBox();

	private string[] _items = new string[] { "" };

	private Color BaseColor = Color.FromArgb(45, 47, 49);

	private Color _SelectedColor = Helpers._FlatColor;

	private bool EventsSubscribed = false;

	[Category("Options")]
	public string[] items
	{
		get
		{
			return this._items;
		}
		set
		{
			this._items = value;
			this.ListBx.Items.Clear();
			this.ListBx.Items.AddRange(value);
			base.Invalidate();
		}
	}

	[Category("Colors")]
	public Color SelectedColor
	{
		get
		{
			return this._SelectedColor;
		}
		set
		{
			this._SelectedColor = value;
		}
	}

	public int SelectedIndex
	{
		get
		{
			int num;
			num = (this.ListBx.SelectedIndex >= 0 ? this.ListBx.SelectedIndex : 0);
			return num;
		}
	}

	public string SelectedItem
	{
		get
		{
			return this.ListBx.SelectedItem.ToString();
		}
	}

	public FlatListBox()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.ListBx.DrawMode = DrawMode.OwnerDrawFixed;
		this.ListBx.ScrollAlwaysVisible = false;
		this.ListBx.HorizontalScrollbar = false;
		this.ListBx.BorderStyle = BorderStyle.None;
		this.ListBx.BackColor = this.BaseColor;
		this.ListBx.ForeColor = Color.White;
		this.ListBx.Location = new Point(3, 3);
		this.ListBx.Font = new System.Drawing.Font("Segoe UI", 8f);
		this.ListBx.ItemHeight = 20;
		this.ListBx.Items.Clear();
		this.ListBx.IntegralHeight = false;
		base.Size = new System.Drawing.Size(131, 101);
		this.BackColor = this.BaseColor;
		this.SubscribeToEvents();
	}

	public void AddItem(object item)
	{
		this.ListBx.Items.Remove("");
		this.ListBx.Items.Add(item);
	}

	public void AddRange(object[] items)
	{
		this.ListBx.Items.Remove("");
		this.ListBx.Items.AddRange(items);
	}

	public void Clear()
	{
		this.ListBx.Items.Clear();
	}

	public void ClearSelected()
	{
		for (int i = this.ListBx.SelectedItems.Count - 1; i >= 0; i--)
		{
			this.ListBx.Items.Remove(this.ListBx.SelectedItems[i]);
		}
	}

	public bool Contains(string text)
	{
		bool flag;
		flag = (!this.ListBx.Items.Contains(text) ? false : true);
		return flag;
	}

	public int count()
	{
		return this.ListBx.Items.Count;
	}

	public void Drawitem(object sender, DrawItemEventArgs e)
	{
		Rectangle bounds;
		if (e.Index >= 0)
		{
			e.DrawBackground();
			e.DrawFocusRectangle();
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			if (e.State.ToString().IndexOf("Selected,") + 1 <= 0)
			{
				Graphics graphics = e.Graphics;
				SolidBrush solidBrush = new SolidBrush(Color.FromArgb(51, 53, 55));
				int x = e.Bounds.X;
				int y = e.Bounds.Y;
				int width = e.Bounds.Width;
				bounds = e.Bounds;
				graphics.FillRectangle(solidBrush, new Rectangle(x, y, width, bounds.Height));
				Graphics graphic = e.Graphics;
				string str = string.Concat(" ", this.ListBx.Items[e.Index].ToString());
				System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 8f);
				Brush white = Brushes.White;
				float single = (float)e.Bounds.X;
				bounds = e.Bounds;
				graphic.DrawString(str, font, white, single, (float)(bounds.Y + 2));
			}
			else
			{
				Graphics graphics1 = e.Graphics;
				SolidBrush solidBrush1 = new SolidBrush(this._SelectedColor);
				int num = e.Bounds.X;
				int y1 = e.Bounds.Y;
				int width1 = e.Bounds.Width;
				bounds = e.Bounds;
				graphics1.FillRectangle(solidBrush1, new Rectangle(num, y1, width1, bounds.Height));
				Graphics graphic1 = e.Graphics;
				string str1 = string.Concat(" ", this.ListBx.Items[e.Index].ToString());
				System.Drawing.Font font1 = new System.Drawing.Font("Segoe UI", 8f);
				Brush brush = Brushes.White;
				float x1 = (float)e.Bounds.X;
				bounds = e.Bounds;
				graphic1.DrawString(str1, font1, brush, x1, (float)(bounds.Y + 2));
			}
			e.Graphics.Dispose();
		}
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();
		if (!base.Controls.Contains(this.ListBx))
		{
			base.Controls.Add(this.ListBx);
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Helpers.B = new Bitmap(base.Width, base.Height);
		Helpers.G = Graphics.FromImage(Helpers.B);
		Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height);
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.PixelOffsetMode = PixelOffsetMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this.BackColor);
		this.ListBx.Size = new System.Drawing.Size(base.Width - 6, base.Height - 2);
		Helpers.G.FillRectangle(new SolidBrush(this.BaseColor), rectangle);
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
			this.ListBx.DrawItem += new DrawItemEventHandler(this.Drawitem);
		}
	}
}