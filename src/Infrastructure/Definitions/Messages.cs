using System.ComponentModel;

namespace Infrastructure.Definitions;

public class Messages
{
    [DisplayName("Middlewares")]
    public static class Middlewares
    {
        public const string IPAddressForbidden = "Mes.Middlewares.IPAddress.Forbidden";
    }

    [DisplayName("Users")]
    public static class Users
    {
        public const string EmailIsRequired = "Mes.Users.Email.IsRequired";
        public const string CodeIsRequired = "Mes.Users.Code.IsRequired";
        public const string CodeIsExpires = "Mes.Users.Code.Expires";
        public const string NewPasswordIsRequired = "Mes.Users.NewPassword.IsRequired";
        public const string OldPasswordIsRequired = "Mes.Users.OldPassword.IsRequired";
        public const string EmailBadFormat = "Mes.Users.Email.BadFormat";
        public const string EmailIsExisting = "Mes.Users.Email.Existing";
        public const string FullNameIsRequired = "Mes.Users.FullName.IsRequired";
        public const string PasswordIsRequired = "Mes.Users.Password.IsRequired";
        public const string PasswordConfirmIsRequired = "Mes.Users.PasswordConfirm.IsRequired";
        public const string PasswordConfirmNotMatch = "Mes.Users.PasswordConfirm.NotMatch";
        public const string EmailNotFound = "Mes.Users.Email.NotFound";
        public const string EmailAddressAlreadyExist = "Mes.Users.EmailAddress.AlreadyExist";
        public const string IdNotFound = "Mes.Users.Id.NotFound";
        public const string GetDetailSuccessfully = "Mes.Users.GetDetail.Successfully";
        public const string GetSuccessfully = "Mes.Users.Get.Successfully";
        public const string UpdateSuccessfully = "Mes.Users.Update.Successfully";
        public const string SendMailSuccessfully = "Mes.Users.SendMai.Successfully";
        public const string ResetPasswordlSuccessfully = "Mes.Users.ResetPassword.Successfully";
        public const string CheckCodeSuccessfully = "Mes.Users.CheckCode.Successfully";
        public const string DeleteSuccessfully = "Mes.Users.Delete.Successfully";
        public const string RegisterSuccessfully = "Mes.Users.Register.Successfully";
        public const string LoginSuccessfully = "Mes.Users.Login.Successfully";
        public const string WrongPassword = "Mes.Users.Password.Wrong";
        public const string WrongCode = "Mes.Users.Code.Wrong";
        public const string PasswordOverLength = "Mes.Users.Password.OverLength";
    }
    [DisplayName("Sessions")]
    public static class Sessions
    {
        public const string NameIsRequired = "Mes.Sessions.Name.IsRequired";
        public const string IdIsRequired = "Mes.Sessions.Id.IsRequired";
        public const string IdNotFound = "Mes.Sessions.Id.NotFound";

        public const string GetSuccessfully = "Mes.Sessions.Get.Successfully";
        public const string CreateSuccessfully = "Mes.Sessions.Create.Successfully";
        public const string UpdateSuccessfully = "Mes.Sessions.Update.Successfully";
        public const string DeleteSuccessfully = "Mes.Sessions.Delete.Successfully";
    }



    [DisplayName("ContentSessions")]
    public static class ContentSessions
    {
        public const string ContentIdIsRequired = "Mes.ContentSessions.ContentId.IsRequired";
        public const string ContentIdNotFound = "Mes.ContentSessions.ContentId.NotFound";
        public const string IdNotFound = "Mes.ContentSessions.Id.NotFound";
        public const string SessionIdIsRequired = "Mes.ContentSessions.SessionId.IsRequired";
        public const string SessionIdNotFound = "Mes.ContentSessions.SessionId.NotFound";
        public const string GetSuccessfully = "Mes.ContentSessions.Get.Successfully";
        public const string CreateSuccessfully = "Mes.ContentSessions.Create.Successfully";
        public const string UpdateSuccessfully = "Mes.ContentSessions.Update.Successfully";
        public const string DeleteSuccessfully = "Mes.ContentSessions.Delete.Successfully";
    }



