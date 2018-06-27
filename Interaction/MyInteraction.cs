using BorderlessForm.Animation;
using BorderlessForm.MyComponent;
using System.Windows.Forms;

namespace BorderlessForm.Interaction
{
    class MyInteraction
    {
        static public void listClick(Control s, bool isArea = false)
        {
            Label title;
            if (s is MyListItem) isArea = ((MyListItem)s).ItemKind;

            foreach (Control child in s.Controls)
                if (child is Label)
                {
                    title = (Label)child;
                    if (IsNum(title.Text)) continue;
                    if (isArea && (!MainForm.Data.userArea.ContainsKey(title.Text)))
                    {
                        MainForm.Data.userArea.Add(title.Text, (MyAreaPanel)s.Parent);
                        MainForm.Data.userAreas.Add((MyAreaPanel)s.Parent);
                    }
                    DisplayList(title.Text, isArea);
                    break;
                }

        }
        static private void DisplayList(string title,bool isArea = false)
        {
            MyContentPanel toDisplay;

            if (MainForm.Data.contPanels.ContainsKey(title))
            {
                toDisplay = MainForm.Data.contPanels[title];
                toDisplay.updateData();
               // MainForm.mainform.Controls.Remove(Data.contPanels[title]);
               // Data.contPanels[title] = toDisplay;
            }
            else
            {
                toDisplay = GetDisplayList(title, isArea);
                MainForm.Data.contPanels.Add(title, toDisplay);
                MyAnimation.AddMouseDownEvent(toDisplay);
                MainForm.mainform.Controls.Add(toDisplay);
            }
            toDisplay.BringToFront();
            MainForm.Data.nowDisplayList = toDisplay;
        }
        static private MyContentPanel GetDisplayList(string title,bool isArea = false)
        {
            if (isArea) return new MyUserAreaPanel(title);
            switch (title)
            {
                case "Inbox":
                    return new MyInboxPanel(title);
                case "Today":
                    return new MyTodayPanel(title);
                case "Upcoming":
                    return new MyUpcomingPanel(title);
                case "Anytime":
                    return new MyAnytimePanel(title);
                case "Someday":
                    return new MySomedayPanel(title);
                case "Logbook":
                    return new MyLogbookPanel(title);
                case "Trash":
                    return new MyTrashPanel(title);
                default:
                    return new MyUserListPanel(title);

            }
        }
        static private bool IsNum(string text)
        {
            foreach (char c in text)
                if (c < 48 || c > 57) return false;
            return true;
        }
    }
}
