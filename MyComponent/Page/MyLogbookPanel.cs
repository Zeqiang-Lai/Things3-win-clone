using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderlessForm.MyComponent
{
    class MyLogbookPanel : MyContentPanel
    {
        public MyLogbookPanel(string title):base(title)
        {
            TitleIcon.Image = MyImage.bigLogbook;
            TitleIcon.Size = new System.Drawing.Size(25, 27);
            TitleIcon.Margin = new System.Windows.Forms.Padding(0, 7, 0, 3);
            AddTitleToTitilePanel(0);
            updateData();
        }

        public override void updateData()
        {
            base.updateData();
            base.Todos.Controls.Clear();
            foreach (MyTodoItem td in MainForm.Data.todoItems)
            {
                if (td.IsDelete == true) continue;
                if (td.Cstate == MyImage.clickedCheckbox) 
                {
                    MyTodoItem tdi = new MyTodoItem(td.Content, td.Cstate, td.Id);
                    tdi.Due = td.Due;
                    Todos.Controls.Add(tdi);
                }
            }
        }
    }
}
