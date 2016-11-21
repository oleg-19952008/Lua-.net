using System;
using System.Linq;
namespace Lua_net_ex.Core_.Net.Tcp.ParserPacket
{
    public class Packet_parser  
    {
        private string[] pck_data;
        private int count = -1;

        public Packet_parser(string packet)
        {
            if (packet.Contains("|"))
            {
                pck_data = packet.Split('|');
            }
            else
            {
                pck_data = packet.Split(' ');
            }
        }

        public int ReadInt()
        {
            count++;
            return Convert.ToInt32(pck_data[count]);
        }

        public short ReadShort()
        {
            count++;
            return Convert.ToInt16(pck_data[count]);
        }

        public uint ReadUInt32()
        {
            count++;
            return Convert.ToUInt32(pck_data[count]);
        }

        public ulong ReadULong()
        {
            count++;
            return Convert.ToUInt64(pck_data[count]);
        }

        public ushort ReadUShort()
        {
            count++;
            return Convert.ToUInt16(pck_data[count]);
        }

        public string getString()
        {
            count++;
            return pck_data[count];
        }

        //public bool getBool()
        //{
        //    count++;
        //    return Program.ToBool(pck_data[count]);
        //}

        public bool MoreToRead
        {
            get { return (pck_data.Count() > (count + 1)) ? true : false; }
        }

        //public void Dispose()
        //{
        //    GC.SuppressFinalize(this);
        //}
    }
}
namespace Lua_net_ex.Core_.Net.Tcp.ParserPacket
{
    //    [System.Diagnostics.DebuggerDisplay("Pos: {position} / {_params.Length}\t {packet}")]
    public class PacketReader
    {
        //#if DEBUG
        //        string packet { get { return System.String.Join("|", this._params); } }
        //#endif
        readonly string[] _params;
        int position;

        public static implicit operator PacketReader(string pack)
        {
            return new PacketReader(pack);
        }

        public static explicit operator string(PacketReader us)
        {
            return System.String.Join("|", us._params);
        }

        public PacketReader(string[] _params)
        {
            this._params = _params;
            position = -1;
        }

        public PacketReader(string packet) : this(packet.Split('|')) { }

        public int ReadInt()
        {
            return int.Parse(_params[++position]);
        }

        public short ReadShort()
        {
            return short.Parse(_params[++position]);
        }

        public uint ReadUInt32()
        {
            return uint.Parse(_params[++position]);
        }

        public ulong ReadULong()
        {
            return ulong.Parse(_params[++position]);
        }

        public ushort ReadUShort()
        {
            return ushort.Parse(_params[++position]);
        }

        public string ReadString()
        {
            return _params[++position];
        }

        public string ReadAt(int index)
        {
            return _params[index];
        }

        public void Skip(int count)
        {
            this.position += count;
        }

        public bool ReadBoolean()
        {
            var nxt = _params[++position];
            return nxt.Equals("true") || nxt.Equals("1");
        }

        public void Back(int count)
        {
            position -= count;
        }

        public bool MoreToRead
        {
            get { return (_params.Length > (position + 1)); }
        }
    }
}
