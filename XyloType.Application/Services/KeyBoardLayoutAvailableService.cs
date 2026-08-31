using XyloType.Domain.Enums;
using XyloType.Application.DTOs;
using XyloType.Application.Mappers;
using XyloType.Application.Interfaces;

namespace XyloType.Application.Services;

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
