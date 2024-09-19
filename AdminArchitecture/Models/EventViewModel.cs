using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminEmployeeTool.Models
{
    public class EventViewModel

    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public string Repeat { get; set; }

        public string Location { get; set; }

        public string Attendees { get; set; }

        public string Description { get; set; }

        public string Reminder { get; set; }

        public string Label { get; set; }
    }
}
