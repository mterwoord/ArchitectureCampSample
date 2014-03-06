using System.ComponentModel;

namespace Services.SessionServiceReference
{
    public partial class SessionDto : object, IEditableObject
    {
        public void BeginEdit()
        {
            EditableObjectHelper.BeginEdit(this);
        }

        public void CancelEdit()
        {
            EditableObjectHelper.CancelEdit(this);
        }

        public void EndEdit()
        {
            EditableObjectHelper.EndEdit(this);
        }
    }
}
