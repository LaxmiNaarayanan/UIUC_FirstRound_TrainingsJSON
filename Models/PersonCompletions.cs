using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIUC_FirstRound_TrainingsJSON.Models
{
    public class PersonCompletions
    {
        // Person Name
        public string? name { get; set; }

        // Completions list of all the assigned training completions for the given person
        public Completion[]? completions { get; set; }
    }
}
