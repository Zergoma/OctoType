using OctoType.Application.DTOs;
using OctoType.Domain.Enums;
using OctoType.Application.Mappers;
using OctoType.Application.Interfaces;

namespace OctoType.Application.Services;

public class KeyBoardLayoutAvailableService : IKeyBoardLayoutAvailableService
{
    public List<KeyBoardLayoutDto> GetKeyBoardAvailable()
    {
        return
        [
            KeyboardLayout.AzertyFr.ToDto().GetValue,
            // TODO
            //KeyboardLayout.QwertyUs.ToDto().Value!,
            //KeyboardLayout.QwertzDe.ToDto().Value!,
            //KeyboardLayout.Bepo.ToDto().Value!,
        ];
    }
}
