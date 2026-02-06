using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScoreManager : MonoBehaviour
{
    [System.Serializable]
    public class OverallScore
    {
        public float day;
        public float score;
    }
    public List<OverallScore> overallScore = new List<OverallScore>();

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
