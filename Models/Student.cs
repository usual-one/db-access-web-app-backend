using System.Collections.Generic;

#nullable enable


namespace Backend.Models
{
    public class Student
    {
        public string name { get; set; }

        public Group group_ { get; set; }

        public List<Mark>? marks { get; set; }

        public List<Point>? points { get; set; }

        public int? rating { get; set; }

        // 'studying' | 'academic leave' | 'dismissed' | 'graduated'
        public string state { get; set; }
    }
}

