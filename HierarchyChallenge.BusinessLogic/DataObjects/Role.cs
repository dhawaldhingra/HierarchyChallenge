using System;
using System.Collections.Generic;
using System.Text;

namespace HierarchyChallenge.DataObjects
{
    /// <summary>
    /// Class Representing a Role
    /// </summary>
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Parent { get; set; }
    }
}
