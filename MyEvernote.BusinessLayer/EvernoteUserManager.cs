using MyEvernote.DataAccessLayer.Entity;
using MyEverNote.Entities;
using MyEverNote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.Threading.Tasks;
using MyEverNote.Entities.Messages;

namespace MyEvernote.BusinessLayer
{
  public  class EvernoteUserManager
    {

        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();

        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
        {
            //Kullanıcı username kontrolü
            //kullanıcı  e-posta kontrollü 
            // kayıt işlemi
            // Aktivasyon e-postası gönderimi

            EvernoteUser user=repo_user.Find(x => x.Username == data.Username || x.Email==data.Email);
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();

            if (user!=null)
            {
                
                if(user.Username==data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists,
                        "kullanıcı adı kayıtlı");
                }

                if (user.Email==data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists,
                        "Mail adresi kayıtlı");
                }
            }
            else
            {
              int dbResult= repo_user.Insert(new EvernoteUser()
                {
                    Username = data.Username,
                    Email=data.Email,
                    Paswword=data.Password,
                    ActivateGuid=Guid.NewGuid(),
                    IsActive =false,
                    IsAdmin=false,


                });

                if (dbResult>1)
                {
                    res.Result=repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);
                    

                    //TOdo:aktivasyon maili atılacak
                   // layerResult.Result.ActivateGuid
                }

            }


            return res;
        }

        public BusinessLayerResult<EvernoteUser>  LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
            res.Result = repo_user.Find(x => x.Username == data.Username && x.Paswword == data.Password);


            if (res.Result != null)
            {
                if(!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotAvtive, "Kullanıcı aktifleştirilmemiştir");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz.");
                }

            }
            else
            {
                res.AddError(ErrorMessageCode.UserNakeOrPassWrong,
                "Kullanıcı adı veya şifre yanlış");
            }

            return res;
        }
    }
}
