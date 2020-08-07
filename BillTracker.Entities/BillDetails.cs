using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillTracker.Entities
{
    public class BillDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String BillId { get; set; }

        [BsonElement("Bill Title")]
        [BsonRequired]
        public String Title { get; set; }

        [BsonElement("Bill Category")]
        [BsonRequired]
        public BillCategory Catagory { get; set; }

        [BsonElement("Bill Amount")]
        [BsonRequired]
        public long Amount { get; set; }

        [BsonElement("Bill Date Of Entry")]
        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DateOfEntry{ get; set; }

        [BsonElement("Bill Due Date")]
        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DueDate { get; set; }

        [BsonElement("Bill Payment Mode")]
        [BsonRequired]
        public BillPaymentMode PaymentMode { get; set; }

        [BsonElement("Bill Status")]
        [BsonRequired]
        public BillStatus Status { get; set; }

    }
}
