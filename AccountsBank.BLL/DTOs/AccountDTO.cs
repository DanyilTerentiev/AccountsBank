namespace AccountsBank.BLL.DTOs;

public class AccountDTO
{
    public Guid? Id { get; set; }

    public double Balance { get; set; }

    public string CardType { get; set; } = null!;

    public Guid UserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }
}