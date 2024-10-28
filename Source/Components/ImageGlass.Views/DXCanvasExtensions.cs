/*
ImageGlass Project - Image viewer for Windows
Copyright (C) 2010 - 2024 DUONG DIEU PHAP
Project homepage: https://imageglass.org

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using ImageGlass.Base.PhotoBox;

namespace ImageGlass.Viewer;


public static class DXCanvasExtensions
{

    /// <summary>
    /// Checks if the input point is inside the navigation buttons.
    /// </summary>
    public static MouseAndNavLocation CheckWhichNav(this DXCanvas c, Point point,
        NavCheck navCheck = NavCheck.Both)
    {
        var isLocationInNavLeft = false;
        var isLocationInNavRight = false;

        if (c.Width < c.NavButtonSize.Width * 2)
        {
            return MouseAndNavLocation.Outside;
        }


        if (c.NavDisplay == NavButtonDisplay.Left || c.NavDisplay == NavButtonDisplay.Both)
        {
            if (navCheck == NavCheck.Both || navCheck == NavCheck.RightOnly)
            {
                // right clickable region
                var rightClickable = new RectangleF(
                    c.NavRightPos.X - c.NavButtonSize.Width / 2,
                    c.DrawingArea.Top,
                    c.NavButtonSize.Width + DXCanvas.NAV_PADDING,
                    c.DrawingArea.Height);

                // check if the point inside the rect;
                isLocationInNavRight = rightClickable.Contains(point);
            }
        }


        if (c.NavDisplay == NavButtonDisplay.Right || c.NavDisplay == NavButtonDisplay.Both)
        {
            if (navCheck == NavCheck.Both || navCheck == NavCheck.LeftOnly)
            {
                // left clickable region
                var leftClickable = new RectangleF(
                    c.NavLeftPos.X - c.NavButtonSize.Width / 2 - DXCanvas.NAV_PADDING,
                    c.DrawingArea.Top,
                    c.NavButtonSize.Width + DXCanvas.NAV_PADDING,
                    c.DrawingArea.Height);

                // check if the point inside the rect
                isLocationInNavLeft = leftClickable.Contains(point);
            }
        }


        if (isLocationInNavLeft && isLocationInNavRight)
        {
            return MouseAndNavLocation.BothNavs;
        }

        if (isLocationInNavLeft)
        {
            return MouseAndNavLocation.LeftNav;
        }

        if (isLocationInNavRight)
        {
            return MouseAndNavLocation.RightNav;
        }

        return MouseAndNavLocation.Outside;
    }


    /// <summary>
    /// Computes the location of the client point into image source coords.
    /// </summary>
    public static PointF PointClientToSource(this DXCanvas c, PointF clientPoint)
    {
        var x = (clientPoint.X - c.ImageDestBounds.X) / c.ZoomFactor + c.ImageSourceBounds.X;
        var y = (clientPoint.Y - c.ImageDestBounds.Y) / c.ZoomFactor + c.ImageSourceBounds.Y;

        return new PointF(x, y);
    }


    /// <summary>
    /// Computes and scale the rectangle of the client to image source coords
    /// </summary>
    public static RectangleF RectClientToSource(this DXCanvas c, RectangleF rect)
    {
        var p1 = c.PointClientToSource(rect.Location);
        var p2 = c.PointClientToSource(new PointF(rect.Right, rect.Bottom));


        // get the min int value
        var floorP1 = new PointF(
            (float)Math.Floor(Math.Round(p1.X, 1)),
            (float)Math.Floor(Math.Round(p1.Y, 1)));
        if (floorP1.X < 0) floorP1.X = 0;
        if (floorP1.Y < 0) floorP1.Y = 0;
        if (floorP1.X > c.SourceWidth) floorP1.X = c.SourceWidth;
        if (floorP1.Y > c.SourceHeight) floorP1.Y = c.SourceHeight;

        if (p1 == p2)
        {
            return new RectangleF(floorP1, new SizeF(0, 0));
        }


        // get the max int value
        var ceilP2 = new PointF(
            (float)Math.Ceiling(Math.Round(p2.X, 1)),
            (float)Math.Ceiling(Math.Round(p2.Y, 1)));
        if (ceilP2.X < 0) ceilP2.X = 0;
        if (ceilP2.Y < 0) ceilP2.Y = 0;
        if (ceilP2.X > c.SourceWidth) ceilP2.X = c.SourceWidth;
        if (ceilP2.Y > c.SourceHeight) ceilP2.Y = c.SourceHeight;


        // the selection area is where the p1 and p2 intersected.
        return new RectangleF(
            floorP1,
            new SizeF(ceilP2.X - floorP1.X, ceilP2.Y - floorP1.Y));
    }


    /// <summary>
    /// Computes the location of the image source point into client coords.
    /// </summary>
    public static PointF PointSourceToClient(this DXCanvas c, PointF srcPoint)
    {
        var x = (srcPoint.X - c.ImageSourceBounds.X) * c.ZoomFactor + c.ImageDestBounds.X;
        var y = (srcPoint.Y - c.ImageSourceBounds.Y) * c.ZoomFactor + c.ImageDestBounds.Y;

        return new PointF(x, y);
    }


    /// <summary>
    /// Computes and scale the rectangle of the image source to client coords
    /// </summary>
    public static RectangleF RectSourceToClient(this DXCanvas c, RectangleF rect)
    {
        var loc = c.PointSourceToClient(rect.Location);
        var size = new SizeF(rect.Width * c.ZoomFactor, rect.Height * c.ZoomFactor);

        return new RectangleF(loc, size);
    }
}