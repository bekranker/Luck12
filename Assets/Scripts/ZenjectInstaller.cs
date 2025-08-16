using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    [Header("---Components")]
    [SerializeField] private PieceHandler _pieceHandler;
    [SerializeField] private PieceVisualizer _pieceVisulazier;
    [SerializeField] private InteractionHandler2D _interactionHandler;
    [SerializeField] private DiceHandler _diceHandler;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<PieceHandler>().FromInstance(_pieceHandler).AsCached();
        Container.BindInterfacesTo<DiceHandler>().FromInstance(_diceHandler).AsCached();
        Container.Bind<PieceVisualizer>().FromInstance(_pieceVisulazier).AsCached();
        Container.BindInterfacesTo<InteractionHandler2D>().FromInstance(_interactionHandler).AsCached();
    }
}