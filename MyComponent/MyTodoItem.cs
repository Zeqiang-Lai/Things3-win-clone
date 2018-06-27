using BorderlessForm.Animation;
using BorderlessForm.Datas;
using BorderlessForm.MyComponent.Menu;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace BorderlessForm.MyComponent
{
    [Serializable]
    public class MyTodoItem : Panel
    {
        static itemMenu itemMenu = new itemMenu();

        PictureBox checkBox;
        PictureBox editBtn;
        MyAutoSizeTextbox todoText;

        DateTime due;
        DateTime finishDate;
        bool isDelete = false;
        string content;
        string parentName;
        Image cstate;
        bool isComplete;
        int id;

        //bool lastOne;

        public MyTodoItem(string text = null, Image state = null, int _id = -1)
        {
            id = _id;
            Due = MainForm.Data.initDate;

            Size = new Size(392, 24);
            Margin = new Padding(18, 0, 3, 0);
            this.ContextMenuStrip = itemMenu;
            this.GotFocus += TodoText_GotFocus;
            MyAnimation.SetListItemColor(this);

            /* editBtn = new PictureBox();
             editBtn.Size = new Size(18, 12);
             editBtn.Location = new Point(2, 6);
             editBtn.Image = Properties.Resources.editbtn;*/

            CheckBox = new PictureBox();
            CheckBox.Size = new Size(12, 13);
            if (state != null) CheckBox.Image = state;
            else CheckBox.Image = MyImage.checkbox;
            CheckBox.Location = new Point(4, 6);
            CheckBox.Click += CheckBox_Click;

            TodoText = new MyAutoSizeTextbox();
            TodoText.Location = new Point(24, 4);
            TodoText.Font = new Font("微软雅黑", 9f);
            if (text != null) TodoText.Text = text;
            else TodoText.Text = "New To-Do";
            if (TodoText.Text == "New To-Do") TodoText.ForeColor = Color.FromArgb(204, 202, 204);
            TodoText.KeyPress += TodoText_KeyPress;
            TodoText.KeyDown += TodoText_KeyDown;
            TodoText.GotFocus += TodoText_GotFocus;
            TodoText.LostFocus += TodoText_LostFocus;

            Content = TodoText.Text;
            Cstate = CheckBox.Image;

            this.Controls.Add(CheckBox);
            this.Controls.Add(TodoText);
        }

        public MyTodoItem(int _id, string _parent, string _content, DateTime _due,
            DateTime _finish, bool _isDelete, bool _isComplete)
        {
            id = _id;
            ParentName = _parent;
            content = _content;
            due = _due;
            FinishDate = _finish;
            IsDelete = _isDelete;
            IsComplete = _isComplete;
        }

        internal void delete()
        {
            IsDelete = true;
            updateTodo();
            Thread.Sleep(200);
            base.Parent.Controls.Remove(this);
        }

        private void TodoText_KeyDown(object sender, KeyEventArgs e)
        {
            Control p = base.Parent;
            int index = p.Controls.IndexOf(this);

            Keys key = e.KeyCode;
            switch (key)
            {
                case Keys.Up:
                    if (index != 0)
                    {
                        e.SuppressKeyPress = true;
                        SendKeys.Send("+{Tab}");
                    }
                    break;
                case Keys.Down:
                    if (index != p.Controls.Count - 1)
                    {
                        e.SuppressKeyPress = true;
                        SendKeys.Send("{Tab}");
                    }
                    break;
            }
        }

        private void TodoText_LostFocus(object sender, EventArgs e)
        {
            if (TodoText.Text == string.Empty)
            {
                TodoText.ForeColor = Color.FromArgb(204, 202, 204);
                TodoText.Text = "New To-Do";
            }
            else
            {
                content = TodoText.Text;
                updateTodo();
            }
        }

        private void TodoText_GotFocus(object sender, EventArgs E)
        {
            if (todoText.Text == "New To-Do")
            {
                TodoText.ForeColor = Color.Black;
                TodoText.Text = string.Empty;
            }
            //if(MainForm.lastFocusControl != null)
            //    MainForm.lastFocusControl.MouseLeave += (s, e) => MyAnimation.SetListItem((Control)s, MyAnimation.MouseState.Normal);
            MainForm.lastFocusControl = this;
            //this.MouseLeave -= (s, e) => MyAnimation.SetListItem((Control)s, MyAnimation.MouseState.Normal);
        }

        private void CheckBox_Click(object sender, EventArgs e)
        {
            if (CheckBox.Image == MyImage.checkbox)
            {
                CheckBox.Image = MyImage.clickedCheckbox;
                checkBox.Refresh();
                Thread.Sleep(200);
                base.Parent.Controls.Remove(this);
            }
            else
                CheckBox.Image = MyImage.checkbox;
            cstate = CheckBox.Image;
            updateTodo();
            //if (Parent.Controls.Count == 0)
            //    Parent.Parent.Parent.Controls.Remove(Parent.Parent);
        }

        private void TodoText_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyAutoSizeTextbox tb = (MyAutoSizeTextbox)sender;
            Control p = base.Parent;
            int index = p.Controls.IndexOf(this);

            if (e.KeyChar == (char)Keys.Enter)
            {
                content = TodoText.Text;
                updateTodo();

                MyTodoItem td = new MyTodoItem();
                e.Handled = true;
                p.Controls.Add(td);
                p.Controls.SetChildIndex(td, index + 1);
                td.TodoText.Focus();
            }
            else if (e.KeyChar == (char)Keys.Back && tb.Text == "")
            {
                if (index > 0) p.Controls[index - 1].Focus();
                p.Controls.Remove(this);
                if (MainForm.Data.todoIs.ContainsKey(Id)) MainForm.Data.todoIs.Remove(Id);
            }
        }

        public void updateTodo()
        {
            content = todoText.Text;
            ParentName = MainForm.Data.nowDisplayList.TitleName;
            if (Id == -1)
            {
                Id = MainForm.Data.idNum++;
                MainForm.Data.todoItems.Add(this);
                MainForm.Data.todoIs[Id] = this;
            }
            else if (MainForm.Data.todoIs.ContainsKey(Id))
            {
                MyTodoItem his = MainForm.Data.todoIs[Id];
                his.content = content;
                his.Due = Due;
                his.IsDelete = IsDelete;
                his.cstate = checkBox.Image;
            }
        }

        internal MyAutoSizeTextbox TodoText { get => todoText; set => todoText = value; }
        public Image Cstate { get => cstate; set => cstate = value; }
        public PictureBox CheckBox { get => checkBox; set => checkBox = value; }

        public int Id { get => id; set => id = value; }
        public string Content { get => content; set => content = value; }
        public DateTime Due { get => due; set => due = value; }
        public DateTime FinishDate { get => finishDate; set => finishDate = value; }
        public bool IsComplete { get => isComplete; set => isComplete = value; }
        public string ParentName { get => parentName; set => parentName = value; }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
    }
}
