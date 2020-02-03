using System.Collections.Generic;

namespace NinjaSaiGon.Data.Models
{
    public class ControllerAction
    {
        public ControllerAction()
        {
            IsShow = true;
        }

        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Params { get; set; }
        public string MenuGroup { get; set; }
        public string Site { get; set; }
        public string Description { get; set; }
        public bool IsShow { get; set; }

        public virtual ICollection<ControllerActionPermission> Permissions { get; set; }
    }
}
