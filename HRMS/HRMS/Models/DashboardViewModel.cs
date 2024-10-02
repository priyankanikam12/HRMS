namespace HRMS.Models
{
    public class DashboardViewModel
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int InactiveEmployees { get; set; }
        public int PendingLeaveRequests { get; set; }
        public int ApprovedLeaveRequests { get; set; }
        public int TotalAttendanceRecords { get; set; }
        public decimal TotalSalariesProcessed { get; set; }
        public int TotalLeaveRequests { get; set; } // Add this property
        public int PendingApprovals { get; set; } // Add this property
    }
}
