namespace Sat.Recruitment.Model
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }

        public override string ToString()
        {
            return $"{this.Name},{Email},{Phone},{Address},{UserType},{Money}";
        }

        public bool Equals(User user)
        {
            var compareWithMailAndPhone = (Email == user.Email || Phone == user.Phone);
            var compareWithNameAndAdress = (Name == user.Name && Address == user.Address);
            return (compareWithMailAndPhone) || (compareWithNameAndAdress);
            
        }
    }
}
