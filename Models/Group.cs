namespace Backend.Models
{
    public class Group
    {
        public int id { get; set; }
        
        public string name { get; set; }

        public Faculty faculty { get; set; }

        public int year { get; set; }

        // 'bachelor' | 'master' | 'speciality' | 'post-graduate'
        public string type { get; set; }
    }
}
