using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UYGS203.ViewModel;
using UYGS203.Models;


namespace UYGS203.Controllers
{
    public class baseController : ApiController
    {
        DBEntities5 db = new DBEntities5();
        ResultModel result = new ResultModel();

        #region Estate

        [HttpGet]
        [Route("api/listEstate")]
        public List<EstateModel> ListEstates()
        {
            List<EstateModel> l = db.Estate.Select(x => new EstateModel()
            {

                EstateAdress = x.EstateAdress,
                EstateEditDate = x.EstateEditDate,
                EstateDescription = x.EstateDescription,
                EstateLocationId = x.EstateLocationId,
                EstateId = x.EstateId,
                Clicks = x.Clicks,
                EstateName = x.EstateName,
                EstatePrice = x.EstatePrice,
                EstateRegDate = x.EstateRegDate,
                DiscountPrice = x.DiscountPrice,
                IsActive = x.IsActive,
                IsDiscount = x.IsDiscount

            }).ToList();

            return l;
        }


        [HttpGet]
        [Route("api/Estatebyid/{Id}")]
        public EstateModel EstateById(string Id)
        {
            EstateModel sub = db.Estate.Where(s => s.EstateId == Id).Select(x => new
             EstateModel()
            {
                EstateAdress = x.EstateAdress,
                EstateEditDate = x.EstateEditDate,
                EstateDescription = x.EstateDescription,
                EstateLocationId = x.EstateLocationId,
                EstateId = x.EstateId,
                Clicks = x.Clicks,
                EstateName = x.EstateName,
                EstatePrice = x.EstatePrice,
                EstateRegDate = x.EstateRegDate,
                DiscountPrice = x.DiscountPrice,
                IsActive = x.IsActive,
                IsDiscount = x.IsDiscount

            }).FirstOrDefault();
            return sub;
        }
        [HttpPost]
        [Route("api/addEstate")]
        public ResultModel AddEstate(EstateModel NewModel)
        {
            if (db.Estate.Count(s => s.EstateAdress == NewModel.EstateAdress && s.EstateName == NewModel.EstateName) > 0)
            {
                result.Result = false;
                result.msg = "Bu Ev Kayıtlıdır";
            }
            Estate sub = new Estate();
            sub.EstateId          = Guid.NewGuid().ToString();
            sub.EstateName        = NewModel.EstateName;
            sub.EstateAdress      = NewModel.EstateAdress;
            sub.EstateEditDate    = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            sub.EstateRegDate     = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            sub.EstateDescription = NewModel.EstateDescription;
            sub.EstateLocationId  = NewModel.EstateLocationId;
            sub.Clicks            = "0";
            sub.EstateName        = NewModel.EstateName;
            sub.EstatePrice       = NewModel.EstatePrice;
            sub.DiscountPrice     = NewModel.DiscountPrice;
            sub.IsActive          = NewModel.IsActive;
            sub.IsDiscount        = NewModel.IsDiscount;
            db.Estate.Add(sub);
            db.SaveChanges();

            result.msg = "Ev Eklendi";
            result.Result = true;
            return result;
        }

        [HttpPut]
        [Route("api/updateEstate")]
        public ResultModel UpdateEstate(EstateModel update)
        {
            Estate sub = db.Estate.Where(s => s.EstateId == update.EstateId).FirstOrDefault();

            if (sub == null)
            {
                result.Result = false;
                result.msg = "Kayıt Bulunamadı";
                return result;
            }

            sub.EstateName = update.EstateName;
            sub.EstateAdress = update.EstateAdress;
            sub.EstateEditDate = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            sub.EstateDescription = update.EstateDescription;
            sub.EstateLocationId = update.EstateLocationId;
            sub.EstatePrice = update.EstatePrice;
            sub.DiscountPrice = update.DiscountPrice;
            sub.IsActive = update.IsActive;
            sub.IsDiscount = update.IsDiscount;

            db.SaveChanges();

            result.Result = true;
            result.msg = "Ders değiştirildi";

            return result;
        }

        [HttpDelete]
        [Route("api/deleteEstate/{Id}")]
        public ResultModel DeleteEstate(String Id)
        {
            Estate sub = db.Estate.Where(s => s.EstateId == Id).FirstOrDefault();

            if (sub == null)
            {
                result.Result = false;
                result.msg = "Kayıt Bulunamadı";
                return result;
            }

            if (db.Banner.Count(s=>s.BannerEstateId ==Id)>0)
            {
                result.Result = false;
                result.msg = "Ev kayıtlı kullanıcı var, ilan silinemez.";
                return result;
            }
            db.Estate.Remove(sub);
            db.SaveChanges();
            result.Result = true;
            result.msg = "Ev silindi.";

            return result;
        }
        #endregion

        #region User


        [HttpGet]
        [Route("api/listUsers")]

        public List<UserModel> ListUsers()
        {
            List<UserModel> l = db.User.Select(x => new UserModel()
            {
              UserId = x.UserId,
              UserFullName = x.UserFullName,
              UserIsAdmin = x.UserIsAdmin,
              UserMail = x.UserMail,
              UserPassword = x.UserPassword,
              UserRegDate = x.UserRegDate,
           

            }).ToList();

            return l;
        }

        [HttpGet]
        [Route("api/UserbyId/{Id}")]

