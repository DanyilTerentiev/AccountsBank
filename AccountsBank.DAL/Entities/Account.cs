using AccountsBank.DAL.Enums;

namespace AccountsBank.DAL.Entities;

public class Account : BaseEntity
{
    public double Balance { get; set; }

    public string CardType { get; set; } = null!;

    public Guid UserId { get; set; }
}