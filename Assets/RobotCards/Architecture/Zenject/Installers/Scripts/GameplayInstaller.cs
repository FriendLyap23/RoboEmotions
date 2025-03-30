using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Cards>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ShowCards>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Emotion>().FromComponentInHierarchy().AsSingle();

        Container.Bind<CardUI>().FromComponentInHierarchy().AsSingle(); 
    }
}
