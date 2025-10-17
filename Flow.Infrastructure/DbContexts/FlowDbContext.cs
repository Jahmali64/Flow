using Flow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flow.Infrastructure.DbContexts;

public partial class FlowDbContext : DbContext {
    public FlowDbContext() { }

    public FlowDbContext(DbContextOptions<FlowDbContext> options) : base(options) { }

    public virtual DbSet<DemandRecord> DemandRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=app.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<DemandRecord>(entity => {
            entity.HasIndex(e => e.DemandRecordId, "IX_DemandRecords_DemandRecordId").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}