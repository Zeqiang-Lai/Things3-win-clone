using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BorderlessForm.MyComponent
{
    class MyInputBox : Panel
    {
        Color shadowGrey = Color.FromArgb(225, 223, 225);
        Color textGrey = Color.FromArgb(204, 202, 204);

        FlowLayoutPanel innerPanel;

        Panel todoItem;
        PictureBox checkbox;
        MyAutoSizeTextbox todocont;

        TextBox shortcut;
        Size shortCutSize = new Size(366, 34);

        Panel optionPanel;
        Panel datePanel;
        PictureBox pic;
        Panel borderPanel;
        Panel hideBorderPanel;
        DateTimePicker datePicker;

        MyTodoItem shortTodoPanel;
        FlowLayoutPanel container;

        DateTime date;

        public MyInputBox(FlowLayoutPanel parent, MyTodoItem todo = null)
        {
            shortTodoPanel = todo;
            container = parent;

            AutoSize = true;
            BackColor = shadowGrey;

            innerPanel = new FlowLayoutPanel();
            innerPanel.Location = new Point(1, 1);
            innerPanel.Margin = new Padding(0, 0, 1, 1);
            innerPanel.BackColor = Color.White;
            innerPanel.WrapContents = false;
            innerPanel.FlowDirection = FlowDirection.TopDown;
            innerPanel.Size = new Size(429, 118);
            innerPanel.AutoSize = true;

            AddTodoItemToInnerPanel();
            AddShortcutToInnerPanel();
            AddOptionPanelToInnerPanel();

            InitInputBox();

            Controls.Add(innerPanel);

            addLostFocusEvent(this);
        }

        private void InitInputBox()
        {
            todocont.Text = shortTodoPanel.TodoText.Text;
            if (todocont.Text != "New To-Do") todocont.ForeColor = Color.Black;
            datePicker.Value = shortTodoPanel.Due;
        }

        private void AddOptionPanelToInnerPanel()
        {
            optionPanel = new Panel();
            optionPanel.Size = new Size(365, 23);
            optionPanel.Margin = new Padding(39, 3, 3, 10);
            optionPanel.BackColor = Color.White;

            datePanel = new Panel();
            datePanel.Size = new Size(80, 25);
            datePanel.Location = new Point(0, 0);

            borderPanel = new Panel();
            borderPanel.Size = new Size(200, 23);
            borderPanel.Location = new Point(27, 0);
            borderPanel.BackColor = Color.Linen;

            hideBorderPanel = new Panel();
            hideBorderPanel.Size = new Size(198, 21);
            hideBorderPanel.Location = new Point(1, 1);

            datePicker = new DateTimePicker();
            datePicker.Font = new Font("微软雅黑", 9f);
            datePicker.CalendarFont = new Font("微软雅黑", 9f);
            datePicker.Location = new Point(-1, -1);
            datePicker.Value = MainForm.Data.initDate;
            datePicker.ValueChanged += DatePicker_ValueChanged;
            hideBorderPanel.Controls.Add(datePicker);
            borderPanel.Controls.Add(hideBorderPanel);

            pic = new PictureBox();
            pic.Size = new Size(15, 15);
            pic.Location = new Point(2, 4);
            pic.Image = Properties.Resources.Star;

            optionPanel.Controls.Add(pic);
            optionPanel.Controls.Add(borderPanel);

            innerPanel.Controls.Add(optionPanel);
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            date = datePicker.Value;
        }

        private void C_LostFocus(object sender, EventArgs e)
        {
            if (AllLostFocus(this))
            {
                int index = container.Controls.GetChildIndex(this);
                shortTodoPanel.TodoText.Text = Todocont.Text;
                shortTodoPanel.TodoText.ForeColor = Color.Black;
                shortTodoPanel.Due = datePicker.Value;
                shortTodoPanel.updateTodo();
                container.Controls.Remove(this);
                container.Controls.Add(shortTodoPanel);
                container.Controls.SetChildIndex(shortTodoPanel, index);
            }

        }

        private void addLostFocusEvent(Control p)
        {
            p.LostFocus += C_LostFocus;
            foreach (Control c in p.Controls)
                addLostFocusEvent(c);
        }

        private bool AllLostFocus(Control p)
        {
            if (p.Focused) return false;
            foreach (Control c in p.Controls)
                if (!AllLostFocus(c)) return false;
            return true;
        }

        private void AddTodoItemToInnerPanel()
        {
            todoItem = new Panel();
            todoItem.Margin = new Padding(0, 16, 3, 3);
            todoItem.Size = new Size(426, 16);

            checkbox = new PictureBox();
            checkbox.Size = new Size(12, 12);
            checkbox.Location = new Point(19, 2);
            checkbox.Image = MyImage.checkbox;

            Todocont = new MyAutoSizeTextbox();
            Todocont.Location = new Point(41, 0);
            Todocont.Font = new Font("微软雅黑", 9f);
            Todocont.BorderStyle = BorderStyle.None;
            todocont.BackColor = Color.White;

            Todocont.Text = "New To-Do";
            Todocont.ForeColor = textGrey;

            todoItem.Controls.Add(checkbox);
            todoItem.Controls.Add(Todocont);

            innerPanel.Controls.Add(todoItem);
        }

        private void AddShortcutToInnerPanel()
        {
            shortcut = new TextBox();
            shortcut.Margin = new Padding(39, 3, 3, 3);
            shortcut.Size = shortCutSize;
            shortcut.Font = new Font("微软雅黑", 9f);
            shortcut.Multiline = true;
            shortcut.BorderStyle = BorderStyle.None;
            shortcut.ForeColor = textGrey;
            shortcut.Text = "Notes";

            shortcut.TextChanged += Shortcut_TextChanged;
            shortcut.GotFocus += Shortcut_GotFocus;
            shortcut.LostFocus += Shortcut_LostFocus;


            innerPanel.Controls.Add(shortcut);
        }

        private void Shortcut_TextChanged(object sender, EventArgs e)
        {
            TextBox s = (TextBox)sender;
            Size initSize = new Size(366, 34);
            Size size = TextRenderer.MeasureText("f", s.Font);
            if (s.Lines.Length > 2)
                s.Height = s.Lines.Length * size.Height;
            else s.Size = initSize;
        }

        private void Shortcut_GotFocus(object sender, EventArgs e)
        {
            TextBox s = (TextBox)sender;

            if (s.Text == "Notes")
            {
                s.ForeColor = Color.Black;
                s.Text = string.Empty;
            }
        }

        private void Shortcut_LostFocus(object sender, EventArgs e)
        {
            TextBox s = (TextBox)sender;

            if (s.Text == string.Empty)
            {
                s.ForeColor = textGrey;
                s.Text = "Notes";
            }
        }

        internal MyAutoSizeTextbox Todocont { get => todocont; set => todocont = value; }
    }
}
