using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore_Spider.Model
{
    public class JobModel
    {
        public int Id { get; set; }

        public string JobName { get; set; }

        public string JobUrl { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUrl { get; set; }

        public string JobAddress { get; set; }

        public string PublishDate { get; set; }
    }
}
