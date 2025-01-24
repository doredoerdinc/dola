using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using LgsLib.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;
using System.Drawing;
using DevExpress.ExpressApp.DC;
namespace dola.Module
{

    public class DocumentOcr:BaseObjectI
    {
        public DocumentOcr() { }

        string _DocumentCode;
        public string DocumentCode
        {
            get { return _DocumentCode; }
            set { _DocumentCode = value; }
        }

        String _DocumentProcessTime;
        public String DocumentProcessTime
        {
            get { return _DocumentProcessTime; }
            set { _DocumentProcessTime = value; }
        }

        String _ReadFolder;
        public String ReadFolder
        {
            get { return _ReadFolder; }
            set { _ReadFolder = value; }
        }

        String _SuccessFolder;
        public String SuccessFolder
        {
            get { return _SuccessFolder; }
            set { _SuccessFolder = value; }
        }

        Person _Employe;
        public virtual Person Employe
        {
            get { return _Employe; }
            set { _Employe = value; }
        }

        Owner _Company;
        public virtual Owner Company
        {
            get { return _Company; }
            set { _Company = value; }
        }  

    }
}
