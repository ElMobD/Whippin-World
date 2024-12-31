using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;    // Tableau de prefabs d'objets à générer
    public int numberOfObjects;     // Nombre total d'objets
    public Vector2 xRange = new Vector2(310, 390); // Plage des coordonnées X
    public Vector2 zRange = new Vector2(110, 190); // Plage des coordonnées Z
    public float fixedHeight;     // Hauteur fixe des objets (Y)
    public float perlinScale = 10.0f;    // Échelle du bruit de Perlin
    public float perlinThreshold = 0.5f; // Seuil du bruit pour générer des objets

    void Start()
    {
        GenerateObjects();
    }

    void GenerateObjects()
    {
        int spawnedObjects = 0; // Compteur d'objets générés

        while (spawnedObjects < numberOfObjects)
        {
            // Génère des coordonnées (x, z) dans la plage définie
            float x = Random.Range(xRange.x, xRange.y);
            float z = Random.Range(zRange.x, zRange.y);

            // Calcule la valeur du bruit de Perlin
            float perlinValue = Mathf.PerlinNoise(x / perlinScale, z / perlinScale);

            // Vérifie si la valeur dépasse le seuil
            if (perlinValue > perlinThreshold)
            {
                // Fixe la hauteur constante ou calculée
                float y = fixedHeight;

                // Choisir aléatoirement un prefab à partir du tableau
                GameObject randomPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

                // Instancier l'objet à la position calculée
                Vector3 position = new Vector3(x, y, z);
                Instantiate(randomPrefab, position, Quaternion.identity);

                // Incrémente le compteur
                spawnedObjects++;
            }
        }
    }
}

