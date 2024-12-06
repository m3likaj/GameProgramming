using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANN_sc
{
	public int numberOfInputs;
	public int numberOfOutputs;
	public int numberOfHiddenLayers;
	public int numberOfNeuronsPerHiddenLayer;
	public double alpha;
	List<layer_sc> layers = new List<layer_sc>();

	public ANN_sc(int numberOfInputs, int numberOfOutputs, 
				int numberOfHiddenLayers, 
				int numberOfNeuronsPerHiddenLayer, 
				double alpha)
	{
		this.numberOfInputs = numberOfInputs;
		this.numberOfOutputs = numberOfOutputs;
		this.numberOfHiddenLayers = numberOfHiddenLayers;
		this.numberOfNeuronsPerHiddenLayer = numberOfNeuronsPerHiddenLayer;
		this.alpha = alpha;

		if(numberOfHiddenLayers > 0)
		{
			layers.Add(new layer_sc(numberOfNeuronsPerHiddenLayer, numberOfInputs));

			for(int i = 0; i < numberOfHiddenLayers-1; i++)
			{
				layers.Add(new layer_sc(numberOfNeuronsPerHiddenLayer, numberOfNeuronsPerHiddenLayer));
			}

			layers.Add(new layer_sc(numberOfOutputs, numberOfNeuronsPerHiddenLayer));
		}
		else
		{
			layers.Add(new layer_sc(numberOfOutputs, numberOfInputs));
		}
	}

	public List<double> Run(List<double> inputValues, List<double> desiredOutput)
	{
		List<double> inputs = new List<double>();
		List<double> outputs = new List<double>();

		if(inputValues.Count != numberOfInputs)
		{
			Debug.Log("ERROR: Number of Inputs must be " + numberOfInputs);
			return outputs;
		}

		inputs = new List<double>(inputValues);
		for(int i = 0; i < numberOfHiddenLayers + 1; i++)
		{
				if(i > 0)
				{
					inputs = new List<double>(outputs);
				}
				outputs.Clear();

				for(int j = 0; j < layers[i].numberOfNeurons; j++)
				{
					double N = 0;
					layers[i].neurons[j].inputs.Clear();

					for(int k = 0; k < layers[i].neurons[j].numberOfInputs; k++)
					{
					    layers[i].neurons[j].inputs.Add(inputs[k]);
						N += layers[i].neurons[j].weights[k] * inputs[k];
					}

					N -= layers[i].neurons[j].bias;
					layers[i].neurons[j].output = ActivationFunction(N);
					outputs.Add(layers[i].neurons[j].output);
				}
		}

		UpdateWeights(outputs, desiredOutput);

		return outputs;
	}
	
	void UpdateWeights(List<double> outputs, List<double> desiredOutput)
	{
		double error;
		for(int i = numberOfHiddenLayers; i >= 0; i--)
		{
			for(int j = 0; j < layers[i].numberOfNeurons; j++)
			{
				if(i == numberOfHiddenLayers)
				{
					error = desiredOutput[j] - outputs[j];
					layers[i].neurons[j].errorGradient = outputs[j] * (1-outputs[j]) * error;
					//errorGradient calculated with Delta Rule: en.wikipedia.org/wiki/Delta_rule
				}
				else
				{
					layers[i].neurons[j].errorGradient = layers[i].neurons[j].output * 
															(1-layers[i].neurons[j].output);
					double errorGradSum = 0;
					for(int p = 0; p < layers[i+1].numberOfNeurons; p++)
					{
						errorGradSum += layers[i+1].neurons[p].errorGradient * 
															layers[i+1].neurons[p].weights[j];
					}
					layers[i].neurons[j].errorGradient *= errorGradSum;
				}	
				for(int k = 0; k < layers[i].neurons[j].numberOfInputs; k++)
				{
					if(i == numberOfHiddenLayers)
					{
						error = desiredOutput[j] - outputs[j];
						layers[i].neurons[j].weights[k] += alpha * layers[i].neurons[j].inputs[k] * 
																	error;
					}
					else
					{
						layers[i].neurons[j].weights[k] += alpha * layers[i].neurons[j].inputs[k] * 
																	layers[i].neurons[j].errorGradient;
					}
				}
				layers[i].neurons[j].bias += alpha * -1 * layers[i].neurons[j].errorGradient;
			}
		}
	}

	//for full list of activation functions
	//see en.wikipedia.org/wiki/Activation_function
	double ActivationFunction(double value)
	{
		return Sigmoid(value);
	}

	double Step(double value) //(aka binary step)
	{
		if(value < 0) return 0;
		else return 1;
	}

	double Sigmoid(double value) //(aka logistic softstep)
	{
    	double k = (double) System.Math.Exp(value);
    	return k / (1.0f + k);
	}
}
