namespace TradeLedgerWebConsole.Domain
{
    public interface IAuditable
    {
        int CreatedBy { get; set; }
        int? LastModifiedBy { get; set; }
    }
}
