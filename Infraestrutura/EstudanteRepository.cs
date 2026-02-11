using WebApi.Model;

namespace WebApi.Infraestrutura
{
    public class EstudanteRepository : IStudentRepository
    {
        private readonly ConnectionContext context;
        public EstudanteRepository(ConnectionContext context)
        {
            this.context = context;
        }

        void IStudentRepository.Create(Student student)
        {
            context.Add(student);
            context.SaveChanges();
        }
        
        List<Student> IStudentRepository.FindAll()
        {
            return context.Students.ToList();
        }
    }
}
