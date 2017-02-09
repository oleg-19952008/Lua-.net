using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace Lua_net_ex.Core.Net.Tcp
{
    class socket
    {
        public byte[] buffer = new byte[1024];
        public static Socket socket_ = null;
        public socket(Socket sock)
        {
            socket_ = sock;
        }
        public static  ManualResetEvent alldone = new ManualResetEvent(false);
        public void a()
        {
            while (true)
            {
            }
            }
        public   void start(string ip,int port)
        {
            //var s = new Socket(socket_);
    
            var LEP = new IPEndPoint(IPAddress.Parse(ip), port);
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(LEP); listener.Listen(100);
                while (true)
                {
                 
         
              alldone.Reset();
                    listener.BeginAccept( socket.Accept_call_back, listener);
                alldone.WaitOne();
                }
            }
            catch
            {

            }
            
        }
        public static  void Accept_call_back(IAsyncResult ar)
        {
     
              alldone.Set();
                var listener = (Socket)ar.AsyncState;
                var handler = listener.EndAccept(ar);
                new Core.Net.Tcp.Tcp_(handler);
         
        }
    }

    public class Tcp_
    {
        public byte[] buffer = new byte[1024];

        private Socket Socket { get; set; }
        public Tcp_(Socket hnd)
        {
            Socket = hnd;
            hnd.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(Read_call_back), this);
        }
        public void Console(string a)
        {
            System.Console.WriteLine("PACKET - " + a);
        }
        public string SetFilter(string old_char, string new_char, string packet)
        {
       
            var s = new Lua_net_ex.Core_.Net.Tcp.ParserPacket.PacketReader(packet).ReadString().Replace(old_char, new_char);
            System.Console.WriteLine(s+"as");        return s;
        }
        public void Read_call_back(IAsyncResult ar)
        {

            try
            {

                var rec_bytes = Socket.EndReceive(ar);

                var current_packet = Encoding.UTF8.GetString(buffer, 0, rec_bytes);
        
                System.Console.WriteLine(current_packet);


                var pck_parser =           SetFilter("\n\0","", current_packet);
              
                Console("rec"+pck_parser);
                if (pck_parser.StartsWith( "<policy-file-request/>") /*|| pck_parser.StartsWith("<policy-file-request/>"*/)
                {
                    Console(pck_parser);
                    Send("<?xml version=\"1.0\"?><cross-domain-policy xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"http://www.adobe.com/xml/schemas/PolicyFileSocket.xsd\"><allow-access-from domain=\"*\" to-ports=\"*\" secure=\"false\" /><site-control permitted-cross-domain-policies=\"master-only\" /></cross-domain-policy>");
                  //  Socket.Shutdown(SocketShutdown.Both);
                  //  Socket.Close();
                }
                else
                {
                    var pck = pck_parser;
                    if (pck.StartsWith("LOGIN"))//RDY|I|playerID|username|shipID|maxSpeed|shield|maxShield|health|maxHealth|cargo|maxCargo|user.x|user.y|mapId|factionId|clanId|shipAmmo|shipRockets|expansion|premium|exp|honor|level|credits|uridium|jackpot|rank|clanTag|ggates|0|cloaked
                    {
                        this.Send("0|A|SET|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|0|0|1|1|0|0|1|1|1|1");
                        this.Send("0|7|MINIMAP_SCALE|0|11");

                        this.Send("0|7|DISPLAY_PLAYER_NAMES|1");
                        this.Send("0|7|DISPLAY_CHAT|1");
                        this.Send("0|7|PLAY_MUSIC|0");
                        this.Send("0|7|PLAY_SFX|1");
                        this.Send("0|7|BAR_STATUS|23,0,24,0,25,1,26,0,27,0");
                        this.Send("0|7|WINDOW_SETTINGS,0|0,9,4,1,1,232,3,1,3,780,388,1,5,5,5,0,10,5,288,0,13,187,50,0,20,5,402,1,22,347,188,0,23,458,1,1,24,284,25,0");
                        this.Send("0|7|AUTO_REFINEMENT|1");
                        this.Send("0|7|QUICKSLOT_STOP_ATTACK|1");
                        this.Send("0|7|DOUBLECLICK_ATTACK|1");
                        this.Send("0|7|AUTO_START|1");
                        this.Send("0|7|DISPLAY_NOTIFICATIONS|1");
                        this.Send("0|7|SHOW_DRONES|1");
                        this.Send("0|7|DISPLAY_WINDOW_BACKGROUND|1");
                        this.Send("0|7|ALWAYS_DRAGGABLE_WINDOWS|1");
                        this.Send("0|7|PRELOAD_USER_SHIPS|1");
                        this.Send("0|7|QUALITY_PRESETTING|1");
                        this.Send("0|7|QUALITY_CUSTOMIZED|1");
                        this.Send("0|7|QUALITY_BACKGROUND|1");
                        this.Send("0|7|QUALITY_POIZONE|1");
                        this.Send("0|7|QUALITY_SHIP|1");
                        this.Send("0|7|QUALITY_ENGINE|1");
                        this.Send("0|7|QUALITY_COLLECTABLE|1");
                        this.Send("0|7|QUALITY_ATTACK|1");
                        this.Send("0|7|QUALITY_EFFECT|1");
                        this.Send("0|7|QUALITY_EXPLOSION|1");
                        this.Send("0|7|QUICKBAR_SLOT|-1,-1,-1,-1,-1,-1,-1,-1,-1,-1");
                        this.Send("0|7|SLOTMENU_POSITION|313,451");
                        this.Send("0|7|SLOTMENU_ORDER|0");
                        Send("RDY|I|12972|SeVeN|67|356|1|2|1|2|1|2|1|1|16|1|0|1|1|51200|1|51200|51200|21|777|888|0|21|0|4|0|0");


                        this.Send("0|B|50|50|50|123|5000|50"); // LCB-10(x1)|MCB-25(x2)|MCB-50(x3)|UCB-100(x4)|SAB-50(roba escudo)|RSB-75(x5)
                        this.Send("0|7|HS");
                        this.Send("0|S|CFG|1");

                        this.Send("0|A|ITM|0|0|0|0|4|1|0|0|0|0|0|0|0|0|0|0|0");
                        this.Send("0|g|a|b,1000,1,10000,C,2,500,U,3,1000,U,5,1000,U|r,100,1,10000,C,2,50000,C,3,500,U,4,700,U"); // Store
                        this.Send("0|TX|S|1|15|0|1|15|0|1|15|0|1|15|0|1|15|0");
                        this.Send("0|ps|blk|0");

                        this.Send("0|m|1|1|1");
                        this.Send("0|POI|RDY");
                        this.Send("0|n|d|12972|3/4-25-25-25-25,3/4-25-25-25-25,3/4-25-25-25-25,3/4-25-25-25-25");
                        this.Send("0|A|STD|*");
                    }

                }
            }
            catch
            {

            }
        }
        public void Send(string data)
        {
            Console(" SENT -" + data);
            try
            {
                if (this.Socket != null && this.Socket.Connected)
                {
                    //  Out.WriteLine("To client: " + data, "Packets", ConsoleColor.DarkGreen);
                    byte[] byteData = Encoding.UTF8.GetBytes(data + (char)0x00);
                    this.Socket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(this.SendCallback), Socket);
                }
            }
            catch (SocketException e)
            {


            }
        }
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception e)
            {
                //.WriteLine(e.Message, "Exception", ConsoleColor.Red);
            }
        }
    }
}