using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour
{
    public static float elapsed;
     GUIStyle guiStyle =  new GUIStyle();
     public int populationSize = 10;
    int generation = 1;
    int timeToDie = 0;
    int trialTime = 15;
    List<GameObject> population;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
       population = new List<GameObject>();
       for(int i = 0; i<populationSize; i++){
        Vector3 pos = new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(-3.4f, 5.4f), 0);
        GameObject o = Instantiate(prefab, pos, Quaternion.identity);
        o.GetComponent<DNA>().r = Random.Range(0.0f,1.0f);
        o.GetComponent<DNA>().g= Random.Range(0.0f,1.0f);
        o.GetComponent<DNA>().b = Random.Range(0.0f,1.0f);
        o.GetComponent<DNA>().s = Random.Range(0.1f,0.3f);
        population.Add(o);
       }
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed> trialTime){
            BreedNewPopulation();
            elapsed=0;
        }
    }
        void OnGUI(){
       guiStyle.fontSize = 20;
       guiStyle.normal.textColor = Color.white;
       GUI.Label(new Rect(10,10,100,20), "Generation: " + generation, guiStyle);
       GUI.Label(new Rect(10,30,100,20), "Time: " + (int) elapsed, guiStyle);

    }

    void BreedNewPopulation(){
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();
        population.Clear();

        for(int i = (int)(sortedList.Count/2.0f)-1; i<sortedList.Count-1;i++){ //start from the middle because the first half have short lives
            population.Add(Breed(sortedList[i], sortedList[i+1]));
            population.Add(Breed(sortedList[i+1], sortedList[i]));
        }
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }
    GameObject Breed(GameObject parent1, GameObject parent2){
        Vector3 pos =  new Vector3(Random.Range(-9.5f,9.5f), Random.Range(-3.4f, 5.4f),0);
        GameObject offspring = Instantiate(prefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();
        if(Random.Range(0,10)< 8)
        {
            offspring.GetComponent<DNA>().r= Random.Range(0,10)<5 ? dna1.r : dna2.r;
            offspring.GetComponent<DNA>().g= Random.Range(0,10)<5 ? dna1.g : dna2.g;
            offspring.GetComponent<DNA>().b= Random.Range(0,10)<5 ? dna1.b : dna2.b;
            offspring.GetComponent<DNA>().s= Random.Range(0,10)<5 ? dna1.s : dna2.s;
        }
        else
        {
            offspring.GetComponent<DNA>().r= Random.Range(0.0f,1.0f);
            offspring.GetComponent<DNA>().g= Random.Range(0.0f,1.0f);
            offspring.GetComponent<DNA>().b= Random.Range(0.0f,1.0f);
            offspring.GetComponent<DNA>().s = Random.Range(0.1f,0.3f);
         }
        return offspring;
    }
}

