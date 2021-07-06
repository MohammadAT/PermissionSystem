using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionSystem.Models
{
    public class LeavePermissionRequest
    {
        public int Id { get; set; }

        [Display(Name = " Time From")]
        public string TimeFrom { get; set; }

        [Display(Name = " Time To")]
        public string TimeTo { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Employee Note ")]
        public string EmployeeNote { get; set; }

        [Display(Name = "Manager Note")]
        public string ManagerNote { get; set; }
        public int RequestReasonId { get; set; }

        [Display(Name = "Request Reason")]
        public RequestReason RequestReason { get; set; }
        public int RequestStatusId { get; set; }

        [Display(Name = "Reaquest Status")]
        public RequestStatus RequestStatus { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
    }
}
