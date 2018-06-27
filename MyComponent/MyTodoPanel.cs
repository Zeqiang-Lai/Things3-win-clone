using System;
using System.Drawing;
using System.Windows.Forms;

namespace BorderlessForm.MyComponent
{
    internal class MyTodoPanel : FlowLayoutPanel
    {
        Panel subtitle;
        FlowLayoutPanel todoList;
        MyAutoSizeTextbox head;
        PictureBox moreBtn;
        Panel line;
        private string subtitleName;

        // 0 : no subtitle(head)
        // 1 : initial
        public MyTodoPanel(int mode = 1, string subName = "", FlowLayoutPanel todos = null)
        {
            this.AutoSize = true;
            this.Margin = new Padding(23, 8, 3, 3);

            TodoList = new FlowLayoutPanel();
            TodoList.Margin= new Padding(0, 6, 0, 0);
            TodoList.AutoSize = true;
            TodoList.FlowDirection = FlowDirection.TopDown;
            TodoList.WrapContents = false;


            if (subName != "") subtitleName = subName;
            else AddNewItem();

            if (todos != null)
            {
                CopyTodos(todos);
            }

            if (mode == 1)
                GreateSubtitlePanel();
            this.Controls.Add(subtitle);
            
            this.Controls.Add(TodoList);

            //if(head != null) head.Focus();
        }

        private void CopyTodos(FlowLayoutPanel todos)
        {
            foreach(MyTodoItem c in todos.Controls)
              if(c.Cstate == MyImage.checkbox)
                    todoList.Controls.Add(new MyTodoItem(c.Content, c.Cstate,c.Id));
        }

        public FlowLayoutPanel TodoList { get => todoList; set => todoList = value; }

        public void AddNewItem()
        {
            MyTodoItem todoItem = new MyTodoItem();
            TodoList.Controls.Add(todoItem);
            todoItem.TodoText.Focus();
        }

        private void GreateSubtitlePanel()
        {
            subtitle = new Panel();
            subtitle.Size = new Size(392, 25);
            subtitle.Margin = new Padding(18, 3, 3, 3);

            head = new MyAutoSizeTextbox();
            head.Location = new Point(4, 3);
            head.Font = new Font("微软雅黑", 9f, FontStyle.Bold);
            head.Text = subtitleName;
            head.ForeColor = Color.FromArgb(13, 87, 191);

            moreBtn = new PictureBox();
            moreBtn.Image = Properties.Resources.bluethreedot;
            moreBtn.Size = new Size(18, 17);
            moreBtn.Location = new Point(367, 3);
            moreBtn.SizeMode = PictureBoxSizeMode.CenterImage;

            line = new Panel();
            line.Size = new Size(382, 1);
            line.Margin = new Padding(0, 0, 0, 0);
            line.Location = new Point(4, 23);
            line.BackColor = Color.FromArgb(230, 230, 230);

            subtitle.Controls.Add(head);
            subtitle.Controls.Add(moreBtn);
            subtitle.Controls.Add(line);
        }

    }
}