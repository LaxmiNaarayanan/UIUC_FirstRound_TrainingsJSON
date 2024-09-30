using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIUC_FirstRound_TrainingsJSON.Models
{
    public class PersonCompletions
    {
        public string? name { get; set; }
        public Completion[]? completions { get; set; }
    }
}
