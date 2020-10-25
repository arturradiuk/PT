namespace BookStore
{
    public class Client
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhoneNumber { get; set; }

        public Client(string email, string firstName, string secondName, string phoneNumber)
        {
            Email = email;
            FirstName = firstName;
            SecondName = secondName;
            PhoneNumber = phoneNumber;
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Client c = (Client) obj;
                return (this.Email.Equals(c.Email)) && (this.FirstName.Equals(c.Email)) &&
                       (this.SecondName.Equals(c.SecondName)) && (this.PhoneNumber.Equals(c.PhoneNumber));
            }
        }
    }
}