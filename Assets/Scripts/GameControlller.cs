using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlller : MonoBehaviour
{
    public GameObject player;
    private GameObject InstantiatedPlayer;
    public GameObject ball;
    public GameObject[] levels;
    public int currentLevel = 0;
    private string currentStage = "";
    private float cameraOrthographicSize;
    private Camera mainCamera;
    public GameObject arrow;
    private Image arrowImage;
    private GameObject canvas;
    private ScoreManager scoreManager;
    private GameObject levelController;
    private bool isIncreasing = false;
    private bool levelInstantiated = false;
    Dictionary<int, string[]> stageValues = new Dictionary<int, string[]>();

    public bool onGoing = false;
    void Start()
    {
        FillStageNames();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        scoreManager = canvas.GetComponent<ScoreManager>();

        float startValue = float.Parse(stageValues[0][1]);

        cameraOrthographicSize = mainCamera.orthographicSize;

        mainCamera.orthographicSize *= startValue;
        transform.localScale *= startValue;
        currentStage = stageValues[0][0];
        scoreManager.SetStageName(currentStage);

        Instantiate(arrow, canvas.transform.position, Quaternion.identity);
        arrowImage = GameObject.FindGameObjectWithTag("Arrow").GetComponentInChildren<Image>();

        InstantiateGameObject("PlayerSpawnPoint", player, Vector3.zero);
        InstantiatedPlayer = GameObject.FindGameObjectWithTag("Player");

        InitiateLevel();
    }

    public void SetBall(){
        onGoing = false;
        arrowImage.enabled = true;
        Instantiate(ball, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
    }

    private IEnumerator IncreaseStageSize(float size){
        while(isIncreasing){
            yield return new WaitForSeconds(0);
            mainCamera.orthographicSize += Time.deltaTime;
            transform.localScale += new Vector3(Time.deltaTime / (cameraOrthographicSize), Time.deltaTime / (cameraOrthographicSize), 0);

            if(transform.localScale.x >= size){
                isIncreasing = false;
                InitiateLevel();
            }
        }
    }

    private IEnumerator InstantiateLevel(){
        while(!levelInstantiated){
            yield return new WaitForSeconds(0);
            if(GameObject.FindGameObjectWithTag("Level") is null){
                InstantiateGameObject("LevelSpawnPoint", levels[currentLevel], Vector3.zero);
                levelInstantiated = true;
            }
        }
    }

    public void InitiateLevel(){
        DestroyCurrentLevel();
        StartCoroutine(InstantiateLevel());
        
        Invoke("SetBall", 2f);
    }

    private void InstantiateGameObject(string tag, GameObject go, Vector3 offset){
        GameObject spawnPoint = GameObject.FindGameObjectWithTag(tag);
        if(spawnPoint != null && go != null){
            Instantiate(go, spawnPoint.transform.position + offset, Quaternion.identity);
        }else{
            Debug.Log(go.name + " not attached in Inspector/" + go.name + " spawn point not found");
        }
    }

    public void PrepareNewLevel(int level){
        BallBehavior ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallBehavior>();
        ball.DissolveBall();
        currentLevel = level;
        
        float multiplier = float.Parse(stageValues[currentLevel][1]);
        string stage = stageValues[currentLevel][0];

        if(currentStage != stage){
            isIncreasing = true;
            StartCoroutine(IncreaseStageSize(multiplier));
            currentStage = stage;
            scoreManager.SetStageName(stage);
        }else{
            InitiateLevel();
        }
    }

    private void DestroyCurrentLevel(){
        if(GameObject.FindGameObjectWithTag("Level") != null){
            Destroy(GameObject.FindGameObjectWithTag("Level"));
            levelInstantiated = false;
        }
    }

    private void FillStageNames(){
        string[] firstStage = new string[]{"PINK SPLASH", "0.5"};
        
        stageValues.Add(0, firstStage);
        stageValues.Add(1, firstStage);
        stageValues.Add(2, firstStage);

        string[] secondStage = new string[]{"PURPLE DASH", "0.6"};
        
        stageValues.Add(3, secondStage);
        stageValues.Add(4, secondStage);
        stageValues.Add(5, secondStage);

        string[] thirdStage = new string[]{"RED INSANE", "0.8"};

        stageValues.Add(6, thirdStage);
        stageValues.Add(7, thirdStage);
        stageValues.Add(8, thirdStage);

        string[] fourthStage = new string[]{"BLACK DEATH", "1"};

        stageValues.Add(9, fourthStage);
        stageValues.Add(10, fourthStage);
        stageValues.Add(11, fourthStage);
    }
}
