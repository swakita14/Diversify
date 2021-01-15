﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diversify_Server.Interfaces.Services;

namespace Diversify_Server.Controller.DiversifyWebAssembly
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyOverviewController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyOverviewController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
    }
}
