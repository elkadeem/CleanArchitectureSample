using System;
using System.Collections.Generic;
using System.Text;
using static Application.Core.Model.EmergancyDetail;

namespace Application.Core.Model
{
    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public EmergancyDetail EmergancyDetail { get; set; }
    }

    public class EmergancyDetail
    {
        public string Phone { get; set; }

        public string Reference { get; set; }

        public string Details { get; set; }

    }
    public class Address
    {
        public string Line1 { get; set; }

        public string Line2 { get; set;}

        public string City { get; set; }
    }
}
