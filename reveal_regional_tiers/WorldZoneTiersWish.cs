//credit to egocarib/syntaxaire. XRL.Core.XRLCore.Log prints to player.log.
 [XRL.Wish.HasWishCommand]
    class WorldZoneTierWish
    {
        [XRL.Wish.WishCommand(Command = "dumpzonetiers")]
        public static void DumpZoneTiers()
        {
            string tierMap = "\n";
            for (int j = 0; j < 25; j++)
            {
                for (int i = 0; i < 80; i++)
                {
                    int zoneTier = XRL.World.ZoneManager.instance.GetZoneTier("JoppaWorld", i, j, 10);
                    tierMap += $"{zoneTier}";
                }
                tierMap += "\n";
            }
            XRL.Core.XRLCore.Log($"Zone Tier Map:\n{tierMap}\n");
        }
    }