using System.ComponentModel.DataAnnotations;

namespace CRUDWebAPIYoutube.Utilities
{
    public class ValidEmailAttribute :ValidationAttribute
    {
        private readonly string email;

        public ValidEmailAttribute(string email)
        {
            this.email = email;
        }
        public override bool IsValid(object? value)
        {
            var dummyValidEmail = "bhavya@gmail.com";
            if (value.Equals(dummyValidEmail))
            {
                return true;
            }
            return false;
        }


    }
}
