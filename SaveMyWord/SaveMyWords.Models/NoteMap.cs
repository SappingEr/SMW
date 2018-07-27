using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyWord.Models
{
    public class NoteMap: ClassMap<Note>
    {
        public NoteMap()
        {
            Id(n => n.Id).GeneratedBy.Identity();
            Map(n => n.NoteName).Length(50);
            Map(n => n.Text);
            Map(n => n.CreationDate);
            References(f => f.CreationAuthor);
        }
        


    }
}
