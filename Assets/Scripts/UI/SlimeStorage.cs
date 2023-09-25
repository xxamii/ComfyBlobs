using System.Collections.Generic;
using UnityEngine;

public class SlimeStorage : MonoBehaviour
{

    public GameObject storageElementPrefab;

    private Eater _player;
    private List<SlimeStorageElement> _slimes;

    private void Awake()
    {
        _slimes = new List<SlimeStorageElement>();
    }

    public void Start()
    {
        _player = FindObjectOfType<Eater>();
        _player.OnSplitWeightChange += UpdateView;

        UpdateView();
    }

    public void Update()
    {
        UpdateView();
    }
    public void UpdateView()
    {
        if (_slimes.Count == 0)
            CreateElement();

        _slimes[0].Text = _player.GetCurrentWeight().ToString();
    }

    // TODO This should use slime type
    public void CreateElement()
    {
        var newEl = Instantiate(storageElementPrefab, transform);
        _slimes.Add(newEl.GetComponent<SlimeStorageElement>());
    }

    public void OnDisable()
    {
        _player.OnSplitWeightChange -= UpdateView;
    }
}


