using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aws.Models
{
    public class IdentificationCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdCardImagePath { get; set; }
    }
}