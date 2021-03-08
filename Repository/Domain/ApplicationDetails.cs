using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ApplicationDetails
    {
        public int ApplicationDetailsId { get; set; }
        public string Name { get; set; }
        public long DownloadCount { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}