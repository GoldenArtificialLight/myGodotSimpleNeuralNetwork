using System;
using Godot;
using System.Collections.Generic;

namespace neuralNetwork.Scripts.NeuralNetwork;

public class NeuralNetwork(Neuron[][] layers, float? defaultBias = null)
{
	public Neuron[][] layers = layers;

	public float? defaultBias = defaultBias;

	public float[] CalculateOutputs(float[] inputs, float bias)
	{
		#nullable enable
		float[]? inputLayer = inputs;
		#nullable disable

		float result;
		float[] results = [];
		List<float> r = [];

		foreach(Neuron[] nl in layers)
		{
			foreach(Neuron n in nl)
			{
				float sum = n.CalculateWeightedSum(inputLayer ?? results, defaultBias ?? bias);
				result = n.Activate(sum);
				r.Add(result);
			}
			inputLayer = null;
			results = [..r];
			r.Clear();
		}
		results = [.. r];
		return results;
	}

/*   // mean squared error ???
	public float[] Loss(float[] predictions, float[] labels)
	{
		List<float> squaredErrors = [];
		for(int i=0; i<predictions.Length; i++)
		{
			float sum = predictions[i] - labels[i];
			squaredErrors.Add(sum*sum);
		}

		return [..squaredErrors];
	}

	// adjust the weights
	public void Optimize(float[] loss_values, float learningRate)
	{
		for(int i=0; i<loss_values.Length; i++)
		{
			Neuron currNeuron = layers[i];
			for(int j=0; j<currNeuron.weights.Length; j++)
			{
				// ajusta os pesos
			}
		}
	} */

	// mean squared error
	public float Loss(float[] predictions, float[] labels)
	{
		float squaredErrors = 0.0f;
		for(int i=0; i<predictions.Length; i++)
		{
			float sum = predictions[i] - labels[i];
			squaredErrors += sum*sum;
		}

		return squaredErrors / predictions.Length;
	}

	// adjust the weights
	public void Optimize(float loss_value, float learningRate)
	{
		foreach(Neuron[] nl in layers)
		{
			foreach(Neuron n in nl)
			{
				for(int j=0; j<n.weights.Length; j++)
				{
					// ajusta os pesos
				}
			}
		}
	}


	public void TrainNetwork(
		float[][] dataset, float[][] labels,
		float bias, int epochs,
		float learningRate = 0.001f
	) {
		for(int i=0; i<epochs; i++)
		{
			for(int j=0; j<dataset.Length; j++)
			{
				float[] predictions = CalculateOutputs(dataset[j], bias);
				/*
					Agora, fica a questão. Eu ajusto os pesos após
					cada entrada ou ajusto somente ao final da época?
					Acho que deixei algum detalhe passar
				*/
				// float[] lossValues = Loss(predictions, labels[j]);
				// Optimize(lossValues, learningRate);
				float lossValue = Loss(predictions, labels[j]);
				Optimize(lossValue, learningRate);
			}
		}
	}
}

public class Neuron
{
	public float[] weights = [];
	
	public const float EULER = 2.71828f;

	public float CalculateWeightedSum(float[] input, float bias)
	{
		float sum = 0.0f;

		for(int i=0; i<input.Length; i++)
		{
			sum += input[i] * weights[i] + bias;
		}

		return sum;
	}

	// sigmoid or something
	public float Activate(float sum)
	{
		float result = 1 / (1 + Mathf.Pow(EULER, -sum));
		// do i really need this?
		return result>0.75f ? result : 0.0f;
	}

	public static Neuron[] Factory(int numNeurons, int numWeights)
	{
		List<Neuron> neurons = [];

		for(int i=0; i<numNeurons; i++)
		{
			neurons.Add(new Neuron());
			for(int j=0; j<numWeights; j++)
			{
				neurons[i].weights[j] = new Random().NextSingle();
			}
		}

		return [.. neurons];
	}
}