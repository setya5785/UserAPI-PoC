namespace UserAPI.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string NamaLengkap { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public char Status { get; set; }
    }
}
