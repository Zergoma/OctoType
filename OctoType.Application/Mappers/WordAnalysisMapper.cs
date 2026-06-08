//using OctoType.Application.DTOs;
//using OctoType.Domain.Entities;

//namespace OctoType.Application.Mappers;

//static public class WordAnalysisMapper
//{
//    static public WordAnalysisDto ToDto(this WordAnalysis wordAnalysis)
//    => new ()
//        {
//            Id = wordAnalysis.Id,
//            Layout = wordAnalysis.Layout,
//            FingerMask = wordAnalysis.FingerMask,
//            RowMask = wordAnalysis.RowMask,
//            UsesLeftHand = wordAnalysis.UsesLeftHand,
//            UsesRightHand = wordAnalysis.UsesRightHand,
//        };

    

//    static public WordAnalysis ToEntityWithoutMetadata(this WordAnalysisDto wordAnalysis)
//    => new()
//    {
//        Id = wordAnalysis.Id,
//        Layout = wordAnalysis.Layout,
//        FingerMask = wordAnalysis.FingerMask,
//        RowMask = wordAnalysis.RowMask,
//        UsesLeftHand = wordAnalysis.UsesLeftHand,
//        UsesRightHand = wordAnalysis.UsesRightHand,
//    };
//}