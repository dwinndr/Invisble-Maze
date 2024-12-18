using UnityEngine;

public class FitWorkgroundToCamera : MonoBehaviour
{
    public Camera mainCamera; // Kamera utama yang digunakan
    public GameObject container; // Container GameObject yang berisi child objects
    public float padding = 0.1f; // Spasi di setiap sisi (dalam satuan dunia)

    void Start()
    {
        FitContainerToScreen();
    }

    void FitContainerToScreen()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Kamera utama belum diatur.");
            return;
        }

        if (container == null)
        {
            Debug.LogError("Container belum diatur.");
            return;
        }

        // Hitung bounds dari semua child objects di dalam container
        Bounds containerBounds = CalculateBounds(container);

        // Dapatkan ukuran kamera dalam satuan dunia (world units)
        float cameraWidth = mainCamera.orthographicSize * 2.0f * mainCamera.aspect - 2 * padding;
        float cameraHeight = mainCamera.orthographicSize * 2.0f - 2 * padding;

        // Hitung faktor skala
        float scaleX = cameraWidth / containerBounds.size.x;
        float scaleY = cameraHeight / containerBounds.size.y;

        // Terapkan skala baru pada container, mempertimbangkan kedua faktor
        float scaleFactor = Mathf.Min(scaleX, scaleY);
        container.transform.localScale = new Vector3(scaleFactor, scaleFactor, container.transform.localScale.z);

        // Menyesuaikan posisi container agar dimulai dari posisi x = -1.775, y = 0
        float adjustedX = 0f;
        float adjustedY = 0f;

        container.transform.position = new Vector3(adjustedX, adjustedY, container.transform.position.z);
    }

    Bounds CalculateBounds(GameObject obj)
    {
        Bounds bounds = new Bounds(obj.transform.position, Vector3.zero);

        foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }
}