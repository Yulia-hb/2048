using UnityEngine;
using ChainCube.Scripts.Records;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private RecordManager _recordManager;

    private bool _runFinished;

    public void FinishRun()
    {
        if (_runFinished)
            return;

        _runFinished = true;

        _recordManager?.CheckRecord();

        Debug.Log("Run finished");
    }

    public void StartNewRun()
    {
        _runFinished = false;

        // Пізніше сюди додамо:
        // reset score
        // reset timer
        // clear cubes
        // reset merge chain
        // hide results panel
    }
}
