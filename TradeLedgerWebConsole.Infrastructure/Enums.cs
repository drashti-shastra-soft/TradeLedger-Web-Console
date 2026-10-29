
namespace TradeLedgerWebConsole.Infrastructure
{
    public class Enums
    {
        public enum Operation
        {
            Save = 1,
            Delete = 2,
        }

        public enum MenuItem
        {
            Company = 1,
            User = 2,
            Client = 3,
            ChangePassword = 4,
            Address = 5,
            Broker = 6,
            Item = 7,
            Contract = 8,
        }

        public enum Result
        {
            Success = 1,
            Error = 2
        }

        public enum Role
        {
            System_Owner = 1,
            Super_Admin = 2,
            Admin = 3,
            Employee = 4
        }
    }
}
