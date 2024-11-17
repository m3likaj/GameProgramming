
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainingSet{
    public double[] input;
    public double output;
}

public class Perceptron_sc : MonoBehaviour
{

    public TrainingSet[] ts;
    double [] weights = {0,0};
    double bias = 0;
    double totalError = 0;
    
    double DotProductBias(double[] v1, double[] v2){
        if(v1==null || v2==null)
            return -1;
        
        if(v1.Length != v2.Length)
            return -1;
        double d=0;
        for (int i = 0; i < v1.Length; i++)
        {
            d+= v1[i] * v2[i];
        }
        d+= bias;
        return d;
    }

    double CalcOutPut(int i){
        double dp = DotProductBias(weights, ts[i].input);
        if (dp>0) return 1;
        return 0;
    }
    double CalcOutPut(double i1, double i2){
        double[] inp = new double[] {i1, i2};
        double dp = DotProductBias(weights, inp);
        if(dp>0) return 1;
        return 0;
    }

    void InitializeWeights(){
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] =  Random.Range(-1.0f, 1.0f);
        }
        bias =  Random.Range(-1.0f, 1.0f);
    }

    void UpdateWeights(int j){
        double error = ts[j].output - CalcOutPut(j);
        totalError += Mathf.Abs((float)error);
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] =  weights[i] + error* ts[j].input[i];
        }
        bias += error;
    }

    void Train(int epochs){
        InitializeWeights();

        for (int i = 0; i < epochs; i++)
        {
            totalError = 0;
            for (int j = 0; j < ts.Length; j++)
            {
                UpdateWeights(j);
                Debug.Log("W1: " + weights[0] + "\t W2: " + weights[1] + "\t B: " + bias);
            }
            Debug.Log("Total Error: " + totalError);
        }
    }

    void Start (){
        Train(8);
        Debug.Log("Test 0 0: " + CalcOutPut(0,0));
        Debug.Log("Test 0 1: " + CalcOutPut(0,1));
        Debug.Log("Test 1 0: " + CalcOutPut(1,0));
        Debug.Log("Test 1 1: " + CalcOutPut(1,1));
    }

}
