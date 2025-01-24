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

namespace dola.Module
{ 
    public interface IStock
    {
        Item Item { get; set; }
        double? AvailableQuantity { get; set; }
        String Container { get; set; }
        LocationWarehouse Location { get; set; }
        String Batch { get; set; }
        String ExpireDate { get; set; }

        Owner Owner { get; set; }

    }
    [Table("StockItem")]
    [DefaultClassOptions]
    public class StockItem : BaseObjectWarehouseID
    {
        public StockItem()
        {
            TaskStepTransactions = new List<TaskTransaction>();
            ItemReservations = new List<StockItemReservation>();
        }

        public virtual IList<TaskTransaction> TaskStepTransactions { get; set; }

        public virtual IList<StockItemReservation> ItemReservations { get; set; }

        Owner _Owner;
        public virtual Owner Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        Item _Item;
        public virtual Item Item
        {
            get { return _Item; }
            set { _Item = value; }
        }

        Container _Container;
        public virtual Container Container
        {
            get { return _Container; }
            set { _Container = value; }
        }
         
        //LocationWarehouse _LocationWarehouse;
        //public LocationWarehouse LocationWarehouse
        //{
        //    get { return _LocationWarehouse; }
        //    set { _LocationWarehouse = value; }
        //} 

        StockItem _LinkStockItem;
        public virtual StockItem LinkStockItem
        {
            get { return _LinkStockItem; }
            set { _LinkStockItem = value; }
        } 

        Task _Task;
        public virtual Task Task
        {
            get { return _Task; }
            set { _Task = value; }
        } 

        String _Batch;
        public String Batch
        {
            get { return _Batch; }
            set { _Batch = value; }
        } 
        string _ExpireDate;
        public string ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }

        double? _Temperature;
        public double? Temperature
        {
            get { return _Temperature; }
            set { _Temperature = value; }
        }

        double? _Reservation;
        public double? ReservationQuantity
        {
            get { return _Reservation; }
            set { _Reservation = value; }
        }

        double? _AvailableQuantity;
        public double? AvailableQuantity
        {
            get { return _AvailableQuantity; }
            set { _AvailableQuantity = value; }
        }

        //TaskTransaction _LastTransaction;
        //public virtual TaskTransaction LastTransaction
        //{
        //    get { return _LastTransaction; }
        //    set { _LastTransaction = value; }
        //}

    }
}
