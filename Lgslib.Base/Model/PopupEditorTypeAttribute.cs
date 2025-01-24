using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LgsLib.Base
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PopupEditorTypeAttribute : Attribute
    {
        public PopupEditorTypeAttribute(string objectTypeMemberName, string parametersMemberName)
        {
            this.ObjectTypeMemberName = objectTypeMemberName;
        }
        public PopupEditorTypeAttribute(string objectTypeMemberName) : this(objectTypeMemberName, null) { }
        public string ObjectTypeMemberName { get; set; }
     }
}
