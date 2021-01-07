namespace Backend.Models.Db
{
    public class Student
    {
        public int id { get; set; }

        public string name { get; set; }

        public int group_id { get; set; }

        // 'studying' | 'academic leave' | 'dismissed' | 'graduated'
        public string state { get; set; }
    }
}
