using System.Collections.Generic;

namespace TradeLedgerWebConsole.Domain
{
    public class Contract : IAuditable ,ICompanyScoped
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int SellerClientId { get; set; }
        public int? SellerBrokerId { get; set; }
        public int BuyerClientId { get; set; }
        public int? BuyerBrokerId { get; set; }
        public decimal? SellerBrokerage { get; set; }
        public decimal? BuyerBrokerage { get; set; }
        public int AuditId { get; set; }
        public bool IsActive { get; set; } = true;
        public string Status { get; set; }
        public string SellerClient { get; set; }
        public string SellerBroker { get; set; }
        public string BuyerClient { get; set; }
        public string BuyerBroker { get; set; }
        public string Company { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }

        public List<Domain.ContractItem> ContractItems { get; set; } = new List<Domain.ContractItem>();
    }
}
