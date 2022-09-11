using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class LigaContext:DbContext
    {
        public LigaContext(DbContextOptions options):base(options){}

        public DbSet<Ekipa> Ekipe {get; set;}
        public DbSet<Igrac> Igraci {get; set;}
        public DbSet<Kolo> Kola {get; set;}
        public DbSet<Stadion> Stadioni {get; set;}
        public DbSet<Strelac> Strelci {get; set;}
        public DbSet<Utakmica> Utakmice {get; set;}
        public DbSet<Domacin> Domacin {get; set;}
        public DbSet<Gost> Gost {get; set;}


    }
}