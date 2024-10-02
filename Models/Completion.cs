
namespace UIUC_FirstRound_TrainingsJSON.Models
{
    public class Completion
    {
        // Training Tag Name
        public string? name { get; set; }

        // Completion Creation Time - Time when it got assigned to the person
        public string? timestamp { get; set; }

        // Time before which the task has to be completed
        public string? expires { get; set; }
    }
}
