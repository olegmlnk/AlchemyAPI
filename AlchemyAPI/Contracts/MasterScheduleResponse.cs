namespace AlchemyAPI.Contracts;

public record MasterScheduleResponse(
    long Id,
    long MasterId,
    string MasterName,
    DateTime SlotTime, 
    bool IsBooked
    );