using System;

namespace BorderlessForm.MyComponent
{
    class MyTodayPanel:MyContentPanel
    {

        public MyTodayPanel(string title) : base(title)
        {
            TitleIcon.Image = MyImage.bigToday;
            TitleIcon.Size = new System.Drawing.Size(25, 25);
            TitleIcon.Margin = new System.Windows.Forms.Padding(0, 5, 0, 3);
            AddTitleToTitilePanel(0);
            updateData();
        }

        public override void updateData()
        {
            base.updateData();
            base.Todos.Controls.Clear();
            foreach(MyTodoItem td in MainForm.Data.todoItems)
            {
                if (td.Cstate == MyImage.clickedCheckbox) continue;
                if (td.IsDelete == true) continue;
                if(td.Due.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    MyTodoItem tdi = new MyTodoItem(td.Content, td.Cstate, td.Id);
                    tdi.Due = td.Due;
                    Todos.Controls.Add(tdi);
                }
            }
        }
    }
}
