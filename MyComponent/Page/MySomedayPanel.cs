using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderlessForm.MyComponent
{
    class MySomedayPanel:MyContentPanel
    {
        public MySomedayPanel(string title):base(title)
        {
            TitleIcon.Image = MyImage.bigSomeday;
            TitleIcon.Size = new System.Drawing.Size(25, 25);
            TitleIcon.Margin = new System.Windows.Forms.Padding(0, 7, 0, 3);
            AddTitleToTitilePanel(0);
        }
    }
}