        public UserModel UserById(string Id)
        {
            UserModel l = db.User.Where(s=> s.UserId == Id).Select(x => new UserModel()
            {
                UserId = x.UserId,
                UserFullName = x.UserFullName,
                UserIsAdmin = x.UserIsAdmin,
                UserMail = x.UserMail,
                UserPassword = x.UserPassword,
                UserRegDate = x.UserRegDate,

            }).FirstOrDefault();

            return l;
        }

        [HttpPost]
        [Route("api/addUser")]
        public ResultModel AddUser(UserModel NewUser)
        {
            if (db.User.Count(s=> s.UserMail == NewUser.UserMail)> 0)
            {
                result.Result = false;
                result.msg = "Kullanıcı zaten var";
                return result;
            }

            User NewAddition = new User
            {
                
                UserId = Guid.NewGuid().ToString(),
                UserFullName = NewUser.UserFullName,
                UserIsAdmin = NewUser.UserIsAdmin,
                UserMail = NewUser.UserMail,
                UserPassword = NewUser.UserPassword,
                UserRegDate = DateTime.Now.ToString("MM/dd/yyyy h:mm tt")
        };
            db.User.Add(NewAddition);
            db.SaveChanges();

            result.Result = true;
            result.msg = "Kullanıcı Eklendi";
            return result;
        }
        [HttpPut]
        [Route("api/updateUser")]
        public ResultModel UpdateUser(UserModel update)
        {
            User sub = db.User.Where(s => s.UserId == update.UserId).FirstOrDefault();

            if (sub == null)
            {
                result.Result = false;
                result.msg = "Kayıt Bulunamadı";
                return result;
            }

            sub.UserFullName = update.UserFullName;
            sub.UserIsAdmin = update.UserIsAdmin;
            sub.UserPassword = update.UserPassword;

            db.SaveChanges();

            result.Result = true;
            result.msg = "Öğrenci değiştirildi";

            return result;
        }
        [HttpDelete]
        [Route("api/deleteUser/{Id}")]
        public ResultModel DeleteUser(String Id)
        {
            User sub = db.User.Where(s => s.UserId == Id).FirstOrDefault();

            if (sub == null)
            {
                result.Result = false;
                result.msg = "Kayıt Bulunamadı";
                return result;
            }

            if (db.Banner.Count(s=>s.BannerUserId==Id)>0)
            {
                result.Result = false;
                result.msg = "Kullanıcı ilan sahibi olduğu için silinemez.";
                return result;
            }


            db.User.Remove(sub);
            db.SaveChanges();
            result.Result = true;
            result.msg = "Kullanıcı Silindi";

            return result;
        }
        #endregion


        #region Banner
        [HttpGet]
        [Route("api/EstatesofUser/{UserId}")]
        public List<BannerModel> ListEstates_of_Users(string UserId)
        {
            List<BannerModel> l = db.Banner.Where(s => s.BannerUserId == UserId).Select(x => new
              BannerModel()
            {
                BannerId = x.BannerId,
                BannerEstateId = x.BannerEstateId,
                BannerUserId = x.BannerUserId

            }).ToList(); 

            foreach (var banner in l)
            {
                banner.EstateInfo = EstateById(banner.BannerEstateId);
                banner.UserInfo = UserById(banner.BannerUserId);

            }
            return l;

        }

        [HttpGet]
        [Route("api/UsersofEstate/{EstateId}")]
        public List<BannerModel> ListUsers_of_Estate(string EstateId)
        {
            List<BannerModel> l = db.Banner.Where(s => s.BannerEstateId == EstateId).Select(x => new
              BannerModel()
            {
                 BannerId = x.BannerId,
                BannerEstateId = x.BannerEstateId,
                BannerUserId = x.BannerUserId

            }).ToList();

            foreach (var banner in l)
            {
                banner.EstateInfo = EstateById(banner.BannerEstateId);
                banner.UserInfo = UserById(banner.BannerUserId);

            }
            return l;

        }

        [HttpPost]
        [Route("api/addbanner")]
        public ResultModel addBanner(BannerModel model)
        {
            if (db.Banner.Count(s=> s.BannerEstateId==model.BannerEstateId && s.BannerUserId == model.BannerUserId)>0)
            {
                result.Result = false;
                result.msg = "Öğrenci zaten kayıtlıdır.";
                return result;
            }
            Banner NewBanner = new Banner();
            NewBanner.BannerId = Guid.NewGuid().ToString();
            NewBanner.BannerUserId = model.BannerUserId;
            NewBanner.BannerEstateId = model.BannerEstateId;
            db.Banner.Add(NewBanner);
            db.SaveChanges();

            result.Result = true;
            result.msg = "Kayıt eklendi";

            return result;
        }

        [HttpDelete]
        [Route("api/deletebanner/{BannerId}")]
        public ResultModel deleteBanner(string BannerId)
        {
            Banner Bnr = db.Banner.Where(s => s.BannerId == BannerId).SingleOrDefault();

            if (Bnr == null)
            {
                result.Result = false;
                result.msg = "Kayıt bulunamadı";
                return result;
            }
            db.Banner.Remove(Bnr);
            db.SaveChanges();

            result.Result = true;
            result.msg = "Kayıt silindi";
            return result;
        }
        #endregion
    }
}
