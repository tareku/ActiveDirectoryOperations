using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveDirectoryOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            UserAccountByUsername("username");
            UsersAccounts();
            GroupsAccounts
        }

        private static void UserAccountByUsername(string username)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, "mydomain.com");
                UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                PrincipalSearchResult<Principal> userGroups = userPrincipal.GetGroups();

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine($"User account by username {username}, run for {elapsedTime}");
            }
            catch (InvalidEnumArgumentException) { throw; }
            catch (ArgumentException) { throw; }
            catch (MultipleMatchesException) { throw; }
        }

        private static void UsersAccounts()
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, "mydomain.com");
                PrincipalSearcher principalSearcher = new PrincipalSearcher(new UserPrincipal(principalContext));
                PrincipalSearchResult<Principal> users = principalSearcher.FindAll();

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine($"{users.Count()} users accounts, run for {elapsedTime}");
            }
            catch (ArgumentException) { throw; }
            catch (InvalidOperationException) { throw; }
        }

        private static void GroupsAccounts()
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, "mydomain.com");
                PrincipalSearcher principalSearcher = new PrincipalSearcher(new GroupPrincipal(principalContext));
                PrincipalSearchResult<Principal> groups = principalSearcher.FindAll();

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine($"{groups.Count()} groups, run for {elapsedTime}");
            }
            catch (ArgumentException) { throw; }
            catch (InvalidOperationException) { throw; }
        }
    }
}
