using AutoMapper;
using MaxicoursDownloader.Api.Contracts;
using MaxicoursDownloader.Api.Interfaces;
using MaxicoursDownloader.Api.Models;
using MaxicoursDownloader.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Services
{
    public partial class ExportService : IExportService
    {
        private readonly IMapper _mapper;
        private readonly IMaxicoursService _maxicoursService;
        private readonly IPdfConverterService _pdfConverterService;
        private readonly IDirectoryService _directoryService;

        private readonly MaxicoursSettingsModel _maxicoursSettings;

        public ExportService(IOptions<MaxicoursSettingsModel> configuration, IMapper mapper, IMaxicoursService maxicoursService, IPdfConverterService pdfConverterService, IDirectoryService directoryService)
        {
            _maxicoursSettings = configuration.Value;
            _mapper = mapper;
            _maxicoursService = maxicoursService;
            _pdfConverterService = pdfConverterService;
            _directoryService = directoryService;
        }

        public void Dispose()
        {
            _maxicoursService.Dispose();
        }
    }
}