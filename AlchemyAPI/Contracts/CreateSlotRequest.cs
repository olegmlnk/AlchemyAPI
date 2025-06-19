namespace AlchemyAPI.Contracts;

public record CreateSlotRequest
(
    long MasterId,
        DateTime SlotTime
);