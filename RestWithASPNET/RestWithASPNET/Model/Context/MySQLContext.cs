using Microsoft.EntityFrameworkCore;


namespace RestWithASPNET.Model.Context
{
    public class MySQLContext : DbContext //extends
    {
        public MySQLContext()
        {

        } 
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {

        }
        public DbSet<Person> People { get; set; } //similar a uma lista
    }
}
