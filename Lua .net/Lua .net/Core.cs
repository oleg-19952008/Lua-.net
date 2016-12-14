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
        public void memory()
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
                new Core().echo("Lua interpeter v0.1.1 based on core Lua 5.2 and used NLua lib\n");
                lua.DoFile("init.lua");
            }

            try
            {
                Console.Write(">");
                var command = Console.ReadLine();
                lua.DoString(command);

                //  lua.DoFile( "init.lua");    
                Console.Read();
            }
            catch (NLua.Exceptions.LuaException e)
            {
                new Core().Exceprion(e.Message.ToString());
                //Console.WriteLine("init.lua not found\nCreate file 'init.lua and add code in file.'\nFor exit press any key");
                Console.Read();
            }
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
            echo("Execute script " + a.ToString());
            new Lua().DoFile(a.ToString());
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
        public void BackgroundColor(int a)
        {
            // var s = Console.ForegroundColor= ConsoleColor
        }
        public void Exceprion(string s)
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