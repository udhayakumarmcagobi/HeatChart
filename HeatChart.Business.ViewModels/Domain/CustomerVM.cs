﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatChart.Business.ViewModels.Domain
{
    public class CustomerVM
    {
        public int ID { get; set; } // Auto generated ID
        public string Name { get; set; } // Name 
        public string Address { get; set; } // Address 
        public string Email { get; set; } // Email ID 
        public string Landline { get; set; } // Landline phone
        public string Mobile { get; set; } // Mobile phone
        public string AdditionalDetails { get; set; } // To store any additional details of the customer. 
        public string CreatedBy { get; set; } // Created By
        public string ModifiedBy { get; set; } // Modified BY
        public DateTime CreatedOn { get; set; } // Created Date
        public DateTime ModifiedOn { get; set; } // Modified Date	
        public Boolean IsDeleted { get; set; } // It is used for soft delete
    }
}
