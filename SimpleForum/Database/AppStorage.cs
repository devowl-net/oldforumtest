using SimpleForum.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SimpleForum.Database
{
    //http://msdn.microsoft.com/en-us/data/jj574232.aspx
    public class AppStorage : DbContext
    {
        //private static AppStorage _instance = null;
        //public static AppStorage Instance
        //{
        //    get
        //    {
        //        if(_instance == null)
        //        {
        //            _instance = new AppStorage();
        //        }
        //        return _instance;
        //    }
        //}
        public AppStorage()
            : base(@"DefaultConnection")
        { }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<MainForumPartition> MainForumPartitions { get; set; }

        public DbSet<ForumPartition> ForumPartitions { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<User> Users { get; set; }
    }
}