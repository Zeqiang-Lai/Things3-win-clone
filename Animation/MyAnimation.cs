using BorderlessForm.MyComponent;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BorderlessForm.Animation
{
    class MyAnimation
    {
        static private Color listNormalColor = Color.FromArgb(245, 246, 247);
        static private Color listHoverColor = Color.FromArgb(225, 227, 231);

        static Color itemNormalColor = MyColor.contentBackColor;
        static Color itemHoverColor = Color.FromArgb(235, 235, 235);

        public enum MouseState
        {
            Normal,
            Hover,
            Down
        }

        static public void SetListBtnColor(Panel control)
        {
            control.MouseEnter += (s, e) => SetListColors((Control)s, MouseState.Hover);
            control.MouseLeave += (s, e) => SetListColors((Control)s, MouseState.Normal);
            //control.MouseDown += (s, e) => SetListColors((Control)s, MouseState.Hover);
            foreach (Control childControl in control.Controls)
            {
                childControl.MouseEnter += (s, e) => SetListColors(((Control)s).Parent, MouseState.Hover);
              //  childControl.MouseEnter += (s, e) => SetListColors((Control)s, MouseState.Hover);
                childControl.MouseLeave += (s, e) => SetListColors(((Control)s).Parent, MouseState.Normal);
              //  childControl.MouseLeave += (s, e) => SetListColors((Control)s, MouseState.Normal);
            }
        }
        static public void SetListColors(Control control, MouseState state)
        {
            //if (!control.ContainsFocus) return;
            Color backColor = listNormalColor;
            if (state == MouseState.Hover) backColor = listHoverColor;
            control.BackColor = backColor;
            foreach (Control child in control.Controls)
                child.BackColor = backColor;
        }

        static public void AddMouseDownEvent(Control control)
        {
            if (control.Name == "panel12") return;
            
            control.MouseDown += (s, e) => SetGetFocus((Control)s);
            foreach (Control child in control.Controls)
            {
                AddMouseDownEvent(child);
            }
        }

        static public void SetGetFocus(Control control)
        {
            control.Focus();
        }

        static public void SetbuttomBarBtnColor(Panel control)
        {
            control.MouseEnter += (s, e) => SetBorder((Control)s, MouseState.Hover);
            control.MouseLeave += (s, e) => SetBorder((Control)s, MouseState.Normal);
            control.MouseDown += (s, e) => SetBorder((Control)s, MouseState.Normal);
            foreach (Control childControl in control.Controls)
            {
                childControl.MouseEnter += (s, e) => SetBorder(((Control)s).Parent, MouseState.Hover);
                childControl.MouseLeave += (s, e) => SetBorder(((Control)s).Parent, MouseState.Normal);
                control.MouseDown += (s, e) => SetBorder(((Control)s).Parent, MouseState.Normal);
            }
        }

        static public void SetListItemColor(MyTodoItem control)
        {
            control.MouseEnter += (s, e) => SetListItem((Control)s, MouseState.Hover);
            control.MouseLeave += (s, e) => SetListItem((Control)s, MouseState.Normal);
            control.MouseDown += (s, e) => SetItemDown((Control)s, MouseState.Down,e);
        }

        private static void SetItemDown(Control s, MouseState down, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
                SetListItem((Control)s, MouseState.Down);
        }

        public static void SetListItem(Control s, MouseState state)
        {
            Color backColor = itemHoverColor;
            if (state == MouseState.Normal) backColor = itemNormalColor;
            s.BackColor = backColor;
            foreach (Control child in s.Controls)
                child.BackColor = backColor;
        }

        private static void SetBorder(Control control, MouseState state)
        {
            Color backColor = Color.FromArgb(250, 249, 250);
            if (state == MouseState.Hover) backColor = listHoverColor;
            control.Parent.BackColor = backColor;
        }


    }
}
