using System;

namespace Workshop.Domain.Models
{
    public class WorkshopTask
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public int EmployyId { get; set; }
        public string TaskDescription { get; set; }
        public string ResultDescription { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Deadline { get; set; }
    }

    public class WorkshopProblem
    {
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public string ProblemDescription { get; set; }
        public string ResultDescription { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Deadline { get; set; }
    }

    public class WorkshopObject
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public int Model { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Usersame { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserAccount
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public int EmployeeId { get; set; }
    }
}
