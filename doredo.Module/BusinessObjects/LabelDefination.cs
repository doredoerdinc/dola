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

using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;

namespace dola.Module
{


    [Table("ContainerTemplate")]
    [DefaultClassOptions]
    [XafDefaultProperty("Name")]
    public class ContainerTemplate : BaseObjectI, INotifyPropertyChanged
    {
        public ContainerTemplate()
        {
            Containeries = new List<Container>();

        }

        public virtual IList<Container> Containeries { get; set; }
        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string getCode(int val)
        {
            string CodeDefination = null;
            if (Format != null)
            {
                try
                {
                    //Random rnd = new Random();
                    //char randomChar = (char)rnd.Next('A', 'Z');
                    CodeDefination = string.Format(val.ToString(Format));
                    return CodeDefination;
                }
                catch (Exception)
                {
                }
            }
            return CodeDefination;
        }


        Type _ObjectTypeCore;
        [NotMapped]
        [ImmediatePostData]
        public Type ObjectTypeCore
        {
            get { return _ObjectTypeCore; }
            set
            {
                _ObjectTypeCore = value;
                OnPropertyChanged();
            }
        }

        string _ObjectTypeName;
        public string ObjectTypeName
        {
            get { return _ObjectTypeName; }
            set { _ObjectTypeName = value; }
        }

        public int TotalCharacter
        {
            get
            {
                if (CodeExample != null)
                {
                    return CodeExample.Count();
                }
                return 0;
            }
        }
               
        string _Format;
        [DefaultValue("####################")]
        [ImmediatePostData]
        public string Format
        {
            get { return _Format; }
            set { _Format = value; }
        }

        [NotMapped]
        public string CodeExample
        {
            get
            {
                if (Format != null && ObjectTypeName != null)
                {
                    var example = getCode(this.LastValue);
                    return example;
                }
                return null;
            } 
        }

        int _LastValue;
        public int LastValue
        {
            get { return _LastValue; }
            set { _LastValue = value; }
        }

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == "ObjectTypeCore")
            {
                ObjectTypeName = this.ObjectTypeCore.FullName;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    [Table("Container")]
    [DefaultClassOptions]
    public class Container:BaseObjectC
    {
        public Container()
        {
            StockItems = new List<StockItem>();
        } 

        public virtual IList<StockItem> StockItems { get; set; }


        ContainerTemplate _ContainerTemplate;
        public virtual ContainerTemplate ContainerTemplate
        {
            get { return _ContainerTemplate; }
            set { _ContainerTemplate = value; }
        }

        Order _Order;
        public virtual Order Order
        {
            get { return _Order; }
            set { _Order = value; }
        }

        Trip _Trip;
        public virtual Trip Trip
        {
            get { return _Trip; }
            set { _Trip = value; }
        }

        OrderLine _OrderLine;
        public virtual OrderLine OrderLine
        {
            get { return _OrderLine; }
            set { _OrderLine = value; }
        }

        LocationWarehouse _LocationWarehouse;
        public virtual LocationWarehouse LocationWarehouse
        {
            get { return _LocationWarehouse; }
            set { _LocationWarehouse = value; }
        } 

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        
        String _LinkCode;
        public String LinkCode
        {
            get { return _LinkCode; }
            set { _LinkCode = value; }
        } 
        String _Code;
        public String Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

    }
  
} 
