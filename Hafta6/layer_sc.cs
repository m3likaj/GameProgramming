using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layer_sc
{
	public int numberOfNeurons;
	public List<Neuron_sc> neurons = new List<Neuron_sc>();

	public layer_sc(int numberOfNeurons, int numberOfInputs)
	{
		this.numberOfNeurons = numberOfNeurons;
		for(int i = 0; i < numberOfNeurons; i++)
		{
			neurons.Add(new Neuron_sc(numberOfInputs));
		}
	}
}
