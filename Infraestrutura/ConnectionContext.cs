using Microsoft.EntityFrameworkCore;
using WebApi.Model;
using WebApi.Infraestrutura;

namespace WebApi.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }

}
