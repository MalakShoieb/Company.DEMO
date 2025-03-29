namespace Company.DEMO.BLL.Repository
{
    public interface IUnitOfWork
    {  DepartmentRepository DepartmentRepository { get; }
         EmployeeRepository EmployeeRepository { get; }


    }
}