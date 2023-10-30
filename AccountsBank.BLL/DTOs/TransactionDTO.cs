namespace AccountsBank.BLL.DTOs;

public class TransactionDTO
{
    public Guid? Id { get; set; }
    
    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public double Sum { get; set; }
}