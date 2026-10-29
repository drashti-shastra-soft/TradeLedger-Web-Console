namespace TradeLedgerWebConsole.Domain
{
    public class Address : IAuditable
    {
        public int Id { get; set; }
        public int BrokerId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int AuditId { get; set; }
        public int ClientId { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }

    }
}
