using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderlessForm.MyComponent
{
    class MyTrashPanel:MyContentPanel
    {
        public MyTrashPanel(string title):base(title)
        {
            TitleIcon.Image = MyImage.bigTrash;
            TitleIcon.Size = new System.Drawing.Size(25, 26);
            TitleIcon.Margin = new System.Windows.Forms.Padding(0, 4, 0, 3);
            AddTitleToTitilePanel(0);
            updateData();
        }
        public override void updateData()
        {
            base.updateData();
            Todos.Controls.Clear();
            foreach (MyTodoItem td in MainForm.Data.todoItems)
            {
                if (td.IsDelete == true)
                {
                    MyTodoItem tdi = new MyTodoItem(td.Content, td.Cstate, td.Id);
                    tdi.Due = td.Due;
                    tdi.IsDelete = true;
                    Todos.Controls.Add(tdi);
                }
            }
        }
    }
}
