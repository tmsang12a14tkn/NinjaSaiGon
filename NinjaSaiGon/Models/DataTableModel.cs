using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.DataTable
{
    public class DataTableAjaxPostModel
    {
        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<DT_Column> columns { get; set; }
        public DT_Search search { get; set; }
        public List<DT_Order> order { get; set; }
    }

    public class DT_Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public DT_Search search { get; set; }
    }

    public class DT_Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class DT_Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
}
