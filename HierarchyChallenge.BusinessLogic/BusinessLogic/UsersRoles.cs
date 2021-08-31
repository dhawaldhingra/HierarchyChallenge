using HierarchyChallenge.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HierarchyChallenge.BusinessLogic
{
    /// <summary>
    /// This class represents mapping of
    /// </summary>
    public class UsersRoles
    {
        public List<User> Users { get; set; }
        public List<Role> Roles { get; set; }
        
        /// <summary>
        /// Private variable that will maintain a 2-way roles hierachy
        /// </summary>
        private List<InternalisedRole> rolesMap;

        /// <summary>
        /// Gets the subordinates of a given user. The method works like this-
        /// 1. Validate the input provided
        /// 2. Find the user ID supplied in the input in the list of users and read their role ID
        /// 3. Read the list of roles and add them to a list of InternalisedRoles along with their parent and immediate subordinates. This establishes a link to direct subordinates.
        /// 4. Now we've the immediate subordinates of each role, add the user IDs of the  matching roles to a list
        /// 5. Recursively repeat step 4 until there are no child roles found
        /// 6. Now we've got the list of all the user IDs which are direct/inderct subordinates of a given user/role. Return the list of all the users with those User IDs/.
        /// </summary>
        /// <param name="userId">The ID of a user whose subordinates need to be returned</param>
        /// <returns>A list of users that are subordinates of the given UserID</returns>
        public List<User> GetSubordinates(int userId)
        {
            // A blank object for storing the output. 
            List<User> outputObj = new List<User>();

            // If input is invalid, return a blank response
            if (!ValidateInput())
            {
                return outputObj;
            }

            // Select the role id of the user whose subordinates need to be returned. 
            // For a valid input supplied, every user will have a unique ID, so the below statement will return either one or no userRole
            var userRole = Users.Where(u => u.Id == userId).Select(u => u.Role).FirstOrDefault();

            // If the userId supplied in input is not present in the list of users, return blank result
            if (userRole == 0)
            {
                return outputObj;
            }

            // Select the role of the User whose subordinates need to be returned. The input validations done above will dictate that this is always a single role.
            var selectedUserRoleId = Roles.Where(r => r.Id == userRole).FirstOrDefault().Id;

            // Create a collection of InternalisedRoles. 
            // As this is a tree structure, it will alow us traversing the subordinates
            rolesMap = new List<InternalisedRole>();

            // Populate the values rolesMap
            Roles.ForEach(role =>
            {
                rolesMap.Add(new InternalisedRole
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Parent = Roles.Where(r => r.Id == role.Parent)
                    .Select(r => new InternalisedRole()
                    {
                        RoleId = r.Id,
                        RoleName = r.Name
                    }).FirstOrDefault(),
                    Children = Roles.Where(r=>r.Parent==role.Id).Select(r=>new InternalisedRole()
                    {
                        RoleId = r.Id,
                        RoleName = r.Name
                    }).ToList()
                });
            });

            // Get all the subordinate roles for the selected user's role
            List<int> childRoleIds = new List<int>();
            foreach (var child in rolesMap.Where(r => r.RoleId == selectedUserRoleId).FirstOrDefault().Children)
            {
                AddChildToList(ref childRoleIds, rolesMap.Where(r=>r.RoleId==child.RoleId).FirstOrDefault());
            }
            // AddChildToList(ref childRoleIds, rolesMap.Where(r=>r.RoleId==selectedUserRoleId).FirstOrDefault());

            // Now that we've all the subordinate roles, we can query the userlist to return the users matching those roles
            childRoleIds.ForEach(id =>
            {
                outputObj.AddRange(Users.Where(u => u.Role == id));
            });

            return outputObj;
        }

        /// <summary>
        /// This method will add the role IDs of all the subordinates of a given role recursively
        /// </summary>
        /// <param name="childRoleIds">The list of role IDs to which the subordinate has to be added</param>
        /// <param name="role">The child node</param>
        private void AddChildToList(ref List<int> childRoleIds, InternalisedRole role)
        {
            childRoleIds.Add(role.RoleId);
            if (role.Children != null && role.Children.Count > 0)
            {
                foreach (var child in role.Children)
                {
                    AddChildToList(ref childRoleIds, rolesMap.Where(r=>r.RoleId == child.RoleId).FirstOrDefault());
                }
            }
            else
            {
                return;
            }
            
        }

        /// <summary>
        /// This method performs the basic validations on the input fields.
        /// The following validation criteria is applied-
        ///     1. UserIDs should be all unique
        ///     2. RoleIDs should be all unique
        ///     3. A role assigned to a user must be present in the Roles list
        ///     4. The parent of a role is also present in the role list unless the role ID of parent is 0
        /// </summary>
        private bool ValidateInput()
        {
            // Check if any user IDs are duplicate
            var maxCount = this.Users.GroupBy(usr => usr.Id).Where(u => u.Count() > 1).Count();
            if(maxCount > 0)
            {
                return false;
            }

            // Check if any role IDs are duplicate
            maxCount = this.Roles.GroupBy(role => role.Id).Where(r => r.Count() > 1).Count();
            if(maxCount>0)
            {
                return false;
            }

            // Check that all the User roles are present in the roles list
            bool missingRole = false;
            this.Users.ForEach(usr =>
            {
                if (this.Roles.Where(r => r.Id == usr.Role && usr.Role != 0).Count() == 0)
                {
                    missingRole = true;
                }
            });

            //Check that the parent of a role is also present in the role list
            bool parentRoleMissing = false;
            this.Roles.ForEach(rl =>
            {
                if(rl.Parent!=0)
                {
                    if (this.Roles.Where(r => r.Id == rl.Parent).Count() == 0)
                    {
                        parentRoleMissing = true;
                    }

                }
                  
            });

            return (!missingRole && !parentRoleMissing);
        }
    }
}
