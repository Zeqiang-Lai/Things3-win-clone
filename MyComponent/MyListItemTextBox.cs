using BorderlessForm.Interaction;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BorderlessForm.MyComponent
{
    class MyListItemTextBox : TextBox
    {
        private Color listNormalColor = Color.FromArgb(245, 246, 247);
        private Color listHoverColor = Color.FromArgb(225, 227, 231);
        FlowLayoutPanel userAreaPanel;
        Label textLbl;
        bool isArea;

        public MyListItemTextBox(FlowLayoutPanel area,Label lbl, bool is_Area)
        {
            userAreaPanel = area;
            textLbl = lbl;
            isArea = is_Area;

            this.Location = new Point(21, 2);
            this.Size = new Size(100, 17);
            this.BorderStyle = BorderStyle.None;
            this.Font = new Font("Corbel", 9.75f, FontStyle.Bold);

            this.BackColor = listHoverColor;
            this.KeyPress += textBoxKeyPress;
            this.GotFocus += textBoxGetfocus;
            this.LostFocus += textBoxLosefocus;
        }

        private void textBoxLosefocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb.Text == string.Empty)
            {
                tb.ForeColor = Color.FromArgb(204, 202, 204);
                tb.Text = "New List";
                if (isArea) tb.Text = "New Area";
            }

            tb.Parent.BackColor = listNormalColor;
            foreach (Control child in tb.Parent.Controls)
                child.BackColor = listNormalColor;
            EditComplete(tb);
        }
        private void textBoxGetfocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (tb.Text == "New Area" || tb.Text == "New List")
            {
                tb.ForeColor = Color.Black;
                tb.Text = string.Empty;
            }

            tb.Parent.BackColor = listHoverColor;
            foreach(Control child in tb.Parent.Controls)
                child.BackColor = listHoverColor;
        }
        private void textBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                EditComplete(tb);
            }
        }

        private void EditComplete(TextBox tb)
        {
            textLbl.Text = this.Text;
            textLbl.Visible = true;
            ((MyListItem)tb.Parent).ItemName = Text;
            this.Visible = false;
            userAreaPanel.Focus();

            MyInteraction.listClick(Parent);
        }
    }
}
