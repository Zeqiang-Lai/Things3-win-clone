using BorderlessForm.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace BorderlessForm.MyComponent.Menu
{
    class itemMenu : ContextMenuStrip
    {
        ToolStripMenuItem editBtn;
        ToolStripMenuItem dueBtn;
        ToolStripMenuItem deleteBtn;

        public itemMenu()
        {
            editBtn = new ToolStripMenuItem();
            editBtn.Text = "Edit";
            editBtn.Click += EditBtn_Click;
            Items.Add(editBtn);

            dueBtn = new ToolStripMenuItem();
            dueBtn.Text = "Due";
            Items.Add(dueBtn);

            deleteBtn = new ToolStripMenuItem();
            deleteBtn.Text = "Delete";
            deleteBtn.Click += DeleteBtn_Click;

            Items.Add(deleteBtn);
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem delete = sender as ToolStripMenuItem;
            ContextMenuStrip menu = delete.Owner as ContextMenuStrip;
            MyTodoItem td = menu.SourceControl as MyTodoItem;

            FlowLayoutPanel container = td.Parent as FlowLayoutPanel;
            int index = container.Controls.GetChildIndex(td);
            container.Controls.Remove(td);
            MyInputBox input = new MyInputBox(container, td);
            container.Controls.Add(input);
            container.Controls.SetChildIndex(input, index);
            input.Todocont.Focus();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem delete = sender as ToolStripMenuItem;
            ContextMenuStrip menu = delete.Owner as ContextMenuStrip;
            MyTodoItem item = menu.SourceControl as MyTodoItem;
            item.delete();
        }
    }
}
