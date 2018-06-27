using BorderlessForm.Animation;
using BorderlessForm.Interaction;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BorderlessForm.MyComponent
{
    [Serializable]
    public class MyListItem : Panel
    {
        private Color listNormalColor = Color.FromArgb(245, 246, 247);
        private Color listHoverColor = Color.FromArgb(225, 227, 231);
        FlowLayoutPanel userAreaPanel;

        PictureBox pic = new PictureBox();
        Label lbl = new Label();
        MyListItemTextBox tb;
        bool itemKind;
        string itemName;

        public MyListItem(FlowLayoutPanel area, bool isArea)
        {
            ItemKind = isArea;

            userAreaPanel = area;

            Tb = new MyListItemTextBox(userAreaPanel, lbl, isArea);

            tb.ForeColor = Color.FromArgb(204, 202, 204);
            tb.Text = "New List";
            if (isArea) tb.Text = "New Area";

            this.Size = new Size(167, 18);
            this.Margin = new Padding(0, 1, 0, 0);
            this.BackColor = listHoverColor;

            if (!isArea)
            {
                pic.Image = Properties.Resources.circle;
                pic.Size = new Size(12, 12);
                lbl.Font = new Font("Corbel", 9.75f);
                Tb.Font = new Font("Corbel", 9.75f);
            }
            else
            {
                pic.Size = new Size(12, 13);
                pic.Image = Properties.Resources.area;
                lbl.Font = new Font("Corbel", 9.75f, FontStyle.Bold);
            }
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Location = new Point(3, 3);
            
            lbl.Location = new Point(19, 2);
            lbl.Size = new Size(130, 17);
            lbl.DoubleClick += textBoxDoubleClick;
            
            this.Controls.Add(pic);
            this.Controls.Add(lbl);
            this.Controls.Add(Tb);
            this.Click += (s, e) => MyInteraction.listClick(this);
            foreach (Control childControl in this.Controls)
                childControl.Click += (s, e) => MyInteraction.listClick(((Control)s).Parent);


            if (!isArea) MyAnimation.SetListBtnColor(this);
            else this.Click += (s, e) => addAllList(this);

            lbl.Visible = false;
        }

        private void addAllList(MyListItem myListItem)
        {
            Control p = myListItem.Parent;
            foreach (Control child in p.Controls)
                MyInteraction.listClick(child);
        }

        private void textBoxDoubleClick(object sender, EventArgs e)
        {

            Tb.Visible = true;
            ((Label)sender).Visible = false;
            Tb.SelectAll();
            Tb.Focus();
        }

        internal MyListItemTextBox Tb { get => tb; set => tb = value; }
        public bool ItemKind { get => itemKind; set => itemKind = value; }
        public string ItemName { get => itemName; set => itemName = value; }
    }
}