    [DisplayName("Items")]
    public static class Items
    {
        public const string NameIsRequired = "Mes.Items.Name.IsRequired";
        public const string ItemIsExisting = "Mes.Items.IsExisting";
        public const string IdIsRequired = "Mes.Items.Id.IsRequired";
        public const string OrderIsRequired = "Mes.Items.Order.IsRequired";
        public const string ItemTypeIsRequired = "Mes.Items.ItemType.IsRequired";
        public const string RoundTypeIsInEnum = "Mes.Items.RoundType.IsInEnum";
        public const string RoundTypeIsMany = "Mes.Items.RoundType.IsMany";
        public const string ItemTypeIsInEnum = "Mes.Items.ItemType.IsInEnum";
        public const string RoundTypeIsRequired = "Mes.Items.RoundType.IsRequired";
        public const string SessionIdIsRequired = "Mes.Items.SessionId.IsRequired";
        public const string SessionIdNotFound = "Mes.Items.SessionId.NotFound";
        public const string IdNotFound = "Mes.Items.Id.NotFound";
        public const string GetSuccessfully = "Mes.Items.Get.Successfully";
        public const string CreateSuccessfully = "Mes.Items.Create.Successfully";
        public const string UpdateSuccessfully = "Mes.Items.Update.Successfully";
        public const string DeleteSuccessfully = "Mes.Items.Delete.Successfully";
    }


    [DisplayName("Contents")]
    public static class Contents
    {
        public const string SuggestIsRequired = "Mes.Contents.Name.IsRequired";
        public const string IdIsRequired = "Mes.Contents.Id.IsRequired";
        public const string IdIsDuplicate = "Mes.Contents.Id.IsDuplicate";

        public const string OrderIsRequired = "Mes.Contents.Order.IsRequired";
        public const string ItemTypeIsRequired = "Mes.Contents.ItemType.IsRequired";
        public const string RoundTypeIsRequired = "Mes.Contents.RoundType.IsRequired";
        public const string ContentTypeIsInEnum = "Mes.Contents.ContentType.IsInEnum";
        public const string SessionIdIsRequired = "Mes.Contents.SessionId.IsRequired";
        public const string IdNotFound = "Mes.Contents.Id.NotFound";
        public const string GetSuccessfully = "Mes.Contents.Get.Successfully";
        public const string CreateSuccessfully = "Mes.Contents.Create.Successfully";
        public const string CreateFail = "Mes.Contents.Create.Fail";
        public const string UpdateSuccessfully = "Mes.Contents.Update.Successfully";
        public const string DeleteSuccessfully = "Mes.Contents.Delete.Successfully";
        public const string DeleteFail = "Mes.Contents.Delete.Fail";
    }

    [DisplayName("Questions")]
    public static class Questions
    {
        public const string IdNotFound = "Mes.Questions.Id.NotFound";
        public const string IdNotNull = "Mes.Questions.Id.IdNotNull";
        public const string IdIsRequired = "Mes.Questions.Id.IdNotEmpty";
        public const string GetDetailSuccessfully = "Mes.Questions.GetDetail.Successfully";
        public const string GetSuccessfully = "Mes.Questions.Get.Successfully";
        public const string CreateSuccessfully = "Mes.Questions.Create.Successfully";
        public const string UpdateSuccessfully = "Mes.Questions.Update.Successfully";
        public const string DeleteSuccessfully = "Mes.Questions.Delete.Successfully";
        public const string QuestionContentNotNull = "Mes.Questions.QuestionContent.NotFound";
        public const string QuestionContentIsRequired = "Mes.Questions.QuestionContent.IsRequired";

    }

    public static class Answers
    {
        public const string IdNotFound = "Mes.Answers.Id.NotFound";
        public const string GetDetailSuccessfully = "Mes.Answers.GetDetail.Successfully";
        public const string GetSuccessfully = "Mes.Answers.Get.Successfully";
        public const string CreateSuccessfully = "Mes.Answers.Create.Successfully";
        public const string UpdateSuccessfully = "Mes.Answers.Update.Successfully";
        public const string DeleteSuccessfully = "Mes.Answers.Delete.Successfully";
        public const string AnswerContentNotNull = "Mes.Answers.AnswerContent.NotNull";
        public const string AnswerContentIsRequired = "Mes.Answers.AnswerContent.IsRequired";



    }
}
