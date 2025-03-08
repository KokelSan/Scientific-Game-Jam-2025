using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class NPC : MonoBehaviour
{
    private Dialog _dialog;
    private bool _hadDialog  = false;
    private Player _player;

    public void Initialize(int id, Dialog dialog)
    {
        name = $"NPC {id}";
        _dialog = dialog;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hadDialog) return;

        if (other.gameObject.TryGetComponent(out Player player))
        {
            _hadDialog = true;
            _player = player;

            _player.Inputs.FreezeInputs();
            _player.LookAt(transform, StartDialog);
        }
    }

    private void StartDialog()
    {        
        DialogUIManager.Instance.ShowDialog(_dialog.Text, OnDialogShowed, OnDialogClosed);
    }

    private void OnDialogShowed() 
    {
        _player.AddEffect(_dialog.IsPositive);
    }

    private void OnDialogClosed()
    {        
        _player.Inputs.RestoreInputs();
        _player = null;
    }
}
