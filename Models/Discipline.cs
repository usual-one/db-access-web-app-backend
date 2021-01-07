namespace Backend.Models
{
    public class Discipline
    {
        public string name { get; set; }

        public Teacher teacher { get; set; }

        // 'exam' | 'offset'
        public string type { get; set; }
    }
}
