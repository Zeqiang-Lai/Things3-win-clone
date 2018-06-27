using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderlessForm.MyComponent
{
    class MyAnytimePanel :MyContentPanel
    {
        public MyAnytimePanel(string title):base(title)
        {
            TitleIcon.Image = MyImage.bigAnytime;
            TitleIcon.Size = new System.Drawing.Size(34, 26);
            TitleIcon.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            AddTitleToTitilePanel(0);
            updateData();
        }

        public override void updateData()
        {
            base.updateData();
            Todos.Controls.Clear();

            foreach (MyTodoItem td in MainForm.Data.todoItems)
            {
                if (td.IsDelete == true) continue;
                if (td.Cstate == MyImage.clickedCheckbox) continue;
                MyTodoItem tdi = new MyTodoItem(td.Content, td.Cstate, td.Id);
                tdi.Due = td.Due;
                Todos.Controls.Add(tdi);
            }
        }
    }
}
