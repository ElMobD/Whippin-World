using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float deltaTime = 0.0f;

    void Update()
    {
        // Calculer le temps écoulé
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        // Calculer les FPS
        float fps = 1.0f / deltaTime;

        GUIStyle style = new GUIStyle();
        style.fontSize = 20; // Taille de la police
        style.normal.textColor = Color.white; // Couleur du texte

        int screenWidth = Screen.width;

        // Position de l'affichage
        Rect rect = new Rect(screenWidth - 110, 10, 200, 50); // x, y, largeur, hauteur
        string text = string.Format("{0:0.} FPS", fps); // Formater le texte
        GUI.Label(rect, text, style); // Afficher le texte
    }
}
