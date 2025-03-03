namespace Backend.Services
{
    public class Converter
    {
        public Models.Faculty fromDbModel(Models.Db.Faculty faculty)
        {
            return new Models.Faculty()
            {
                id = faculty.id,
                name = faculty.name,
                briefName = faculty.brief_name
            };
        }

        public Models.Group fromDbModel(Models.Db.Group group,
                                        Models.Db.Faculty faculty)
        {
            return new Models.Group()
            {
                id = group.id,
                name = group.name,
                year = group.year,
                type = group.type,
                faculty = this.fromDbModel(faculty)
            };
        }

        public Models.Student fromDbModel(Models.Db.Student student,
                                          Models.Db.Group group,
                                          Models.Db.Faculty faculty)
        {
            return new Models.Student()
            {
                id = student.id,
                name = student.name,
                group = this.fromDbModel(group, faculty),
                state = student.state
            };
        }

        public Models.Teacher fromDbModel(Models.Db.Teacher teacher,
                                          Models.Db.Faculty faculty)
        {
            return new Models.Teacher()
            {
                id = teacher.id,
                name = teacher.name,
                faculty = this.fromDbModel(faculty)
            };
        }

    }
}
