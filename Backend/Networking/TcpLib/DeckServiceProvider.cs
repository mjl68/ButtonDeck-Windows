﻿using NickAc.Backend.Networking.Implementation;
using NickAc.Backend.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NickAc.Backend.Networking.TcpLib
{
    public class DeckServiceProvider : TcpServiceProvider
    {

        private static List<INetworkPacket> networkPackets = new List<INetworkPacket>();


        static DeckServiceProvider()
        {
            RegisterNetworkPacket(new HelloPacket());
            RegisterNetworkPacket(new DeviceIdentityPacket());
        }

        public static void RegisterNetworkPacket(INetworkPacket packet)
        {
            networkPackets.Add(packet);
        }

        public static INetworkPacket GetNewNetworkPacketById(long id)
        {
            try {
                return (INetworkPacket)networkPackets.FirstOrDefault(p => p.GetPacketNumber() == id).Clone();
            } catch (Exception) {
                throw new Exception($"NetworkPacket[ID: {id}] wasn't registered to the packet storage.");
            }
        }

        public override void OnAcceptConnection(ConnectionState state)
        {
        }

        public override void OnDropConnection(ConnectionState state)
        {
            DevicePersistManager.RemoveConnectionState(state);
        }

        public override void OnReceiveData(ConnectionState state)
        {
            int countToWait = 0;
            int countToFinal = 1000;
            List<byte> allData = new List<byte>();
            byte[] buffer;
            System.Diagnostics.Debug.WriteLine("AvailiableData: " + state.AvailableData);
            while (++countToWait < countToFinal) {
                continue;
            }
            System.Diagnostics.Debug.WriteLine("Waited - AvailiableData: " + state.AvailableData);
            while (state.AvailableData > 0) {
                buffer = new byte[1024];
                state.Read(buffer, 0, 1024);
                allData.AddRange(buffer);
            }
            var packet = state.ReadPacket(allData.ToArray());

        }
        public override object Clone()
        {
            return new DeckServiceProvider();
        }

    }
}