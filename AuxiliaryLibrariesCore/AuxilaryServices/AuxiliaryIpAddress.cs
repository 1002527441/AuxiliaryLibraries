using System.Net;
using System.Net.Http;
using System.Web;
using System.ServiceModel.Channels;
using NWebsec.AspNetCore.Core.Web;
using Microsoft.AspNetCore.Http;

namespace AuxiliaryLibraries.Core.AuxilaryServices
{
    /// <summary>
    /// AuxiliaryIpAddress helps you to work with IPs
    /// </summary>
    public static class AuxiliaryIpAddress
    {
        /// <summary>
        /// Get real client IP from request
        /// </summary>
        /// <param name="request">HttpRequestMessage</param>
        /// <returns>IPAddress</returns>
        public static IPAddress GetClientIp(this Microsoft.AspNetCore.Http.HttpRequest request)
        {
            if (request == null) return null;
            return request.HttpContext.Connection.RemoteIpAddress;
        }

        /// <summary>
        /// You can get Local IP Address
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }

        /// <summary>
        /// Return true, if ip is in the range of lowerRange and upperRange
        /// </summary>
        /// <param name="ip">Current IP</param>
        /// <param name="lowerRange">Lowest IP range</param>
        /// <param name="upperRange">Uppest IP range</param>
        /// <returns>bool</returns>
        public static bool IsInRange(IPAddress ip, IPAddress lowerRange, IPAddress upperRange)
        {
            var localAddress = new IPAddress(new byte[4] { 127, 0, 0, 1 });
            if (ip.Equals(localAddress))
                return false;

            byte[] addressBytes = ip.GetAddressBytes();
            byte[] rangeLowerBytes = lowerRange.GetAddressBytes();
            byte[] rangeUpperBytes = upperRange.GetAddressBytes();

            if (addressBytes.Length != rangeLowerBytes.Length ||
                    addressBytes.Length != rangeUpperBytes.Length)
            {
                // IPv4/IPv6 mismatch
                return false;
            }

            bool lowerBoundary = true, upperBoundary = true;

            for (int i = 0; i < rangeLowerBytes.Length &&
                (lowerBoundary || upperBoundary); i++)
            {
                if (lowerBoundary && addressBytes[i] < rangeLowerBytes[i] ||
                    upperBoundary && addressBytes[i] > rangeUpperBytes[i])
                {
                    return false;
                }

                lowerBoundary &= addressBytes[i] == rangeLowerBytes[i];
                upperBoundary &= addressBytes[i] == rangeUpperBytes[i];
            }
            return true;
        }
    }
}