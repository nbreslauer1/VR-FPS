using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Camera gameCamera;
    public GameObject metakPrefab;
    public GameObject neprijateljPrefab;
    private float vrijemePucnja = 0;
    private float vrijemeMirovanjaPucnja = 0;
    private float NeprijateljSpawningTimer = 0;
    public float NeprijateljSpawningMirovanje = 1f;
    public float NeprijateljSpawningDistance = 7f;
    // Use this for initialization
    void Start()
    {
    }
        void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Neprijatelj")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Update is called once per frame
    void Update()
    {
                vrijemePucnja -= Time.deltaTime;
        NeprijateljSpawningTimer -= Time.deltaTime;

        if (NeprijateljSpawningTimer <= 0f)
        {
            NeprijateljSpawningTimer = NeprijateljSpawningMirovanje;

            GameObject enemyObject = Instantiate(neprijateljPrefab);

            float randomAngle = Random.Range(0, Mathf.PI * 2);
            enemyObject.transform.position = new Vector3(
                gameCamera.transform.position.x + Mathf.Cos(randomAngle) * NeprijateljSpawningDistance,
                0,
                gameCamera.transform.position.z + Mathf.Sin(randomAngle) * NeprijateljSpawningDistance);

            Neprijatelj nerpijatelj = enemyObject.GetComponent<Neprijatelj>();
            nerpijatelj.direction = (gameCamera.transform.position - nerpijatelj.transform.position).normalized;
            nerpijatelj.transform.LookAt(Vector3.zero);
        }
            
        RaycastHit hit;

        if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out hit))
        {

            if (hit.transform.tag == "Neprijatelj" && vrijemePucnja <= 0f)
            {
                vrijemePucnja =   vrijemeMirovanjaPucnja = 0;


                GameObject metakObject = Instantiate(metakPrefab);
                metakObject.transform.position = gameCamera.transform.position;

                Metak metak = metakObject.GetComponent<Metak>();
                metak.direction = gameCamera.transform.forward;
            }

        }

    }
}
