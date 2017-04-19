namespace SimpleForum.Migrations
{
    using SimpleForum.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleForum.Database.AppStorage>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(SimpleForum.Database.AppStorage context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            DoSeed(context);
        }

        /// * Если всю Глючит то __MigrationHistory !!!!
         // 1. Enable-Migrations -ContextTypeName AppStorage
		 //	2. AutomaticMigrationsEnabled = true; 
		 //	3. Update-Database
         // * /
         // 
        /// *
         // *  SELECT * FROM Users
         //    SELECT * FROM Topics
         //    SELECT * FROM ForumPartitions
         //    SELECT * FROM MainForumPartitions
         //    SELECT * FROM Messages
        // /

        public static void DoSeed(SimpleForum.Database.AppStorage context)
        {
            var Eddi_Brown = new User()
            {
                Birthday = GetRandomDateTime(),
                NickName = "Eddi_Brown",
                Postscript = "Запомните раз и навсегда: помочь != \"решите за меня\"!",
                Type = UserType.Common,
                RegistrationDate = GetRandomDateTime(),
                Email = "test1@test1.ru",
                MessageCount = 19
            };

            var p51x = new User()
            {
                Birthday = GetRandomDateTime(),
                NickName = "p51x",
                Type = UserType.Common,
                RegistrationDate = GetRandomDateTime(),
                Email = "test2@test1.ru",
                MessageCount = 4413
            };

            var Dima9595 = new User()
            {
                Birthday = GetRandomDateTime(),
                NickName = "Dima9595",
                Type = UserType.Common,
                RegistrationDate = GetRandomDateTime(),
                Email = "test3@test1.ru",
                MessageCount = 1
            };

            context.MainForumPartitions.AddOrUpdate(
                m => m.PartitionName,

                new MainForumPartition()
                {
                    CreationDate = GetRandomDateTime(),
                    PartitionName = "Новички в программировании",
                    ForumPartitions = new List<ForumPartition>() 
                        {
                            new ForumPartition() {
                                CreationDate = GetRandomDateTime(),
                                PartitionName = "Помощь студентам",
                                PartitionDescription = "Каждый из нас в чём-то \"новичок\", потому просьба писать в этот раздел, с подробным описанием вопроса в названии темы, а также просьбы о решении задач, следует постить сюда.",
                                Topics = new List<Topic>() {
                                    new Topic() {
                                        TopicName = "Не работает программа",
                                        TopicOwner = Eddi_Brown,
                                        TopicType = TopicTypes.Common,
                                        CreationDate = GetRandomDateTime(),
                                        FirstMessage = new Message() {
                                            CreationDate = GetRandomDateTime(),
                                            MessageOwner = Eddi_Brown,
                                            Text = "Задание: написать программу, в которой будут объявлены структуры Points и Ellipse, создан динамический массив объектов Ellipse и нарисованы все объекты, занесенные в массив. Вот код: ????"
                                        },
                                        Messages = new List<Message>() {
                                            new Message() {
                                                MessageOwner = p51x,
                                                Text = "Вас не смущает, что вы сначала рисуете, а только потом задаете значение точек?",
                                                CreationDate = GetRandomDateTime()
                                            },
                                            new Message() {
                                                MessageOwner = Eddi_Brown,
                                                Text = "Как можно быть таким идиотом? Спасибо, вы бы не сказали, я бы и не заметил.",
                                                CreationDate = GetRandomDateTime()
                                            }
                                        }
                                    },
                                
                                }
                            },
                            new ForumPartition() {
                                CreationDate = GetRandomDateTime(),
                                PartitionName = "Паскаль",
                                PartitionDescription = "Помощь программистам на паскале",
                                Topics = new List<Topic>() {
                                    new Topic() {
                                        TopicName = "Ошибка в коде",
                                        TopicOwner = Dima9595,
                                        TopicType = TopicTypes.Common,
                                        CreationDate = GetRandomDateTime(),
                                        FirstMessage = new Message() {
                                            CreationDate = GetRandomDateTime(),
                                            MessageOwner = Dima9595,
                                            Text = "Ребят, помогите пожалуйста! Программа должна подсчитать среднее значение всех групп за все экзамены и вывести в конце все ср. знач, но выводит только последнее среднее значение.Не работает конкретно эта строка(красным цветом)и в дело, наверно, в функции(красным цветом). Заранее благодарен!",
                                        }
                                    }
                                }
                            }
                    }
                });
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static DateTime? GetRandomDateTime()
        {
            return DateTime.Today.Add(-TimeSpan.FromDays(new Random().Next(0, 10000)));
        }
    }
}