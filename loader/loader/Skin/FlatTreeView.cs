using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

internal class FlatTreeView : TreeView
{
	private Color _BaseColor = Color.FromArgb(45, 47, 49);

	private Color _LineColor = Color.FromArgb(25, 27, 29);

	public FlatTreeView()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.BackColor = this._BaseColor;
		this.ForeColor = Color.White;
		base.LineColor = this._LineColor;
		base.DrawMode = TreeViewDrawMode.OwnerDrawAll;
	}

	protected override void OnDrawNode(DrawTreeNodeEventArgs e)
	{
		try
		{
			Point location = e.Bounds.Location;
			int x = location.X;
			location = e.Bounds.Location;
			int y = location.Y;
			int width = e.Bounds.Width;
			Rectangle bounds = e.Bounds;
			Rectangle rectangle = new Rectangle(x, y, width, bounds.Height);
		}
		catch (Exception exception)
		{
			MessageBox.Show(exception.Message);
		}
		base.OnDrawNode(e);
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
		Helpers.G.FillRectangle(new SolidBrush(this._BaseColor), rectangle);
		Graphics g = Helpers.G;
		string text = this.Text;
		System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 8f);
		Brush black = Brushes.Black;
		Rectangle bounds = base.Bounds;
		int x = bounds.X + 2;
		bounds = base.Bounds;
		int y = bounds.Y + 2;
		int width = base.Bounds.Width;
		bounds = base.Bounds;
		g.DrawString(text, font, black, new Rectangle(x, y, width, bounds.Height), Helpers.NearSF);
		base.OnPaint(e);
		Helpers.G.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
		Helpers.B.Dispose();
	}
}