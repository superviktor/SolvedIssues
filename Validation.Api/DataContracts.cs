namespace Validation.Api
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressDto[] Addresses { get; set; }
        public string PhoneNumber { get; set; }
        //public PhoneNumberDto PhoneNumber { get; set; }
    }


    //public abstract class PhoneNumberDto
    //{
    //    public string Value { get; set; }
    //}

    //public class UsPhoneNumber : PhoneNumberDto
    //{

    //}
    //public class InternationalPhoneNumber : PhoneNumberDto
    //{

    //}

    public class RegisterResponse
    {
        public long Id { get; set; }
    }

    public class EditPersonalInfoRequest
    {
        public string Name { get; set; }
        public AddressDto[] Addresses { get; set; }
    }

    public class EnrollRequest
    {
        public CourseEnrollmentDto[] Enrollments { get; set; }
    }

    public class CourseEnrollmentDto
    {
        public string Course { get; set; }
        public string Grade { get; set; }
    }

    public class GetResonse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressDto[] Addresses { get; set; }
        public CourseEnrollmentDto[] Enrollments { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
