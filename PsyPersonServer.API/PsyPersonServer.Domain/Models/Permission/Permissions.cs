using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Models.Permission
{
    public static class Permissions
    {
        // Users
        public const string Users = "Permissions.Users.Pages";
        public const string Users_View = "Permissions.Users.View";
        public const string Users_Create = "Permissions.Users.Create";
        public const string Users_Edit = "Permissions.Users.Edit";
        public const string Users_Delete = "Permissions.Users.Delete";

        // Roles
        public const string Roles = "Permissions.Roles.Pages";
        public const string Roles_View = "Permissions.Roles.View";
        public const string Roles_Create = "Permissions.Roles.Create";
        public const string Roles_Edit = "Permissions.Roles.Edit";
        public const string Roles_Delete = "Permissions.Roles.Delete";

        //UserRoles
        public const string UserRoles = "Permissions.UserRoles.Pages";
        public const string UserRoles_View = "Permissions.UserRoles.View";
        public const string UserRoles_Create = "Permissions.UserRoles.Create";
        public const string UserRoles_Edit = "Permissions.UserRoles.Edit";
        public const string UserRoles_Delete = "Permissions.UserRoles.Delete";

        // Permission
        public const string Permission = "Permissions.Permission.Pages";
        public const string Permission_View = "Permissions.Roles.View";
        public const string Permission_Create = "Permissions.Roles.Create";

        // Tests
        public const string Tests = "Permissions.Tests.Pages";
        public const string Tests_View = "Permissions.Tests.View";
        public const string Tests_Create = "Permissions.Tests.Create";
        public const string Tests_Edit = "Permissions.Tests.Edit";
        public const string Tests_Delete = "Permissions.Tests.Delete";

        // TestQuestions
        public const string TestQuestions = "Permissions.TestQuestions.Pages";
        public const string TestQuestions_View = "Permissions.TestQuestions.View";
        public const string TestQuestions_Create = "Permissions.TestQuestions.Create";
        public const string TestQuestions_Edit = "Permissions.TestQuestions.Edit";
        public const string TestQuestions_Upload = "Permissions.TestQuestions.Upload";

        // Testings
        public const string Testings = "Permissions.Testings.Pages";
        public const string Testings_View = "Permissions.Testings.View";
        public const string Testings_ViewHistory = "Permissions.Testings.ViewHistory";
        public const string Testings_Create = "Permissions.Testings.Create";
        public const string Testings_ForCheck = "Permissions.Testings.ForCheck";

        // UserTests
        public const string UserTests = "Permissions.UserTests.Pages";
        public const string UserTestUsers = "Permissions.UserTestUsers.Pages";
        public const string UserTests_View = "Permissions.UserTests.View";
        public const string UserTests_Create = "Permissions.UserTests.Create";
        public const string UserTests_Edit = "Permissions.UserTests.Edit";
        public const string UserTests_ViewUsers = "Permissions.UserTests.ViewUsers";
        public const string UserTests_ViewDetails = "Permissions.UserTests.ViewDetails";

        // AppFiles
        public const string AppFiles_Download = "Permissions.AppFiles.Download";
        public const string AppFiles_Upload = "Permissions.AppFiles.Upload";
    }
}
