namespace VladyslavOrlovPromo.DataAccess
{
    using System.Data.Entity;

    public partial class EntityDataModel : DbContext
    {
        public EntityDataModel()
            : base("name=VladyslavOrlovDb")
        {
        }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Ranking> Rankings { get; set; }
        public virtual DbSet<SlideImage> SlideImages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
