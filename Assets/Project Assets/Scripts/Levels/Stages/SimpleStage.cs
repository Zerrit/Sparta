namespace Zparta.Levels.Stages
{
    public class SimpleStage : AbstractStage
    {
        public override void Activate()
        {
            base.Activate();
            
            finishBridge.HideBarrier();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            
            finishBridge.ShowBarrier();
        }
    }
}