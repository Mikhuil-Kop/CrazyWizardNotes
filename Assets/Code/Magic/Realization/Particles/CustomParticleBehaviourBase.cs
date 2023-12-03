namespace CrazyWizardNotes.Magic.Realization
{
    public abstract class CustomParticleBehaviourBase
    {
        public virtual void OnBendStart(ICustomParticleBehaviorContext context) { }
        public virtual void OnBendUpdate(ICustomParticleBehaviorContext context) { }
        public virtual void OnBendEnd(ICustomParticleBehaviorContext context) { }

        public virtual void OnUpdate(ICustomParticleBehaviorContext context) { }
        public virtual void OnFixedUpdate(ICustomParticleBehaviorContext context) { }
    }
}
