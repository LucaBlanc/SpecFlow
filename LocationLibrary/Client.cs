using System;

namespace LocationLibrary
{
    public class Client
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public DateTime Birth { get; }
        public int LicenseNumber { get; }
        public DateTime License { get; }

        public Client(string username, string password, DateTime birth, int licenseNumber, DateTime license )
        {
            this.Username       = username;
            this.Password       = password;
            this.Birth          = birth;
            this.LicenseNumber  = licenseNumber;
            this.License        = license;
        }
    }
}