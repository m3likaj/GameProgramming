using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_sc : MonoBehaviour
{
	ANN_sc ann;
	double sumSquareError = 0;

	void Start () {

		int numberOfInputs = 2;
		int numberOfOutputs = 1;
		int numberOfHiddenLayers = 1;
		int numberOfNeuronsPerHiddenLayer = 2;
		double alpha = 0.8;

		int epoch = 2000;

		ann = new ANN_sc(numberOfInputs, numberOfOutputs, 
						numberOfHiddenLayers, 
						numberOfNeuronsPerHiddenLayer, 
						alpha);
		
		List<double> result;
		
		for(int i = 0; i < epoch; i++)
		{
			sumSquareError = 0;
			result = Train(1, 1, 0);
			sumSquareError += Mathf.Pow((float)result[0] - 0, 2);
			result = Train(1, 0, 1);
			sumSquareError += Mathf.Pow((float)result[0] - 1, 2);
			result = Train(0, 1, 1);
			sumSquareError += Mathf.Pow((float)result[0] - 1, 2);
			result = Train(0, 0, 0);
			sumSquareError += Mathf.Pow((float)result[0] - 0, 2);
		}
		Debug.Log("SSE: " + sumSquareError);

		result = Train(1, 1, 0);
		Debug.Log(" 1 1 " + result[0]);
		result = Train(1, 0, 1);
		Debug.Log(" 1 0 " + result[0]);
		result = Train(0, 1, 1);
		Debug.Log(" 0 1 " + result[0]);
		result = Train(0, 0, 0);
		Debug.Log(" 0 0 " + result[0]);
	}

	List<double> Train(double input1, double input2, double output)
	{
		List<double> inputs = new List<double>(){input1, input2};
		List<double> outputs = new List<double>(){output};
		return ann.Run(inputs, outputs);
	}
}
