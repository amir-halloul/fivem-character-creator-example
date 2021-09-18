using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Character_Creator_test.Client
{
    public class ClientMain : BaseScript
    {
        bool firstTick = true;

        public ClientMain()
        {
            Debug.WriteLine("Character Creator test started!");
        }

        [Tick]
        public async Task OnTick()
        {
            if (firstTick)
            {
                firstTick = false;
                API.ShutdownLoadingScreen();
                API.RequestModel((uint)PedHash.Michael);
                while (!API.HasModelLoaded((uint)PedHash.Michael))
                {
                    API.RequestModel((uint)PedHash.Michael);
                    await BaseScript.Delay(50);
                }

                API.SetPlayerModel(API.PlayerId(), (uint)PedHash.Michael);

                API.SetModelAsNoLongerNeeded((uint)PedHash.Michael);
                API.SetEntityCoordsNoOffset(Game.PlayerPed.Handle, 0, 0, 71, true, false, false);
                API.NetworkResurrectLocalPlayer(0, 0, 71, 0, true, true);
                API.ClearPedTasksImmediately(Game.PlayerPed.Handle);
                API.FreezeEntityPosition(Game.PlayerPed.Handle, false);
            }

            // MultiplayerInfo = Z (W on AZERTY keyboards)
            if (Game.IsControlJustReleased(0, Control.MultiplayerInfo))
            {
                TriggerServerEvent("char-creator-test:start-test");
            }
            await Task.FromResult(0);
        }
    }
}