using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable


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
                $"select * from faculty order by brief_name asc limit {count} offset {offset}")
                .ToListAsync();

            List<Models.Faculty> faculties = new List<Models.Faculty>();
            foreach (Models.Db.Faculty faculty in dbFaculties)
                faculties.Add(this.converter.fromDbModel(faculty));

            return faculties;
        }

        public async Task<List<Models.Group>> getGroups(int facultyId,
                                                        int count = -1,
                                                        int offset = -1)
        {
            Models.Db.Faculty dbFaculty = await this.getDbFaculty(id: facultyId);            

            List<Models.Db.Group> dbGroups = null; 
            if (count == -1) 
              dbGroups = await this.student_group.FromSqlRaw(
                  $"select * from student_group where faculty_id={facultyId} order by name asc")
                  .ToListAsync();
            else 
                dbGroups = await this.student_group.FromSqlRaw(
                    $"select * from student_group where faculty_id={facultyId} order by name asc limit {count} offset {offset}")
                    .ToListAsync();

            List<Models.Group> groups  = new List<Models.Group>();
            foreach (Models.Db.Group group in dbGroups)
                groups.Add(this.converter.fromDbModel(group, dbFaculty));

            return groups;
        }

        public async Task<List<Models.Student>> getStudents(int groupId,
                                                            int count,
                                                            int offset)
        {
            Models.Db.Group dbGroup = await this.getDbGroup(id: groupId);
            Models.Db.Faculty dbFaculty = await this.getDbFaculty(id: dbGroup.faculty_id);

            List<Models.Db.Student> dbStudents = await this.student.FromSqlRaw(
                $"select * from student where group_id={groupId} order by name asc limit {count} offset {offset}")
                .ToListAsync();

            List<Models.Student> students = new List<Models.Student>();
            foreach (Models.Db.Student student in dbStudents)
                students.Add(this.converter.fromDbModel(student, dbGroup, dbFaculty));

            return students;
        }

        public async Task<List<Models.Teacher>> getTeachers(int facultyId,
                                                            int count,
                                                            int offset)
        {
            Models.Db.Faculty dbFaculty = await this.getDbFaculty(id: facultyId);

            List<Models.Db.Teacher> dbTeachers = await this.teacher.FromSqlRaw(
                $"select * from teacher where faculty_id={facultyId} order by name asc limit {count} offset {offset}")
                .ToListAsync();

            List<Models.Teacher> teachers = new List<Models.Teacher>();
            foreach (Models.Db.Teacher teacher in dbTeachers)
                teachers.Add(this.converter.fromDbModel(teacher, dbFaculty));

            return teachers;
        }

        public async Task insertFaculty(Models.Faculty faculty)
        {
            this.Database.ExecuteSqlCommand(
                "insert into faculty (name, brief_name) values ({0}, {1})",
                faculty.name, faculty.briefName);
        }
        
        public async Task insertGroup(Models.Group group)
        {
            Models.Db.Faculty dbFaculty = await this.getDbFaculty(briefName: group.faculty.briefName);
            this.Database.ExecuteSqlCommand(
                "insert into student_group (name, year, type, faculty_id) values ({0}, {1}, {2}, {3})",
                group.name, group.year, group.type, group.faculty.id);
        }

        public async Task insertStudent(Models.Student student)
        {
            Models.Db.Group dbGroup = await this.getDbGroup(name: student.group.name);
            this.Database.ExecuteSqlCommand(
                "insert into student (name, state, group_id) values ({0}, {1}, {2})",
                student.name, student.state, dbGroup.id);
        }

        public async Task insertTeacher(Models.Teacher teacher)
        {
            Models.Db.Faculty dbFaculty = await this.getDbFaculty(briefName: teacher.faculty.briefName);
            this.Database.ExecuteSqlCommand(
                "insert into teacher (name, faculty_id) values ({0}, {1})",
                teacher.name, dbFaculty.id);
        }

        public async Task removeFaculty(int id)
        {
            List<Models.Group> groups = await this.getGroups(id);

            foreach (Models.Group group in groups)
                await this.removeGroup(group.id);

            this.Database.ExecuteSqlCommand(
                "delete from teacher where faculty_id={0}", id);

            this.Database.ExecuteSqlCommand(
                "delete from faculty where id={0}", id);
        }

        public async Task removeGroup(int id)
        {
            this.Database.ExecuteSqlCommand(
                "delete from student where group_id={0}", id);

            this.Database.ExecuteSqlCommand(
                "delete from student_group where id={0}", id);
        }

        public async Task removeStudent(int id)
        {
            this.Database.ExecuteSqlCommand(
                "delete from student where id={0}", id);
        }

        public async Task removeTeacher(int id)
        {
            this.Database.ExecuteSqlCommand(
                "delete from teacher where id={0}", id);
        }

        public async Task updateFaculty(int id, Models.Faculty update)
        {
            this.Database.ExecuteSqlCommand(
                "update faculty set name={0}, brief_name={1} where id={2}",
                update.name, update.briefName, id);
        }

        public async Task updateGroup(int id, Models.Group update)
        {
            Models.Db.Faculty dbFaculty = await this.getDbFaculty(briefName: update.faculty.briefName);
            this.Database.ExecuteSqlCommand(
                "update student_group set name={0}, faculty_id={1}, year={2}, type={3} where id={4}",
                update.name, dbFaculty.id, update.year, update.type, id);
        }

        public async Task updateStudent(int id, Models.Student update)
        {
            Models.Db.Group dbGroup = await this.getDbGroup(name: update.group.name);
            this.Database.ExecuteSqlCommand(
                "update student set name={0}, state={1}, group_id={2} where id={3}",
                update.name, update.state, dbGroup.id, id);
        }

        public async Task updateTeacher(int id, Models.Teacher update)
        {
            Models.Db.Faculty dbFaculty = await this.getDbFaculty(briefName: update.faculty.briefName);
            this.Database.ExecuteSqlCommand(
                "update teacher set name={0}, faculty_id={1} where id={2}",
                update.name, dbFaculty.id, id);
        }

        private async Task<Models.Db.Faculty> getDbFaculty(string? name = null,
                                                           string? briefName = null,
                                                           int? id = null)
        {
            if (name != null)
                return (await this.faculty.FromSqlRaw(
                    $"select * from faculty where name='{name}'")
                    .ToListAsync())[0];
            else if (briefName != null)
                return (await this.faculty.FromSqlRaw(
                    $"select * from faculty where brief_name='{briefName}'")
                    .ToListAsync())[0];
            else if (id != null)
                return (await this.faculty.FromSqlRaw(
                    $"select * from faculty where id={id}")
                    .ToListAsync())[0];
            else
                return null;
        }

        private async Task<Models.Db.Group> getDbGroup(string? name = null,
                                                       int? id = null)
        {
            if (name != null)
                return (await this.student_group.FromSqlRaw(
                    $"select * from student_group where name='{name}'")
                    .ToListAsync())[0];
            else if (id != null)
                return (await this.student_group.FromSqlRaw(
                    $"select * from student_group where id={id}")
                    .ToListAsync())[0];
            else
                return null;
        }

    }
}
