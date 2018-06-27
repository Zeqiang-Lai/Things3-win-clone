using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BorderlessForm.MyComponent
{
    [Serializable]
    public class MyAreaPanel : FlowLayoutPanel
    {
        private Color listNormalColor = Color.FromArgb(245, 246, 247);
        private Color listHoverColor = Color.FromArgb(225, 227, 231);

        MyListItem title;
        MyListItemTextBox tb;

        public List<string> items = new List<string>();

        public MyAreaPanel(FlowLayoutPanel area, string name = "")
        {
            this.FlowDirection = FlowDirection.TopDown;
            this.WrapContents = false;
            this.Size = new Size(167, 19);
            this.Margin = new Padding(0, 0, 0, 13);

            Title = new MyListItem(area, true);
            Title.LostFocus += titleLoseFocus;
            foreach (Control child in Title.Controls)
                child.MouseDown += titleMouseDown;
            Tb = Title.Tb;

            if (name != "")
            {
                title.ItemName = name;
                tb.Text = name;
            }

            this.Controls.Add(Title);
        }

        private void titleMouseDown(object sender, MouseEventArgs e)
        {
            Control s = (Control)sender;
            if (s.Parent.Focused) return;

            foreach (Control child in s.Parent.Controls)
                child.BackColor = listHoverColor;
            s.Parent.BackColor = listHoverColor;
            s.Parent.Focus();

            if (s is MyListItem)
                MainForm.lastFocus = s;
            else MainForm.lastFocus = s.Parent;
        }

        private void titleLoseFocus(object sender, EventArgs e)
        {
            Control s = (Control)sender;
            s.BackColor = listNormalColor;
            foreach (Control child in s.Controls)
                child.BackColor = listNormalColor;
        }
        internal MyListItemTextBox Tb { get => tb; set => tb = value; }
        internal MyListItem Title { get => title; set => title = value; }
    }
}
