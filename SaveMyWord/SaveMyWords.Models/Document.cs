using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyWord.Models
{
    public class Document
    {

        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Path { get; set; }

        public virtual Note Note { get; set; }



    }
}
