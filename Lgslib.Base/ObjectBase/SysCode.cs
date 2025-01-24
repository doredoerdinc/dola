using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.DC;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using System.ComponentModel.DataAnnotations.Schema;
using LgsLib.Base.PermissionPolicy;
using DevExpress.Persistent.Validation;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
namespace LgsLib.Base
{ 

    [Table("ObjectCode")]
    [DefaultClassOptions]
    public class ObjectCode : BaseObjectI, INotifyPropertyChanged
    {  
        public ObjectCode()
        {
            
        }

        Type _ObjectTypeCore;
        [NotMapped]
        [ImmediatePostData]
        public Type ObjectTypeCore
        {
            get { return _ObjectTypeCore; }
            set { 
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

        Warehouse _Warehouse;
        public virtual Warehouse Warehouse
        {
            get { return _Warehouse; }
            set { _Warehouse = value; }
        }


        [DefaultValue(999999999)]
        long _MaxNumber;
        public long MaxNumber
        {
            get { return _MaxNumber; }
            set { _MaxNumber = value; }
        } 
       
        long _MinNumber;
        [DefaultValue(999)]
        public long MinNumber
        {
            get { return _MinNumber; }
            set { _MinNumber = value; }
        }
         public int TotalCharacter
        {
            get {
                if(CodeExample != null) {
                          return CodeExample.Count();
                     }
                return 0;
            } 
            
        } 
        
        int _Increasing;
        [DefaultValue(1)]
        public int Increasing
        {
            get { return _Increasing; }
            set { _Increasing = value; }
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
            get {

                if (Format != null&&ObjectTypeName!=null)
                {
                    var example = GlobalSys.getSysCode(this, SecuritySystemBase.Warehouse); 
                    return example;
                }
                return null;
               }
           
        } 
         
        int _NextValue;
        public int NextValue
        {
            get { return _NextValue; }
            set { _NextValue = value; }
        }
        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if(propertyName== "ObjectTypeCore")
            {

                //var items = mdw.GetItems<EntityType>(DataSpace.CSpace);

                //foreach (var item in items)
                //{
                //    var a = 1;

                //}
               // var typeName = ObjectSpace.TypesInfo.PersistentTypes.Where(x => x.Name == ObjectTypeCore.Name).FirstOrDefault();

                //var a = 1; 
                

                // var entityType = _dbContext.Model.FindEntityType("Chadwick.Database.Entities." + className);

                ObjectTypeName = this.ObjectTypeCore.FullName;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }

    public static class GlobalSys
    {
        public static string getSysCode(ObjectCode objectCode, Warehouse mainCompany)
        {
            string CodeDefination = null;
            if (objectCode.Format != null)
            {
                try
                {
                    //Random rnd = new Random();
                    //char randomChar = (char)rnd.Next('A', 'Z');
                    CodeDefination = string.Format(objectCode.NextValue.ToString(objectCode.Format));
                    return CodeDefination;
                }
                catch (Exception)
                {
                }
            }
            return CodeDefination;
        }


    }
}
 

	 