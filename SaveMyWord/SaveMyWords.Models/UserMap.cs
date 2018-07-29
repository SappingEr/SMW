using FluentNHibernate.Mapping;


namespace SaveMyWord.Models
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(u => u.Id).GeneratedBy.Identity();
            Map(u => u.UserName).Length(50);
            Map(u => u.Password);
            Map(u => u.Email);
            Map(u => u.CreationDate);
            


        }
    }
}