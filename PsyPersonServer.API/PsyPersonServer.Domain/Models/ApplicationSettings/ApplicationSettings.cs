﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Models.ApplicationSettings
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
    }
}
