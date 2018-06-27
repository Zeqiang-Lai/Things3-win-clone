using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BorderlessForm
{
    class SettingForm : FormBase
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
        private Label MinimizeLabel;
        private Label CloseLabel;
        private Label TitleLabel;
        private Panel RightBorderPanel;
        private Panel LeftBorderPanel;
        private Panel BottomBorderPanel;
        private Panel TopBorderPanel;
        private Panel TopRightCornerPanel;
        private Panel BottomLeftCornerPanel;
        private Panel BottomRightCornerPanel;
        private Panel TopLeftCornerPanel;

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

        private void InitializeComponent()
        {
            this.MinimizeLabel = new System.Windows.Forms.Label();
            this.CloseLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.RightBorderPanel = new System.Windows.Forms.Panel();
            this.LeftBorderPanel = new System.Windows.Forms.Panel();
            this.BottomBorderPanel = new System.Windows.Forms.Panel();
            this.TopBorderPanel = new System.Windows.Forms.Panel();
            this.TopRightCornerPanel = new System.Windows.Forms.Panel();
            this.BottomLeftCornerPanel = new System.Windows.Forms.Panel();
            this.BottomRightCornerPanel = new System.Windows.Forms.Panel();
            this.TopLeftCornerPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // MinimizeLabel
            // 
            this.MinimizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.MinimizeLabel.Font = new System.Drawing.Font("Marlett", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeLabel.Location = new System.Drawing.Point(415, 1);
            this.MinimizeLabel.Name = "MinimizeLabel";
            this.MinimizeLabel.Size = new System.Drawing.Size(24, 22);
            this.MinimizeLabel.TabIndex = 11;
            this.MinimizeLabel.Text = "0";
            this.MinimizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CloseLabel
            // 
            this.CloseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.CloseLabel.Font = new System.Drawing.Font("Marlett", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseLabel.Location = new System.Drawing.Point(439, 1);
            this.CloseLabel.Name = "CloseLabel";
            this.CloseLabel.Size = new System.Drawing.Size(24, 22);
            this.CloseLabel.TabIndex = 12;
            this.CloseLabel.Text = "r";
            this.CloseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TitleLabel
            // 
            this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.TitleLabel.Location = new System.Drawing.Point(1, 1);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(420, 22);
            this.TitleLabel.TabIndex = 13;
            this.TitleLabel.Text = "Setting";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RightBorderPanel
            // 
            this.RightBorderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RightBorderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.RightBorderPanel.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.RightBorderPanel.Location = new System.Drawing.Point(463, -192);
            this.RightBorderPanel.Name = "RightBorderPanel";
            this.RightBorderPanel.Size = new System.Drawing.Size(1, 565);
            this.RightBorderPanel.TabIndex = 14;
            // 
            // LeftBorderPanel
            // 
            this.LeftBorderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LeftBorderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.LeftBorderPanel.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.LeftBorderPanel.Location = new System.Drawing.Point(0, -184);
            this.LeftBorderPanel.Name = "LeftBorderPanel";
            this.LeftBorderPanel.Size = new System.Drawing.Size(1, 565);
            this.LeftBorderPanel.TabIndex = 15;
            // 
            // BottomBorderPanel
            // 
            this.BottomBorderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomBorderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.BottomBorderPanel.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.BottomBorderPanel.Location = new System.Drawing.Point(-174, 180);
            this.BottomBorderPanel.Name = "BottomBorderPanel";
            this.BottomBorderPanel.Size = new System.Drawing.Size(813, 1);
            this.BottomBorderPanel.TabIndex = 16;
            // 
            // TopBorderPanel
            // 
            this.TopBorderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TopBorderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.TopBorderPanel.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.TopBorderPanel.Location = new System.Drawing.Point(-166, 0);
            this.TopBorderPanel.Name = "TopBorderPanel";
            this.TopBorderPanel.Size = new System.Drawing.Size(813, 1);
            this.TopBorderPanel.TabIndex = 17;
            // 
            // TopRightCornerPanel
            // 
            this.TopRightCornerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TopRightCornerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.TopRightCornerPanel.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            this.TopRightCornerPanel.Location = new System.Drawing.Point(463, 0);
            this.TopRightCornerPanel.Name = "TopRightCornerPanel";
            this.TopRightCornerPanel.Size = new System.Drawing.Size(1, 1);
            this.TopRightCornerPanel.TabIndex = 18;
            // 
            // BottomLeftCornerPanel
            // 
            this.BottomLeftCornerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BottomLeftCornerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.BottomLeftCornerPanel.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            this.BottomLeftCornerPanel.Location = new System.Drawing.Point(0, 180);
            this.BottomLeftCornerPanel.Name = "BottomLeftCornerPanel";
            this.BottomLeftCornerPanel.Size = new System.Drawing.Size(1, 1);
            this.BottomLeftCornerPanel.TabIndex = 19;
            // 
            // BottomRightCornerPanel
            // 
            this.BottomRightCornerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomRightCornerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.BottomRightCornerPanel.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.BottomRightCornerPanel.Location = new System.Drawing.Point(463, 180);
            this.BottomRightCornerPanel.Name = "BottomRightCornerPanel";
            this.BottomRightCornerPanel.Size = new System.Drawing.Size(1, 1);
            this.BottomRightCornerPanel.TabIndex = 20;
            // 
            // TopLeftCornerPanel
            // 
            this.TopLeftCornerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154)))));
            this.TopLeftCornerPanel.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.TopLeftCornerPanel.Location = new System.Drawing.Point(0, 0);
            this.TopLeftCornerPanel.Name = "TopLeftCornerPanel";
            this.TopLeftCornerPanel.Size = new System.Drawing.Size(1, 1);
            this.TopLeftCornerPanel.TabIndex = 21;
            // 
            // SettingForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(464, 181);
            this.Controls.Add(this.TopRightCornerPanel);
            this.Controls.Add(this.TopLeftCornerPanel);
            this.Controls.Add(this.BottomRightCornerPanel);
            this.Controls.Add(this.BottomLeftCornerPanel);
            this.Controls.Add(this.TopBorderPanel);
            this.Controls.Add(this.BottomBorderPanel);
            this.Controls.Add(this.LeftBorderPanel);
            this.Controls.Add(this.RightBorderPanel);
            this.Controls.Add(this.MinimizeLabel);
            this.Controls.Add(this.CloseLabel);
            this.Controls.Add(this.TitleLabel);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        public SettingForm()
        {
            InitializeComponent();
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
        }
    }
}
