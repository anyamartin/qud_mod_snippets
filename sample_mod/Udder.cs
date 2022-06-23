using System;
using System.Collections.Generic;
using System.Text;

using XRL.Rules;
using XRL.Messages;
using ConsoleLib.Console;

namespace XRL.World.Parts.Mutation
{
    [Serializable]
    class FreeholdTutorial_Udder : BaseMutation
    {
        public override void Register(GameObject Object)
        {
        }

        public override string GetDescription()
        {
            return "description";
        }

        public override string GetLevelText(int Level)
        {
            string Ret = "You have udderrrs.\n";
            return Ret;
        }

        public override bool WantEvent(int ID, int cascade)
        {
            return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
        }

        public override bool HandleEvent(BeforeRenderEvent e)
        {
            if (ParentObject.IsPlayer())
            {
                if (ParentObject.pPhysics != null && ParentObject.pPhysics.CurrentCell != null)
                {
                    ParentObject.pPhysics.CurrentCell.ParentZone.AddLight(ParentObject.pPhysics.CurrentCell.X, ParentObject.pPhysics.CurrentCell.Y, Level, LightLevel.Darkvision);
                }
            }
            return true;
        }

        public override bool FireEvent(Event E)
        {
            return base.FireEvent(E);
        }

        public override bool ChangeLevel(int NewLevel)
        {
            return true;
        }

        public override bool Mutate(GameObject GO, int Level)
        {
            return true;
        }

        public override bool Unmutate(GameObject GO)
        {
            return true;
        }
    }
}