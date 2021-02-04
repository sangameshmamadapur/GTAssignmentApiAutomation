using System;
using System.Collections.Generic;
using System.Text;
using GTAssignment.Models;

namespace GTAssignment.Utils
{
    public static class TestDataHelper
    {

        public static User GetValidUserData()
        {
            return new User()
            {
                id = GetRandomNumbers(5),
                username = GetRandomString(8),
                firstName = GetRandomString(4),
                lastName = GetRandomString(4),
                email = GetRandomString(4) + GetRandomNumbers(3) + "@testmail.com",
                password = GetRandomString(8),
                phone = "1234567890",
                userStatus = 1,
            };
        }


        private static string GetRandomString(int size)
        {
            var random = new Random();
            var builder = new StringBuilder(size);

            //char is a single unicode character
            char offset = 'a';
            const int lettersOffset = 26; //a...z length=26

            for (var i = 0; i < size; i++)
            {
                var @char = (char)random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return builder.ToString();

        }

        private static int GetRandomNumbers(int size)
        {
            var random = new Random();
            var builder = new StringBuilder(size);

            //char is a single unicode character
            char offset = '0';
            const int lettersOffset = 10; //0...9 length=10

            for (var i = 0; i < size; i++)
            {
                var @char = (char)random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return Convert.ToInt32(builder.ToString());

        }
    }
}
