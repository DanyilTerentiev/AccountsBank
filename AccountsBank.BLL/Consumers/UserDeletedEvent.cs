﻿namespace AccountsBank.BLL.Consumers;

public class UserDeletedEvent
{
    public Guid UserId { get; set; }

    public DateTime DeletedAt { get; set; }
}