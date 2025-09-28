namespace PaymentApp.Application.Dto.Auth
{
    public class TokenDto
    {
        public string Token { get; set; } = null!;

        public DateTime Expiry { get; set; }

        public bool IsActive { get; set; } = false;
    }
}
