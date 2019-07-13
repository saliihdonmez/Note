using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.Entity
{
   public class MyInitializer: CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            EvernoteUser admin = new EvernoteUser()
            {
                Name = "salih",
                Surname = "Dönmez",
                Email = "saliihdonmez@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive=true,
                IsAdmin=true,
                Username="saliihdonmez",
                Paswword="salih1903",
                CreateOn=DateTime.Now,
                Modifiedon=DateTime.Now.AddMinutes(5),
                ModifiedUsername="saliihdonmez"


            };

            EvernoteUser standartUser = new EvernoteUser()
            {
                Name = "salih",
                Surname = "Dönmez",
                Email = "saliihdonmezz@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "saliihdonmezz",
                Paswword = "salih1903",
                CreateOn = DateTime.Now.AddHours(1),
                Modifiedon = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "saliihdonmez"


            };


            context.EvernoteUsers.Add(admin);
            context.EvernoteUsers.Add(standartUser);


            for (int i = 0; i < 8; i++)
            {
                EvernoteUser user = new EvernoteUser
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{i}",
                    Paswword = "123",
                    CreateOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    Modifiedon = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = "saliihdonmez"

                };
                context.EvernoteUsers.Add(user);


            }


            context.SaveChanges();


            // User List for using
            List<EvernoteUser> userlist = context.EvernoteUsers.ToList();
            
            
            //Adding fake categories..

            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title=FakeData.PlaceData.GetStreetName(),
                    Description=FakeData.PlaceData.GetAddress(),
                    CreateOn=DateTime.Now,
                    Modifiedon=DateTime.Now,
                    ModifiedUsername="saliihdonmez"

                };

                context.Categories.Add(cat);

                //adding Notes
                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); k++)
                {

                    EvernoteUser owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                    Note note = new Note()
                    {
                        Title=FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5,25)),
                        Text=FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1,3)),
                        IsDraft=false,
                        LikeCount= FakeData.NumberData.GetNumber(1, 9),
                        Owner=owner,
                        CreateOn=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                        Modifiedon = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = owner.Username,

                    };

                    cat.Notes.Add(note);

                    //Adding fake comments

                    for (int j = 0; j < note.LikeCount; j++)
                    {
                        EvernoteUser comment_owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                        Comment comment = new Comment()
                        {

                            Text = FakeData.TextData.GetSentence(),
                            Owner = (j % 2 == 0) ? admin : standartUser,
                            CreateOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            Modifiedon = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername = comment_owner.Username,
                        };

                        note.Comments.Add(comment);
                    }

                    //Adding Fake Likes ..


                    for (int m = 0; m < FakeData.NumberData.GetNumber(1, 9); m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userlist[m]
                        };

                        note.Likes.Add(liked);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
