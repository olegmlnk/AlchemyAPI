namespace Alchemy.Domain.Contracts;

public record MasterScheduleResponse(
    Guid Id,
    Guid MasterId,
    string MasterName,
    DateTime SlotTime, 
    bool IsBooked
    );