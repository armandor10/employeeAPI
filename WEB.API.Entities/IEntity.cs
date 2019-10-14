using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WEB.API.Entities
{
    interface IEntity
    {
        Guid Id { get; set; }
    }
}
