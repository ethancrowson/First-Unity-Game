using UnityEngine;

public class SpawnButton : MonoBehaviour, Interactable
{
    public string InteractMessage => objectInteractMessage;

    [SerializeField]
    GameObject spawnPrefab;

    [SerializeField]
    string objectInteractMessage;

    public float offsetRange = 1f;
    public float explosionForce = 5f;

    void Spawn()
    {
        // Calculate a random horizontal offset
        Vector3 randomOffset = new Vector3(
            Random.Range(-offsetRange, offsetRange),
            0f,
            Random.Range(-offsetRange, offsetRange)
        );

        // Final spawn position
        Vector3 spawnPosition = transform.position + Vector3.up + randomOffset;

        // Instantiate at calculated position
        var spawnedObject = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);

        // Apply explosive force if Rigidbody is present
        Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 explosionDirection = (randomOffset.normalized + Vector3.up).normalized;
            rb.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
        }

        // Apply random scale
        float randomSize = Random.Range(0.1f, 1f);
        spawnedObject.transform.localScale = Vector3.one * randomSize;

        // Apply random color if it has a MeshRenderer
        MeshRenderer renderer = spawnedObject.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            Color randomColour = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            renderer.material.color = randomColour;
        }
    }

    public void Interact()
    {
        Spawn();
    }
}
