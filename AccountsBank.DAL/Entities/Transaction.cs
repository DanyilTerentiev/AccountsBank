namespace AccountsBank.DAL.Entities;

public class Transaction : BaseEntity
{
    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public double Sum { get; set; }
}