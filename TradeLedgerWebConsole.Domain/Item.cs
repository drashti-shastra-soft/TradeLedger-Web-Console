namespace TradeLedgerWebConsole.Domain
{
    public class Item : IAuditable, ICompanyScoped
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string HSNCode { get; set; }
        public string Status { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int UnitId { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
    }
}
