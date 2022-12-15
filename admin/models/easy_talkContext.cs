using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using easy_talk.models;

#nullable disable

namespace easy_talk.models
{
    public class easy_talkContext : IdentityDbContext<SysUser, SysRole, int>
    {

        public virtual DbSet<UserFriend> UserFriends { get; set; }

        public easy_talkContext()
        {
        }

        public easy_talkContext(DbContextOptions<easy_talkContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);


            modelBuilder.HasCharSet("utf8mb4")
                 .UseCollation("utf8mb4_general_ci");

            modelBuilder.Entity<UserFriend>(entity =>
            {
                entity.ToTable("user_friend");

                entity.HasKey(e => e.id)
                    .HasName("PRIMARY");


                entity.Property(e => e.userId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.Property(e => e.friendId)
                    .HasColumnType("int(11)")
                    .HasColumnName("friend_id");



            });

        }

    }
}
