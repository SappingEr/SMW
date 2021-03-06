﻿using FluentNHibernate.Mapping;
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
            Map(n => n.Text).Length(int.MaxValue); 
            Map(n => n.CreationDate);
            References(n => n.CreationAuthor);
            HasMany(n => n.Documents);
        }
        


    }
}
