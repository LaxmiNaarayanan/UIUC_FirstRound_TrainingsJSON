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
        // Training Tag Name
        public string? name { get; set; }

        // Completion Creation Time - Time when it got asssigned to the person
        public string? timestamp { get; set; }

        // Time before which the task has to be completed
        public string? expires { get; set; }
    }
}
