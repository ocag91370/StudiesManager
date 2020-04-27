using AutoMapper;
using MaxicoursDownloader.Api.Entities;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Api.Models.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Profiles
{
    public class AuditProfiles : Profile
    {
        public AuditProfiles()
        {
            CreateMap<ItemModel, AuditItemModel > ()
                .ReverseMap();
        }
    }
}
