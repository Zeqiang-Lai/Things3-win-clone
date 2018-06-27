using BorderlessForm.Animation;
using BorderlessForm.Interaction;
using BorderlessForm.MyComponent;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using BorderlessForm.Datas;

namespace BorderlessForm
{
    public partial class MainForm : FormBase
    {
        #region BasicFormStyle
        private FormWindowState previousWindowState;

        private Color hoverTextColor = Color.FromArgb(62, 109, 181);

        public Color HoverTextColor
        {
            get { return hoverTextColor; }
            set { hoverTextColor = value; }
        }

        private Color downTextColor = Color.FromArgb(25, 71, 138);

        public Color DownTextColor
        {
            get { return downTextColor; }
            set { downTextColor = value; }
        }

        private Color hoverBackColor = Color.FromArgb(213, 225, 242);

        public Color HoverBackColor
        {
            get { return hoverBackColor; }
            set { hoverBackColor = value; }
        }

        private Color downBackColor = Color.FromArgb(163, 189, 227);

        public Color DownBackColor
        {
            get { return downBackColor; }
            set { downBackColor = value; }
        }

        private Color normalBackColor = Color.FromArgb(250, 249, 250);

        public Color NormalBackColor
        {
            get { return normalBackColor; }
            set { normalBackColor = value; }
        }

        public enum MouseState
        {
            Normal,
            Hover,
            Down
        }

        protected void SetLabelColors(Control control, MouseState state)
        {
            if (!ContainsFocus) return;

            var textColor = ActiveTextColor;
            var backColor = NormalBackColor;

            if (control.Name == "SystemLabel") backColor = Color.FromArgb(245, 246, 247);

            switch (state)
            {
                case MouseState.Hover:
                    textColor = HoverTextColor;
                    backColor = HoverBackColor;
                    break;
                case MouseState.Down:
                    textColor = DownTextColor;
                    backColor = DownBackColor;
                    break;
            }

            control.ForeColor = textColor;
            control.BackColor = backColor;
        }


