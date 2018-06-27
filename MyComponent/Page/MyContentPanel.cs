using System;
using System.Drawing;
using System.Windows.Forms;

namespace BorderlessForm.MyComponent
{
    [Serializable]
    public class MyContentPanel : FlowLayoutPanel
    {
        FlowLayoutPanel titlePanel;
        PictureBox titleIcon;
        PictureBox moreBtn;
        //MyAutoSizeTextbox title;
        Control title;

        TextBox shortcut;
        MyTodoPanel todos;

        string titleName;

        public MyContentPanel(string listName)
        {
            TitleName = listName;

            this.Location = new Point(206, 26);
            this.FlowDirection = FlowDirection.TopDown;
            this.Size = new Size(490, 563);
            this.WrapContents = false;
            this.BackColor = MyColor.contentBackColor;
            this.AutoScroll = true;

            AddTitlePanel();
           //AddShortcut();
            Todos = new MyTodoPanel(0);
            this.Controls.Add(Todos);

        }   

        public void AddShortcut()
        {
            shortcut = new TextBox();
            shortcut.Size = new Size(392, 20);
            shortcut.Margin = new Padding(37, 5, 3, 3);
            shortcut.Multiline = true;
            shortcut.Font = new Font("微软雅黑", 9f);
            shortcut.BorderStyle = BorderStyle.None;

            this.Controls.Add(shortcut);
        }

        private void AddTitlePanel()
        {
            titlePanel = new FlowLayoutPanel();
            titlePanel.Margin = new Padding(41, 30, 3, 3);
            titlePanel.AutoSize = true;
            titlePanel.WrapContents = false;

            TitleIcon = new PictureBox();
            TitleIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            TitleIcon.Size = new Size(19, 19);
            TitleIcon.Margin = new Padding(0, 10, 0, 3);
            TitleIcon.Image = Properties.Resources.blueCircle;

            titlePanel.Controls.Add(TitleIcon);
            

            this.Controls.Add(titlePanel);
        }
        // 1 editable, 0 readonly
        public void AddTitleToTitilePanel(int mode)
        {

            if (mode == 1) title = new MyAutoSizeTextbox();
            else { title = new Label(); ((Label)title).AutoSize = true; }
            title.Margin = new Padding(5, 3, 0, 3);
            title.Font = new Font("微软雅黑", 18f, FontStyle.Bold);
            title.Text = TitleName;

            titlePanel.Controls.Add(title);
        }

        public void AddMoreBtnToTitlePanel()
        {
            moreBtn = new PictureBox();
            moreBtn.Size = new Size(18, 17);
            moreBtn.Margin = new Padding(5, 11, 3, 3);
            moreBtn.Image = Properties.Resources.threedot;
            moreBtn.SizeMode = PictureBoxSizeMode.CenterImage;
            titlePanel.Controls.Add(moreBtn);
        }

        public virtual void updateData()
        {

        }
        public PictureBox TitleIcon { get => titleIcon; set => titleIcon = value; }
        internal MyTodoPanel Todos { get => todos; set => todos = value; }
        public string TitleName { get => titleName; set => titleName = value; }
    }
}
