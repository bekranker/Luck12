using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    [Header("---Components")]
    [SerializeField] private PieceHandler _pieceHandler;
    [SerializeField] private PieceVisualizer _pieceVisulazier;
    [SerializeField] private InteractionHandler2D _interactionHandler;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<PieceHandler>().FromInstance(_pieceHandler).AsCached();
        Container.Bind<PieceVisualizer>().FromInstance(_pieceVisulazier).AsCached();
        Container.Bind<InteractionHandler2D>().FromInstance(_interactionHandler).AsCached();
    }
}