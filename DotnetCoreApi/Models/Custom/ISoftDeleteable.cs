﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreApi.Models
{
    public interface ISoftDeleteable
    {
        public bool IsDeleted { get; set; }
    }
}
