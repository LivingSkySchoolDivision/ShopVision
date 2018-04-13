using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices.AccountManagement;

namespace ShopVision
{
    public static class Authentication
    {
        public static bool ValidateADCredentials(string domain, string username, string password)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                return pc.ValidateCredentials(username, password);
            }
        }

        public static List<string> GetADGroupMembers(string domain, string groupName)
        {
            List<string> returnMe = new List<string>();

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                using (GroupPrincipal grp = GroupPrincipal.FindByIdentity(pc, IdentityType.Name, groupName))
                {
                    if (grp != null)
                    {
                        foreach (Principal p in grp.GetMembers(true))
                        {
                            returnMe.Add(p.SamAccountName.ToLower());
                        }
                    }
                }
            }
            return returnMe;
        }

        public static string GetSessionIDFromCookies(string cookieName, HttpRequest request)
        {
            HttpCookie sessionCookie = request.Cookies[cookieName];
            if (sessionCookie != null)
            {
                return sessionCookie.Value;
            }
            else
            {
                return String.Empty;
            }
        }

        public static string ParseUsername(string givenUsername)
        {
            return givenUsername.ToLower().Replace("@lskysd.ca", "").Replace(@"lskysd\", "");
        }

        private static List<string> GetUsersGroups(string domain, string username)
        {
            List<string> returnMe = new List<string>();

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                using (UserPrincipal user = UserPrincipal.FindByIdentity(pc, username))
                {
                    if (user != null)
                    {
                        PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

                        foreach (Principal p in groups)
                        {
                            if (p is GroupPrincipal)
                            {
                                returnMe.Add(p.SamAccountName.ToLower());
                            }
                        }
                    }
                }
            }

            return returnMe;
        }

        public static UserPermissionResponse GetUserPermissions(string domain, string username)
        {
            bool canLogin = false;
            bool isAdmin = false;

            List<string> usersGroupMembership = GetUsersGroups(domain, username);

            // Check if this user is in an allowed AD group
            foreach (string group in Settings.Groups_AllowedAccess)
            {
                if (usersGroupMembership.Contains(group))
                {
                    canLogin = true;
                }
            }

            // Check if this user is in an administrator AD group
            foreach (string group in Settings.Groups_AdminAccess)
            {
                if (usersGroupMembership.Contains(group))
                {
                    canLogin = true;
                    isAdmin = true;
                }
            }
            
            return new UserPermissionResponse()
            {
                CanUserUseSystem = canLogin,
                IsAdministrator = isAdmin
            };
        }
    }
}