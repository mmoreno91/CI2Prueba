using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CI2.Web.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("CI2");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.Id).HasColumnName("IdUsuario");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.UserName).HasColumnName("NombreUsuario");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.Email).HasColumnName("Correo");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.EmailConfirmed).HasColumnName("CorreoConfirmacion");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.PasswordHash).HasColumnName("Contrasena");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.SecurityStamp).HasColumnName("Seguridad");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.PhoneNumber).HasColumnName("Telefono");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.PhoneNumberConfirmed).HasColumnName("TelefonoConfirmacion");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.TwoFactorEnabled).HasColumnName("DosFactoresActivacion");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.LockoutEndDateUtc).HasColumnName("FechaUltimoBloqueo");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.LockoutEnabled).HasColumnName("Bloqueo");
            modelBuilder.Entity<ApplicationUser>().ToTable("TabUsuario").Property(p => p.AccessFailedCount).HasColumnName("NumeroIngresosFallidos");

            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.Id).HasColumnName("IdUsuario");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.UserName).HasColumnName("NombreUsuario");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.Email).HasColumnName("Correo");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.EmailConfirmed).HasColumnName("CorreoConfirmacion");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.PasswordHash).HasColumnName("Contrasena");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.SecurityStamp).HasColumnName("Seguridad");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.PhoneNumber).HasColumnName("Telefono");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.PhoneNumberConfirmed).HasColumnName("TelefonoConfirmacion");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.TwoFactorEnabled).HasColumnName("DosFactoresActivacion");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.LockoutEndDateUtc).HasColumnName("FechaUltimoBloqueo");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.LockoutEnabled).HasColumnName("Bloqueo");
            modelBuilder.Entity<IdentityUser>().ToTable("TabUsuario").Property(p => p.AccessFailedCount).HasColumnName("NumeroIngresosFallidos");

            modelBuilder.Entity<IdentityUserRole>().ToTable("TabUsuarioRol").Property(p => p.RoleId).HasColumnName("IdRol");
            modelBuilder.Entity<IdentityUserRole>().ToTable("TabUsuarioRol").Property(p => p.UserId).HasColumnName("IdUsuario");

            modelBuilder.Entity<IdentityUserLogin>().ToTable("TabUsuarioLogin").Property(p => p.LoginProvider).HasColumnName("IdLogin");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("TabUsuarioLogin").Property(p => p.ProviderKey).HasColumnName("IdLlaveProveedor");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("TabUsuarioLogin").Property(p => p.UserId).HasColumnName("IdUsuario");

            modelBuilder.Entity<IdentityUserClaim>().ToTable("TabUsuarioReclamo").Property(p => p.Id).HasColumnName("IdUsuarioReclamo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("TabUsuarioReclamo").Property(p => p.UserId).HasColumnName("IdUsuario");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("TabUsuarioReclamo").Property(p => p.ClaimType).HasColumnName("TipoReclamo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("TabUsuarioReclamo").Property(p => p.ClaimValue).HasColumnName("Reclamo");

            modelBuilder.Entity<IdentityRole>().ToTable("TabRol").Property(p => p.Id).HasColumnName("IdRol");
            modelBuilder.Entity<IdentityRole>().ToTable("TabRol").Property(p => p.Name).HasColumnName("NombreRol");

        }

        public ApplicationDbContext(string connection)
            : base(connection)
        {
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}