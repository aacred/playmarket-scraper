using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [Index(nameof(Url), IsUnique = true)]
    public class Application
    {
        public int ApplicationId { get; set; }
        public string Url { get; set; }
        public List<ApplicationDetails> Details { get; } = new ();
    }
}