using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FlatTabControl : TabControl
{
	private int W;

	private int H;

	private Color BGColor = Color.FromArgb(60, 70, 73);

	private Color _BaseColor = Color.FromArgb(45, 47, 49);

	private Color _ActiveColor = Helpers._FlatColor;

	[Category("Colors")]
	public Color ActiveColor
	{
		get
		{
			return this._ActiveColor;
		}
		set
		{
			this._ActiveColor = value;
		}
	}

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

	public FlatTabControl()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.BackColor = Color.FromArgb(60, 70, 73);
		this.Font = new System.Drawing.Font("Segoe UI", 10f);
		base.SizeMode = TabSizeMode.Fixed;
		base.ItemSize = new System.Drawing.Size(120, 40);
	}

	protected override void CreateHandle()
	{
		base.CreateHandle();
		base.Alignment = TabAlignment.Top;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Helpers.B = new Bitmap(base.Width, base.Height);
		Helpers.G = Graphics.FromImage(Helpers.B);
		this.W = base.Width - 1;
		this.H = base.Height - 1;
		Helpers.G.SmoothingMode = SmoothingMode.HighQuality;
		Helpers.G.PixelOffsetMode = PixelOffsetMode.HighQuality;
		Helpers.G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		Helpers.G.Clear(this._BaseColor);
		try
		{
			base.SelectedTab.BackColor = this.BGColor;
		}
		catch
		{
		}
		for (int i = 0; i < base.TabCount; i++)
		{
			Point location = base.GetTabRect(i).Location;
			int x = location.X + 2;
			location = base.GetTabRect(i).Location;
			Point point = new Point(x, location.Y);
			int width = base.GetTabRect(i).Width;
			Rectangle tabRect = base.GetTabRect(i);
			Rectangle rectangle = new Rectangle(point, new System.Drawing.Size(width, tabRect.Height));
			Rectangle rectangle1 = new Rectangle(rectangle.Location, new System.Drawing.Size(rectangle.Width, rectangle.Height));
			if (i != base.SelectedIndex)
			{
				Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle1);
				if (base.ImageList == null)
				{
					Graphics g = Helpers.G;
					string text = base.TabPages[i].Text;
					System.Drawing.Font font = this.Font;
					SolidBrush solidBrush = new SolidBrush(Color.White);
					RectangleF rectangleF = rectangle1;
					StringFormat stringFormat = new StringFormat()
					{
						LineAlignment = StringAlignment.Center,
						Alignment = StringAlignment.Center
					};
					g.DrawString(text, font, solidBrush, rectangleF, stringFormat);
				}
				else
				{
					try
					{
						if (base.ImageList.Images[base.TabPages[i].ImageIndex] == null)
						{
							Graphics graphic = Helpers.G;
							string str = base.TabPages[i].Text;
							System.Drawing.Font font1 = this.Font;
							SolidBrush solidBrush1 = new SolidBrush(Color.White);
							RectangleF rectangleF1 = rectangle1;
							StringFormat stringFormat1 = new StringFormat()
							{
								LineAlignment = StringAlignment.Center,
								Alignment = StringAlignment.Center
							};
							graphic.DrawString(str, font1, solidBrush1, rectangleF1, stringFormat1);
						}
						else
						{
							Graphics g1 = Helpers.G;
							Image item = base.ImageList.Images[base.TabPages[i].ImageIndex];
							location = rectangle1.Location;
							int num = location.X + 8;
							location = rectangle1.Location;
							g1.DrawImage(item, new Point(num, location.Y + 6));
							Graphics graphic1 = Helpers.G;
							string str1 = string.Concat("      ", base.TabPages[i].Text);
							System.Drawing.Font font2 = this.Font;
							SolidBrush solidBrush2 = new SolidBrush(Color.White);
							RectangleF rectangleF2 = rectangle1;
							StringFormat stringFormat2 = new StringFormat()
							{
								LineAlignment = StringAlignment.Center,
								Alignment = StringAlignment.Center
							};
							graphic1.DrawString(str1, font2, solidBrush2, rectangleF2, stringFormat2);
						}
					}
					catch (Exception exception)
					{
						throw new Exception(exception.Message);
					}
				}
			}
			else
			{
				Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle1);
				Helpers.G.FillRectangle(new SolidBrush(this._ActiveColor), rectangle1);
				if (base.ImageList == null)
				{
					Helpers.G.DrawString(base.TabPages[i].Text, this.Font, Brushes.White, rectangle1, Helpers.CenterSF);
				}
				else
				{
					try
					{
						if (base.ImageList.Images[base.TabPages[i].ImageIndex] == null)
						{
							Helpers.G.DrawString(base.TabPages[i].Text, this.Font, Brushes.White, rectangle1, Helpers.CenterSF);
						}
						else
						{
							Graphics g2 = Helpers.G;
							Image image = base.ImageList.Images[base.TabPages[i].ImageIndex];
							location = rectangle1.Location;
							int x1 = location.X + 8;
							location = rectangle1.Location;
							g2.DrawImage(image, new Point(x1, location.Y + 6));
							Helpers.G.DrawString(string.Concat("      ", base.TabPages[i].Text), this.Font, Brushes.White, rectangle1, Helpers.CenterSF);
						}
					}
					catch (Exception exception1)
					{
						throw new Exception(exception1.Message);
					}
				}
			}
		}
		base.OnPaint(e);
		Helpers.G.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
		Helpers.B.Dispose();
	}
}