using EmployeeService.Dats;

namespace Timesheets.Services.Impl
{
    public class EmployeeTypeRepository : IEmployeeTypeRepository
    {
        private readonly EmployeeServiceDbContext _context;

        public EmployeeTypeRepository(EmployeeServiceDbContext context)
        {
            _context = _context;
        }


        public int Create(EmployeeType data)
        {
            _context.EmployeeTypes.Add(data);
            _context.SaveChanges();
            return data.Id;
        }

        public void Delete(int id)
        {
            EmployeeType employeeType = GetById(id);
            if (employeeType == null)
                throw new Exception("EmployeeType not found");

            _context.EmployeeTypes.Remove(GetById(id));
            _context.SaveChanges(true);
        }


        public IList<EmployeeType> GetAll()
        {
            return _context.EmployeeTypes.ToList();
        }

        public EmployeeType GetById(int id)
        {
            return _context.EmployeeTypes.FirstOrDefault(et => et.Id == id);
        }

        public void Update(EmployeeType data)
        {

            if (data == null)
                throw new Exception("EmployeeType is null.");
            EmployeeType employeeType = GetById(data.Id);
            employeeType.Description = data.Description;
            _context.SaveChanges(true);
        }
    }
}