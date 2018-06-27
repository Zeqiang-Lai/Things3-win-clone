using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorderlessForm.Datas
{
    [Serializable]
    public class OriginData
    {
        public List<MyTodo> todos = new List<MyTodo>();
        public Dictionary<string, List<string>> areas = new Dictionary<string, List<string>>();
        public List<string> areasName = new List<string>();
        public int idNum;
    }
}
