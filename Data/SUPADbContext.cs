using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using supa.Models;

namespace supa.Data;

public partial class SUPADbContext : DbContext
{
    public SUPADbContext()
    {
    }

    public SUPADbContext(DbContextOptions<SUPADbContext> options)
        : base(options)
    {
    }

    // ========== DbSets de todas las tablas ==========
    public virtual DbSet<SUPAAcademicos> SUPAAcademicos { get; set; }
    public virtual DbSet<SUPAApoyosEco> SUPAApoyosEco { get; set; }
    public virtual DbSet<SUPAApoyosEcoCA> SUPAApoyosEcoCA { get; set; }
    public virtual DbSet<SUPAAreaDedica> SUPAAreaDedica { get; set; }
    public virtual DbSet<SUPACALGCS> SUPACALGCS { get; set; }
    public virtual DbSet<SUPACatAreaDedica> SUPACatAreaDedica { get; set; }
    public virtual DbSet<SUPACatAreas> SUPACatAreas { get; set; }
    public virtual DbSet<SUPACatDisciplinas> SUPACatDisciplinas { get; set; }
    public virtual DbSet<SUPACatEntidades> SUPACatEntidades { get; set; }
    public virtual DbSet<SUPACatEstadoApoyo> SUPACatEstadoApoyo { get; set; }
    public virtual DbSet<SUPACatGeneros> SUPACatGeneros { get; set; }
    public virtual DbSet<SUPACatGradoCA> SUPACatGradoCA { get; set; }
    public virtual DbSet<SUPACatMotivos> SUPACatMotivos { get; set; }
    public virtual DbSet<SUPACatNacionalidades> SUPACatNacionalidades { get; set; }
    public virtual DbSet<SUPACatNivelEstudios> SUPACatNivelEstudios { get; set; }
    public virtual DbSet<SUPACatNivelSNII> SUPACatNivelSNII { get; set; }
    public virtual DbSet<SUPACatPeriodos> SUPACatPeriodos { get; set; }
    public virtual DbSet<SUPACatRegion> SUPACatRegion { get; set; }
    public virtual DbSet<SUPACatRoles> SUPACatRoles { get; set; }
    public virtual DbSet<SUPACatTempContrataciones> SUPACatTempContrataciones { get; set; }
    public virtual DbSet<SUPACatTipoApoyo> SUPACatTipoApoyo { get; set; }
    public virtual DbSet<SUPACatTipoContrataciones> SUPACatTipoContrataciones { get; set; }
    public virtual DbSet<SUPACitas> SUPACitas { get; set; }
    public virtual DbSet<SUPAContrataciones> SUPAContrataciones { get; set; }
    public virtual DbSet<SUPACuerpoAcademicos> SUPACuerpoAcademicos { get; set; }
    public virtual DbSet<SUPADescargasA> SUPADescargasA { get; set; }
    public virtual DbSet<SUPADisciplinas> SUPADisciplinas { get; set; }
    public virtual DbSet<SUPAEntidades> SUPAEntidades { get; set; }
    public virtual DbSet<SUPAEstudios> SUPAEstudios { get; set; }
    public virtual DbSet<SUPAGradosCA> SUPAGradosCA { get; set; }
    public virtual DbSet<SUPAMiembrosCA> SUPAMiembrosCA { get; set; }
    public virtual DbSet<SUPANivelesSNII> SUPANivelesSNII { get; set; }
    public virtual DbSet<SUPAPlazas> SUPAPlazas { get; set; }
    public virtual DbSet<SUPARolesMiembros> SUPARolesMiembros { get; set; }
    public virtual DbSet<SUPAVigenciaCuerpo> SUPAVigenciaCuerpo { get; set; }
    public virtual DbSet<SUPAVigenciaPerfil> SUPAVigenciaPerfil { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ========== SUPAAcademicos ==========
        modelBuilder.Entity<SUPAAcademicos>(entity =>
        {
            entity.HasKey(e => e.IdSUPA).HasName("SUPAAcademicos_PK");

            entity.Property(e => e.IdCatMotivos).HasDefaultValueSql("((1))");
            entity.Property(e => e.Institucion).HasDefaultValueSql("('Universidad Veracruzana')");
            entity.Property(e => e.Baja).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCatGenerosNavigation).WithMany(p => p.SUPAAcademicos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAAcademicos_SUPACatGeneros_FK");

            entity.HasOne(d => d.IdCatMotivosNavigation).WithMany(p => p.SUPAAcademicos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAAcademicos_SUPACatMotivos_FK");

            entity.HasOne(d => d.IdCatNacionalidadNavigation).WithMany(p => p.SUPAAcademicos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAAcademicos_SUPACatNacionalidades_FK");
        });

        // ========== SUPAApoyosEco ==========
        modelBuilder.Entity<SUPAApoyosEco>(entity =>
        {
            entity.HasKey(e => e.IdApoyosEco).HasName("SUPAApoyosEco_PK");

            entity.HasOne(d => d.IdCatEstadoApoyoNavigation).WithMany(p => p.SUPAApoyosEco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAApoyosEco_SUPACatEstadoApoyo_FK");

            entity.HasOne(d => d.IdCatTipoApoyoNavigation).WithMany(p => p.SUPAApoyosEco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAApoyosEco_SUPACatTipoApoyo_FK");

            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPAApoyosEco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAApoyosEco_SUPAAcademicos_FK");
        });

        // ========== SUPAApoyosEcoCA ==========
        modelBuilder.Entity<SUPAApoyosEcoCA>(entity =>
        {
            entity.HasKey(e => e.IdApoyosEcoCA).HasName("SUPAApoyosEcoCA_PK");

            entity.HasOne(d => d.IdCANavigation).WithMany(p => p.SUPAApoyosEcoCA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAApoyosEcoCA_SUPACuerpoAcademicos_FK");

            entity.HasOne(d => d.IdCatEstadoApoyoNavigation).WithMany(p => p.SUPAApoyosEcoCA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAApoyosEcoCA_SUPACatEstadoApoyo_FK");

            entity.HasOne(d => d.IdCatTipoApoyoNavigation).WithMany(p => p.SUPAApoyosEcoCA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAApoyosEcoCA_SUPACatTipoApoyo_FK");
        });

        // ========== SUPAAreaDedica ==========
        modelBuilder.Entity<SUPAAreaDedica>(entity =>
        {
            entity.HasKey(e => e.IdAreaDedica).HasName("SUPAAreaDedica_PK");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UltimaAreaDedica).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCatAreaDedicaNavigation).WithMany(p => p.SUPAAreaDedica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAAreaDedica_SUPACatAreaDedica_FK");

            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPAAreaDedica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAAreaDedica_SUPAAcademicos_FK");
        });

        // ========== SUPACALGCS ==========
        modelBuilder.Entity<SUPACALGCS>(entity =>
        {
            entity.HasKey(e => e.IdCALGCS).HasName("SUPACALGCS_PK");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UltimasLineas).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCANavigation).WithMany(p => p.SUPACALGCS)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPACALGCS_SUPACuerpoAcademicos_FK");
        });

        // ========== CATÁLOGOS ==========

        modelBuilder.Entity<SUPACatAreaDedica>(entity =>
        {
            entity.HasKey(e => e.IdCatAreaDedica).HasName("SUPACatAreaDedica_PK");
        });

        modelBuilder.Entity<SUPACatAreas>(entity =>
        {
            entity.HasKey(e => e.IdCatAreas).HasName("SUPACatAreas_PK");
        });

        modelBuilder.Entity<SUPACatDisciplinas>(entity =>
        {
            entity.HasKey(e => e.IdCatDisciplinas).HasName("SUPACatDisciplinas_PK");
        });

        modelBuilder.Entity<SUPACatEntidades>(entity =>
        {
            entity.HasKey(e => e.IdCatEntidades).HasName("SUPACatEntidades_PK");

            entity.HasOne(d => d.IdCatAreasNavigation).WithMany(p => p.SUPACatEntidades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPACatEntidades_SUPACatAreas_FK");

            entity.HasOne(d => d.IdCatRegionNavigation).WithMany(p => p.SUPACatEntidades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPACatEntidades_SUPACatRegion_FK");
        });

        modelBuilder.Entity<SUPACatEstadoApoyo>(entity =>
        {
            entity.HasKey(e => e.IdCatEstadoApoyo).HasName("SUPACatEstadoApoyo_PK");
        });

        modelBuilder.Entity<SUPACatGeneros>(entity =>
        {
            entity.HasKey(e => e.IdCatGeneros).HasName("SUPACatGeneros_PK");
        });

        modelBuilder.Entity<SUPACatGradoCA>(entity =>
        {
            entity.HasKey(e => e.IdCatGradoCA).HasName("SUPACatGradoCA_PK");
        });

        modelBuilder.Entity<SUPACatMotivos>(entity =>
        {
            entity.HasKey(e => e.IdCatMotivos).HasName("SUPACatMotivos_PK");
        });

        // ✅ CORREGIDO: Removido ValueGeneratedNever()
        modelBuilder.Entity<SUPACatNacionalidades>(entity =>
        {
            entity.HasKey(e => e.IdCatNacionalidad).HasName("SUPACatNacionalidades_PK");
            // ✅ Ya no tiene ValueGeneratedNever() - permite que SQL Server genere el ID
        });

        modelBuilder.Entity<SUPACatNivelEstudios>(entity =>
        {
            entity.HasKey(e => e.IdCatNivelEstudios).HasName("SUPACatNivelEstudios_PK");
        });

        // ✅ CORREGIDO: Removido ValueGeneratedNever()
        modelBuilder.Entity<SUPACatNivelSNII>(entity =>
        {
            entity.HasKey(e => e.IdCatNivelSNII).HasName("SUPACatNivelSNII_PK");
            // ✅ Ya no tiene ValueGeneratedNever() - permite que SQL Server genere el ID
        });

        modelBuilder.Entity<SUPACatPeriodos>(entity =>
        {
            entity.HasKey(e => e.IdCatPeriodos).HasName("SUPACatPeriodos_PK");
        });

        // ✅ CORREGIDO: Removido ValueGeneratedNever()
        modelBuilder.Entity<SUPACatRegion>(entity =>
        {
            entity.HasKey(e => e.IdCatRegion).HasName("SUPACatRegion_PK");
            // ✅ Ya no tiene ValueGeneratedNever() - permite que SQL Server genere el ID
        });

        modelBuilder.Entity<SUPACatRoles>(entity =>
        {
            entity.HasKey(e => e.IdCatRol).HasName("SUPACatRoles_PK");
        });

        modelBuilder.Entity<SUPACatTempContrataciones>(entity =>
        {
            entity.HasKey(e => e.IdCatTempContratacion).HasName("SUPACatTempContrataciones_PK");
        });

        modelBuilder.Entity<SUPACatTipoApoyo>(entity =>
        {
            entity.HasKey(e => e.IdCatTipoApoyo).HasName("SUPACatTipoApoyo_PK");
        });

        modelBuilder.Entity<SUPACatTipoContrataciones>(entity =>
        {
            entity.HasKey(e => e.IdCatTipoContratacion).HasName("SUPACatTipoContrataciones_PK");
        });

        // ========== SUPACitas ==========
        modelBuilder.Entity<SUPACitas>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("SUPACitas_PK");

            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPACitas)
                .HasConstraintName("SUPACitas_SUPAAcademicos_FK");
        });

        // ========== SUPAContrataciones ==========
        modelBuilder.Entity<SUPAContrataciones>(entity =>
        {
            entity.HasKey(e => e.IdContrataciones).HasName("SUPAContrataciones_PK");

            entity.HasOne(d => d.IdCatTempContratacionNavigation).WithMany(p => p.SUPAContrataciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAContrataciones_SUPACatTempContrataciones_FK");

            entity.HasOne(d => d.IdCatTipoContratacionNavigation).WithMany(p => p.SUPAContrataciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAContrataciones_SUPACatTipoContrataciones_FK");

            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPAContrataciones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAContrataciones_SUPAAcademicos_FK");
        });

        // ========== SUPACuerpoAcademicos ==========
        modelBuilder.Entity<SUPACuerpoAcademicos>(entity =>
        {
            entity.HasKey(e => e.IdCA).HasName("SUPACuerpoAcademicos_PK");

            entity.Property(e => e.Baja).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCatMotivosNavigation).WithMany(p => p.SUPACuerpoAcademicos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPACuerpoAcademicos_SUPACatMotivos_FK");
        });

        // ========== SUPADescargasA ==========
        modelBuilder.Entity<SUPADescargasA>(entity =>
        {
            entity.HasKey(e => e.IdDescargaA).HasName("SUPADescargasA_PK");

            entity.HasOne(d => d.IdAreaDescargaNavigation).WithMany(p => p.SUPADescargasA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPADescargasA_SUPACatAreas_FK");

            entity.HasOne(d => d.IdCatPeriodosNavigation).WithMany(p => p.SUPADescargasA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPADescargasA_SUPACatPeriodos_FK");

            entity.HasOne(d => d.IdRegionDescargaNavigation).WithMany(p => p.SUPADescargasA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPADescargasA_SUPACatRegion_FK");

            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPADescargasA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPADescargasA_SUPAAcademicos_FK");
        });

        // ========== SUPADisciplinas ==========
        modelBuilder.Entity<SUPADisciplinas>(entity =>
        {
            entity.HasKey(e => e.IdDisciplinas).HasName("SUPADisciplinas_PK");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UltimaDisciplina).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCatDisciplinasNavigation).WithMany(p => p.SUPADisciplinas)
                .HasConstraintName("SUPADisciplinas_SUPACatDisciplinas_FK");

            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPADisciplinas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPADisciplinas_SUPAAcademicos_FK");
        });

        // ========== SUPAEntidades ==========
        modelBuilder.Entity<SUPAEntidades>(entity =>
        {
            entity.HasKey(e => e.IdEntidades).HasName("SUPAEntidades_PK");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UltimaEntidad).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCatEntidadesNavigation).WithMany(p => p.SUPAEntidades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAEntidades_SUPACatEntidades_FK");

            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPAEntidades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAEntidades_SUPAAcademicos_FK");
        });

        // ========== SUPAEstudios ==========
        modelBuilder.Entity<SUPAEstudios>(entity =>
        {
            entity.HasKey(e => e.IdEstudios).HasName("SUPAEstudios_PK");

            // ✅ CORREGIDO: SUPAEstudios NO tiene FechaRegistro
            entity.Property(e => e.UltimoGrado).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCatNivelEstudiosNavigation).WithMany(p => p.SUPAEstudios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAEstudios_SUPACatNivelEstudios_FK");

            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPAEstudios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAEstudios_SUPAAcademicos_FK");
        });

        // ========== SUPAGradosCA ==========
        modelBuilder.Entity<SUPAGradosCA>(entity =>
        {
            entity.HasKey(e => e.IdGradosCA).HasName("SUPAGradosCA_PK");

            // ✅ CORREGIDO: Es FechaObtencion, no FechaRegistro
            entity.Property(e => e.FechaObtencion).HasDefaultValueSql("(getdate())");
            // ✅ CORREGIDO: Es UltimoGradoCA, no UltimoGrado
            entity.Property(e => e.UltimoGradoCA).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCANavigation).WithMany(p => p.SUPAGradosCA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAGradosCA_SUPACuerpoAcademicos_FK");

            entity.HasOne(d => d.IdCatGradoCANavigation).WithMany(p => p.SUPAGradosCA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAGradosCA_SUPACatGradoCA_FK");
        });

        // ========== SUPAMiembrosCA ==========
        modelBuilder.Entity<SUPAMiembrosCA>(entity =>
        {
            entity.HasKey(e => e.IdMiembrosCA).HasName("SUPAMiembrosCA_PK");

            entity.Property(e => e.Baja).HasDefaultValueSql("((0))");
            entity.Property(e => e.UltimoRegistro).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCANavigation).WithMany(p => p.SUPAMiembrosCA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAMiembrosCA_SUPACuerpoAcademicos_FK");

            entity.HasOne(d => d.IdCatMotivosNavigation).WithMany(p => p.SUPAMiembrosCA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAMiembrosCA_SUPACatMotivos_FK");

            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPAMiembrosCA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAMiembrosCA_SUPAAcademicos_FK");
        });

        // ========== SUPANivelesSNII ==========
        modelBuilder.Entity<SUPANivelesSNII>(entity =>
        {
            entity.HasKey(e => e.IdNivelesSNII).HasName("SUPANivelesSNII_PK");

            entity.Property(e => e.UltimoNivel).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCatNivelSNIINavigation).WithMany(p => p.SUPANivelesSNII)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPANivelesSNII_SUPACatNivelSNII_FK");

            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPANivelesSNII)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPANivelesSNII_SUPAAcademicos_FK");
        });

        // ========== SUPAPlazas ==========
        modelBuilder.Entity<SUPAPlazas>(entity =>
        {
            entity.HasKey(e => e.IdPlaza).HasName("SUPAPlazas_PK");

            entity.HasOne(d => d.IdAreaPlazaNavigation).WithMany(p => p.SUPAPlazas)
                .HasConstraintName("SUPAPlazas_SUPACatAreas_FK");

            entity.HasOne(d => d.IdRegionPlazaNavigation).WithMany(p => p.SUPAPlazas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAPlazas_SUPACatRegion_FK");

            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPAPlazas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAPlazas_SUPAAcademicos_FK");
        });

        // ========== SUPARolesMiembros ==========
        modelBuilder.Entity<SUPARolesMiembros>(entity =>
        {
            entity.HasKey(e => e.IdRolesMiembros).HasName("SUPARolesMiembros_PK");

            entity.Property(e => e.UltimoRol).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCatRolNavigation).WithMany(p => p.SUPARolesMiembros)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPARolesMiembros_SUPACatRoles_FK");

            entity.HasOne(d => d.IdMiembrosCANavigation).WithMany(p => p.SUPARolesMiembros)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPARolesMiembros_SUPAMiembrosCA_FK");
        });

        // ========== SUPAVigenciaCuerpo ==========
        modelBuilder.Entity<SUPAVigenciaCuerpo>(entity =>
        {
            entity.HasKey(e => e.IdVigenciaCuerpo).HasName("SUPAVigenciaCuerpo_PK");

            entity.Property(e => e.UltimaVigencia).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.IdCANavigation).WithMany(p => p.SUPAVigenciaCuerpo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAVigenciaCuerpo_SUPACuerpoAcademicos_FK");
        });

        // ========== SUPAVigenciaPerfil ==========
        modelBuilder.Entity<SUPAVigenciaPerfil>(entity =>
        {
            entity.HasKey(e => e.IdVigenciaPerfil).HasName("SUPAVigenciaPerfil_PK");

            entity.Property(e => e.UltimaVigencia).HasDefaultValueSql("((0))");

            // ✅ CORREGIDO: La colección en SUPAAcademicos se llama SUPAVigenciaPerfiles (plural)
            entity.HasOne(d => d.IdSUPANavigation).WithMany(p => p.SUPAVigenciaPerfiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SUPAVigenciaPerfil_SUPAAcademicos_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}