        void SystemLabel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) ShowSystemMenu(MouseButtons);
        }

        private DateTime systemClickTime = DateTime.MinValue;
        private DateTime systemMenuCloseTime = DateTime.MinValue;

        void SystemLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var clickTime = (DateTime.Now - systemClickTime).TotalMilliseconds;
                if (clickTime < SystemInformation.DoubleClickTime)
                    Close();
                else
                {
                    systemClickTime = DateTime.Now;
                    if ((systemClickTime - systemMenuCloseTime).TotalMilliseconds > 200)
                    {
                      //  SetLabelColors(SystemLabel, MouseState.Normal);
                        ShowSystemMenu(MouseButtons, PointToScreen(new Point(8, 32)));
                        systemMenuCloseTime = DateTime.Now;
                    }
                }
            }
        }

        void Close(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) Close();
        }

        void DecorationMouseDown(MouseEventArgs e, HitTestValues h)
        {
            if (e.Button == MouseButtons.Left) DecorationMouseDown(h);
        }

        private Color activeBorderColor = Color.FromArgb(43, 87, 154);

        public Color ActiveBorderColor
        {
            get { return activeBorderColor; }
            set { activeBorderColor = value; }
        }

        private Color inactiveBorderColor = Color.FromArgb(131, 131, 131);

        public Color InactiveBorderColor
        {
            get { return inactiveBorderColor; }
            set { inactiveBorderColor = value; }
        }

        void MainForm_Deactivate(object sender, EventArgs e)
        {
            SetBorderColor(InactiveBorderColor);
            SetTextColor(InactiveTextColor);
        }

        void MainForm_Activated(object sender, EventArgs e)
        {
            SetBorderColor(ActiveBorderColor);
            SetTextColor(ActiveTextColor);
        }

        private Color activeTextColor = Color.FromArgb(68, 68, 68);

        public Color ActiveTextColor
        {
            get { return activeTextColor; }
            set { activeTextColor = value; }
        }

        private Color inactiveTextColor = Color.FromArgb(177, 177, 177);

        public Color InactiveTextColor
        {
            get { return inactiveTextColor; }
            set { inactiveTextColor = value; }
        }

        protected void SetBorderColor(Color color)
        {
            TopLeftCornerPanel.BackColor = color;
            TopBorderPanel.BackColor = color;
            TopRightCornerPanel.BackColor = color;
            LeftBorderPanel.BackColor = color;
            RightBorderPanel.BackColor = color;
            BottomLeftCornerPanel.BackColor = color;
            BottomBorderPanel.BackColor = color;
            BottomRightCornerPanel.BackColor = color;
        }

        protected void SetTextColor(Color color)
        {
            //SystemLabel.ForeColor = color;
            TitleLabel.ForeColor = color;
            MinimizeLabel.ForeColor = color;
            //MaximizeLabel.ForeColor = color;
            CloseLabel.ForeColor = color;
        }

        void MainForm_SizeChanged(object sender, EventArgs e)
        {
            var maximized = MinMaxState == FormWindowState.Maximized;
            //MaximizeLabel.Text = maximized ? "2" : "1";

            var panels = new[] { TopLeftCornerPanel, TopRightCornerPanel, BottomLeftCornerPanel, BottomRightCornerPanel,
                TopBorderPanel, LeftBorderPanel, RightBorderPanel, BottomBorderPanel };

            foreach (var panel in panels)
            {
                panel.Visible = !maximized;
            }

            if (previousWindowState != MinMaxState)
            {
                if (maximized)
                {
                   // SystemLabel.Left = 0;
                   // SystemLabel.Top = 0;
                    CloseLabel.Left += RightBorderPanel.Width;
                    CloseLabel.Top = 0;
                    //MaximizeLabel.Left += RightBorderPanel.Width;
                    //MaximizeLabel.Top = 0;
                    MinimizeLabel.Left += RightBorderPanel.Width;
                    MinimizeLabel.Top = 0;
                    TitleLabel.Left -= LeftBorderPanel.Width;
                    TitleLabel.Width += LeftBorderPanel.Width + RightBorderPanel.Width;
                    TitleLabel.Top = 0;
                }
                else if (previousWindowState == FormWindowState.Maximized)
                {
                  //  SystemLabel.Left = LeftBorderPanel.Width;
                  //  SystemLabel.Top = TopBorderPanel.Height;
                    CloseLabel.Left -= RightBorderPanel.Width;
                    CloseLabel.Top = TopBorderPanel.Height;
                    //MaximizeLabel.Left -= RightBorderPanel.Width;
                    //MaximizeLabel.Top = TopBorderPanel.Height;
                    MinimizeLabel.Left -= RightBorderPanel.Width;
                    MinimizeLabel.Top = TopBorderPanel.Height;
                    TitleLabel.Left += LeftBorderPanel.Width;
                    TitleLabel.Width -= LeftBorderPanel.Width + RightBorderPanel.Width;
                    TitleLabel.Top = TopBorderPanel.Height;
                }

                previousWindowState = MinMaxState;
            }
        }

        private FormWindowState ToggleMaximize()
        {
            return WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private DateTime titleClickTime = DateTime.MinValue;
        private Point titleClickPosition = Point.Empty;

        void TitleLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //var clickTime = (DateTime.Now - titleClickTime).TotalMilliseconds;
                //if (clickTime < SystemInformation.DoubleClickTime && e.Location == titleClickPosition)
                //   ToggleMaximize();
                //else
                //{
                //   titleClickTime = DateTime.Now;
                //   titleClickPosition = e.Location;
                DecorationMouseDown(HitTestValues.HTCAPTION);
                //}
            }
        }
        #endregion

        #region data
        static public Form mainform;
        static public MyData Data = new MyData();
        OriginData oData = new OriginData();
        #endregion

        public MainForm()
        {
            mainform = this;
            InitializeComponent();
            lastFocus = this;
            #region Basic
            Activated += MainForm_Activated;
            Deactivate += MainForm_Deactivate;

            foreach (var control in new[] { MinimizeLabel, CloseLabel })
            {
                control.MouseEnter += (s, e) => SetLabelColors((Control)s, MouseState.Hover);
                control.MouseLeave += (s, e) => SetLabelColors((Control)s, MouseState.Normal);
                control.MouseDown += (s, e) => SetLabelColors((Control)s, MouseState.Down);
            }

            TopLeftCornerPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTTOPLEFT);
            TopRightCornerPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTTOPRIGHT);
            BottomLeftCornerPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTBOTTOMLEFT);
            BottomRightCornerPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTBOTTOMRIGHT);

            TopBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTTOP);
            LeftBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTLEFT);
            RightBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTRIGHT);
            BottomBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTBOTTOM);

            //SystemLabel.MouseDown += SystemLabel_MouseDown;
            //SystemLabel.MouseUp += SystemLabel_MouseUp;

            TitleLabel.MouseDown += TitleLabel_MouseDown;
            //TitleLabel.MouseUp += (s, e) => { if (e.Button == MouseButtons.Right && TitleLabel.ClientRectangle.Contains(e.Location)) ShowSystemMenu(MouseButtons); };
            //TitleLabel.Text = Text;
            TextChanged += (s, e) => TitleLabel.Text = Text;

            var marlett = new Font("Marlett", 8.5f);

            MinimizeLabel.Font = marlett;
            //MaximizeLabel.Font = marlett;
            CloseLabel.Font = marlett;
            //SystemLabel.Font = marlett;

            MinimizeLabel.MouseClick += (s, e) => { if (e.Button == MouseButtons.Left) WindowState = FormWindowState.Minimized; };
            //MaximizeLabel.MouseClick += (s, e) => { if (e.Button == MouseButtons.Left) ToggleMaximize(); };
            previousWindowState = MinMaxState;
            SizeChanged += MainForm_SizeChanged;
            CloseLabel.MouseClick += (s, e) => Close(e);
            #endregion

            foreach (var control in new[] 
            { panel2, panel3, panel4, panel5, panel6, panel7, panel8})
            {
                MyAnimation.SetListBtnColor(control);
                control.Click += (s, e) => MyInteraction.listClick((Control)s);
                foreach (Control childControl in control.Controls)
                {
                    childControl.Click += (s, e) => MyInteraction.listClick(((Control)s).Parent);
                }
            }
            MyAnimation.AddMouseDownEvent(this);
            MyAnimation.SetListBtnColor(panel12);

            MyAnimation.SetbuttomBarBtnColor(panel28);
            AddNewItemBtn.Click += AddNewItemBtn_Click;
            panel28.Click += AddNewItemBtn_Click;

            MyAnimation.SetbuttomBarBtnColor(panel31);
            editBtn.Click += EditBtn_Click;
            panel31.Click += EditBtn_Click;

            //MyInteraction.listClick(panel2);
        }

        #region LeftPanel
        static public Control lastFocus;

        private Color listNormalColor = Color.FromArgb(245, 246, 247);
        private Color listHoverColor = Color.FromArgb(225, 227, 231);

        /// <summary>
        /// New List Button Click
        /// </summary>
        private void panel12_MouseDown(object sender, MouseEventArgs e)
        {
            ((Panel)sender).Focus();
            if (e.Button == MouseButtons.Left)
            {
                contextMenuStrip1.Show(panel1, 3, 546);
            }
        }
        private void pictureBox8_MouseDown(object sender, MouseEventArgs e)
        {
            ((PictureBox)sender).Focus();
            if (e.Button == MouseButtons.Left)
            {
                contextMenuStrip1.Show(panel1, 3, 546);
            }
        }
        private void label16_MouseDown(object sender, MouseEventArgs e)
        {
            ((Label)sender).Focus();
            if (e.Button == MouseButtons.Left)
            {
                contextMenuStrip1.Show(panel1, 3, 546);
            }
        }


        /// <summary>
        /// new list
        /// </summary>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addNewAreaItem();
        }

        private void addNewAreaItem(string name = "")
        {
            if (lastFocus is MyListItem)
            {
                MyAreaPanel area = (MyAreaPanel)(((MyListItem)lastFocus).Parent);
                //MyAreaPanel area = userArea[0];
                MyListItem item = new MyListItem(userAreaPanel, false);
                area.Height += 19;
                area.items.Add(item.ItemName);
                area.Controls.Add(item);
                item.Tb.Focus();
                if (name != "")
                {
                    item.ItemName = name;
                    item.Tb.Text = name;
                    MyInteraction.listClick(item);
                }
            }
        }

        /// <summary>
        /// new area
        /// </summary>
        /// <returns></returns>
        private void addNewArea(string name = "")
        {
            MyAreaPanel area;
            if (name == "")
               area = new MyAreaPanel(userAreaPanel);
            else
               area = new MyAreaPanel(userAreaPanel,name);
            userAreaPanel.Controls.Add(area);
            if (name == "")
            {
                area.Tb.Focus();
                lastFocus = area.Title;
            }
            else
            {
                mainform.Focus();

            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            addNewArea();
        }

#endregion
       
        #region RightPanel
        static public Control lastFocusControl;

        private void AddNewItemBtn_Click(object sender, EventArgs e)
        {
            if (Data.nowDisplayList != null)
            {
                MyContentPanel panel = Data.nowDisplayList;
                //panel.Todos.AddNewItem();
                MyTodoItem newItem = new MyTodoItem();
                MyInputBox input = new MyInputBox(panel.Todos.TodoList,newItem);
                panel.Todos.TodoList.Controls.Add(input);
                input.Todocont.Focus();
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            MyTodoItem td;
            
            if (lastFocusControl is MyTodoItem) td = lastFocusControl as MyTodoItem;
            else if (lastFocusControl is MyAutoSizeTextbox) td = lastFocusControl.Parent as MyTodoItem;
            else return;

            FlowLayoutPanel container = td.Parent as FlowLayoutPanel;
            int index = container.Controls.GetChildIndex(td);
            container.Controls.Remove(td);
            MyInputBox input = new MyInputBox(container, td);
            container.Controls.Add(input);
            container.Controls.SetChildIndex(input, index);
            input.Todocont.Focus();
        }

        private void settingBtn_Click(object sender, EventArgs e)
        {
            SettingForm form2 = new SettingForm();
            form2.Show();
        }

        #endregion
        // this is a test code
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            if(pic.Image.Equals(Properties.Resources.checkbox))
                pic.Image = Properties.Resources.checkboxclick;
            else
                pic.Image = Properties.Resources.checkbox;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(Properties.Resources.path))
            {
                try
                {
                    FileStream fs = new FileStream(Properties.Resources.path, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    oData = bf.Deserialize(fs) as OriginData;
                    LoadOriginDataToData();
                    fs.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("读取数据发生了错误。\n如果您是首次使用，请删除文件Thing3.dat后重试");
                }
            }
        }

        private void LoadOriginDataToData()
        {
            foreach (MyTodo todo in oData.todos)
                Data.todoItems.Add(new MyTodoItem(todo.Id, todo.ParentName, todo.Content, todo.Due, todo.Finish, todo.IsDelete, todo.IsComplete));
            Data.idNum = oData.idNum;
            foreach (MyTodoItem todo in Data.todoItems)
                Data.todoIs.Add(todo.Id, todo);

            foreach(string name in oData.areasName)
            {
                addNewArea(name);
                foreach (string itemName in oData.areas[name])
                {
                    addNewAreaItem(itemName);
                    this.Focus();
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OriginData raw = new OriginData();
            Data.SaveToFile(raw);
            FileStream fileStream = new FileStream(Properties.Resources.path, FileMode.Create);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(fileStream, raw);
            fileStream.Close();
        }
    }
}
