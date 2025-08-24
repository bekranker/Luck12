using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    [Header("---Components")]
    [SerializeField] private PieceHandler _pieceHandler;
    [SerializeField] private PieceMoveHandler _pieceMoveHandler;
    [SerializeField] private PieceInteraction _pieceInteraction;
    [SerializeField] private PieceVisualizer _pieceVisualizer;
    [SerializeField] private MatchHandler _matchHandler;
    [SerializeField] private InteractionHandler2D _interactionHandler2D;
    [SerializeField] private DiceHandler _diceHandler;
    [SerializeField] private TurnHandler _turnHandler;
    [SerializeField] private ScoreHandler _scoreHandler;
    [SerializeField] private RuneHandler _runeHandler;
    [SerializeField] private RuneVisualizer _runeVisualizer;
    [SerializeField] private RuneMoveHandler _runeMoveHandler;
    [SerializeField] private RuneEffectHandler _runeEffectHandler;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PieceHandler>().FromInstance(_pieceHandler).AsCached();
        Container.BindInterfacesAndSelfTo<PieceVisualizer>().FromInstance(_pieceVisualizer).AsCached();
        Container.BindInterfacesAndSelfTo<DiceHandler>().FromInstance(_diceHandler).AsCached();
        Container.BindInterfacesAndSelfTo<InteractionHandler2D>().FromInstance(_interactionHandler2D).AsCached();
        Container.BindInterfacesAndSelfTo<MatchHandler>().FromInstance(_matchHandler).AsCached();
        Container.BindInterfacesAndSelfTo<PieceInteraction>().FromInstance(_pieceInteraction).AsCached();
        Container.BindInterfacesAndSelfTo<PieceMoveHandler>().FromInstance(_pieceMoveHandler).AsCached();
        Container.BindInterfacesAndSelfTo<TurnHandler>().FromInstance(_turnHandler).AsCached();
        Container.BindInterfacesAndSelfTo<ScoreHandler>().FromInstance(_scoreHandler).AsCached();
        Container.BindInterfacesAndSelfTo<RuneHandler>().FromInstance(_runeHandler).AsCached();
        Container.BindInterfacesAndSelfTo<RuneVisualizer>().FromInstance(_runeVisualizer).AsCached();
        Container.BindInterfacesAndSelfTo<RuneMoveHandler>().FromInstance(_runeMoveHandler).AsCached();
        Container.BindInterfacesAndSelfTo<RuneEffectHandler>().FromInstance(_runeEffectHandler).AsCached();
    }
}