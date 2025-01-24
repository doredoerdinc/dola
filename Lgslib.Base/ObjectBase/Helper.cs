using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgsLib.Base
{
    static public class Helper
    {
       static public Type getXafType(string typeFullName)
        {
            var typeInfo = XafTypesInfo.Instance.PersistentTypes.First(X => X.FullName == typeFullName).Type;
            if (typeInfo != null)
            {
                return typeInfo;
            }
            return null;
        }
    }
}
