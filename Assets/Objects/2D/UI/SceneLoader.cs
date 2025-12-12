using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Метод для загрузки следующей сцены
    /// </summary>
    public void LoadNextScene()
    {
        // Получаем индекс текущей активной сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Определяем индекс следующей сцены
        int nextSceneIndex = currentSceneIndex + 1;

        // Проверяем, существует ли следующая сцена в билде проекта
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Загружаем следующую сцену асинхронно
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Следующая сцена отсутствует!");
        }
    }
}