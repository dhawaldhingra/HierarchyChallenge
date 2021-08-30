using Microsoft.VisualStudio.TestTools.UnitTesting;
using HierarchyChallenge.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;
using HierarchyChallenge.DataObjects;
using System.Linq;

namespace HierarchyChallenge.BusinessLogic.Tests
{
    [TestClass()]
    public class UsersRolesTests
    {
        [TestMethod()]
        public void GetSubordinatesTest()
        {
            UsersRoles usersAndRoles = new UsersRoles();
            List<User> users = new List<User>()
            {
                new User { Id = 1, Name = "Tom", Role = 1 },
                new User { Id = 2, Name = "Dick", Role = 1 },
                new User { Id = 3, Name = "Harry", Role = 2 },
                new User { Id = 4, Name = "Russell", Role = 2 },
                new User { Id = 5, Name = "Peter", Role = 2 },
                new User { Id = 6, Name = "Dr. Strange", Role = 11 },
                new User { Id = 7, Name = "Thor Odinson", Role = 4 },
                new User { Id = 8, Name = "Mobius", Role = 9 },
                new User { Id = 9, Name = "Tony Stark", Role = 3 },
                new User { Id = 10, Name = "Nick Fury", Role = 3 },
                new User { Id = 11, Name = "Thanos", Role = 4 },
                new User { Id = 12, Name = "Odin", Role = 10 },
                new User { Id = 13, Name = "Friga", Role = 10 },
                new User { Id = 14, Name = "Ancient One", Role = 7 },
                new User { Id = 15, Name = "Steve Lang", Role = 7 },
                new User { Id = 16, Name = "Peter Quill", Role = 8 },
                new User { Id = 17, Name = "Galactus", Role = 6 },
            };

            List<Role> roles = new List<Role>()
            {
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Domain Admin", Parent = 1 },
                new Role { Id = 3, Name = "Security Admin", Parent = 1 },
                new Role { Id = 4, Name = "Power User", Parent = 1 },
                new Role { Id = 5, Name = "System User", Parent = 2 },
                new Role { Id = 6, Name = "Remote User", Parent = 5 },
                new Role { Id = 7, Name = "Guest User", Parent = 5 },
                new Role { Id = 8, Name = "Restricted User", Parent = 5 },
                new Role { Id = 9, Name = "Trainee User", Parent = 5 },
                new Role { Id = 10, Name = "Trainer", Parent = 4 },
                new Role { Id = 11, Name = "Super User", Parent = 0 },
            };

            usersAndRoles.Users = users;
            usersAndRoles.Roles = roles;

            List<User> expectedOutput;

            expectedOutput = new List<User>()
            {
                new User { Id = 3, Name = "Harry", Role = 2 },
                new User { Id = 4, Name = "Russell", Role = 2 },
                new User { Id = 5, Name = "Peter", Role = 2 },
                new User { Id = 7, Name = "Thor Odinson", Role = 4 },
                new User { Id = 8, Name = "Mobius", Role = 9 },
                new User { Id = 9, Name = "Tony Stark", Role = 3 },
                new User { Id = 10, Name = "Nick Fury", Role = 3 },
                new User { Id = 11, Name = "Thanos", Role = 4 },
                new User { Id = 12, Name = "Odin", Role = 10 },
                new User { Id = 13, Name = "Friga", Role = 10 },
                new User { Id = 14, Name = "Ancient One", Role = 7 },
                new User { Id = 15, Name = "Steve Lang", Role = 7 },
                new User { Id = 16, Name = "Peter Quill", Role = 8 },
                new User { Id = 17, Name = "Galactus", Role = 6 },
            };

            var output = usersAndRoles.GetSubordinates(1);
            Assert.AreEqual(expectedOutput.Count, output.Count);
            expectedOutput.ForEach(eo =>
            {
                if (output.Where(o => o.Id == eo.Id).Count() == 1)
                {
                    Assert.Fail();
                }
            });

            // Test Case II.
            expectedOutput = new List<User>
            {
                new User { Id = 8, Name = "Mobius", Role = 9 },
                new User { Id = 14, Name = "Ancient One", Role = 7 },
                new User { Id = 15, Name = "Steve Lang", Role = 7 },
                new User { Id = 16, Name = "Peter Quill", Role = 8 },
                new User { Id = 17, Name = "Galactus", Role = 6 },
            };
            output = usersAndRoles.GetSubordinates(5);
            Assert.AreEqual(expectedOutput.Count, output.Count);
            expectedOutput.ForEach(eo =>
            {
                if (output.Where(o => o.Id == eo.Id).Count() == 1)
                {
                    Assert.Fail();
                }
            });
        }
    }
}