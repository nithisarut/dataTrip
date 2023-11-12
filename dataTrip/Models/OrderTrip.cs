using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dataTrip.Models
{
    public class OrderTrip
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public DateTime ContactTime { get; set; }
        public int AmountAdult { get; set; }
        public int AmountKid { get; set; }
        public int Total { get; set; }
        public int  SingleStay { get; set; } 
        public int  Stay2Persons { get; set; }
        public int  Stay3Persons { get; set; }
        public DateTime Created { get; set; } = new DateTime();
        public OrderStatus Status { get; set; } = OrderStatus.WaitingForPayment;

        public int AccountsId { get; set; }
        [ForeignKey("AccountsId")]
        public virtual Accounts Accounts { get; set; }

        public int TripId { get; set; }
        [ForeignKey("TripId")]
        public virtual Trip Trip { get; set; }


    }
}
