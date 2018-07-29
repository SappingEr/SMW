using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyWord.Models
{
    public class DocumentMap: ClassMap<Document>
    {
        public DocumentMap()
        {
            Id(f => f.Id).GeneratedBy.Identity();
            Map(f => f.Name);
            Map(f => f.Path).Length(250);
            References(f => f.Note);
        }




    }
}
