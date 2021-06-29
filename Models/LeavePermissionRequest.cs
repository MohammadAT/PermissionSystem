using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionSystem.Models
{
    public class LeavePermissionRequest
    {
        public int Id { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeNote { get; set; }
        public string ManagerNote { get; set; }
        public int RequestReasonId { get; set; }
        public RequestReason RequestReason { get; set; }
        public int RequestStatusId { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
    }
}
