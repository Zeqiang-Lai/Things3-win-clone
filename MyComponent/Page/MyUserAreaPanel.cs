using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BorderlessForm.MyComponent
{
    [Serializable]
    class MyUserAreaPanel : MyContentPanel
    {
        List<MyTodoPanel> sublists = new List<MyTodoPanel>();
        string Title;

        public MyUserAreaPanel(string title):base(title)
        {
            Title = title;

            TitleIcon.Size = new System.Drawing.Size(28, 28);
            TitleIcon.Image = MyImage.bigArea;
            TitleIcon.Margin = new Padding(0, 5, 0, 3);
            AddTitleToTitilePanel(0);
            updateData();
        }

        private void AddSubList(string title)
        {
            MyAreaPanel area = MainForm.Data.userArea[title];
            foreach(MyListItem child in area.Controls)
            {
                if (child.ItemName == title) continue;
                MyUserListPanel temp = (MyUserListPanel)MainForm.Data.contPanels[child.ItemName];
                if (temp.Todos.TodoList.Controls.Count > 0)
                {
                    MyTodoPanel sublist = new MyTodoPanel(1, child.ItemName, temp.Todos.TodoList);
                    sublists.Add(sublist);
                    Controls.Add(sublist);
                }
            }
        }

        public override void updateData()
        {
            foreach (Control c in sublists)
                Controls.Remove(c);
            sublists.Clear();
            AddSubList(Title);
        }
    }
}
