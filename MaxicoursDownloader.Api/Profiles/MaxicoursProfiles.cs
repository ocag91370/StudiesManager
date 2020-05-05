using AutoMapper;
using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Models.Result;
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

            CreateMap<ItemModel, ItemKeyModel>()
                .ReverseMap();

            CreateMap<SubjectModel, SubjectResultModel>()
                .ReverseMap();

            CreateMap<SummarySubjectModel, SubjectResultModel>()
                .ReverseMap();

            CreateMap<SchoolLevelModel, SchoolLevelResultModel>()
                .ReverseMap();

            CreateMap<IEnumerable<ItemModel>, IEnumerable<ItemKeyResultModel>>()
                .ConvertUsing<ItemModelListToItemKeyResultModelListConverter>();

            CreateMap<IEnumerable<ItemModel>, SubjectKeyResultModel>()
                .ConvertUsing<ItemModelListToSubjectKeyResultModelConverter>();

            CreateMap<IEnumerable<ItemModel>, SchoolLevelKeyResultModel>()
                .ConvertUsing<ItemModelListToSchoolLevelKeyResultModelConverter>();

            CreateMap<IEnumerable<ItemModel>, SubjectCountResultModel>()
                .ConvertUsing<ItemModelListToSubjectCountResultModelConverter>();

            CreateMap<IEnumerable<ItemModel>, SchoolLevelCountResultModel>()
                .ConvertUsing<ItemModelListToSchoolLevelCountResultModelConverter>();
        }

        public class ItemModelListToItemKeyResultModelListConverter : ITypeConverter<IEnumerable<ItemModel>, IEnumerable<ItemKeyResultModel>>
        {
            public IEnumerable<ItemKeyResultModel> Convert(IEnumerable<ItemModel> source, IEnumerable<ItemKeyResultModel> destination, ResolutionContext context)
            {
                foreach (var item in source)
                {
                    yield return new ItemKeyResultModel { Id = item.Id, Index = item.Index };
                }
            }
        }

        public class ItemModelListToSubjectKeyResultModelConverter : ITypeConverter<IEnumerable<ItemModel>, SubjectKeyResultModel>
        {
            public SubjectKeyResultModel Convert(IEnumerable<ItemModel> source, SubjectKeyResultModel destination, ResolutionContext context)
            {
                var summarySubject = source.FirstOrDefault().SummarySubject;

                return new SubjectKeyResultModel
                {
                    Id = summarySubject.Id,
                    Items = context.Mapper.Map<IEnumerable<ItemKeyResultModel>>(source).ToList()
                };
            }
        }

        public class ItemModelListToSchoolLevelKeyResultModelConverter : ITypeConverter<IEnumerable<ItemModel>, SchoolLevelKeyResultModel>
        {
            public SchoolLevelKeyResultModel Convert(IEnumerable<ItemModel> source, SchoolLevelKeyResultModel destination, ResolutionContext context)
            {
                var schoolLevel = source.First().SummarySubject.SchoolLevel;

                var groupBySummarySubjectList = source.GroupBy(
                    o => o.SummarySubject.Id,
                    o => o,
                    (subjectId, subjectItemList) =>
                    {
                        (SummarySubjectModel Subject, List<ItemModel> Items) result = (subjectItemList.FirstOrDefault().SummarySubject, subjectItemList.ToList());
                        return result;
                    })
                        .ToList();

                return new SchoolLevelKeyResultModel
                {
                    Id = schoolLevel.Id,
                    Subjects = context.Mapper.Map<IEnumerable<SubjectKeyResultModel>>(groupBySummarySubjectList.Select(o => o.Items)).ToList()
                };
            }
        }

        public class ItemModelListToSubjectCountResultModelConverter : ITypeConverter<IEnumerable<ItemModel>, SubjectCountResultModel>
        {
            public SubjectCountResultModel Convert(IEnumerable<ItemModel> source, SubjectCountResultModel destination, ResolutionContext context)
            {
                var summarySubject = source.FirstOrDefault().SummarySubject;

                return new SubjectCountResultModel
                {
                    Subject = context.Mapper.Map<SubjectResultModel>(summarySubject),
                    NbItems = source.Count()
                };
            }
        }

        public class ItemModelListToSchoolLevelCountResultModelConverter : ITypeConverter<IEnumerable<ItemModel>, SchoolLevelCountResultModel>
        {
            public SchoolLevelCountResultModel Convert(IEnumerable<ItemModel> source, SchoolLevelCountResultModel destination, ResolutionContext context)
            {
                var schoolLevel = source.First().SummarySubject.SchoolLevel;

                var groupBySummarySubjectList = source.GroupBy(
                    o => o.SummarySubject.Id,
                    o => o,
                    (subjectId, subjectItemList) =>
                    {
                        (SummarySubjectModel Subject, List<ItemModel> Items) result = (subjectItemList.FirstOrDefault().SummarySubject, subjectItemList.ToList());
                        return result;
                    })
                        .ToList();

                return new SchoolLevelCountResultModel
                {
                    SchoolLevel = context.Mapper.Map<SchoolLevelResultModel>(schoolLevel),
                    NbItems = source.Count(),
                    NbSubjects = groupBySummarySubjectList.Count(),
                    Subjects = context.Mapper.Map<IEnumerable<SubjectCountResultModel>>(groupBySummarySubjectList.Select(o => o.Items)).ToList()
                };
            }
        }
    }
}
