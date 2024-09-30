using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIUC_FirstRound_TrainingsJSON.Models
{
    public class Completion
    {
        public string? name { get; set; }
        public string? timestamp { get; set; }
        public string? expires { get; set; }
    }
}
