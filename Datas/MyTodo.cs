using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderlessForm.Datas
{
    [Serializable]
    public class MyTodo
    {
        int id;
        string parentName;
        string content;
        DateTime due;
        DateTime finish;
        bool isDelete;
        bool isComplete;

        public MyTodo(int _id, string _parent, string _content, DateTime _due,
            DateTime _finish, bool _isDelete, bool _isComplete)
        {
            Id = _id;
            ParentName = _parent;
            Content = _content;
            Due = Due;
            Finish = _finish;
            IsDelete = _isDelete;
            IsComplete = _isComplete;
        }

        public int Id { get => id; set => id = value; }
        public string Content { get => content; set => content = value; }
        public DateTime Due { get => due; set => due = value; }
        public DateTime Finish { get => finish; set => finish = value; }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
        public bool IsComplete { get => isComplete; set => isComplete = value; }
        public string ParentName { get => parentName; set => parentName = value; }
    }
}
