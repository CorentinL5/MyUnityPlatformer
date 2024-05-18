using UnityEngine;

public class SlimeDrops : MonoBehaviour
{
    // Déclare une variable publique de type Transform pour la préfabriquée
    public Transform prefabToSpawn;

    // Déclare une variable publique de type Transform pour la position de spawn
    public Transform spawnDrops;

    // Déclare une variable pour régler l'intervalle de temps entre les instanciations
    public float spawnInterval = 0.5f;
    private EnnemyPatrol ennemyPatrol;

    void Awake()
    {
         ennemyPatrol = GetComponent<EnnemyPatrol>();
    }

    void Start()
    {
        // Vérifie si la préfabriquée et la position de spawn sont assignées dans l'inspecteur Unity
        if (prefabToSpawn != null && spawnDrops != null)
        {
            // Utilise InvokeRepeating pour instancier la préfabriquée toutes les 0.3 secondes
            InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
        }
        else
        {
            // Affiche un avertissement si la préfabriquée ou la position de spawn n'est pas assignée
            Debug.LogWarning("Prefab ou position de spawn non assignée dans le script SlimeDrops.");
        }
    }

    void SpawnPrefab()
    {
        if (ennemyPatrol != null && !ennemyPatrol.EnnemyDied)
        {
            // Instancie la préfabriquée à la position spécifiée
            Instantiate(prefabToSpawn, spawnDrops.position, Quaternion.identity);
        }

    }
}
