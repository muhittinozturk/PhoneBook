﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreatePersonContactDto
    {
        public ContactType Type { get; set; }
        public string? Content { get; set; }
    }
}
