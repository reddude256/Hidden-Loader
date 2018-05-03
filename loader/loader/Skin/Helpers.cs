using System;
using System.Drawing;
using System.Drawing.Drawing2D;

internal static class Helpers
{
	internal static Graphics G;

	internal static Bitmap B;

	internal static Color _FlatColor;

	internal static StringFormat NearSF;

	internal static StringFormat CenterSF;

	static Helpers()
	{
		Helpers._FlatColor = Color.FromArgb(35, 168, 109);
		StringFormat stringFormat = new StringFormat()
		{
			Alignment = StringAlignment.Near,
			LineAlignment = StringAlignment.Near
		};
		Helpers.NearSF = stringFormat;
		StringFormat stringFormat1 = new StringFormat()
		{
			Alignment = StringAlignment.Center,
			LineAlignment = StringAlignment.Center
		};
		Helpers.CenterSF = stringFormat1;
	}

	public static GraphicsPath DrawArrow(int x, int y, bool flip)
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		int num = 12;
		int num1 = 6;
		if (!flip)
		{
			graphicsPath.AddLine(x, y + num1, x + num, y + num1);
			graphicsPath.AddLine(x + num, y + num1, x + num1, y);
		}
		else
		{
			graphicsPath.AddLine(x + 1, y, x + num + 1, y);
			graphicsPath.AddLine(x + num, y, x + num1, y + num1 - 1);
		}
		graphicsPath.CloseFigure();
		return graphicsPath;
	}

	public static GraphicsPath RoundRec(System.Drawing.Rectangle Rectangle, int Curve)
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		int curve = Curve * 2;
		graphicsPath.AddArc(new System.Drawing.Rectangle(Rectangle.X, Rectangle.Y, curve, curve), -180f, 90f);
		graphicsPath.AddArc(new System.Drawing.Rectangle(Rectangle.Width - curve + Rectangle.X, Rectangle.Y, curve, curve), -90f, 90f);
		graphicsPath.AddArc(new System.Drawing.Rectangle(Rectangle.Width - curve + Rectangle.X, Rectangle.Height - curve + Rectangle.Y, curve, curve), 0f, 90f);
		graphicsPath.AddArc(new System.Drawing.Rectangle(Rectangle.X, Rectangle.Height - curve + Rectangle.Y, curve, curve), 90f, 90f);
		graphicsPath.AddLine(new Point(Rectangle.X, Rectangle.Height - curve + Rectangle.Y), new Point(Rectangle.X, Curve + Rectangle.Y));
		return graphicsPath;
	}

	public static GraphicsPath RoundRect(float x, float y, float w, float h, float r = 0.3f, bool TL = true, bool TR = true, bool BR = true, bool BL = true)
	{
		GraphicsPath graphicsPath = null;
		float single = Math.Min(w, h) * r;
		float single1 = x + w;
		float single2 = y + h;
		graphicsPath = new GraphicsPath();
		if (!TL)
		{
			graphicsPath.AddLine(x, y, x, y);
		}
		else
		{
			graphicsPath.AddArc(x, y, single, single, 180f, 90f);
		}
		if (!TR)
		{
			graphicsPath.AddLine(single1, y, single1, y);
		}
		else
		{
			graphicsPath.AddArc(single1 - single, y, single, single, 270f, 90f);
		}
		if (!BR)
		{
			graphicsPath.AddLine(single1, single2, single1, single2);
		}
		else
		{
			graphicsPath.AddArc(single1 - single, single2 - single, single, single, 0f, 90f);
		}
		if (!BL)
		{
			graphicsPath.AddLine(x, single2, x, single2);
		}
		else
		{
			graphicsPath.AddArc(x, single2 - single, single, single, 90f, 90f);
		}
		graphicsPath.CloseFigure();
		return graphicsPath;
	}
}