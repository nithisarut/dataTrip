namespace dataTrip.Setting
{
    public class JwtSetting
    {
        public string Key { get; set; } = "SecretKey@12345678";
        public string Issuer { get; set; } = "Firstza";
        public string Audience { get; set; } = "nithisarut sinkhong";
        public string Expire { get; set; } = "1";
    }
}
