using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderlessForm.MyComponent
{
    class MyUpcomingPanel:MyContentPanel
    {
        public MyUpcomingPanel(string title):base(title)
        {
            TitleIcon.Image = MyImage.bigUpcoming;
            TitleIcon.Size = new System.Drawing.Size(27, 25);
            TitleIcon.Margin = new System.Windows.Forms.Padding(0, 7, 0, 3);
            AddTitleToTitilePanel(0);
            updateData();
        }

        public override void updateData()
        {
            base.updateData();
            base.Todos.Controls.Clear();
            MainForm.Data.todoItems.Sort((a,b)=> a.Due.CompareTo(b.Due));

            foreach (MyTodoItem td in MainForm.Data.todoItems)
            {
                if (td.Cstate == MyImage.clickedCheckbox) continue;
                if (td.IsDelete == true) continue;
                //if (td.Due.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                //{
                    MyTodoItem tdi = new MyTodoItem(td.Content, td.Cstate, td.Id);
                    tdi.Due = td.Due;
                    tdi.TodoText.Text = td.Due.ToString() +": " + td.Content;
                    Todos.Controls.Add(tdi);
                //}
            }
        }
    }
}
