namespace Alchemy.Domain.Contracts;

public record CreateSlotRequest
(
    Guid MasterId,
        DateTime SlotTime
);