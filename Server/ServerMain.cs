using System;
using CitizenFX.Core;

namespace Character_Creator_test.Server
{
    public class ServerMain : BaseScript
    {
        public ServerMain()
        {
            Debug.WriteLine("Hi from Character_Creator_test.Server!");
        }

        [EventHandler("char-creator-test:start-test")]
        public void StartCreation([FromSource]Player player)
        {
            Debug.WriteLine("Starting character creator!");
            /*
             * Exports["character-creator"].startCreation(player, genderId);
             * player: player whose character you want to edit
             * genderId: 0 = male, 1 = female, -1 = let player choose
             */
            Exports["character-creator"].startCreation(player.Name, -1);
        }
    }
}