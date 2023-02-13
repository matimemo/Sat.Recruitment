using System;

namespace Sat.Recruitment.Model
{
    public static class UserHelper
    {
        public static User ToUser(this string lineFromFile)
        {
            if(string.IsNullOrEmpty(lineFromFile))
                return null;
            var row = lineFromFile.Split(',');
            return new User
            {
                Name = row[0],
                Email = row[1],
                Phone = row[2],
                Address = row[3],
                UserType = Enum.Parse<UserType>(row[4]),
                Money = decimal.Parse(row[5])
            };
        }
    }
}