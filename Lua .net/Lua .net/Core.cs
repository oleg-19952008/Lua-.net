using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows; 
using System.IO;
using System.Runtime.InteropServices;
using NLua;
using lib;
using System.Diagnostics;
namespace Lua_net_ex_
{
    class Core
    {
        /*
        http://nlua.org/
        https://github.com/NLua/NLua
        https://habrahabr.ru/post/197262/
        */
        public static Socket socket_ = null;
        public void MEmory()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.Title = new lib.core.title.title().title_mem_string_64();
                System.Threading.Thread.Sleep(1000);
            }
        }
        public string GetFreeMemory()
        {
            return new lib.core.title.title().title_mem_string_64();
        }
        public void Memory()
        {
            /*
             * For not block threads this block code
             */
            new System.Threading.Thread(MEmory).Start();
        }
        static void Main(string[] args)
        {
            //new Lua_net_ex.Core.Net.Tcp.socket(socket_).start("127.0.0.1", 8080);
            //      new System.Threading.Thread(new Core().memory).Start();

            var lua = new Lua();
            lua["Core"] = new Lua_net_ex_.Core();
            //lua["C"] = new Core();
            //lua["Core_net_tcp"] = new Lua_net_ex.Core.Net.Tcp.socket(socket_);
            if (args.Length != 0)
            {
                lua.DoFile(args[0]);
            }
            else
            {
                new Core().echo("Lua interpeter v0.1.2 based on core Lua 5.2 and used NLua lib\n");
                lua.DoFile("init.lua");
                Console.Write(">");
            }

            try
            {
                Console.Write(">");
                var command = Console.ReadLine();
                if(command.Split(' ')[0]== "exec")
                {
                    new Core().exec(command.Split(' ')[1]);
                }
                lua.DoString(command);
                string[]  sp = { "", " " };
                //  lua.DoFile( "init.lua");    
                Main(sp);
            }
            catch (NLua.Exceptions.LuaException e)
            {
                new Core().Excteption(e.Message.ToString());
                //Console.WriteLine("init.lua not found\nCreate file 'init.lua and add code in file.'\nFor exit press any key");
                Console.Read();
            }
        }

        // class io.*
        public string FileReadToString(object adr)
        {
            return System.IO.File.ReadAllText(adr.ToString());
        }
        public string[] FileReadToStringArray(object adr)
        {
            return System.IO.File.ReadAllLines(adr.ToString());
        }
        public byte[] FileReadByte(string adr)
        {
            return System.IO.File.ReadAllBytes(adr.ToString());
        }
        public string DecodeFileFromBase64(object s)
        {
            echo("Decoding file " + s.ToString());
            var dec = Encoding.UTF8.GetString(Convert.FromBase64String(FileReadToString(s)));
            File.WriteAllText(Environment.CurrentDirectory + "/Decoded_" + s.ToString(), dec);
            return Encoding.UTF8.GetString(Convert.FromBase64String(FileReadToString(s)));
        }
        public string EncodeFileToBase64(object s)
        {
            echo("Encoding file " + s.ToString());
            var enc = Convert.ToBase64String(Encoding.UTF8.GetBytes(FileReadToString(s)));
            File.WriteAllText(Environment.CurrentDirectory + "/Encoded_" + s.ToString(), enc);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(FileReadToString(s)));
        }
        public void LoadNetDll(string a, string namespace_)
        {
            echo("Not coded it`s ");
            //    if (namespace_ == null)
            //    {
            //        namespace_ = "";
            //        var lua = new Lua();
            //        lua.LoadCLRPackage();
            //        lua.DoString("import('" + a + "');");
            //        echo("Loaded CLR Dll " + a);
            //    }
            //    else
            //    {
            //        var lua = new Lua();

            //        lua.LoadCLRPackage();
            //        lua.DoString("import('" + a + "','"+namespace_+"');");
            //        lua["a"] = new object();
            //        echo("Loaded CLR Dll " + a +" and loaded namespace "+namespace_);
            //    }
        }
        public void exec(object a)
        {
            var lua = new Lua();
            lua["Core"] = new Lua_net_ex_.Core();
            echo("Execute script " + a.ToString());
           lua.DoFile(a.ToString());
        }
        public void run(string a)
        {
            Process.Start(a);
        }
        public void run_arg(string a, string arg)
        {
            Process.Start(a, arg);
        }
        public void echo(object a)
        {
            Console.WriteLine(a);
        }
        public void MessageBox(string a)
        {
            System.Windows.Forms.MessageBox.Show(a);
        }
        public void pause_()
        {
            Console.Read();
        }
        public void ForegroundColor(int a)
        {
            /* Foreground */
            if (a == 1)
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            if (a == 2)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            if (a ==3)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            if (a == 4)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            if (a == 5)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
            if (a == 6)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            if (a == 7)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            if (a == 8)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }
            if (a == 9)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            if (a == 10)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            if (a ==11)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            if (a == 12)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            if (a == 13)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            if (a == 14)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (a == 15)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (a == 16)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }           

        }
        public string ReadLine()
        {
           return Console.ReadLine();
        }
        public void Excteption(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Exception at " + s);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void import(string a)
        {
            // var la = new Lua;
            new Lua().DoString("require('" + a + "');"); System.Threading.Thread.Sleep(10);
        }
        public void ping(string ip)
        {
            while (true)
            {
                var s = new System.Net.NetworkInformation.Ping().Send(ip);
                echo("Received bytes from " + ip + " time " + s.RoundtripTime.ToString() + " ms");
            }
        }
        public string IP = "";
        public int thread_ = 0;
        public int BUFFER = 32;
        public void ddos(int th, string ip, int buffer)
        {
            IP = ip;
            BUFFER = buffer;
            for (var a = 0; a < th; a++)
            {
                thread_ = a;
                new System.Threading.Thread(texaDC).Start();
                //  new Task(texaDC).Start();
            }
        }
        public int count_bytes = 0;
        public void texaDC()
        {
            var t = thread_;
            while (true)
            {
                //   new System.Net.NetworkInformation.Ping().SendAsync(IP, 101, new byte[BUFFER]);
                var s = new System.Net.NetworkInformation.Ping().Send(IP, 1, new byte[BUFFER]);
                count_bytes = (count_bytes + BUFFER);
                // count_bytes = (count_bytes *2) / 3072;
                echo("[TH #" + t + " ]Received bytes from " + IP + " time " + s.RoundtripTime.ToString() + " ms || All KB sent - " + count_bytes / 1024);

            }
        }
        public int ToInt(object s)
        {
            var ss = (int)0;
            try
            {
                ss = Convert.ToInt32(s);
            }
            catch (Exception e)
            {
                echo(e.ToString());
            }
            return ss;
        }
        public string EncodeBase64(object a)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(a.ToString()));
        }
        public string DecodeBase64(object a)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(a.ToString()));
        }
    }
}