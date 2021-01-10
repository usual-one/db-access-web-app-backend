namespace Backend.Models
{
    public class Student
    {
        public string name { get; set; }

        public Group group { get; set; }

        // 'studying' | 'academic leave' | 'dismissed' | 'graduated'
        public string state { get; set; }
    }
}

