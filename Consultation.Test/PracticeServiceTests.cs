using System;
using Xunit;
using Consultation.Data.Models;
using Consultation.Data.Services;
using System.Collections.Generic;

namespace Consultation.Test
{
    public class PracticeServiceTests
    {
        private PracticeService svc;

        public PracticeServiceTests()
        {
            svc = new PracticeService();

            // empty data source before each test
            svc.Initialise();
        }

       
    }
}
