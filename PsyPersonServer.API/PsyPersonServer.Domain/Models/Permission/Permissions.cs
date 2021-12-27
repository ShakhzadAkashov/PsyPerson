using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Models.Permission
{
    public static class Permissions
    {
        // Users
        public const string Users_View = "Permissions.Users.View";
        public const string Users_Create = "Permissions.Users.Create";
        public const string Users_Edit = "Permissions.Users.Edit";
        public const string Users_Delete = "Permissions.Users.Delete";

        // Roles
        public const string Roles_View = "Permissions.Roles.View";
        public const string Roles_Create = "Permissions.Roles.Create";
        public const string Roles_Edit = "Permissions.Roles.Edit";
        public const string Roles_Delete = "Permissions.Roles.Delete";

        // Tests
        public const string Tests_View = "Permissions.Tests.View";
        public const string Tests_Create = "Permissions.Tests.Create";
        public const string Tests_Edit = "Permissions.Tests.Edit";
        public const string Tests_Delete = "Permissions.Tests.Delete";
    }
}
