﻿/*
ImageGlass Project - Image viewer for Windows
Copyright (C) 2019 DUONG DIEU PHAP
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

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImageGlass
{
    /// <summary>
    /// Common functionality for floating 'tool' windows
    /// </summary>
    public class ToolForm : Form
    {
        protected Form _currentOwner;


        #region Borderless form moving

        private bool mouseDown; // moving windows is taking place
        private Point lastLocation; // initial mouse position
        private bool moveSnapped; // move toolform windows together

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
                mouseDown = true;
            if (ModifierKeys == Keys.Control)
                moveSnapped = true;

            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown) return; // not moving windows, ignore

            if (moveSnapped)
            {
                _manager.MoveSnappedTools(lastLocation, e.Location);
            }
            else
            {
                Location = new Point((Location.X - lastLocation.X) + e.X, 
                    (Location.Y - lastLocation.Y) + e.Y);

                Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            moveSnapped = false;
        }

        #endregion

        #region Create shadow for borderless form

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        public const int CS_DROPSHADOW = 0x00020000;
        public const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        protected bool m_aeroEnabled;              // variables for box shadow

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        protected bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT: // box shadow
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);

                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };

                        DwmExtendFrameIntoClientArea(Handle, ref margins);
                    }
                    break;
                default:
                    break;
            }

            base.WndProc(ref m);
        }
        #endregion

        #region Properties to make a tool window

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;
                baseParams.ExStyle |= 0x8000000 // WS_EX_NOACTIVATE
                                      | 0x00000080;   // WS_EX_TOOLWINDOW


                #region Shadow for Borderless form
                m_aeroEnabled = CheckAeroEnabled();

                if (!m_aeroEnabled)
                    baseParams.ClassStyle |= CS_DROPSHADOW;
                #endregion


                return baseParams;
            }
        }

        #endregion

        #region Events to manage the form location relative to parent

        protected Point parentOffset = Point.Empty;
        private bool formOwnerMoving;
        protected Point _locationOffset;


        private void _AttachEventsToParent(Form frmOwner)
        {
            if (frmOwner == null)
                return;

            frmOwner.Move += Owner_Move;
            frmOwner.SizeChanged += Owner_Move;
            frmOwner.VisibleChanged += Owner_Move;
            frmOwner.LocationChanged += FrmOwner_LocationChanged;
        }


        private void FrmOwner_LocationChanged(object sender, EventArgs e)
        {
            formOwnerMoving = false;
        }


        private void _DetachEventsFromParent(Form frmOwner)
        {
            if (frmOwner == null)
                return;

            frmOwner.Move -= Owner_Move;
            frmOwner.SizeChanged -= Owner_Move;
            frmOwner.VisibleChanged -= Owner_Move;
            frmOwner.LocationChanged -= FrmOwner_LocationChanged;
        }


        private void Owner_Move(object sender, EventArgs e)
        {
            if (Owner == null) return;

            formOwnerMoving = true;

            _SetLocationBasedOnParent();
        }


        // The tool windows itself has moved; track its location relative to parent
        private void ToolForm_Move(object sender, EventArgs e)
        {
            if (!formOwnerMoving)
            {
                _locationOffset = new Point(Left - Owner.Left, Top - Owner.Top);
                parentOffset = _locationOffset;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            if (Owner != _currentOwner)
            {
                _DetachEventsFromParent(_currentOwner);
                _currentOwner = Owner;
                _AttachEventsToParent(_currentOwner);
            }
            
            base.OnShown(e);
        }


        protected void _SetLocationBasedOnParent()
        {
            if (Owner == null)
                return;

            if (Owner.WindowState == FormWindowState.Minimized || !Owner.Visible)
            {
                Visible = false;
                return;
            }

            // set location based on the main form
            Point ownerLocation = Owner.Location;
            ownerLocation.Offset(parentOffset);

            Location = ownerLocation;
        }

        #endregion


        /// <summary>
        /// Apply theme colors to controls
        /// </summary>
        internal void SetColors()
        {
            var bColor = LocalSetting.Theme.BackgroundColor;
            var fColor = Theme.Theme.InvertBlackAndWhiteColor(bColor);

            foreach (Control control in Controls)
            {
                if (control is Button button)
                {
                    button.FlatAppearance.BorderColor = bColor;
                }

                if (control is Label ||
                    control is TextBox ||
                    control is Button)
                {
                    control.BackColor = bColor;
                    control.ForeColor = fColor;
                }
            }

            BackColor = bColor;
        }


        #region ToolForm "Snap" support
        private ToolFormManager _manager;
        public void SetToolFormManager(ToolFormManager manager) 
        {
            _manager = manager;
            _manager.Add(this);
        }


        internal void SnapButton_Click(object sender, EventArgs e)
        {
            _manager.SnapToNearest(this);
        }
        #endregion


        // Initialize all event handlers required to manage
        // borderless window movement.
        internal void RegisterToolFormEvents()
        {
            Move += ToolForm_Move;

            MouseDown += Form1_MouseDown;
            MouseUp += Form1_MouseUp;
            MouseMove += Form1_MouseMove;

            foreach (Control control in Controls)
            {
                if (control is Label ||
                    control is Panel)
                {
                    control.MouseDown += Form1_MouseDown;
                    control.MouseUp += Form1_MouseUp;
                    control.MouseMove += Form1_MouseMove;
                }
            }
        }
    }
}