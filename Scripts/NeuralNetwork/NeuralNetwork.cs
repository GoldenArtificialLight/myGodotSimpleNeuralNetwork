using System;
using Godot;
using System.Collections.Generic;

namespace neuralNetwork.Scripts.NeuralNetwork;

public class NeuralNetwork(Neuron[] outLayerNeurons)
{
     
     public Neuron[] outLayerNeurons = outLayerNeurons;
     
     public float[] CalculateResults(float[] inputs)
     {
          float result;
          List<float> r = [];
          foreach(Neuron n in outLayerNeurons)
          {
               float sum = n.CalculateWeightedSum(inputs);
               result = n.Activate(sum);
               r.Add(result);
          }
          float[] results = [.. r];
          return results;
     }

     // mean square error
     public float[] Loss(float[] predictions, float[] labels){ return [0.0f]; }

     // adjust the weights
     public void Optimize(float[] loss_values, float learningRate)
     {
          for(int i=0; i<loss_values.Length; i++)
          {
               Neuron currNeuron = outLayerNeurons[i];
               for(int j=0; j<currNeuron.weights.Length; j++)
               {
                    // ajusta os pesos
               }
          }
     }
     
     public void TrainNetwork(
          float[][] dataset, float[][] labels,
          int epochs, float learningRate = 0.001f
     ) {
          for(int i=0; i<epochs; i++)
          {
               for(int j=0; j<dataset.Length; j++)
               {
                    float[] predictions = CalculateResults(dataset[j]);
                    /*
                         Agora, fica a questão. Eu ajusto os pesos após
                         cada entrada ou ajusto somente ao final da época?
                         Acho que deixei algum detalhe passar
                    */
                    float[] lossValues = Loss(predictions, labels[j]);
                    Optimize(lossValues, learningRate);
               }
          }
     }
}

public class Neuron
{
     public float[] weights = [];

     public float CalculateWeightedSum(float[] input)
     {
          float sum = 0.0f;

          for(int i=0; i<input.Length; i++)
          {
               sum += input[i] * weights[i];
          }

          return sum;
     }
     // sigmoid or something
     public float Activate(float sum){ return 0.0f; }

     public static Neuron[] Factory(int numNeurons, int numWeights)
     {
          Neuron[] neurons = [];
          
          for(int i=0; i<numNeurons; i++)
          {
               neurons[i] = new Neuron();
               for(int j=0; j<numWeights; j++)
               {
                    neurons[i].weights[j] = 1.0f;
               }
          }
          
          return neurons;
     }
}