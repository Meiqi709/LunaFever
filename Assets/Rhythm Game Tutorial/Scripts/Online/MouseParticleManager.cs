using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class MouseParticleManager : NetworkBehaviour
{
    [SerializeField] private Transform particlePrefab;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            SpawnMyParticle(mousePos);
        }
    }

        void SpawnMyParticle(Vector3 position)
    {
        OnlineGameManager.PlayerType type = OnlineGameManager.Instance.GetLocalPlayerType();
        SpawnParticleServerRpc(position, type);
    }

    [Rpc(SendTo.Server)] //only server is allowed to spawn object 
    // network transform info about position
    void SpawnParticleServerRpc(Vector3 position, OnlineGameManager.PlayerType type)
    {
        Color chosenColor = Color.white;
        switch (type)
        {
            case OnlineGameManager.PlayerType.Host:
                chosenColor = Color.yellow;
                break;
            case OnlineGameManager.PlayerType.Client:
                break;
        }
        Transform particleInstance = Instantiate(particlePrefab, position, Quaternion.identity);
        ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            var main = ps.main;
            main.startColor = chosenColor;
        }
        NetworkObject networkObject = particleInstance.GetComponent<NetworkObject>(); 
        networkObject.Spawn(true);

        StartCoroutine(DespawnAfterDelay(networkObject, 1f));
    }

    private IEnumerator DespawnAfterDelay(NetworkObject networkObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (networkObject != null && networkObject.IsSpawned)
        {
            networkObject.Despawn();
        }
    }
}

