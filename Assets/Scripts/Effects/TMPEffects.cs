using UnityEngine;
using TMPro;

public class TMPEffects : MonoBehaviour
{
    public TMP_Text tmpText;
    public float amplitude = 5f;
    public float frequency = 2f;
    public float speed = 2f;

    private TMP_TextInfo textInfo;
    private Vector3[][] originalVertices;

    void Start()
    {
        tmpText.ForceMeshUpdate();
        textInfo = tmpText.textInfo;

        // Orijinal vertex pozisyonlarını kaydet
        originalVertices = new Vector3[textInfo.meshInfo.Length][];
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            originalVertices[i] = (Vector3[])textInfo.meshInfo[i].vertices.Clone();
        }
    }

    void Update()
    {
        tmpText.ForceMeshUpdate();
        textInfo = tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            float wave = Mathf.Sin(Time.time * speed + i * frequency) * amplitude;

            vertices[vertexIndex + 0] = originalVertices[materialIndex][vertexIndex + 0] + new Vector3(0, wave, 0);
            vertices[vertexIndex + 1] = originalVertices[materialIndex][vertexIndex + 1] + new Vector3(0, wave, 0);
            vertices[vertexIndex + 2] = originalVertices[materialIndex][vertexIndex + 2] + new Vector3(0, wave, 0);
            vertices[vertexIndex + 3] = originalVertices[materialIndex][vertexIndex + 3] + new Vector3(0, wave, 0);
        }

        // Mesh’i TMP’ye geri uygula
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            tmpText.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
