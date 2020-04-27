using AutoMapper;
using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Profiles
{
    public class MaxicoursProfiles : Profile
    {
        public MaxicoursProfiles()
        {
            CreateMap<ReferenceEntity, ReferenceModel>()
                .ReverseMap();

            CreateMap<BreadcrumbEntity, BreadcrumbModel>()
                .ReverseMap();

            CreateMap<HeaderEntity, HeaderModel>()
                .ReverseMap();

            CreateMap<ThemeEntity, ThemeModel>()
                .ReverseMap();

            CreateMap<CategoryEntity, CategoryModel>()
                .ReverseMap();

            CreateMap<ItemEntity, ItemModel>()
                .ReverseMap();

            CreateMap<LessonEntity, LessonModel>()
                .ReverseMap();

            CreateMap<VideoLessonEntity, VideoLessonModel>()
                .ReverseMap();

            CreateMap<VideoExerciseEntity, VideoExerciseModel>()
                .ReverseMap();

            CreateMap<SummarySheetEntity, SummarySheetModel>()
                 .ReverseMap();

            CreateMap<SubjectEntity, SubjectModel>()
                .ReverseMap();

            CreateMap<SummarySubjectEntity, SummarySubjectModel>()
                .ReverseMap();

            CreateMap<TestEntity, TestModel>()
                .ReverseMap();

            CreateMap<SchoolLevelEntity, SchoolLevelModel>()
                .ReverseMap();
        }
    }
}
