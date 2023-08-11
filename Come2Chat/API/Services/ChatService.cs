using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace API.Services
{
    public class ChatService
    {

        //key, value eg: {{"john", "asdsh!@1231"}, {"david", "asakg23121"}
        private static readonly Dictionary<string, string> Users = new Dictionary<string, string>();

        public bool AddUserToList(string userToAdd)
        {
            lock (Users)
            {
                foreach (var user in Users)
                {
                   
                    // JOhn john
                    if (user.Key.ToLower() == userToAdd.ToLower())
                    {
                        return false;
                    }
                }
                Users.Add(userToAdd, null);
                return true;
            }
        }

        public void AddUserConnectionId(string user,string connectionId)
        {
            lock (Users)
            {
                if(Users.ContainsKey(user))
                {
                    Users[user] = connectionId;
                }
            }
        }
        public string GetUserByConnectionId(string connectionId)
        {
            lock(Users)
            {
                return Users.Where(x=>x.Value==connectionId).Select(x=>x.Key).FirstOrDefault();
            }
        }

        public string GetConnectionIdByUser(string user)
        {
            lock (Users)
            {
                return Users.Where(x => x.Key == user).Select(x => x.Key).FirstOrDefault();
            }
        }
        public void RemoveUserFromList(string user)
        {
            lock (Users)
            {
                if (!Users.ContainsKey(user))
                {
                    Users.Remove(user);
                }
            }
        }

        public string[] GetOnlineUsers()
        {
            lock(Users)
            {
                return Users.OrderBy(x => x.Key).Select(x => x.Key).ToArray(); 
            }
        }
    }
}
