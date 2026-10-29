using System;

namespace TradeLedgerWebConsole.Infrastructure.IService
{
    public class EnumsHelper
    {
        public static string GetEnumString(Enums.Operation operation)
        {

            switch (operation)
            {
                case Enums.Operation.Save:
                    return nameof(Enums.Operation.Save);
                case Enums.Operation.Delete:
                    return nameof(Enums.Operation.Delete);
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
        }

        public static string GetEnumString(Enums.MenuItem menuItem)
        {

            switch (menuItem)
            {
                case Enums.MenuItem.Company:
                    return nameof(Enums.MenuItem.Company);
                case Enums.MenuItem.User:
                    return nameof(Enums.MenuItem.User);
                case Enums.MenuItem.Client:
                    return nameof(Enums.MenuItem.Client);
                case Enums.MenuItem.ChangePassword:
                    return nameof(Enums.MenuItem.ChangePassword);
                case Enums.MenuItem.Address:
                    return nameof(Enums.MenuItem.Address);
                case Enums.MenuItem.Broker:
                    return nameof(Enums.MenuItem.Broker);
                case Enums.MenuItem.Item:
                    return nameof(Enums.MenuItem.Item);
                case Enums.MenuItem.Contract:
                    return nameof(Enums.MenuItem.Contract);
                default:
                    throw new ArgumentOutOfRangeException(nameof(menuItem), menuItem, null);
            }
        }

        public static string GetEnumString(Enums.Result result)
        {
            switch (result)
            {
                case Enums.Result.Success:
                    return nameof(Enums.Result.Success);
                case Enums.Result.Error:
                    return nameof(Enums.Result.Error);
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        public static int GetEnumIndex(Enums.Role role)
        {
            switch (role)
            {
                case Enums.Role.System_Owner:
                    return (int)Enums.Role.System_Owner;
                case Enums.Role.Super_Admin:
                    return (int)Enums.Role.Super_Admin;
                case Enums.Role.Admin:
                    return (int)Enums.Role.Admin;
                case Enums.Role.Employee:
                    return (int)Enums.Role.Employee;
                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, null);
            }
        }

    }
}
