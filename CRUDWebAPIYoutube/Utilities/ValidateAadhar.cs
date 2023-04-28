using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;

namespace CRUDWebAPIYoutube.Utilities
{
    public class ValidateAadhar :ValidationAttribute
    {
        private readonly long AadharNumber;

        public ValidateAadhar(long aadharNumber) =>AadharNumber = aadharNumber;

        public override bool IsValid(object? value)
        {
            long aadharNumber = (long)value;
            bool result= (aadharNumber == 123123123123)? true: false;          
            return result;
        }


    }
}
