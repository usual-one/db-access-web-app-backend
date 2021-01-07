namespace Backend.Services
{
    public class Converter
    {
        public Models.Faculty fromDbModel(Models.Db.Faculty faculty)
        {
            return new Models.Faculty()
            {
                name = faculty.name,
                briefName = faculty.brief_name
            };
        }

        public Models.Group fromDbModel(Models.Db.Group group,
                                        Models.Db.Faculty groupFaculty)
        {
            return new Models.Group()
            {
                name = group.name,
                year = group.year,
                type = group.type,
                faculty = this.fromDbModel(groupFaculty)
            };
        }
    }
}
