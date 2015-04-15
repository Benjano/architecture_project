using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coupons.Enums;

namespace Coupons.Models
{
    public class ClientNetwork
    {
        private int mClientId;
        public int ClientId
        {
            get { return mClientId; }
        }

        private SocialNetwork mName;
        public SocialNetwork Name
        {
            get { return mName; }
        }
        private string mUsername;

        public string Username
        {
            get { return mUsername; }
        }   
        
        private string mToken;
        public string Token
        {
            get { return mToken; }
        }

        public ClientNetwork(int clientId, SocialNetwork name, string username, string token)
        {
            mClientId = clientId;
            mName = name;
            mUsername = username;
            mToken = token;
        }
    }
}
