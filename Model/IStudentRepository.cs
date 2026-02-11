namespace WebApi.Model
{
    public interface IStudentRepository
    {
        void Create(Student estudante);
        List<Student> FindAll();
    }
}
