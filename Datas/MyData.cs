using BorderlessForm.MyComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderlessForm.Datas
{
    public class MyData
    {
        public Dictionary<string, MyContentPanel> contPanels = new Dictionary<string, MyContentPanel>();

        public Dictionary<string, MyAreaPanel> userArea = new Dictionary<string, MyAreaPanel>();

        public List<MyAreaPanel> userAreas = new List<MyAreaPanel>();

        public List<MyListItem> listItems = new List<MyListItem>();

        public List<MyTodoItem> todoItems = new List<MyTodoItem>();

        public Dictionary<int, MyTodoItem> todoIs = new Dictionary<int, MyTodoItem>();

        public MyContentPanel nowDisplayList = null;

        public int idNum = 0;

        public DateTime initDate = DateTime.Parse("1900-1-1");

        public void SaveToFile(OriginData rawData)
        {
            foreach(MyTodoItem todo in todoItems)
                rawData.todos.Add(new MyTodo(todo.Id,todo.ParentName,todo.Content,todo.Due,todo.FinishDate,todo.IsDelete,todo.IsComplete));
            rawData.idNum = idNum;

            foreach (MyAreaPanel area in userAreas)
            {
                rawData.areas.Add(area.Title.ItemName, area.items);
                rawData.areasName.Add(area.Title.ItemName);
            }
        }
    }
}
