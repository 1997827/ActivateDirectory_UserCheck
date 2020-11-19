using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;

namespace ValidationUserCredentials
{
    public class Class1
    {
        public static string domain = null;
        public static DirectorySearcher ds = null;
        public static DirectorySearcher dirSearch = null;
        public static SearchResult rs = null;


        public static bool Validation(string a, string b)
        {
            domain = Domain.GetComputerDomain().ToString().ToLower();
            rs = SearchUserByUserName(GetDirectorySearcher(domain, a, b), a);
            if (rs != null)
            {
                //  Console.WriteLine("User Credentials are Correct");
                return true;
            }
            else
            {
                //   Console.WriteLine("User Credentials are Incorrect");
                return false;
            }
        }

        public static SearchResult SearchUserByUserName(DirectorySearcher ds, string username)
        {
            ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + username + "))";

            ds.SearchScope = SearchScope.Subtree;
            ds.ServerTimeLimit = TimeSpan.FromSeconds(90);
            try
            {
                SearchResult userObject = ds.FindOne();
                if (userObject != null)
                    return userObject;
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }


        }
        public static DirectorySearcher GetDirectorySearcher(string domain, string username, string password)
        {
            if (dirSearch == null)
            {
                try
                {
                    dirSearch = new DirectorySearcher(new DirectoryEntry("LDAP://" + domain, username, password));
                }
                catch (DirectoryServicesCOMException e)
                {

                    e.Message.ToString();
                }
                return dirSearch;
            }
            else
            {
                return dirSearch;
            }
        }
        //Connecting to domain


    }
}

