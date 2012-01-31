using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Phorcys.Core;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Data.NHibernate;
using System.Collections.Specialized;
using NHibernate;
using SharpArchContrib.Castle.Logging;
using log4net;

namespace Phorcys.UI.Web.Security {
  public class DiveLogMembershipProvider : MembershipProvider {
    private Phorcys.Core.User user = new Phorcys.Core.User();
    private IRepository<User> userRepository = new Repository<User>();
    private readonly SqlMembershipProvider sqlMembershipProvider = new SqlMembershipProvider();
    private log4net.Core.LogException logger = new log4net.Core.LogException();
    private ILog log = LogManager.GetLogger(typeof(DiveLogMembershipProvider));

    public DiveLogMembershipProvider()
    {
      this.user = new User();
      //this.userRepository = new Repository<User>();
    }

    public DiveLogMembershipProvider(User user) {
      this.user = user;
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
      User user = new User();
     
      user.LoginId = username;
      user.Password = password;
      user.Created = DateTime.Now;
      user.LastModified = DateTime.Now;
      userRepository.SaveOrUpdate(user);
      status = MembershipCreateStatus.Success;
      return GetUser(username, false);
    }

    public User RetrieveUser(User user) {
      User retVal;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary.Add("LoginId", user.LoginId);
      retVal = userRepository.FindOne(dictionary);
      return retVal;
    }

    public User RetrieveUser(string username) {
      User user = new User();
      User retVal;
      user.LoginId = username;
      retVal = RetrieveUser(user);
      return retVal;
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer) {
      throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer) {
      throw new NotImplementedException();
    }

    public override bool ChangePassword(string username, string oldPassword, string newPassword) {
      throw new NotImplementedException();
    }

    public override string ResetPassword(string username, string answer) {
      throw new NotImplementedException();
    }

    public override void UpdateUser(MembershipUser user) {
      throw new NotImplementedException();
    }

    public override bool ValidateUser(string username, string password) {
      bool retVal = false;
      User user;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary.Add("LoginId", username);
      dictionary.Add("Password", password);

      try
      {
          user = userRepository.FindOne(dictionary);
          if (user != null) {
              user.LoginCount++;
              user.LastModified = DateTime.Now;
              userRepository.SaveOrUpdate(user);
              //user = userRepository.FindOne(dictionary);
              userRepository.DbContext.CommitChanges();
              retVal = true;
          }
      }
      catch (Exception ex) {
         log.Warn("error accessing User table: ", ex); 
      }

      return retVal;
    }

    public override bool UnlockUser(string userName) {
      throw new NotImplementedException();
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) {
      throw new NotImplementedException();
    }

    public override MembershipUser GetUser(string username, bool userIsOnline) {
      User user;
      MembershipUser retVal;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary.Add("LoginId", username);
      user = userRepository.FindOne(dictionary);
      if (user != null)
      {
        retVal = new MembershipUser("DiveLogMembershipProvider",username, null, "","","",true,false,new DateTime(), new DateTime(), new DateTime(), new DateTime(), new DateTime() );
      }else
      {
        retVal = null;
      }
      return retVal;
    }

    public override string GetUserNameByEmail(string email) {
      throw new NotImplementedException();
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData) {
      throw new NotImplementedException();
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {
      throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline() {
      throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
      throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords) {
      throw new NotImplementedException();
    }

    public override bool EnablePasswordRetrieval {
      get { return sqlMembershipProvider.EnablePasswordRetrieval; }
    }

    public override bool EnablePasswordReset {
      get { return sqlMembershipProvider.EnablePasswordReset; }
    }

    public override bool RequiresQuestionAndAnswer {
      get { return sqlMembershipProvider.RequiresQuestionAndAnswer; }
    }

    public override string ApplicationName {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }

    public override int MaxInvalidPasswordAttempts {
      get { return sqlMembershipProvider.MaxInvalidPasswordAttempts; }
    }

    public override int PasswordAttemptWindow {
      get { return sqlMembershipProvider.PasswordAttemptWindow; }
    }

    public override bool RequiresUniqueEmail {
      get { return sqlMembershipProvider.RequiresUniqueEmail; }
    }

    public override MembershipPasswordFormat PasswordFormat {
      get { return sqlMembershipProvider.PasswordFormat; }
    }

    public override int MinRequiredPasswordLength {
      get {
        NameValueCollection settings = System.Configuration.ConfigurationManager.GetSection("membership/providers/add") as NameValueCollection;
        //string minRequiredPasswordLength = settings["minRequiredPasswordLength"];
        //int min = Int32.Parse(minRequiredPasswordLength);
        return 4; 
      }
    }

    public override int MinRequiredNonAlphanumericCharacters {
      get { return sqlMembershipProvider.MinRequiredNonAlphanumericCharacters; }
    }

    public override string PasswordStrengthRegularExpression {
      get { throw new NotImplementedException(); }
    }
  }
}