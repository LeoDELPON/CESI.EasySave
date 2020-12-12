using CESI.Server.EasySave.DTO;
using System.Net;
using System.Text.RegularExpressions;

namespace CESI.Server.EasySave.Services
{
    public static class HostInfoBuilder
    {
        public static DTOHostMachine GetHostIpAndName()
        {
            DTOHostMachine _dtoHostMachine = new DTOHostMachine();
            _dtoHostMachine.hostname = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(_dtoHostMachine.hostname);
            IPAddress[] ipAddrList = ipHost.AddressList;

            foreach (IPAddress ip in ipAddrList)
            {
                if (ValidateIpAddressV4(ip.ToString()))
                    _dtoHostMachine.ipAddress = ip;
            }

            return _dtoHostMachine;
        }

        public static bool ValidateIpAddressV4(string ip)
        {
            bool isValidIpAddress;
            string ipPattern = @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";

            Regex regex = new Regex(ipPattern);
            isValidIpAddress = regex.IsMatch(ip);

            return isValidIpAddress;
        }
    }
}
