using System;
using System.Drawing;
using System.Windows.Forms;

namespace BorderlessForm.MyComponent
{
    class MyAutoSizeTextbox : TextBox
    {
        Size initSize = new Size(100,16);
        public MyAutoSizeTextbox()
        {
            BorderStyle = BorderStyle.None;
            BackColor = MyColor.contentBackColor;
            TextChanged += MyAutoSizeTextbox_TextChanged;
            GotFocus += TextBox_GotFocus;
            LostFocus += TextBox_LostFocus;

        }

        private void MyAutoSizeTextbox_TextChanged(object sender, EventArgs e)
        { 
            Size size = TextRenderer.MeasureText(Text, Font);
            if (size.Width < initSize.Width)
                Size = initSize;
            else Size = size;
        }

        public virtual void TextBox_LostFocus(object sender, System.EventArgs e)
        {
            if (Text == string.Empty)
            {
                ForeColor = Color.FromArgb(204, 202, 204);
                Text = "New To-Do";
            }
           
        }

        public virtual void TextBox_GotFocus(object sender, System.EventArgs e)
        {
            if (Text == "New To-Do")
            {
                ForeColor = Color.Black;
                Text = string.Empty;
            }
        }
    }
}
