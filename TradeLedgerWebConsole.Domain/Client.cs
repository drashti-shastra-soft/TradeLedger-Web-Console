namespace TradeLedgerWebConsole.Domain
{
    public class Client : IAuditable, ICompanyScoped
    {
        public int Id { get; set; }
        public int ClientTypeId { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Brand { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string PanNumber { get; set; }
        public string GSTNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public int AuditId { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public string Token { get; set; }
        public string Status { get; set; }
        public string ClientType { get; set; }
        public int CompanyId { get; set; }
    }
}
