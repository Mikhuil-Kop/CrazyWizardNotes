namespace CrazyWizardNotes.Magic.Realization
{
    public abstract class CustomParticleBehaviourBase
    {
        public virtual void OnBendStart(CustomParticleStateContext context) { }
        public virtual void OnBendUpdate(CustomParticleStateContext context) { }
        public virtual void OnBendEnd(CustomParticleStateContext context) { }

        public virtual void OnReleaseByAny(CustomParticleStateContext context) { }
        public virtual void OnReleaseByThrow(CustomParticleStateContext context) { }

        public virtual void OnStartByAny(CustomParticleStateContext context) { }
        public virtual void OnStartBySound(CustomParticleStateContext context) { }
        public virtual void OnStartByMerge(CustomParticleStateContext context) { }

        public virtual void OnUpdate(CustomParticleStateContext context) { }

        public virtual void OnEndByAny(CustomParticleStateContext context) { }
        public virtual void OnEndByMerge(CustomParticleStateContext context) { }
    }
}
