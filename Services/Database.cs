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
            Models.Db.Faculty dbFaculty = await this.getDbFaculty(name: facultyName);            

            List<Models.Db.Group> dbGroups = await this.student_group.FromSqlRaw(
                $"select * from student_group where faculty_id={dbFaculty.id} limit {count} offset {offset}")
                .ToListAsync();

            List<Models.Group> groups  = new List<Models.Group>();
            foreach (Models.Db.Group group in dbGroups)
                groups.Add(this.converter.fromDbModel(group, dbFaculty));

            return groups;
        }

        public async Task<List<Models.Student>> getStudents(string groupName,
                                                            int count,
                                                            int offset)
        {
            Models.Db.Group dbGroup = await this.getDbGroup(name: groupName);
            Models.Db.Faculty dbFaculty = await this.getDbFaculty(id: dbGroup.faculty_id);

            List<Models.Db.Student> dbStudents = await this.student.FromSqlRaw(
                $"select * from student where group_id={dbGroup.id} limit {count} offset {offset}")
                .ToListAsync();

            List<Models.Student> students = new List<Models.Student>();
            foreach (Models.Db.Student student in dbStudents)
                students.Add(this.converter.fromDbModel(student, dbGroup, dbFaculty));

            return students;
        }

        public async Task<List<Models.Teacher>> getTeachers(string facultyName,
                                                            int count,
                                                            int offset)
        {
            Models.Db.Faculty dbFaculty = await this.getDbFaculty(name: facultyName);

            List<Models.Db.Teacher> dbTeachers = await this.teacher.FromSqlRaw(
                $"select * from teacher where faculty_id={dbFaculty}")
                .ToListAsync();

            List<Models.Teacher> teachers = new List<Models.Teacher>();
            foreach (Models.Db.Teacher teacher in dbTeachers)
                teachers.Add(this.converter.fromDbModel(teacher, dbFaculty));

            return teachers;
        }

        public async Task insertStudent(Models.Student student)
        {
            Models.Db.Group dbGroup = await this.getDbGroup(name: student.group.name);

            this.student.FromSqlRaw(
                $"insert into student (name, state, group_id) values ('{student.name}', '{student.state}', {dbGroup.id})");
        }

        public async Task insertTeacher(Models.Teacher teacher)
        {
            Models.Db.Faculty dbFaculty = await this.getDbFaculty(name: teacher.faculty.name);

            this.teacher.FromSqlRaw(
                $"insert into teacher (name, faculty_id) values ({teacher.name}, {dbFaculty.id})");
        }

        public async Task removeStudent(string studentName)
        {
            this.student.FromSqlRaw(
                $"delete from student where name='{studentName}'");
        }

        public async Task removeTeacher(string teacherName)
        {
            this.teacher.FromSqlRaw(
                $"delete from teacher where name='{teacherName}'");
        }

        public async Task updateStudent(string studentName,
                                        Models.Student update)
        {
            Models.Db.Group group = await this.getDbGroup(name: update.group.name);

            this.student.FromSqlRaw(
                $"update student set name='{update.name}', state='{update.state}', group_id={group.id} where name='{studentName}'");
        }

        public async Task updateTeacher(string teacherName,
                                        Models.Teacher update)
        {
            Models.Db.Faculty faculty = await this.getDbFaculty(name: update.faculty.name);

            this.teacher.FromSqlRaw(
                $"update teacher set name='{update.name}', faculty_id={faculty.id} where name='{teacherName}'");
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
                    $"select * from faculty where id='{id}'")
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
                    $"select * from student_group where id='{id}'")
                    .ToListAsync())[0];
            else
                return null;
        }

    }
}
