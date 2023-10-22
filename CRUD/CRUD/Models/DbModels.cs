using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class Institute
    {
        public Institute()
        {
            this.Premises = new List<Premise>();
        }
        public int InstituteId { get; set; }
        [Required, StringLength(30, MinimumLength = 4), Display(Name = "Institute Name ")]
        public string InstituteName { get; set; } = default!;
        public int Established { get; set; }
        public virtual ICollection<Premise> Premises { get; set; }


    }

    public class Premise
    {
        public int PremiseId { get; set; }
        public string PrincipalName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Phone { get; set; } = default!;
        [ForeignKey("Institute")]
        [Display(Name = "Institute")]
        public int? InstituteId { get; set; }

        public virtual Institute? Institute { get; set; }

    }

    public class InstituteDbContext : DbContext
    {
        public InstituteDbContext(DbContextOptions<InstituteDbContext> options) : base(options)
        {

        }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Premise> Premises { get; set; }

    }


}
