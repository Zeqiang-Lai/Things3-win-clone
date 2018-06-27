using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderlessForm.MyComponent
{
    class MyInboxPanel : MyContentPanel
    {
        public MyInboxPanel(string titleName) : base(titleName)
        {
            TitleIcon.Image = MyImage.bigInbox;
            TitleIcon.Size = new System.Drawing.Size(25, 25);
            TitleIcon.Margin = new System.Windows.Forms.Padding(0, 5, 0, 3);
            AddTitleToTitilePanel(0);
        }

        
    }
}
