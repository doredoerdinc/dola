using DevExpress.Persistent.Base;
using LgsLib.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dola.Module
{

    [Table("DocumentType")]
    [DefaultClassOptions]
    public class DocumentType : BaseObjectC
    {

    }

    [Table("DocumentTracking")]
    [DefaultClassOptions]
    public class DocumentTracking : BaseObjectC
    {

        string _DocumentCode;
        public string DocumentCode
        {
            get { return _DocumentCode; }
            set { _DocumentCode = value; }
        }

        DocumentType _DocumentType;
        public virtual DocumentType DocumentType
        {
            get { return _DocumentType; }
            set { _DocumentType = value; }
        }



        Vehicle _Vehicle;
        public virtual Vehicle Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; }
        }

        Person _Employee;
        public virtual Person Employee
        {
            get { return _Employee; }
            set { _Employee = value; }
        }

        
        DateTime? _StartingTime;
        public virtual DateTime? StartingTime
        {
            get { return _StartingTime; }
            set { _StartingTime = value; }
        }

        DateTime? _FinishTime;
        public virtual DateTime? FinishTime
        {
            get { return _FinishTime; }
            set { _FinishTime = value; }
        } 
    }
}
