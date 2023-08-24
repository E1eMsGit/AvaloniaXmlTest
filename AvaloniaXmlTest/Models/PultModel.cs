using AvaloniaXmlTest.Domains;
using System.Collections.Generic;

namespace AvaloniaXmlTest.Models
{
    public class PultModel
    {
        public List<RelayModel> RelaysX { get; }
        public List<RelayModel> RelaysY { get; }
        public List<RelayModel> RelaysK { get; }
        public RelayModel RelayOnACC { get; }
        public RelayModel RelayOffACC { get; }
        public RelayModel RelayCK { get; }
        public RelayModel RelayKB { get; }
        public RelayModel RelayKBGen { get; }
        public RelayModel RelayK3 { get; }
        public RelayModel RelayK3Gen { get; }
        public List<RelayModel> RelaysA { get; }
        public List<ModuleConnectionSettings> RKs { get; set; }
        public List<ModuleConnectionSettings> PKUs { get; set; }

        public PultModel(int step, List<RelayModel> relays)
        {
            RelaysX = new List<RelayModel>();
            RelaysY = new List<RelayModel>();
            RelaysK = new List<RelayModel>();
            RelaysA = new List<RelayModel>();

            for (int i = 0; i < 10; i++)
            {
                RelaysX.Add(relays[i + step]);
            }

            for (int i = 10; i < 22; i++)
            {
                RelaysY.Add(relays[i + step]);
            }

            RelayCK = relays[22 + step];

            for (int i = 23; i < 33; i++)
            {
                RelaysK.Add(relays[i + step]);
            }

            RelayKB = relays[33 + step];
            RelayKBGen = relays[34 + step];

            RelayOnACC = relays[35 + step];
            RelayOffACC = relays[36 + step];

            RelayK3 = relays[37 + step];
            RelayK3Gen = relays[38 + step];

            for (int i = 39; i < 44; i++)
            {
                RelaysA.Add(relays[i + step]);
            }
        }
    }
}
