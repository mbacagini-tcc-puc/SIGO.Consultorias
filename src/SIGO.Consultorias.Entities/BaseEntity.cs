using System;
using System.Collections.Generic;
using System.Text;

namespace SIGO.Consultorias.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset DataInclusao { get; set; }
        public DateTimeOffset? DataAlteracao { get; set; }
    }
}
