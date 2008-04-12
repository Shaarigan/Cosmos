﻿using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Cosmos.Hardware.Extension.NumberSystem;

namespace Cosmos.Hardware.Network.TCPIPModel.PhysicalLayer.Ethernet2
{
    public class Ethernet2Frame
    {
        public Ethernet2Frame()
        {
        }

        private MACAddress xDestination = null;
        public MACAddress Destination
        {
            get { return xDestination; }
            set { xDestination = value; }
        }

        private MACAddress xSource = null;
        public MACAddress Source
        {
            get { return xSource; }
            set { xSource = value; }
        }

        public byte[] EtherType
        {
            get
            {
                byte[] type = new byte[2];
                type[0] = 0x08; //Hardcoded to IP
                type[1] = 0x00;
                return type;
            }
            private set { ;}
        }

        private byte[] xPayload;
        public byte[] Payload
        {
            get { return xPayload; }
            set { xPayload = value; }
        }

        public UInt32 CRC32 { get; set; }

        //public byte InterframeGap { get; set; }

        /// <summary>
        /// The entire frame as bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] RawBytes()
        {
            List<byte> xBytes = new List<byte>();

            //foreach (byte dest in this.Destination.bytes)
            //    xBytes.Add(dest);
            for (int i = 0; i < this.Destination.bytes.Length; i++)
                xBytes.Add(this.Destination.bytes[i]);

            //foreach (byte src in this.Source.bytes)
            //    xBytes.Add(src);
            for (int s = 0; s < this.Source.bytes.Length; s++)
                xBytes.Add(this.Source.bytes[s]);

            xBytes.Add(this.EtherType[0]);
            xBytes.Add(this.EtherType[1]);

            if (this.Payload != null)
            {
                //xBytes.AddRange(this.Payload);
                for (int i = 0; i < this.Payload.Length; i++)
                {
                    xBytes.Add(this.Payload[i]);
                }
            }

            return xBytes.ToArray();
        }

        public override string ToString()
        {
            //Outputs frame as Wireshark displays it

            StringBuilder sb = new StringBuilder();
            sb.Append("---- Ethernet II Frame ----");
            sb.Append(Environment.NewLine);
            sb.Append("Destination: " + this.Destination.ToString());
            sb.Append(Environment.NewLine);
            sb.Append("Source: " + this.Source.ToString());
            sb.Append(Environment.NewLine);
            sb.Append("Type: 0x" + this.EtherType[0].ToHex() + this.EtherType[1].ToHex());
            sb.Append(Environment.NewLine);
            sb.Append("Payload size: " + this.Payload.Length);
            sb.Append(Environment.NewLine);
            sb.Append("--------------------");
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }

        
    }
}
