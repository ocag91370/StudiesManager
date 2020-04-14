using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Contracts
{
    public interface IDirectoryService
    {
        bool Create(string path);

        bool Delete(string path);

        int GetNbFiles(string path, string pattern);
    }
}
