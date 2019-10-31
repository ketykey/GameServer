using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Modeling
{
    public class Room
    {
        //存储当前房间里的玩家名字
        string[] userList;
        //检测当前
        bool[] user;
        int[] color;
        bool onGame;
        int RoomNumber;
        public Room(int number){
            for (int i = 0; i < 2; i++) {
                userList[i] = "";
                user[i] = false;
            }
            RoomNumber = number;
            onGame = false;
        }
        public bool Join(string username)
        {
            for(int i = 0; i < 2; i++)
            {
                if (user[i] == false)
                {
                    userList[i] = username;
                    user[i] = true;
                    return true;
                }
                else { }
            }
            return false;
        }

        public bool Leave(string username) {
            for (int i = 0; i < 2; i++)
            {
                if (userList[i].Equals(username) && user[i] == true)
                {
                    userList[i] = "";
                    user[i] = false;
                    return true;
                }
                else { }
            }
            return false;
        }
        public void CheckStart()
        {
            if (user[0] && user[1])
            {
                onGame = true;
            }
            else
            {
                onGame = false;
            }
        }

        public void Start()
        {
            if (onGame == true)
            {
                Random ra = new Random();
                int random = ra.Next(1, 2);
                if (random == 1)
                {
                    color[0] = -1;
                    color[1] = 1;
                }
                else
                {
                    color[0] = 1;
                    color[1] = -1;
                }
                
            }
        }
    }
}
