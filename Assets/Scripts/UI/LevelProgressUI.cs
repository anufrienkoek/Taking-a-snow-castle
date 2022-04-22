using UnityEngine ;
using UnityEngine.UI ;
using UnityEngine.SceneManagement;

public class LevelProgressUI : MonoBehaviour {

   [Header ("UI references :")]
   [SerializeField] private Image uiFillImage;
   [SerializeField] private Text uiStartText;
   [SerializeField] private Text uiEndText;

   [Header ("Player & Endline references :")]
   [SerializeField] private Transform playerTransform;
   [SerializeField] private Transform endLineTransform;
   [SerializeField] private bool isFinished = false;
   public GameManager GameManager;
   
   private Vector3 endLinePosition;
   private float fullDistance;

   private void Start () {
      endLinePosition = endLineTransform.position;
      fullDistance = GetDistance ();
      SetLevelTexts();
   }

   private void SetLevelTexts()
   {
      int level = SceneManager.GetActiveScene().buildIndex;

      uiStartText.text = level.ToString ();
      uiEndText.text = (level + 1).ToString ();
   }

   private float GetDistance () {
      // Slow
      //return Vector3.Distance (playerTransform.position, endLinePosition) ;

      // Fast
      return (endLinePosition - playerTransform.position).sqrMagnitude ;
   }
   
   private void UpdateProgressFill (float value) 
   {
      uiFillImage.fillAmount = value ;
   }
   
   private void Update () {
      // check if the player doesn't pass the End Line
      if (playerTransform.position.z <= endLinePosition.z) 
      {
         float newDistance = GetDistance () ;
         float progressValue = Mathf.InverseLerp (fullDistance, 0f, newDistance) ;

         UpdateProgressFill (progressValue) ;
         FinishDistance();
      }
   }

   private void FinishDistance()
   {
      if (uiFillImage.fillAmount >= 0.9998f && isFinished == false)
      {
         isFinished = true;
         GameManager.Win();
      }
   }
}
