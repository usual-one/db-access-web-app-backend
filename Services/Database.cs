using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Backend.Services
{
    public class Database : DbContext
    {
        public DbSet<Models.Db.Student> student { get; set; }

        public DbSet<Models.Db.Teacher> teacher { get; set; }

        public DbSet<Models.Db.Group> student_group { get; set; }

        public DbSet<Models.Db.Faculty> faculty { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DBAccessWebApp;Username=postgres;Password=postgres");

        private Converter converter { get; set; } = new Converter();

        public async Task<List<Models.Faculty>> getFaculties(int count, int offset)
        {
            List<Models.Db.Faculty> dbFaculties = await this.faculty.FromSqlRaw(
                $"select * from faculty limit {count} offset {offset}")
                .ToListAsync();

            List<Models.Faculty> faculties = new List<Models.Faculty>();
            foreach (Models.Db.Faculty faculty in dbFaculties)
                faculties.Add(this.converter.fromDbModel(faculty));

            return faculties;
        }

        public async Task<List<Models.Group>> getGroups(string facultyName,
                                                        int count,
                                                        int offset)
        {
            Models.Db.Faculty dbFaculty = (await this.faculty.FromSqlRaw(
                $"select * from faculty where name='{facultyName}'")
                .ToListAsync())[0];
            
            List<Models.Db.Group> dbGroups = await this.student_group.FromSqlRaw(
                $"select * from student_group where faculty_id={dbFaculty.id} limit {count} offset {offset}")
                .ToListAsync();

            List<Models.Group> groups  = new List<Models.Group>();
            foreach (Models.Db.Group group in dbGroups)
                groups.Add(this.converter.fromDbModel(group, dbFaculty));

            return groups;
        }
    }
}
