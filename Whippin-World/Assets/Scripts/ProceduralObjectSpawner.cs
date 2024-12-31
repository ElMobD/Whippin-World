using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;    // Tableau de prefabs d'objets � g�n�rer
    public int numberOfObjects;     // Nombre total d'objets
    public Vector2 xRange = new Vector2(310, 390); // Plage des coordonn�es X
    public Vector2 zRange = new Vector2(110, 190); // Plage des coordonn�es Z
    public float fixedHeight;     // Hauteur fixe des objets (Y)
    public float perlinScale = 10.0f;    // �chelle du bruit de Perlin
    public float perlinThreshold = 0.5f; // Seuil du bruit pour g�n�rer des objets

    void Start()
    {
        GenerateObjects();
    }

    void GenerateObjects()
    {
        int spawnedObjects = 0; // Compteur d'objets g�n�r�s

        while (spawnedObjects < numberOfObjects)
        {
            // G�n�re des coordonn�es (x, z) dans la plage d�finie
            float x = Random.Range(xRange.x, xRange.y);
            float z = Random.Range(zRange.x, zRange.y);

            // Calcule la valeur du bruit de Perlin
            float perlinValue = Mathf.PerlinNoise(x / perlinScale, z / perlinScale);

            // V�rifie si la valeur d�passe le seuil
            if (perlinValue > perlinThreshold)
            {
                // Fixe la hauteur constante ou calcul�e
                float y = fixedHeight;

                // Choisir al�atoirement un prefab � partir du tableau
                GameObject randomPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

                // Instancier l'objet � la position calcul�e
                Vector3 position = new Vector3(x, y, z);
                Instantiate(randomPrefab, position, Quaternion.identity);

                // Incr�mente le compteur
                spawnedObjects++;
            }
        }
    }
}

