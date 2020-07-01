using EcoleDirecteDownloader.Api.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoleDirecteDownloader.Api.Contracts
{
    public interface IEcoleDirecteService : IDisposable
    {
        bool Home();

        bool HomeworkBook();

        HomeworkBookPom GoToHomeworkBookPage();

        string GetWorkToDo(DateTime date);

        string GetSessionsContent(DateTime date);

        void SendMail();

        void GetHomework(DateTime date);
    }
}
