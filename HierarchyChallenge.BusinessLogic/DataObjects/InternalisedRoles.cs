using System;
using System.Collections.Generic;
using System.Text;

namespace HierarchyChallenge.DataObjects
{
    /// <summary>
    /// An internal version of Role class that represents the Roles hirearchy as a tree structure.
    /// Unlike the original Role class that maintains the link to parent role only that too in a simple and less usefull way,
    /// this class maintains the relation to parent as well as children roles
    /// </summary>
    internal class InternalisedRole
    {
        /// <summary>
        /// Unique Role Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// The role name. This is not required but makes debugging easier.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// The parent of the given role instance. This will be null for top level role
        /// </summary>
        public InternalisedRole Parent { get; set; }
        
        /// <summary>
        /// The direct children of a given role instance. This will be null for roles with no children
        /// </summary>
        public List<InternalisedRole> Children { get; set; }
    }
}
