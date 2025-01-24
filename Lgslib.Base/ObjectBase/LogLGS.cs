using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using System.ComponentModel.DataAnnotations.Schema;
using LgsLib.Base.PermissionPolicy;
using DevExpress.Persistent.Validation;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;

namespace LgsLib.Base
{
    [Table("Log")]
    [DefaultClassOptions]
    public class LogLgs
    {
        public LogLgs() { }
        int _ID;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [XmlIgnore]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        DateTime _EventTime;
        public DateTime EventTime
        {
            get { return _EventTime; }
            set { _EventTime = value; }
        }

        String _Source;
        [MaxLength]
        public String Source
        {
            get { return _Source; }
            set { _Source = value; }
        }

        String _Message;
        [MaxLength]
        public String Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        string _Description;
        [MaxLength]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        String _SourceQuery;
        [MaxLength]
        public String SourceQuery
        {
            get { return _SourceQuery; }
            set { _SourceQuery = value; }
        }


    }



}
