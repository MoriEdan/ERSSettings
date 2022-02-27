using System;
using System.DirectoryServices.ActiveDirectory;

namespace ERSSettings.Helpers
{
    internal class DomainHelper
    {
        internal static bool PcInDomain()
        {
            bool result;

            try
            {
                result = Domain.GetComputerDomain() != null;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}