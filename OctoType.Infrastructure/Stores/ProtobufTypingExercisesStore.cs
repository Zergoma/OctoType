using OctoType.Application.Interfaces;
using OctoType.Application.Models.Typing.Exercices;

using OctoType.Infrastructure.Protos;
using OctoType.Infrastructure.Mappers;
using OctoType.Application;

using Google.Protobuf;

namespace OctoType.Infrastructure.Stores;

public class ProtobufTypingExercisesStore : IExerciseSettingsStore
{
    public async Task<Result<TypingExercices>> LoadAsync(string path)
    {
        if (!File.Exists(path))
            return Result<TypingExercices>
                .Fail($"File doesn't exist: {path}");

        ProtoTypingExerciceList exercicesList = new();
        using (var input = File.OpenRead(path))
        {
            exercicesList = ProtoTypingExerciceList.Parser.ParseFrom(input);
        }

        TypingExercices toReturnList = new();
        foreach (ProtoTypingExercice protoExerciceItem in exercicesList.Exercices)
        {
            Result<TypingExercise> typingExerciceResult
                = ProtoTypingExerciceMapper.ToModel(protoExerciceItem);

            if(!typingExerciceResult.Success)
            {
                return Result<TypingExercices>
                    .Fail(typingExerciceResult.Error);
            }

            toReturnList.Exercices.Add(typingExerciceResult.GetValue);
        }
            
        return Result<TypingExercices>.Ok(toReturnList);
    }

    public async Task<Result<bool>> SaveAsync(TypingExercices settings, string path)
    {
        Result<ProtoTypingExerciceList> resu
            = ProtoTypingExerciceMapper.ToProtobuf(settings);

        if (!resu.Success)
            return Result<bool>
                .Fail(resu.Error);

        ProtoTypingExerciceList exercicesList = resu.GetValue;

        using (var output = File.Create(path))
        {
            exercicesList.WriteTo(output);
        }

        return Result<bool>.Ok(true);
    }
}
