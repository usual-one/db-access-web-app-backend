namespace Backend.Models
{
    public class Teacher
    {
        public int id { get; set; }

        public string name { get; set; }

        public Faculty faculty { get; set; }
    }
}
