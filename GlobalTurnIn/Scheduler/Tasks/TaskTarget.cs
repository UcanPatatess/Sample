using Dalamud.Game.ClientState.Objects.Types;
using Lumina.Excel.Sheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTurnIn.Scheduler.Tasks
{
    internal static class TaskTarget
    {
        public static void Enqueue(uint objectID)
        {
            IGameObject? gameObject = null;
            P.taskManager.Enqueue(() => Util.TryGetObjectByObjectId(objectID, out gameObject));
            P.taskManager.Enqueue(() => Util.TargetByID(gameObject));
        }
    }
}