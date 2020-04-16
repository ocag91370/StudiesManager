using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxicoursDownloader.Api.Repositories
{
    public class UrlRepository
    {
        //public static readonly string Token = "8a2h90iqc1beguudv1covmqau0";
        //public static readonly string Token = "rsakhq5fql2kpk6fd3k10cd5v0";
        //public static readonly string Token = "7alf0ehjdig1n5r9fo8psi6ef3";
        public static readonly string Token = "3s96rl321tqepsbvogqk5iukt5";

        public static readonly Dictionary<string, string> Urls = new Dictionary<string, string>
        {
            { "Home",  @"http://r.mail.cours.fr/tr/cl/uQVn67dGsu3VLVV8kZSVL5BVRagoDxwMejRp-6QpPslJOGZZ7IsABnHusm2CLvDHSJrTYgRE7wAa812MJ-111mkNI_j0KHFqVKHxQnkExhiPSZQdFn78Ac_F6SuRXAgM-ZMF9qGlkOesETjANgizI7i0qiuUKrzN6kPUdy8Rx4QZUB-SyZed_ULyMxB0jMBBddfQVxQqE7Jc099Q5RrkPK7Ue9xIuAzF4fGYkDcDiloiG1Uopfe3zzuzoZwXm88AR--Xo1KLKN733ejt6MufqY8nMVSwQjCiLy1Ub_CP1D7VNCKwwtKluWiRkmb0NaU8VXl5yQ"},
            { "HomeWithToken",  $@"https://entraide-covid19.maxicours.com/W/utilisateur/classe/choix.php?_eid={UrlRepository.Token}"},
            { "SchoolLevelWithToken",  $@"https://entraide-covid19.maxicours.com/W/utilisateur/classe/choix.php?_eid={UrlRepository.Token}&cla="},
            { "HomeTest",  $@"https://entraide-covid19.maxicours.com/LSI/prod/Accueil/?_eid={UrlRepository.Token}" },
            { "SubjectTest",  $@"https://entraide-covid19.maxicours.com/LSI/prod/Arbo/home/bo/5006/5088?_eid={UrlRepository.Token}" },
            { "ThemeTest",  $@"https://entraide-covid19.maxicours.com/LSI/prod/Arbo/home/bo/5006/5088/25546?_eid={UrlRepository.Token}" },
            { "PathTest",  $@"https://entraide-covid19.maxicours.com/LSI/prod/Parcours/?act=init&from=fiche&oid=536571&nid=25546&_vp=%2Fhome%2Fbo%2F5006%2F5088%2F25546%2F25364%2F25732%2Fopd%2F536571&_eid={UrlRepository.Token}" },
            { "LessonTest",  $@"https://entraide-covid19.maxicours.com/W/cours/fiche/visualiser.php?oid=536571&fromNid=25732&_vp=%2Fhome%2Fbo%2F5006%2F5088%2F25546%2F25364%2F25732%2Fopd%2F536571&_eid={UrlRepository.Token}" },
            { "VideoTest",  $@"https://entraide-covid19.maxicours.com/W/exercices/enonce_corrige_video/index.php?oid=246384&fromNid=25404&_vp=%2Fhome%2Fbo%2F5006%2F5088%2F25546%2F25364%2F25404%2Fopd%2F246384&_eid={UrlRepository.Token}" },
            { "QuizzTest",  $@"https://entraide-covid19.maxicours.com/LSI/prod/Quizz/?act=init&nid=5088&from=arbo&meta_mat=mat&_vp=%2Fhome%2Fbo%2F5006%2F5088&_eid={UrlRepository.Token}" }
        };
    }
}
