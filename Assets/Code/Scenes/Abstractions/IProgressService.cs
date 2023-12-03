namespace CrazyWizardNotes.Scenes.Abstractions
{
    /// <summary>
    /// Сервис, отвечающий за прогрессией игрока. Сохраняет и загружает его прогресс, а также предоставляет информацию о следующих уровнях.
    /// </summary>
    public interface IProgressService
    {
        public string GetNextSceneName(string currentSceneName);
        public void MarkSceneCompleted(string currentSceneName);
    }
}
