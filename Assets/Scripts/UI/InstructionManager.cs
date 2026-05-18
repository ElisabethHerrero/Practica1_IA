using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionManager : MonoBehaviour
{
    [Header("Texto")]
    public TextMeshProUGUI instructionText;

    [Header("Botones")]
    public Button previousButton;
    public Button nextButton;

    [Header("Instrucciones")]
    [TextArea(3, 6)]
    public string[] instructions =
    {
        "�Buenas noches y bienvenido, joven estudiante!",

        "�Estas listo para tu examen final de Defensa para Magos I?",

        "La prueba consistira en un recorrido por la mazmorra de la escuela.",

        "Deberas recolectar como minimo 25 puntos de energia magica.",

        "Para moverte, usa WASD. Tambien puedes rotar la camara moviendo el raton.",

        "Pero recuerda... �Cuidado con los fantasmas!",

        "Si te tocan, perderas vida. Para atacarlos, presiona E.",

        "�Ten cuidado tambien con las trampas!",

        "Si las pisas, los fantasmas sabran donde estas.",

        "Cuando hayas cumplido el objetivo, busca el portal iluminado de rosa.",

        "Tras ello, colocate sobre el y presiona R.",

        "�Eso es todo! �Buena suerte!"
    };

    private int currentIndex = 0;

    void Start()
    {
        ShowInstruction();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void NextInstruction()
    {
        if (currentIndex < instructions.Length - 1)
        {
            currentIndex++;
            ShowInstruction();
        }
    }

    public void PreviousInstruction()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowInstruction();
        }
    }

    private void ShowInstruction()
    {
        instructionText.text = instructions[currentIndex];

        previousButton.gameObject.SetActive(currentIndex > 0);
        nextButton.gameObject.SetActive(currentIndex < instructions.Length - 1);
    }
}