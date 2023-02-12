using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using prjTCP_ChatRoomServer;
using System.IO;
using Newtonsoft.Json;
using RVIServer.Models;

namespace RVIServer
{
    public partial class RVIServer : Form
    {
        TcpListener Server;             //伺服器網路監聽物件 (電話總機)
        Socket Client;                  //客戶連線物件       (電話分機)
        Thread Th_Svr;                  //伺服器監聽執行緒   (電話總機開放中))
        Thread Th_Clt;                  //客戶通話執行緒     (電話分機連線中)
        Thread Th_forCCTV;
        Hashtable myHashTable = new Hashtable(); //客戶名稱與通訊物件的集合(key:Name, Socket)
        static Queue<string> CCTVWorkQueue = new Queue<string>();
        static string PhotosPath;

        public RVIServer()
        {
            InitializeComponent();
            tb_IP.Text = MyIP();
            using (ReadINI oTINI = new ReadINI(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.ini")))
            {
                tb_Port.Text = oTINI.getKeyValue("ServerPort", "Value"); //Section name=Worklog；Key name=Value
                PhotosPath = oTINI.getKeyValue("PhotosPath", "Value");
            }
            ServerStart();
        }
        private string MyIP()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] IPs = Dns.GetHostEntry(hostName).AddressList;
            foreach (IPAddress item in IPs)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    return item.ToString();//如果是IPv4則回傳此字串
                }
            }
            return "";  //找不到合格回傳空字串
        }
        //建立線上名單
        private TCPClientData OnlineList()
        {
            TCPClientData UpdateUserList= new TCPClientData();
            string UserList = ""; //代表線上名單的命令碼
            for (int i = 0; i < lb_UserList.Items.Count; i++)
            {
                UserList += lb_UserList.Items[i]; //逐一將成員名單加入L字串
                if (i < lb_UserList.Items.Count - 1)
                {
                    UserList += ",";
                }
            }
            UpdateUserList.UserList = UserList;
            UpdateUserList.Sender = "Server";
            UpdateUserList.Target = "ALL";
            UpdateUserList.Command = "UpdateUserList";
            return UpdateUserList;
        }

        private void SendAll(TCPClientData CMDJSON)
        {
            string Msg_JSON = JsonConvert.SerializeObject(CMDJSON);
            byte[] B = Encoding.Default.GetBytes(Msg_JSON);
            foreach (Socket socket in myHashTable.Values)
            {
                socket.Send(B, 0, B.Length, SocketFlags.None);
            }
        }
        //開啟 Server：用 Server Thread 來監聽 Client
        private void btn_ServerStart_Click(object sender, EventArgs e)
        {
            ServerStart();
        }
        private void ServerStart()
        {
            //忽略跨執行緒處理的錯誤
            CheckForIllegalCrossThreadCalls = false;
            Th_Svr = new Thread(ServerSub);
            Th_Svr.IsBackground = true;
            Th_Svr.Start();
        }
        //接受客戶連線要求的程式(如同電話總機的角色)，針對每一客戶會建立一個連線，以及獨立執行緒
        private void ServerSub()
        {

            //建立CCTV拍照監聽執行序
            Th_forCCTV = new Thread(ContinueDoCCTVWork);    //建立監聽執行緒
            Th_forCCTV.IsBackground = true;     //設定為背景執行緒
            Th_forCCTV.Start();
            //建立CCTV拍照監聽執行序

            //Server IP 和 Port
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse(tb_IP.Text), int.Parse(tb_Port.Text));
            Server = new TcpListener(EP);       //建立伺服器端監聽器(總機)
            Server.Start(100);                  //啟動監聽設定允許最多連線數100人
            while (true)                        //無限迴圈監聽連線要求
            {
                Client = Server.AcceptSocket(); //建立此客戶的連線物件Client
                Th_Clt = new Thread(Listen);    //建立監聽這個客戶連線的獨立執行緒
                Th_Clt.IsBackground = true;
                Th_Clt.Start();
            }
        }

        //監聽客戶訊息的程式
        private void Listen()
        {
            Socket Sck = Client;    //複製Client通訊物件到個別客戶專用物件Sck
            Thread Th = Th_Clt;     //複製執行緒Th_Clt到區域變數Th
            while (true)            //持續監聽客戶傳來的訊息
            {
                //用Sck來接收此客戶訊息，inLen是接收訊息的byte數目
                try
                {
                    byte[] B = new byte[1023];  //建立接收資料用的陣列，長度需大於可能的訊息
                    int inLen = Sck.Receive(B); //接收網路資訊(byte陣列)
                    string Msg_JSON = Encoding.Default.GetString(B, 0, inLen);//翻譯實際訊息(長度inLen)
                    TCPClientData JsonData = JsonConvert.DeserializeObject<TCPClientData>(Msg_JSON);

                    string Cmd = JsonData.Command;    //取出命令碼(第一個|之前的字)

                    switch (Cmd)
                    {
                        case "NewUserLogin":               //有新使用者上線:新增使用者到名單中
                            if (!myHashTable.ContainsKey(JsonData.Sender))
                            {
                                myHashTable.Add(JsonData.Sender, Sck);    //連線加入雜湊表，Key:使用者，Valre:連線物件(Socket)
                                lb_UserList.Items.Add(JsonData.Sender);//加入上線者名單
                                SendAll(OnlineList());
                            }
                            break;
                        case "UserLogOut":
                            myHashTable.Remove(JsonData.Sender);    //有使用者離開:從名單中移除該使用者
                            lb_UserList.Items.Remove(JsonData.Sender);
                            Th.Abort();
                            SendAll(OnlineList());
                            break;
                        case "TakePicture":
                            tb_log.AppendText("(銷帳拍照)" + JsonData.DateAndTime + "_" + JsonData.CarId + "\r\n");      //顯示訊息並換行
                            CCTVWorkQueue.Enqueue(JsonData.DateAndTime + "_" + JsonData.CarId);
                            TCPClientData ReplyMsg = new TCPClientData
                            {
                                Command = "Reply",
                                Sender = "Server",
                                Message = "拍照完成"
                            };
                            Communicate.SendJSON(ReplyMsg);
                            break;
                        default:
                            break;
                    }
                }


                catch (Exception ex)
                {

                }
            }
        }
        private void ContinueDoCCTVWork()
        {
            CCTV.PhotosPath = PhotosPath;
            while (true)
            {
                if (CCTVWorkQueue.Count != 0)
                {
                    CCTV.TakePic(CCTVWorkQueue.Dequeue());
                    tb_log.AppendText(CCTV.errMessage);
                }
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            using (ReadINI oTINI = new ReadINI("./Config.ini"))
            {
                oTINI.setKeyValue("ServerPort", "Value", tb_Port.Text);
            }
            Application.ExitThread();
        }

        private void btn_Restart_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void btn_Kick_Click(object sender, EventArgs e)
        {
            //SendTo("9" + "|" + lb_UserList.SelectedItem.ToString(), lb_UserList.SelectedItem.ToString());
        }


        private void btn_KickAll_Click(object sender, EventArgs e)
        {
            ListBox tmp_lb = new ListBox();
            foreach (var item in lb_UserList.Items)
            {
                tmp_lb.Items.Add(item);
            }
            foreach (var item in tmp_lb.Items)
            {
                myHashTable.Remove(item);    //有使用者離開:從名單中移除該使用者
                lb_UserList.Items.Remove(item);
            }
            SendAll(OnlineList());
        }
    }
}
