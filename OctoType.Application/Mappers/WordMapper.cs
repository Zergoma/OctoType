//using System.Reflection.Metadata.Ecma335;

//using OctoType.Application.DTOs;
//using OctoType.Domain.Entities;

//namespace OctoType.Application.Mappers;

//static public class WordMapper
//{
//    static public WordDto ToDto(this Word word)
//        => new ()
//        {
//            Id = word.Id,
//            LanguageCode = word.LanguageCode,
//            Length = word.Length,
//            OccurrenceCount = word.OccurrenceCount,
//            Text = word.Text,
//        };

//    static public Word ToEntityWithoutMetadata(this WordDto wordDto)
//        => new()
//        {
//            Id = wordDto.Id,
//            LanguageCode = wordDto.LanguageCode,
//            Length = wordDto.Length,
//            OccurrenceCount = wordDto.OccurrenceCount,
//            Text = wordDto.Text,
//        };
//}
