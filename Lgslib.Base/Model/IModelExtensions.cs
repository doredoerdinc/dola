using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;

namespace LgsLib.Base
{
    public class ModelCategory
    {
        public const string ColumnList = "01-ColumnSettings";
        public const string ViewList = "02-Views";
        public const string MouseSettings = "Mouse Settings";
        public const string LinkObjects = "Link Objects";


    }
    public enum GridMouseEventType
    {

        Click = 0,
        DoubleClick = 2,
        MouseDown = 3,
        DragOver = 4,
        DragDrop = 5,
        AllowDrop = 6

    }

    public enum GridColumnMouseCursor
    {
        Hand = 0,
        WaitCursor = 1,
        Cross = 2
    }

    public interface ISelectionCriteria
    {
        void AddSelectedObjects(IEnumerable<object> objects);
        CriteriaOperator SelectionCriteria { get; set; }
    }
    

}
