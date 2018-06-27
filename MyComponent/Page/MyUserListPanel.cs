using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderlessForm.MyComponent
{
    class MyUserListPanel : MyContentPanel
    {
        public MyUserListPanel(string title) : base(title)
        {
            AddTitleToTitilePanel(0);
        }

        public override void updateData()
        {
            base.updateData();
            foreach(MyTodoItem tdi in Todos.TodoList.Controls)
            {
                if (tdi.Id == -1) continue;
                if (!MainForm.Data.todoIs.ContainsKey(tdi.Id))
                {
                    Todos.TodoList.Controls.Remove(tdi);
                    continue;
                }
                tdi.TodoText.Text = MainForm.Data.todoIs[tdi.Id].Content;
                tdi.CheckBox.Image = MainForm.Data.todoIs[tdi.Id].Cstate;
                if(MainForm.Data.todoIs[tdi.Id].Cstate == MyImage.clickedCheckbox)
                    Todos.TodoList.Controls.Remove(tdi);
            }
        }
    }
}
